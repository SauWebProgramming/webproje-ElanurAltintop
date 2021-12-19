using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using InventoryBeginners.Models;
using InventoryBeginners.Interfaces;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace InventoryBeginners.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrand _Repo;


        public BrandController(IBrand Repo) //here the repository will be passed by the dependency injection
        {
            _Repo = Repo;
        }

        private SortModel ApplySort(string sortExpression)
        {
            ViewData["SortParamName"] = "name";
            ViewData["SortParamDesc"] = "description";
            ViewData["SortIconName"] = "";
            ViewData["SortIconDesc"] = "";


            SortModel sortModel = new SortModel();

            switch (sortExpression.ToLower())
            {
                case "name_desc":
                    sortModel.SortedOrder = SortOrder.Descending;
                    sortModel.SortedProperty = "name";
                    ViewData["SortIconName"] = "fa fa-arrow-up";
                    ViewData["SortParamName"] = "name";
                    break;


                case "description":
                    sortModel.SortedOrder = SortOrder.Ascending;
                    sortModel.SortedProperty = "description";
                    ViewData["SortIconName"] = "fa fa-arrow-up";
                    ViewData["SortParamName"] = "description_desc";
                    break;

                case "description_desc":
                    sortModel.SortedOrder = SortOrder.Descending;
                    sortModel.SortedProperty = "description";
                    ViewData["SortIconName"] = "fa fa-arrow-up";
                    ViewData["SortParamName"] = "description";
                    break;

                default:
                    sortModel.SortedOrder = SortOrder.Ascending;
                    sortModel.SortedProperty = "name";
                    ViewData["SortIconName"] = "fa fa-arrow-up";
                    ViewData["SortParamName"] = "name_desc";
                    break;

            }
            return sortModel;
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5) // read method of the crud operations. it lists all data from action.
        {
            SortModel sortModel = new SortModel();

            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;


            PaginatedList<Brand> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);//_context.Units.ToList();


            var pager = new PageModel(items.TotalRecords, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;

            return View(items);
        }



        public IActionResult Create()
        {
            Brand item = new Brand();
            return View(item);
        }

        [HttpPost]
        public IActionResult Create(Brand item)
        {

            bool bolret = false;
            string errMessage = "";
            try
            {

                if (item.Description.Length < 4 || item.Description == null)

                    errMessage = "Brand Description Must be atleast 4 Characters";

                if (_Repo.IsItemExists(item.Name) == true)

                    errMessage = errMessage + " " + " Brand Name " + item.Name + " Exists Already";

                if (errMessage == "")
                {
                    item = _Repo.Create(item);
                    bolret = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
            {
                TempData["SuccessMessage"] = "Category" + item.Name + "Created Succesfully";
                return RedirectToAction(nameof(Index));

            }
        }

        public IActionResult Details(int id)
        {
            Brand item = _Repo.GetItem(id);
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            Brand item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Brand item)
        {

            bool bolret = false;
            string errMessage = "";

            try
            {
                if (item.Description.Length < 4 || item.Description == null)
                    errMessage = "Category Description Must be atleast 4 Characters";

                if (_Repo.IsItemExists(item.Name, item.Id) == true)
                    errMessage = errMessage + "Category Name " + item.Name + " Already Exists";

                if (errMessage == "")
                {
                    item = _Repo.Edit(item);
                    TempData["SuccessMessage"] = item.Name + ", Category Saved Succesfully";
                    bolret = true;
                }

                item = _Repo.Edit(item);
            }
            catch (Exception ex)
            {
                errMessage = " " + ex.Message;
            }

            TempData["SuccessMessage"] = "Category " + item.Name + " Saved Succesfully";

            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }

            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }
            else
                return RedirectToAction(nameof(Index), new { pg = currentPage });
        }

        public IActionResult Delete(int id)
        {
            Brand item = _Repo.GetItem(id);
            TempData.Keep();
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Brand item)
        {
            try
            {
                item = _Repo.Delete(item);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(item);
            }

            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
            {
                currentPage = (int)TempData["CurrentPage"];
            }

            TempData["SuccessMessage"] = " Category " + item.Name + " Deleted Succesfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
    }
}

