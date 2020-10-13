using Microsoft.AspNetCore.Mvc;
using DefectComp.Models.SCsTable;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DefectComp.Controllers
{
    [Authorize]
    public class SCController : Controller
    {
        private ISCRepository repository;

        public SCController(ISCRepository repo) => repository = repo;

        // список сервисных центров
        public IActionResult Index(int page = 1, int currentID = 0, bool flagUpdate = false, string sort = "")
        {
            SCViewModel viewModel = new SCViewModel()
            {
                SCs = (IQueryable<SC>) repository.SCs,
                Page = page,
                Sort = sort,
                CurrentID = currentID
            };

            if (currentID != 0 && flagUpdate)
                viewModel.SCs = viewModel.SCs.Where(p => p.SC_ID == currentID);

            return View(viewModel);
        }

        //  вызов формы на редактирование
        public ViewResult Edit(int SC_ID, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Сервисные центры : редактирование";
            SCEditModel editModel = new SCEditModel()
            {
                SC = repository.SCs.FirstOrDefault(p => p.SC_ID == SC_ID),
                Page = page,
                CurrentID = SC_ID,
                ReturnUrl = returnUrl,
                Sort = sort
            };
            return View(editModel);
        }

        // обработка формы после ввода данных
        [HttpPost]
        public IActionResult Edit(SCEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Save(editModel.SC);
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Ошибка сохранения: {e.Message}";
                    return View(editModel);
                }

                TempData["message"] = $"Запись \"{editModel.SC.SCDesc}\" сохранена!";

                return RedirectToAction("Index", new { page = editModel.Page, currentID = editModel.SC.SC_ID, sort = editModel.Sort });
            }
            else
            {
                return View(editModel);
            }
        }

        // добавление данных
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Сервисные центры: добавление";
            return View("Edit", new SCEditModel() { SC = new SC(), Page = page, ReturnUrl = returnUrl, Sort = sort });
        }

        // удаление данных
        [HttpPost]
        public IActionResult Delete(int SC_ID, int page = 1, string sort = "")
        {
            SC delRec;
            try
            {
                delRec = repository.Delete(SC_ID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message}";
                return RedirectToAction("Index", new { page, sort });
            }

            if (delRec != null)
                TempData["message"] = $"Запись \"{delRec.SCDesc}\" успешно удалена!";
            return RedirectToAction("Index", new { page, sort });
        }

    }
}








    