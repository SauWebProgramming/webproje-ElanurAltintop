using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Models;


namespace InventoryBeginners.Interfaces
{
    
    public interface IUnit
    {
        PaginatedList<Unit> GetItems( string SortProperty, SortOrder sortOrder, string SearchText="", int pageIndex = 1, int pageSize = 5); //read all

        Unit GetUnit(int id);  //read particular items

        Unit Create(Unit unit);

        Unit Edit(Unit unit);

        Unit Delete(Unit unit);
        List<Unit> GetItems(SortOrder sortedOrder, string sortedProperty);

        public bool IsUnitNameExists(string name);
        public bool IsUnitNameExists(string name, int Id);

    }
}
