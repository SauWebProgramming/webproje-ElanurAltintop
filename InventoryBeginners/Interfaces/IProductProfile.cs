using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Models;


namespace InventoryBeginners.Interfaces
{

    public interface IProductProfile
    {
        PaginatedList<ProductProfile> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5); //read all

        ProductProfile GetItem(int id);  //read particular items

        ProductProfile Create(ProductProfile productProfile);

        ProductProfile Edit(ProductProfile productProfile);

        ProductProfile Delete(ProductProfile productProfile);
        List<ProductProfile> GetItems(SortOrder sortedOrder, string sortedProperty);

        public bool IsItemExists(string name);
        public bool IsItemExists(string name, int Id);

    }
}
