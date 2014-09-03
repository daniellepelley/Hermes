using System;
using System.Linq;
using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    public class FieldRules<TEntity, TField> : FieldRule<TEntity>
    {
        private readonly RuleCollection<TField> _ruleCollection;
        private readonly Func<TEntity, TField> _getter;
        private readonly Action<TEntity, TField> _setter;

        public RuleCollection<TField> RuleCollection
        {
            get { return _ruleCollection; }
        }

        public override int RulesCount
        {
            get { return RuleCollection.Rules.Count; }
        }

        public FieldRules()
        {
            _ruleCollection = new RuleCollection<TField>();
        }

        public FieldRules(Func<TEntity, TField> getter, Action<TEntity, TField> setter)
            : this()
        {
            _setter = setter;
            _getter = getter;
        }

        public void AddRule(IRule<TField> rule)
        {
            RuleCollection.Add(rule);
        }

        public override string[] Check(TEntity entity)
        {
            var value = _getter(entity);
            return RuleCollection.Check(value);
        }

        public override TEntity Clean(TEntity entity)
        {
            var value = RuleCollection.GetIRules()
                .Where(r => r is IEnforcable)
                .Aggregate(_getter(entity), (current, enforcableRule) => ((IEnforcable<TField>)enforcableRule)
                    .Enforce(current));

            _setter(entity, value);
            return entity;
        }
    }

    public abstract class FieldRule<TEntity>
    {
        public abstract string[] Check(TEntity entity);
        public abstract TEntity Clean(TEntity entity);
        public abstract int RulesCount { get; }
    }
}
