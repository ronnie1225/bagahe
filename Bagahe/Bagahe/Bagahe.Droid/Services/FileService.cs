using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bagahe.Interfaces;
using Xamarin.Forms;
//using SQLite;
using SQLite.Net.Async;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Bagahe.Services;

[assembly: Dependency(typeof(Bagahe.Droid.Services.FileService))]
namespace Bagahe.Droid.Services
{
    public class FileService : IFileService
    {
        public SQLite.Net.SQLiteConnection GetLocalFilePath(string filename)
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentPath, filename);
            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);
            return connection;
            //return path;
        }

        private static SQLite.Net.SQLiteConnection GetSyncConnection()
        {
            var sqliteFileName = "BagaheSqlite.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, sqliteFileName);
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            return new SQLiteConnection(new SQLitePlatformAndroid(), path);
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var x = new SqliteDatabase(GetConnection);
            if (!File.Exists(GetDatabasePath()))
            {
                File.Create(GetDatabasePath());
            }
            //return x;
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(GetDatabasePath(), storeDateTimeAsTicks: false)));
            return new SQLiteAsyncConnection(connectionFactory);
            //return new SQLiteAsyncConnection(() => GetSyncConnection());
        }

        private static string GetDatabasePath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "BagaheSqlite.db3");
        }

        private static SQLiteConnectionWithLock GetConnection()
        {
            return new SQLiteConnectionWithLock(new SQLitePlatformAndroid(), new SQLiteConnectionString(GetDatabasePath(), false));
        }
    }
}