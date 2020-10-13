using Microsoft.AspNetCore.Mvc;
using DefectComp.Models;
using DefectComp.Models.StatusesTable;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DefectComp.Controllers
{
    [Authorize]
    public class StatusController : Controller
    {
        private IStatusesRepository repository;

        public StatusController(IStatusesRepository repo) => repository = repo;

        // список статусов
        public IActionResult Index(int page = 1, int currentID = 0, bool flagUpdate = false, string sort = "")
        {
            StatusViewModel viewModel = new StatusViewModel()
            {
                Statuses = (IQueryable<Status>)repository.Statuses,
                Page = page,
                Sort = sort,
                CurrentID = currentID
            };

            if (currentID != 0 && flagUpdate)
                viewModel.Statuses = viewModel.Statuses.Where(p => p.StatusID == currentID);

            ViewData["Title"] = "Статусы компенсации";

            return View(viewModel);
        }

        //  вызов формы на редактирование
        public ViewResult Edit(int statusID, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Статусы заявки : редактирование";
            StatusEditModel editModel = new StatusEditModel()
            {
                Status = repository.Statuses.FirstOrDefault(p => p.StatusID == statusID),
                Page = page,
                CurrentID = statusID,
                ReturnUrl = returnUrl,
                Sort = sort
            };
            return View(editModel);
        }

        // обработка формы после ввода данных
        [HttpPost]
        public IActionResult Edit(StatusEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Save(editModel.Status);
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Ошибка сохранения: {e.Message}";
                    return View(editModel);
                }

                TempData["message"] = $"Запись \"{editModel.Status.StatusDesc}\" сохранена!";

                return RedirectToAction("Index", new { page = editModel.Page, currentID = editModel.Status.StatusID, sort = editModel.Sort });
            }
            else
            {
                return View(editModel);
            }
        }

        // добавление данных
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Статусы заявки: добавление";
            return View("Edit", new StatusEditModel() { Status  = new Status(), Page = page, ReturnUrl = returnUrl, Sort = sort });
        }

        // удаление данных
        [HttpPost]
        public IActionResult Delete(int statusID, int page = 1, string sort = "")
        {
            Status delRec;
            try
            {
                delRec = repository.Delete(statusID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message}";
                return RedirectToAction("Index", new { page, sort });
            }

            if (delRec != null)
                TempData["message"] = $"Запись \"{delRec.StatusDesc}\" успешно удалена!";
            return RedirectToAction("Index", new { page, sort });
        }

    }
}






