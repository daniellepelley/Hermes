using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    //To be replaced with forms version.
    public enum CasingType { Normal, Lower, Upper };

    /// <summary>
    /// A rule which sets the casing type for a string.
    /// </summary>
    public class CasingRule
        : Rule<string>,
        IEnforcable<string>
    {

        private CasingType casingType = CasingType.Normal;

        public CasingType CasingType
        {
            set { casingType = value; }
            get { return casingType; }
        }

        public CasingRule()
        {
            Logic = value =>
            {
                if (value == null)
                {
                    return string.Empty;
                }
                if (casingType == CasingType.Lower)
                {
                    if (value.ToLower() != value)
                    {
                        return "Must be all lowercase";
                    }
                }
                if (casingType == CasingType.Upper)
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
            this.casingType = casingType;
        }

        /// <summary>
        /// Changes the casing as required
        /// </summary>
        public string Enforce(string value)
        {
            if (casingType == CasingType.Lower)
            {
                return value.ToLower();
            }
            if (casingType == CasingType.Upper)
            {
                return value.ToUpper();
            }
            return value;
        }

        public override string Message
        {
            get
            {
                if (casingType == CasingType.Lower)
                {
                    return "Must be all lowercase";
                }
                if (casingType == CasingType.Upper)
                {
                    return "Must be all uppercase";
                }
                return string.Empty;
            }
        }
    }
}
