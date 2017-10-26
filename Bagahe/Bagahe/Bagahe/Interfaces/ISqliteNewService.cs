using SQLite.Net.Async;

namespace Bagahe.Interfaces
{
    public interface ISqliteNewService
    {
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
