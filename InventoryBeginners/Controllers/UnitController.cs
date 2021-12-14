using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using InventoryBeginners.Models;
using InventoryBeginners.Interfaces;
using System.Linq;

namespace InventoryBeginners.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnit _unitRepo;


        public UnitController(IUnit unitRepo) //here the repository will be passed by the dependency injection
        {
            _unitRepo = unitRepo;
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

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg=1 ,int pageSize = 5) // read method of the crud operations. it lists all data from action.
        {
            SortModel sortModel = new SortModel();

            sortModel.AddColumn("name");
            sortModel.AddColumn("description");   
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = SearchText;


            PaginatedList<Unit> units = _unitRepo.GetItems( sortModel.SortedProperty, sortModel.SortedOrder, SearchText,pg, pageSize);//_context.Units.ToList();

            // int totRcs = ((PaginatedList<Unit>)units).TotalRecords;

            var pager = new PageModel(units.TotalRecords ,pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            // units = units.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            return View(units);
        }
        
       

        public IActionResult Create()
        {
            Unit unit = new Unit();
            return View(unit);
        }

        [HttpPost]
        public IActionResult Create(Unit unit)
        {
            try
            {
               unit= _unitRepo.Create(unit);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Unit unit = _unitRepo.GetUnit(id);
            return View(unit);
        }

        public IActionResult Edit(int id)
        {
            Unit unit = _unitRepo.GetUnit(id);
            return View(unit);
        }

        [HttpPost]
        public IActionResult Edit(Unit unit)
        {
            try
            {
                unit = _unitRepo.Edit(unit);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Unit unit = _unitRepo.GetUnit(id);
            return View(unit);
        }

        [HttpPost]
        public IActionResult Delete (Unit unit)
        {
            try
            {
                unit = _unitRepo.Delete(unit);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }


    }
}
