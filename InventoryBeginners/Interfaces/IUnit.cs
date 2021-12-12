using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Models;


namespace InventoryBeginners.Interfaces
{
    
    public interface IUnit
    {
        List<Unit> GetItems( string SortProperty, SortOrder sortOrder); //read all

        Unit GetUnit(int id);  //read particular items

        Unit Create(Unit unit);

        Unit Edit(Unit unit);

        Unit Delete(Unit unit);
        List<Unit> GetItems(SortOrder sortedOrder, string sortedProperty);
        
    }
}
