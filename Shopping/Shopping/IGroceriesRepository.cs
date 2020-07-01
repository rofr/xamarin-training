using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping
{
    public interface IGroceriesRepository
    {
        Task<int> Save(GroceryListItem groceryListItem);

        Task<List<GroceryListItem>> GetAll();
    }
}