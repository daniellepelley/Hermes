using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hermes.Validation.Interfaces;
using Hermes.Validation.Rules.Preset.Numeric;
using Hermes.Validation.Rules.Preset.String;

namespace Hermes.Mvc.Angular
{
    public class RulesToAttributeConverter
    {
        public Dictionary<string, string> CreateAttributes(IEnumerable<IRule> rules)
        {
            return rules
                .Select(CreateAttribute)
                .Where(result => result.Length == 2)
                .ToDictionary(result => result[0], result => result[1]);
        }

        public string[] CreateAttribute(IRule rule)
        {
            if (rule == null)
                return new string[0];

            if (rule is MaximumLengthRule)
            {
                return new[]
                {
                    "maxlength",
                    ((MaximumLengthRule) rule).Length.ToString()
                };
            }
            if (rule is MaximumRule)
            {
                return new[]
                {
                    "max",
                    ((MaximumRule) rule).ComparisonValue.ToString(CultureInfo.InvariantCulture)
                };
            }
            if (rule is MinimumRule)
            {
                return new[]
                {
                    "min",
                    ((MinimumRule) rule).ComparisonValue.ToString(CultureInfo.InvariantCulture)
                };
            }

            return new string[0];
        }
    }
}
