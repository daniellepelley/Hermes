using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    /// <summary>
    /// A rule which sets a maximum _length for a string.
    /// </summary>
    public class MaximumLengthRule
        : Rule<string>,
        IEnforcable<string>
    {
        #region Parameters

        private int? _length;
        /// <summary>
        /// Maximum _length for the string.
        /// </summary>
        public int? Length
        {
            set { _length = (value < 0 ? 0 : value); }
            get { return _length; }
        }

        public override string Message
        {
            get { return "Must be " + _length.ToString() + " or less characters in _length"; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A rule which sets a maximum _length for a string.
        /// </summary>
        public MaximumLengthRule()
        {
            Logic = value =>
            {
                if (string.IsNullOrEmpty(value)
                    || _length == null
                    || _length.Value <= 0
                    || value.Length <= _length.Value)
                {
                    return string.Empty;
                }
                if (_length > 0)
                    return Message;
                return string.Empty;
            };
        }

        /// <summary>
        /// A rule which sets a maximum _length for a string.
        /// </summary>
        public MaximumLengthRule(int maxLength)
            : this()
        {
            _length = maxLength;
        }

        #endregion

        #region Methods

        /// <summary>h
        /// Compares the current instance with another object of the same type and returns
        //  an integer that indicates whether the current instance precedes, follows,
        //  or occurs in the same position in the sort order as the other object.
        /// </summary>
        public override int CompareTo(object obj)
        {
            if (((MaximumLengthRule)obj).Length <= _length)
                return 1;
            return -1;
        }

        /// <summary>
        /// Enforces the rule
        /// </summary>
        public string Enforce(string value)
        {
            if (_length.HasValue &&
                value.Length > _length)
            {
                return value.Substring(0, _length.Value);
            }

            return value;
        }

        #endregion
    }
}