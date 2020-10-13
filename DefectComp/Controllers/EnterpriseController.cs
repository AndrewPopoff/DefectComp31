using Microsoft.AspNetCore.Mvc;
using DefectComp.Models.EnterpriseTable;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DefectComp.Controllers
{
    [Authorize]
    public class EnterpriseController : Controller
    {
        private IEnterpriseRepository repository;

        public EnterpriseController(IEnterpriseRepository repo) => repository = repo;

        // список статусов
        public IActionResult Index(int page = 1, int currentID = 0, bool flagUpdate = false, string sort = "")
        {
            EnterpriseViewModel viewModel = new EnterpriseViewModel()
            {
                Enterprises = (IQueryable<Enterprise>)repository.Enterprises,
                Page = page,
                Sort = sort,
                CurrentID = currentID
            };

            if (currentID != 0 && flagUpdate)
                viewModel.Enterprises = viewModel.Enterprises.Where(p => p.EnterpriseID == currentID);

            return View(viewModel);
        }

        //  вызов формы на редактирование
        public ViewResult Edit(int enterpriseID, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Предприятие : редактирование";
            EnterpriseEditModel editModel = new EnterpriseEditModel()
            {
                Enterprise = repository.Enterprises.FirstOrDefault(p => p.EnterpriseID == enterpriseID),
                Page = page,
                CurrentID = enterpriseID,
                ReturnUrl = returnUrl,
                Sort = sort
            };
            return View(editModel);
        }

        // обработка формы после ввода данных
        [HttpPost]
        public IActionResult Edit(EnterpriseEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Save(editModel.Enterprise);
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Ошибка сохранения: {e.Message}";
                    return View(editModel);
                }

                TempData["message"] = $"Запись \"{editModel.Enterprise.EnterpriseDesc}\" сохранена!";

                return RedirectToAction("Index", new { page = editModel.Page, currentID = editModel.Enterprise.EnterpriseID, sort = editModel.Sort });
            }
            else
            {
                return View(editModel);
            }
        }

        // добавление данных
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Предприятия: добавление";
            return View("Edit", new EnterpriseEditModel() { Enterprise = new Enterprise(), Page = page, ReturnUrl = returnUrl, Sort = sort });
        }

        // удаление данных
        [HttpPost]
        public IActionResult Delete(int enterpriseID, int page = 1, string sort = "")
        {
            Enterprise delRec;
            try
            {
                delRec = repository.Delete(enterpriseID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message}";
                return RedirectToAction("Index", new { page, sort });
            }

            if (delRec != null)
                TempData["message"] = $"Запись \"{delRec.EnterpriseDesc}\" успешно удалена!";
            return RedirectToAction("Index", new { page, sort });
        }

    }
}











