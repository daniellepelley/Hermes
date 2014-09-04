using System.Collections.Generic;
using System.Linq;
using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    public class RuleCollection<T> : IRuleCollection
    {
        private readonly List<IRule<T>> _rules = new List<IRule<T>>();

        public List<IRule<T>> Rules
        {
            get { return _rules; }
        }

        public void Add(IRule rule)
        {
            _rules.Add((IRule<T>)rule);
        }

        public string[] Check(T value)
        {
            return _rules
                .Select(rule => rule.Check(value))
                .Where(brokenRule => !string.IsNullOrEmpty(brokenRule))
                .ToArray();
        }

        public bool CheckValid(T value)
        {
            return Check(value).Length == 0;
        }

        public IEnumerable<IRule> GetIRules()
        {
            return Rules;
        }
    }

    public interface IRuleCollection
    {
        void Add(IRule rule);
        IEnumerable<IRule> GetIRules();
    }
}

