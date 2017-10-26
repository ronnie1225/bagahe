using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;

namespace Bagahe.Interfaces
{
    public interface IFileService
    {
        SQLiteConnection GetLocalFilePath(string fileName);

        //SQLiteConnection GetConnection();

        SQLiteAsyncConnection GetAsyncConnection();
    }
}
