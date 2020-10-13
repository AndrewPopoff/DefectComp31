using Microsoft.AspNetCore.Mvc;
using DefectComp.Models.DepartmentsTable;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DefectComp.Controllers 
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository repository;
        public DepartmentController(IDepartmentRepository repo) => repository = repo;

        // список отделов
        public IActionResult Index(int page = 1, int currentID = 0, bool flagUpdate = false, string sort = "")
        {
            DepartmentViewModel viewModel = new DepartmentViewModel()
            {
                Departments = (IQueryable<Department>)repository.Departments,
                Page = page,
                Sort = sort,
                CurrentID = currentID
            };

            if (currentID != 0 && flagUpdate)
                viewModel.Departments = viewModel.Departments.Where(p => p.DepartmentID == currentID);

            return View(viewModel);
        }

        //  вызов формы на редактирование
        public ViewResult Edit(int departmentID, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Отделы : редактирование";
            DepartmentEditModel editModel = new DepartmentEditModel()
            {
                Department = repository.Departments.FirstOrDefault(p => p.DepartmentID == departmentID),
                Page = page,
                CurrentID = departmentID,
                ReturnUrl = returnUrl,
                Sort = sort
            };
            return View(editModel);
        }

        // обработка формы после ввода данных
        [HttpPost]
        public IActionResult Edit(DepartmentEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.Save(editModel.Department);
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Ошибка сохранения: {e.Message}";
                    return View(editModel);
                }

                TempData["message"] = $"Запись \"{editModel.Department.DepartmentDesc}\" сохранена!";

                return RedirectToAction("Index", new { page = editModel.Page, currentID = editModel.Department.DepartmentID, sort = editModel.Sort });
            }
            else
            {
                return View(editModel);
            }
        }

        // добавление данных
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Отделы: добавление";
            return View("Edit", new DepartmentEditModel() { Department = new Department(), Page = page, ReturnUrl = returnUrl, Sort = sort });
        }

        // удаление данных
        [HttpPost]
        public IActionResult Delete(int departmentID, int page = 1, string sort = "")
        {
            Department delRec;
            try
            {
                delRec = repository.Delete(departmentID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message}";
                return RedirectToAction("Index", new { page, sort });
            }

            if (delRec != null)
                TempData["message"] = $"Запись \"{delRec.DepartmentDesc}\" успешно удалена!";
            return RedirectToAction("Index", new { page, sort });
        }

    }
}

