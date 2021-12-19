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
    public class BrandRepository : IBrand
    {
        private readonly InventoryContext _context;



        public BrandRepository(InventoryContext context)
        {
            _context = context;
        }

        public Brand Create(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return brand;
        }

        public Brand Delete(Brand brand)
        {
            _context.Brands.Attach(brand);
            _context.Entry(brand).State = EntityState.Deleted;
            _context.SaveChanges();
            return brand;
        }

        public Brand Edit(Brand Brand)
        {
            _context.Brands.Attach(Brand);
            _context.Entry(Brand).State = EntityState.Modified;
            _context.SaveChanges();
            return Brand;
        }

        private List<Brand> DoSort(List<Brand> items, string SortProperty, SortOrder sortOrder)
        {


            if (SortProperty.ToLower() == "name")
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderByDescending(n => n.Name).ToList();
                else
                    items = items.OrderBy(n => n.Name).ToList();
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                    items = items.OrderBy(d => d.Description).ToList();
                else
                    items = items.OrderByDescending(d => d.Description).ToList();
            }
            return items;
        }

        public PaginatedList<Brand> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5) //unitcontroller da bulunan ındex için
        {
            List<Brand> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.Brands.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Brands.ToList();
            }


            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Brand> retItems = new PaginatedList<Brand>(items, pageIndex, pageSize);

            return retItems;
        }



        public Brand GetItem(int id)
        {
            Brand items = _context.Brands.Where(u => u.Id == id).FirstOrDefault();
            return items;

        }

        public List<Brand> GetItems(SortOrder sortedOrder, string sortedProperty)
        {
            throw new NotImplementedException();
        }


        public bool IsItemExists(string name)
        {
            int ct = _context.Categories.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int Id)
        {
            int ct = _context.Categories.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
