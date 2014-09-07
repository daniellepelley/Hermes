using System;

namespace Hermes.Validation.Rules
{
    /// <summary>
    /// A rule which sets the casing type for a string.
    /// </summary>
    public class CustomRule<T>
        : Rule<T>
    {
        public CustomRule(Func<T, string> logicFunc, string message)
            : base(message, logicFunc)
        { }
    }
}
