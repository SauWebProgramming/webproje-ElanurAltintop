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
    public class ProductProfileRepository : IProductProfile
    {
        private readonly InventoryContext _context;



        public ProductProfileRepository(InventoryContext context)
        {
            _context = context;
        }

        public ProductProfile Create(ProductProfile ProductProfile)
        {
            _context.ProductProfiles.Add(ProductProfile);
            _context.SaveChanges();
            return ProductProfile;
        }

        public ProductProfile Delete(ProductProfile ProductProfile)
        {
            _context.ProductProfiles.Attach(ProductProfile);
            _context.Entry(ProductProfile).State = EntityState.Deleted;
            _context.SaveChanges();
            return ProductProfile;
        }

        public ProductProfile Edit(ProductProfile ProductProfile)
        {
            _context.ProductProfiles.Attach(ProductProfile);
            _context.Entry(ProductProfile).State = EntityState.Modified;
            _context.SaveChanges();
            return ProductProfile;
        }

        private List<ProductProfile> DoSort(List<ProductProfile> items, string SortProperty, SortOrder sortOrder)
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

        public PaginatedList<ProductProfile> GetItems(string SortProperty, SortOrder sortOrder, string SearchText = "", int pageIndex = 1, int pageSize = 5) //unitcontroller da bulunan ındex için
        {
            List<ProductProfile> items;

            if (SearchText != "" && SearchText != null)
            {
                items = _context.ProductProfiles.Where(n => n.Name.Contains(SearchText) || n.Description.Contains(SearchText)).ToList();
            }
            else
            {
                items = _context.ProductProfiles.ToList();
            }


            items = DoSort(items, SortProperty, sortOrder);

            PaginatedList<ProductProfile> retItems = new PaginatedList<ProductProfile>(items, pageIndex, pageSize);

            return retItems;
        }



        public ProductProfile GetItem(int id)
        {
            ProductProfile items = _context.ProductProfiles.Where(u => u.Id == id).FirstOrDefault();
            return items;

        }

        public List<ProductProfile> GetItems(SortOrder sortedOrder, string sortedProperty)
        {
            throw new NotImplementedException();
        }


        public bool IsItemExists(string name)
        {
            int ct = _context.ProductProfiles.Where(n => n.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public bool IsItemExists(string name, int Id)
        {
            int ct = _context.ProductProfiles.Where(n => n.Name.ToLower() == name.ToLower() && n.Id != Id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
    }
}
