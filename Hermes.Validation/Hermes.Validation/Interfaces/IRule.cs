namespace Hermes.Validation.Interfaces
{
    public interface IRule
    {
        string Message { get; }
    }

    public interface IRule<T>
    : IRule
    {
        string Check(T value);
        bool CheckValid(T value);
    }
}
