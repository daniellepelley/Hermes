namespace Hermes.Validation.Interfaces
{
    public interface IEnforcable
    { }

    public interface IEnforcable<T> : IEnforcable
    {
        T Enforce(T value);
    }
}
