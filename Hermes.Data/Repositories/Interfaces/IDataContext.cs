namespace Hermes.Data.Repositories.Interfaces
{
    public interface IDataContext
    {
        bool AreChanges();

        void SaveChanges();
    }
}
