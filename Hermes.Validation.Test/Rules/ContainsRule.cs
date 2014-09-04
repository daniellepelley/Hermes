using Hermes.Validation.Rules;

namespace Hermes.Validation.Test.Rules
{
    public class ContainsRule : Rule<string>
    {
        private readonly string _containsString;

        public string ContainsString
        {
            get { return _containsString; }
        }

        public override string Message
        {
            get { return "Must contain " + _containsString; }
        }

        public ContainsRule()
        {
            Logic = value =>
            {
                if (string.IsNullOrEmpty(value) ||
                    string.IsNullOrEmpty(_containsString))
                {
                    return string.Empty;
                }

                if (!value.Contains(_containsString))
                {
                    return Message;
                }
                return string.Empty;
            };

        }

        public ContainsRule(string containsString)
            : this()
        {
            _containsString = containsString;
        }
    }
}
