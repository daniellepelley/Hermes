using System.Globalization;

namespace Hermes.Validation.Rules.Preset.Numeric
{
    public class MaximumRule : ComparisonRuleBase<int>
    {
        public MaximumRule()
            : base((v1, v2) => !v1.HasValue || !v2.HasValue || v1.Value <= v2.Value, string.Empty)
        { }

        public MaximumRule(int minValue)
            : this()
        {
            ComparisonValue = minValue;
            _message = "Cannot be more than " + minValue.ToString(CultureInfo.InvariantCulture);
        }

        public override int CompareTo(object obj)
        {
            if (((MaximumRule) obj).ComparisonValue <= ComparisonValue)
            {
                return 1;
            }
            return -1;
        }
    }
}