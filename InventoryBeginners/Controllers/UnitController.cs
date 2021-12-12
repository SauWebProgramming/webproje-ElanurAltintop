using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using InventoryBeginners.Models;
using InventoryBeginners.Interfaces;
using InventoryBeginners.Models;

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

        public IActionResult Index(string sortExpression="") // read method of the crud operations. it lists all data from 
        {
            SortModel sortModel = new SortModel();

            sortModel.AddColumn("name");
            sortModel.AddColumn("description");   
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            List<Unit> units = _unitRepo.GetItems( sortModel.SortedProperty, sortModel.SortedOrder);//_context.Units.ToList();
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
