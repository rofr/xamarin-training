using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;

namespace Shopping
{
    /// <summary>
    /// Data access class wrapping the SqLite embedded database.
    /// It implements an interface which we can mock during unit testing
    /// </summary>
    public class GroceriesRepository : IGroceriesRepository
    {
        private readonly Lazy<SQLiteAsyncConnection> _connection;

        //Calling Value on the Lazy<> will trigger initialization
        private SQLiteAsyncConnection Connection => _connection.Value;

        public GroceriesRepository()
        {
            var folder = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(folder, "SqliteDatabase.db3");
            var flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;

            _connection = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var c = new SQLiteAsyncConnection(databasePath, flags);
                c.CreateTableAsync<GroceryListItem>();
                return c;
            });
        }

        
        public Task<int> Save(GroceryListItem groceryListItem)
        {
            return Connection.InsertAsync(groceryListItem);
        }


        public Task<List<GroceryListItem>> GetAll()
        {
            return Connection.Table<GroceryListItem>().ToListAsync();
        }

    }
}