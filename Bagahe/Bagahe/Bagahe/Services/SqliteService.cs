using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Interfaces;
using Bagahe.Models;
using SQLite.Net.Async;
using SQLite.Net;

namespace Bagahe.Services
{
    public class SqliteService<T> : ISqliteService<T> where T : class, new()
    {
        public async Task InsertUpdate(T entity)
        {
            await CustomAppStart.Connection.InsertOrReplaceAsync(entity);
        }

        public async Task<T> Load()
        {
            var ret = await CustomAppStart.Connection.Table<T>().FirstOrDefaultAsync();
            return ret;
        }

        public async Task<T> Load(string id)
        {
            var ret = await CustomAppStart.Connection.GetAsync<T>(id);
            return ret;
        }
    }

    public class SqliteDatabase : SQLiteAsyncConnection
    {
        public SqliteDatabase(Func<SQLiteConnectionWithLock> sqliteConnectionFunc, TaskScheduler taskScheduler = null,
           TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
           : base(sqliteConnectionFunc, taskScheduler, taskCreationOptions)
        {

        }
        public async Task InitializeAsync()
        {

            await CreateTableAsync<UserModel>();
            //await CreateTableAsync<BagInfoModel>();

            await InsertAsync(new UserModel { Username = "user", IsLoggedIn = false, Name = String.Empty });
        }

    }
}
