using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using FrisanIonutLab7.Models;

namespace FrisanIonutLab7.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
        }

        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

        public Task<ShopList> GetShopListAsync(int id)
        {
            return _database.Table<ShopList>()
                                .Where(i => i.ID == id)
                                .FirstOrDefaultAsync();
        }

        public Task<int> SaveShopListAsync(ShopList shopList)
        {
            if (shopList.ID != 0)
            {
                return _database.UpdateAsync(shopList);
            }
            return _database.InsertAsync(shopList);
        }

        public Task<int> DeleteShopListAsync(ShopList shopList)
        {
            return _database.DeleteAsync(shopList);
        }
    }
}
