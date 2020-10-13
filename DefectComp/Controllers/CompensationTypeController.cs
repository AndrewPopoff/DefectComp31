using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefectComp.Models.CompensationTypesTable;
using Microsoft.AspNetCore.Authorization;

namespace DefectComp.Controllers
{
    [Authorize]
    public class CompensationTypeController : Controller
    {
        private ICompensationTypeRepository repository;

        public CompensationTypeController(ICompensationTypeRepository repo) => repository = repo;

        // список статусов
        public IActionResult Index(int page = 1, int currentID = 0, bool flagUpdate = false, string sort = "")
        {
            CompensationTypeViewModel viewModel = new CompensationTypeViewModel()
            {
                CompensationTypes = (IQueryable<CompensationType>)repository.CompensationTypes,
                Page = page,
                Sort = sort,
                CurrentID = currentID
            };

            if (currentID != 0 && flagUpdate)
                viewModel.CompensationTypes = viewModel.CompensationTypes.Where(p => p.TypeID == currentID);

            return View(viewModel);
        }

        //  вызов формы на редактирование
        public ViewResult Edit(int typeID, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Типы компенсаций : редактирование";
            CompensationTypeEditModel editModel = new CompensationTypeEditModel()
            {
                CompensationType = repository.CompensationTypes.FirstOrDefault(p => p.TypeID == typeID),
                Page = page,
                CurrentID = typeID,
                ReturnUrl = returnUrl,
                Sort = sort
            };
            return View(editModel);
        }

        // обработка формы после ввода данных
        [HttpPost]
        public IActionResult Edit(CompensationTypeEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Save(editModel.CompensationType);
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Ошибка сохранения: {e.Message}";
                    return View(editModel);
                }

                TempData["message"] = $"Запись \"{editModel.CompensationType.TypeDesc}\" сохранена!";

                return RedirectToAction("Index", new { page = editModel.Page, currentID = editModel.CompensationType.TypeID, sort = editModel.Sort });
            }
            else
            {
                return View(editModel);
            }
        }


        // добавление данных
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Типы компенсаций: добавление";
            return View("Edit", new CompensationTypeEditModel() { CompensationType  = new CompensationType(), Page = page, ReturnUrl = returnUrl, Sort = sort });
        }

        // удаление данных
        [HttpPost]
        public IActionResult Delete(int typeID, int page = 1, string sort = "")
        {
            CompensationType delRec;
            try
            {
                delRec = repository.Delete(typeID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message}";
                return RedirectToAction("Index", new { page, sort });
            }

            if (delRec != null)
                TempData["message"] = $"Запись \"{delRec.TypeDesc}\" успешно удалена!";
            return RedirectToAction("Index", new { page, sort });
        }

    }
}







