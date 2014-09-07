using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules.Preset.String
{
    public class CasingRule
        : Rule<string>,
        IEnforcable<string>
    {
        private readonly CasingType _casingType = CasingType.Normal;

        public CasingType CasingType
        {
            get { return _casingType; }
        }

        public CasingRule()
        {
            Logic = value =>
            {
                if (value == null)
                {
                    return string.Empty;
                }
                if (_casingType == CasingType.Lower)
                {
                    if (value.ToLower() != value)
                    {
                        return "Must be all lowercase";
                    }
                }
                if (_casingType == CasingType.Upper)
                {
                    if (value.ToUpper() != value)
                    {
                        return "Must be all uppercase";                        
                    }
                }
                return string.Empty;
            };
        }

        public CasingRule(CasingType casingType)
            : this()
        {
            _casingType = casingType;
        }

        public string Enforce(string value)
        {
            if (_casingType == CasingType.Lower)
            {
                return value.ToLower();
            }
            if (_casingType == CasingType.Upper)
            {
                return value.ToUpper();
            }
            return value;
        }

        public override string Message
        {
            get
            {
                if (_casingType == CasingType.Lower)
                {
                    return "Must be all lowercase";
                }
                if (_casingType == CasingType.Upper)
                {
                    return "Must be all uppercase";
                }
                return string.Empty;
            }
        }
    }
}
