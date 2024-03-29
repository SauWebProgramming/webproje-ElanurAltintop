﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryBeginners.Data;
using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;
using Microsoft.EntityFrameworkCore;



namespace InventoryBeginners.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly InventoryContext _context;



        public CategoryRepository(InventoryContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(Category category)
        {
            _context.Categories.Attach(category);
            _context.Entry(category).State = EntityState.Deleted;
            _context.SaveChanges();
            return category;
        }

        public Category Edit(Category category)
        {
            _context.Categories.Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return category;
        }

        private List<Category> DoSort(List<Category> items, string SortProperty, SortOrder sortOrder)
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

        public PaginatedList<Category> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5) //unitcontroller da bulunan ındex için
        {
            List<Category> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.Categories.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.Categories.ToList();
            }


            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<Category> retItems = new PaginatedList<Category>(items, pageIndex, pageSize);

            return retItems ;
        }



        public Category GetItem(int id)
        {
            Category items = _context.Categories.Where(u => u.Id == id).FirstOrDefault();
            return items;

        }

        public List<Category> GetItems(SortOrder sortedOrder, string sortedProperty)
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

