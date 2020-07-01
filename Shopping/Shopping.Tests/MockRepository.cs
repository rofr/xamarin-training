using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Tests
{
    public class MockRepository : IGroceriesRepository
    {
        private readonly List<GroceryListItem> _items;
        private int _nextId = 1;

        public Task<int> Save(GroceryListItem groceryListItem)
        {
            groceryListItem.Id = _nextId++;
            _items.Add(groceryListItem);
            return Task.FromResult(groceryListItem.Id);
        }

        public Task<List<GroceryListItem>> GetAll()
        {
            return Task.FromResult(new List<GroceryListItem>(_items));
        }

        public MockRepository(params GroceryListItem[] items)
        {
            _items = new List<GroceryListItem>(items);
        }
    }
}