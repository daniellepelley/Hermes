using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    /// <summary>
    /// An interface representing a rule provider that works with the Mvc framework
    /// </summary>
    public class EntityRuleProvider<TEntity> : IEntityRuleProvider<TEntity>
    {
        #region Properties

        private Dictionary<string, IRuleCollection> _propertyRules = new Dictionary<string, IRuleCollection>();

        public Dictionary<string, IRuleCollection> PropertyRules
        {
            get { return _propertyRules; }
            set { _propertyRules = value; }
        }

        private readonly RuleCollection<TEntity> _entityRules = new RuleCollection<TEntity>();

        public RuleCollection<TEntity> EntityRules
        {
            get { return _entityRules; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a rule to the rule provider
        /// </summary>
        public void AddRule<TFieldType>(string fieldName, IRule<TFieldType> rule)
        {
            if (!_propertyRules.ContainsKey(fieldName))
            {
                var ruleCollection = (IRuleCollection)Activator.CreateInstance(typeof(RuleCollection<>).MakeGenericType(typeof(TFieldType)));
                _propertyRules.Add(fieldName, ruleCollection);
            }

            _propertyRules[fieldName].Add(rule);
        }

        /// <summary>
        /// Validates the entity and returns an IDictionary of fieldName to broken rules pairs
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Validate(TEntity entity)
        {
            var modelState = new Dictionary<string, IEnumerable<string>>();

            //Check each prpoerty in the entity
            foreach (var property in entity.GetType().GetProperties())
            {
                //If mapped in the rules dictionary
                if (PropertyRules.ContainsKey(property.Name))
                {
                    //Gets a rulecollection type on generic type based on the property type
                    Type type = typeof(RuleCollection<>).MakeGenericType(new[] { property.PropertyType });

                    if (PropertyRules[property.Name].GetType() == type)
                    {
                        //Invokes 'Check' method and checks the rules
                        var brokenRules = (string[])type.InvokeMember(
                            "Check",
                            System.Reflection.BindingFlags.InvokeMethod,
                            null,
                            PropertyRules[property.Name],
                            new[] { property.GetValue(entity, null) });

                        //Adds the broken rules to the entity state
                        modelState.Add(property.Name, brokenRules);
                    }
                }
            }

            //Adds the broken rules for the entity to the entity state
            modelState.Add(string.Empty, EntityRules.Check(entity));

            return modelState;
        }

        /// <summary>
        /// Cleans the entity using any enforcable rules
        /// </summary>
        /// <remarks>
        /// Required testing
        /// </remarks>
        public TEntity Clean(TEntity entity)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                //TODO: Requires testing
                if (PropertyRules.ContainsKey(property.Name))
                {
                    var enforcableRules = PropertyRules[property.Name].GetIRules().Where(r => r is IEnforcable);

                    foreach (var enforcableRule in enforcableRules)
                    {
                        //Gets a rulecollection type on generic type based on the property type
                        var type = typeof(IEnforcable<>).MakeGenericType(new[] { property.PropertyType });

                        if (PropertyRules[property.Name].GetType() == type)
                        {
                            //Invokes 'Check' method and checks the rules
                            property.SetValue(
                                entity,
                                type.InvokeMember(
                                    "Enforce",
                                    System.Reflection.BindingFlags.InvokeMethod,
                                    null,
                                    enforcableRule,
                                    new[] { property.GetValue(entity, null) }), null);
                        }
                    }
                }
            }

            return entity;
        }

        #endregion
    }

    public interface IEntityRuleProvider<TEntity>
    {
        IDictionary<string, IEnumerable<string>> Validate(TEntity entity);
        TEntity Clean(TEntity entity);
    }
}
