using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using SQLite.Net;
using SQLite.Net.Async;
using Bagahe.Interfaces;
using System.IO;
using Xamarin.Forms;
using Bagahe.Models;

namespace Bagahe.Services
{
    public class InitializeSqliteService : IInitializeSqliteService
    {
        private static readonly Lazy<InitializeSqliteService> Lazy = new Lazy<InitializeSqliteService>(() => new InitializeSqliteService());

        public static InitializeSqliteService Instance => Lazy.Value;

        private InitializeSqliteService()
        {
        }

        private SQLiteAsyncConnection _dbAsyncConn;
        public SQLiteAsyncConnection DBAsyncConn
        {
            get
            {
                if(_dbAsyncConn == null)
                {
                    LazyInitializer.EnsureInitialized(ref _dbAsyncConn, DependencyService.Get<ISqliteNewService>().GetAsyncConnection);
                }

                return _dbAsyncConn;
            }
        }

        public async Task CreateDB()
        {
            await DBAsyncConn.CreateTableAsync<UserModel>();
            //await DBAsyncConn.InsertAsync(new UserModel { Username = "user", IsLoggedIn = false, Name = String.Empty });

            CustomAppStart.Connection = DBAsyncConn;
        }


        //codes below not used
        public async Task InitializeAsync()
        {
            //dbConn = DependencyService.Get<IFileService>().GetConnection();

            //dbConn.CreateTable<UserModel>();
            //dbConn.CreateTable<BagInfoModel>();
            //var db = new SqliteDatabase(GetConnection);
            //if (!DatabaseExists())
            //    await db.InitializeAsync();
            //CustomAppStart.Connection = db;
            //Xamarin.Forms.DependencyService.Register<IFileService>();
            //_dbAsyncConn = DependencyService.Get<IFileService>().GetAsyncConnection();


            if(_dbAsyncConn == null)
                _dbAsyncConn = DependencyService.Get<ISqliteNewService>().GetAsyncConnection();

            await _dbAsyncConn.CreateTableAsync<UserModel>();
            //await _dbAsyncConn.CreateTableAsync<BagInfoModel>();
            await SeedAsync();
            CustomAppStart.Connection = _dbAsyncConn;
        }       
        
        private async Task SeedAsync()
        {
            await _dbAsyncConn.InsertAsync(new UserModel { Username = "user", IsLoggedIn = false, Name = String.Empty });
        }
    }
}
