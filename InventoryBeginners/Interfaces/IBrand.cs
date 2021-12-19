using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Models;


namespace InventoryBeginners.Interfaces
{

    public interface IBrand
    {
        PaginatedList<Brand> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all

        Brand GetItem(int id);  //read particular items

        Brand Create(Brand brand);

        Brand Edit(Brand brand);

        Brand Delete(Brand brand);
        List<Brand> GetItems(SortOrder sortedOrder, string sortedProperty);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);

    }
}
