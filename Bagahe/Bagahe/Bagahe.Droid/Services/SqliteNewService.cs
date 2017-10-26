using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Bagahe.Interfaces;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(Bagahe.Droid.Services.SqliteNewService))]
namespace Bagahe.Droid.Services
{
    public class SqliteNewService : ISqliteNewService
    {
        public SqliteNewService()
        {
        }
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            string sqliteFileName = "BagaheSqlite.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, sqliteFileName);
            if (!File.Exists(path))
                File.Create(path);
            var connectionWithLock = new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(path, true));
            return new SQLiteAsyncConnection(() => connectionWithLock);            
        }
    }
}