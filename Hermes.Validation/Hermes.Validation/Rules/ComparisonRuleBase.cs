using System;
using Hermes.Validation.Rules;

public abstract class ComparisonRuleBase<T> : Rule<T>
    where T : struct
{
    protected string _message;

    public override string Message
    {
        get { return _message; }
    }

    private T _comparisonValue;

    public T ComparisonValue
    {
        set { _comparisonValue = value; }
        get { return _comparisonValue; }
    }

    private Func<T?, T?, bool> _comparisonFunc;

    public Func<T?, T?, bool> ComparisonFunc
    {
        get { return _comparisonFunc; }
        set { _comparisonFunc = value; }
    }

    protected ComparisonRuleBase(Func<T?, T?, bool> comparisonFunc, string message)
    {
        Logic = value =>
        {
            if (_comparisonFunc != null &&
                _comparisonFunc(value, _comparisonValue))
                return string.Empty;
            return Message;
        };

        _comparisonFunc = comparisonFunc;
        _message = message;
    }

    protected ComparisonRuleBase(T comparisonValue, Func<T?, T?, bool> comparisonFunc, string text)
        : this(comparisonFunc, text)
    {
        _comparisonValue = comparisonValue;
    }

}