using System.Globalization;

namespace Hermes.Validation.Rules.Preset.Numeric
{
    public class MinimumRule : ComparisonRuleBase<int>
    {
        #region Constructors

        public MinimumRule()
            : base((v1, v2) => !v1.HasValue || !v2.HasValue || v1 >= v2, string.Empty)
        { }

        public MinimumRule(int minValue)
            : this()
        {
            ComparisonValue = minValue;
            _message = "Cannot be less than " + minValue.ToString(CultureInfo.InvariantCulture);
        }


        public override int CompareTo(object obj)
        {
            if (((MinimumRule) obj).ComparisonValue <= ComparisonValue)
            {
                return 1;
            }
            return -1;
        }

        #endregion
    }
}