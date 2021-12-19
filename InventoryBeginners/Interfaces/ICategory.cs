using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Models;


namespace InventoryBeginners.Interfaces
{

    public interface ICategory
    {
        PaginatedList<Category> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all

        Category GetItem(int id);  //read particular items

        Category Create(Category category);

        Category Edit(Category category);

        Category Delete(Category category);
        List<Category> GetItems(SortOrder sortedOrder, string sortedProperty);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);

    }
}
