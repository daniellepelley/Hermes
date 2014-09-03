using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    public class EntityRuleProvider<TEntity> : IEntityRuleProvider<TEntity>
    {
        private readonly Dictionary<string, FieldRule<TEntity>> _propertyRules;

        public Dictionary<string, FieldRule<TEntity>> PropertyRules
        {
            get { return _propertyRules; }
        }

        private readonly RuleCollection<TEntity> _entityRules;

        public RuleCollection<TEntity> EntityRules
        {
            get { return _entityRules; }
        }

        public EntityRuleProvider()
        {
            _propertyRules = new Dictionary<string, FieldRule<TEntity>>();
            _entityRules = new RuleCollection<TEntity>();
        }

        public void AddRule<TFieldType>(string fieldName, IRule<TFieldType> rule)
        {
            FieldRules<TEntity, TFieldType> fieldRule;

            if (!PropertyRules.ContainsKey(fieldName))
            {
                var getter = GetPropGetter<TFieldType>(fieldName);
                var setter = GetPropSetter<TFieldType>(fieldName);
                fieldRule = new FieldRules<TEntity, TFieldType>(getter, setter);
                _propertyRules.Add(fieldName, fieldRule);
            }
            else
            {
                fieldRule = (FieldRules<TEntity, TFieldType>)PropertyRules[fieldName];
            }

            fieldRule.AddRule(rule);
        }

        public void AddRules<TFieldType>(string fieldName, params IRule<TFieldType>[] rules)
        {
            foreach (var rule in rules)
            {
                AddRule(fieldName, rule);
            }
        }

        public void AddRules<TFieldType>(Expression<Func<TEntity, TFieldType>> expression, params IRule<TFieldType>[] rules)
        {
            string fieldName = GetMemberInfo(expression).Member.Name;
            AddRules(fieldName, rules);
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private static Func<TEntity, TFieldType> GetPropGetter<TFieldType>(string propertyName)
        {
            var paramExpression = Expression.Parameter(typeof(TEntity), "value");

            var propertyGetterExpression = Expression.Property(paramExpression, propertyName);

            var result =
                Expression.Lambda<Func<TEntity, TFieldType>>(propertyGetterExpression, paramExpression).Compile();

            return result;
        }

        private static Action<TEntity, TFieldType> GetPropSetter<TFieldType>(string propertyName)
        {
            var paramExpression = Expression.Parameter(typeof(TEntity));

            var paramExpression2 = Expression.Parameter(typeof(TFieldType), propertyName);

            var propertyGetterExpression = Expression.Property(paramExpression, propertyName);

            var result = Expression.Lambda<Action<TEntity, TFieldType>>
            (
                Expression.Assign(propertyGetterExpression, paramExpression2), paramExpression, paramExpression2
            ).Compile();

            return result;
        }

        public IDictionary<string, IEnumerable<string>> Validate(TEntity entity)
        {
            var result = PropertyRules
                .ToDictionary<KeyValuePair<string, FieldRule<TEntity>>, string, IEnumerable<string>>(rule => rule.Key, rule => rule.Value.Check(entity));
 
            result.Add(string.Empty, EntityRules.Check(entity));
            return result;
        }

        public TEntity Clean(TEntity entity)
        {
            return PropertyRules.Aggregate(entity, (current, r) => r.Value.Clean(current));
        }
    }

    public interface IEntityRuleProvider<TEntity>
    {
        IDictionary<string, IEnumerable<string>> Validate(TEntity entity);
        TEntity Clean(TEntity entity);
    }
}
