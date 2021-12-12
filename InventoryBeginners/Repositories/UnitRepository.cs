using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Data;
using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;
using Microsoft.EntityFrameworkCore;



namespace InventoryBeginners.Repositories
{
    public class UnitRepository : IUnit
    {
        private readonly InventoryContext _context;



        public UnitRepository(InventoryContext context)
        {
            _context = context;
        }

        public Unit Create(Unit unit)
        {
            _context.Units.Add(unit);
            _context.SaveChanges();
            return unit;
        }

        public Unit Delete(Unit unit)
        {
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Deleted;
            _context.SaveChanges();
            return unit;
        }

        public Unit Edit(Unit unit)
        {
            _context.Units.Attach(unit);
            _context.Entry(unit).State = EntityState.Modified;
            _context.SaveChanges();
            return unit;
        }

        public List<Unit> GetItems(string SortProperty, SortOrder sortOrder) //unitcontroller da bulunan ındex için
        {
            List<Unit> units = _context.Units.ToList();
            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    units = units.OrderByDescending(n => n.Name).ToList();
                else
                    units = units.OrderBy(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    units = units.OrderBy(d => d.Description).ToList();
                else
                    units = units.OrderByDescending(d =>d.Description).ToList();
            }
            return units;
        }

        public List<Unit> GetItems(SortOrder sortedOrder, string sortedProperty)
        {
            throw new NotImplementedException();
        }

        

        public Unit GetUnit(int id)
        {
            Unit unit = _context.Units.Where(u => u.Id == id).FirstOrDefault();
            return unit;
        }

    }
}
