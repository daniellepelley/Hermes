using System;

namespace Hermes.Validation.Rules
{
    public abstract class ComparisonRuleBase<T> : Rule<T>
        where T : struct
    {
        protected string _message;

        public override string Message
        {
            get { return _message; }
        }

        private T _comparisonValue;

        public T ComparisonValue
        {
            set { _comparisonValue = value; }
            get { return _comparisonValue; }
        }

        private readonly Func<T?, T?, bool> _comparisonFunc;

        protected ComparisonRuleBase(Func<T?, T?, bool> comparisonFunc, string message)
        {
            Logic = value =>
            {
                if (_comparisonFunc != null &&
                    _comparisonFunc(value, _comparisonValue))
                    return string.Empty;
                return Message;
            };

            _comparisonFunc = comparisonFunc;
            _message = message;
        }
    }
}