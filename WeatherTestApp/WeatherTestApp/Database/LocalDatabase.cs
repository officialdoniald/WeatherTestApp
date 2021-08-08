using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using WeatherTestApp.Database.Entities;

namespace WeatherTestApp.Database
{
    public class LocalDatabase
    {
        public const string DatabaseFilename = "WeatherTestApp.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabasePath, Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        static bool initialized = false;

        public LocalDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        public async Task<int> Add<T>(T entity)
        {
            return await Database.InsertAsync(entity);
        }

        public async Task<List<TownEntity>> GetTowns()
        {
            return await Database.Table<TownEntity>().ToListAsync();
        }

        public async Task DeleteTown(int id)
        {
            await Database.Table<TownEntity>().DeleteAsync(t => t.Id == id);
        }

        public async Task<TownEntity> GetTownByName(string name)
        {
            return await Database.Table<TownEntity>().FirstOrDefaultAsync(t => t.Town.ToLower() == name.ToLower());
        }

        public async Task InitializeAsync()
        {
            if (!initialized)
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TownEntity).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TownEntity)).ConfigureAwait(false);
                }

            initialized = true;
        }
    }

    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}