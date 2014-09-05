namespace Hermes.Validation.Rules.Preset.String
{
    public class MinimumLengthRule : Rule<string>
    {
        #region Parameters

        private int? _length;

        public int? Length
        {
            set { _length = (value < 0 ? 0 : value); }
            get { return _length; }
        }

        public override string Message
        {
            get
            {
                return string.Format("Must be {0} or more characters in length", _length);
            }
        }

        #endregion

        #region Constructors

        public MinimumLengthRule()
        {
            Logic = value =>
            {
                if (string.IsNullOrEmpty(value)
                    || _length == null
                    || _length.Value <= 0
                    || value.Length >= _length.Value)
                {
                    return string.Empty;
                }
                if (_length > 0)
                    return Message;
                return string.Empty;
            };

        }

        public MinimumLengthRule(int minLength)
            : this()
        {
            Length = minLength;
        }

        #endregion

        #region Methods

        public override int CompareTo(object obj)
        {
            if (((MinimumLengthRule)obj).Length <= _length)
                return 1;

            return -1;
        }

        #endregion

    }
}
