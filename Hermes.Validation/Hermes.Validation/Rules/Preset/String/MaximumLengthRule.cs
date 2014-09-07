using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules.Preset.String
{
    /// <summary>
    /// A rule which sets a maximum _length for a string.
    /// </summary>
    public class MaximumLengthRule
        : Rule<string>,
        IEnforcable<string>
    {
        private int? _length;

        public int? Length
        {
            //set { _length = (value < 0 ? 0 : value); }
            get { return _length; }
        }

        public override string Message
        {
            get { return "Must be " + _length + " or less characters in _length"; }
        }

        public MaximumLengthRule(int maxLength)
        {
            _length = maxLength;
            Logic = value =>
            {
                if (string.IsNullOrEmpty(value)
                    || _length == null
                    || _length.Value <= 0
                    || value.Length <= _length.Value)
                {
                    return string.Empty;
                }
                return Message;
            };
        }

        public override int CompareTo(object obj)
        {
            if (((MaximumLengthRule)obj).Length <= _length)
                return 1;
            return -1;
        }

        public string Enforce(string value)
        {
            if (_length.HasValue &&
                value.Length > _length)
            {
                return value.Substring(0, _length.Value);
            }

            return value;
        }
    }
}