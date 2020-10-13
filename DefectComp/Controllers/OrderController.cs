using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefectComp.Models.OrdersTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NonFactors.Mvc.Lookup;
using DefectComp.Models.GoodsTable;
using DefectComp.Models;
using DefectComp.Models.EnterpriseTable;
using Microsoft.AspNetCore.Authorization;
using DefectComp.Models.NotesTable;
using DefectComp.Models.CommonLogTable;

namespace DefectComp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private ApplicationDBContext context;
        private INoteRepository noteRepository;
        private ICommonLogRepository logRepository;

        public OrderController(IOrderRepository repo,INoteRepository noteRepo,ICommonLogRepository logRepo)
        {
            repository = repo;
            noteRepository = noteRepo;
            logRepository = logRepo;
            context = repo.Context;
        }

        // список статусов
        public IActionResult Index(int page = 1, int currentID = 0, bool flagUpdate = false, string sort = "")
        {
            // читаем из куков 
            DateTime tmpStartDate = DateTime.Today.AddDays(-7);     // начальные значения периода
            DateTime tmpEndDate = DateTime.Today;
            byte tmpFlagClosed = 0;                                 // начальное значение фильтра - смотрим все
            string tmpDate = HttpContext.Request.Cookies["OrderController:StartDate"];
            if (tmpDate != null)
                tmpStartDate = Convert.ToDateTime(tmpDate);
            tmpDate = HttpContext.Request.Cookies["OrderController:EndDate"];
            if (tmpDate != null)
                tmpEndDate = Convert.ToDateTime(tmpDate);
            string tmpFlag = HttpContext.Request.Cookies["OrderController:FlagClosed"];
            if (tmpFlag != null)
                tmpFlagClosed = Convert.ToByte(tmpFlag);
            // конец чтения данных из куков

            OrderViewModel viewModel = new OrderViewModel()
            {
                Orders = repository.Orders
                                    .Include(a => a.Goods)
                                    .Include(b => b.Seller)
                                    .Include(c => c.Department)
                                    .Include(d => d.Status)
                                    .Where(dd => dd.DocDate >= tmpStartDate && dd.DocDate < tmpEndDate.AddDays(1)),
                Page = page,
                Sort = sort,
                CurrentID = currentID,
                StartDate = tmpStartDate,
                EndDate = tmpEndDate,
                FlagClosed = tmpFlagClosed
            };

            if (tmpFlagClosed == 2) // только закрытые
                viewModel.Orders = viewModel.Orders.Where(d => d.FlagClosed == 1);

            if(tmpFlagClosed == 1) // только открытые
                viewModel.Orders = viewModel.Orders.Where(d => d.FlagClosed == 0);

            if (currentID != 0 && flagUpdate) // ищем без учета фильтра по датам и FlagClosed
                viewModel.Orders = repository.Orders
                                    .Include(a => a.Goods)
                                    .Include(b => b.Seller)
                                    .Include(c => c.Department)
                                    .Include(d => d.Status)
                                    .Where(p => p.ID == currentID);

            return View(viewModel);
        }

        // применить фильтр
        public IActionResult ApplyFilter(DateTime StartDate, DateTime EndDate, int FlagClosed, string sort)
        {
            // сохранить в куки
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddYears(666);
            Response.Cookies.Append("OrderController:StartDate", StartDate.ToString(), options);
            Response.Cookies.Append("OrderController:EndDate", EndDate.ToString(), options);
            Response.Cookies.Append("OrderController:FlagClosed", FlagClosed.ToString(), options);

            return RedirectToAction("Index", new {sort});
        }

        // удаление данных
        [HttpPost]
        public IActionResult Delete(int orderID, int page = 1, string sort = "")
        {
            Order delRec;
            try
            {
                delRec = repository.Delete(orderID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message} {e.InnerException.Message}";
                return RedirectToAction("Index", new { page, sort });
            }

            if (delRec != null)
            {
                TempData["message"] = $"Запись № \"{delRec.Number}\" успешно удалена!";
                CommonLog log = new CommonLog()
                {
                    Action = "Orders",
                    ActionID = orderID,
                    ActionDate = DateTime.Now,
                    UserDesc = User.Identity.Name,
                    TxtMsg = "Удаление",
                    _UID = Guid.NewGuid()
                };
                logRepository.Save(log);
            }


            return RedirectToAction("Index", new { page, sort });
        }

        //  вызов формы на редактирование
        public IActionResult Edit(int orderID, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Редактирование заявки";
            OrderEditModel editModel = new OrderEditModel()
            {
                Order = repository.Orders.FirstOrDefault(p => p.ID == orderID),
                Note = new Note()
                {
                    NoteDate = DateTime.Today,
                    DataID = orderID,
                    UserID = User.Identity.Name
                },
                NoteViewModel = new NoteViewModel()
                {
                    Notes = repository.Notes.Where(p => p.DataID == orderID).OrderBy(p => p.NoteDate)
                },
                CommonLogViewModel = new CommonLogViewModel()
                {
                    CommonLogs = repository.CommonLogs.Where(p => p.ActionID == orderID).OrderBy(p => p.ActionDate)
                },
                Page = page,
                CurrentID = orderID,
                ReturnUrl = returnUrl,
                Sort = sort
            };

            if(editModel.Order != null)
                ViewData["Title"] = "Редактирование заявки №" + Convert.ToString(editModel.Order.Number);
            FillSelectItems(editModel);

            return View(editModel);
        }

        // обработка формы после ввода данных
        [HttpPost]
        public IActionResult Edit(OrderEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(editModel.Order.ID == 0) // если это добавление, то нумеруем автоматически
                    {
                        editModel.Order.Number = (repository.Orders.Max(p => p.Number) ?? 0) + 1;
                    }
                    repository.Save(editModel.Order);
                }
                catch (Exception e)
                {
                    TempData["message"] = $"Ошибка сохранения: {e.Message}";
                    return View(editModel);
                }

                TempData["message"] = $"Запись № \"{editModel.Order.Number}\" сохранена!";

                CommonLog log = new CommonLog()
                {
                    Action = "Orders",
                    ActionID = editModel.Order.ID,
                    ActionDate = DateTime.Now,
                    UserDesc = User.Identity.Name,
                    TxtMsg = (editModel.Order.ID == 0) ? "Добавление: " : "Изменение: " +  GetOrderDesc(editModel.Order),
                    _UID = Guid.NewGuid()
                };
                logRepository.Save(log);

                return RedirectToAction("Index", new { page = editModel.Page, currentID = editModel.Order.ID, sort = editModel.Sort });
            }
            else
            {
                FillSelectItems(editModel);
                return View(editModel);
            }
        }

        // получить описание заявки в текстовом формате
        private string GetOrderDesc(Order order)
        {
            IQueryable<Order> orders = repository.Orders
                                .Include(a => a.Goods)
                                .Include(b => b.Seller)
                                .Include(c => c.Department)
                                .Include(d => d.Status)
                                .Include(e => e.SC)
                                .Where(p => p.ID == order.ID);
            string result = "";

            foreach (Order o in orders) // в этом цикле только одна запись
            {
                if (o.Number != null)
                    result += $"№: {o.Number}";

                if (o.DocDate != null)
                    result += $" Дата {o.DocDate,0:d}";

                if (o.RefNumber != null)
                    result += $" № справки: {o.RefNumber}";

                if (o.RefDate != null)
                    result += $" Дата справки: {o.RefDate,0:d}";

                if (o.CustomerDesc  != null)
                    result += $" Клиент: {o.CustomerDesc}";

                if (o.Goods?.GoodsName != null)
                    result += $" Товар: {o.Goods.GoodsName}";

                if (o.Model != null)
                    result += $" Модель: {o.Model}";

                if (o.SN != null)
                    result += $" СН: {o.SN}";

                if (o.Seller?.EnterpriseDesc != null)
                    result += $" Продавец: {o.Seller?.EnterpriseDesc}";

                if (o.Status?.StatusDesc != null)
                    result += $" Статус: {o.Status?.StatusDesc}";

                if (o.CompensationType?.TypeDesc != null)
                    result += $" Тип компенсации: {o.CompensationType.TypeDesc}";

                if (o.Department?.DepartmentDesc != null)
                    result += $" Отдел: {o.Department?.DepartmentDesc}";

                if (o.FlagClosed == 0)
                    result += " ОТКРЫТ";
                else
                    result += " ЗАКРЫТ";
                if (o.ClosedDate != null)
                    result += $" Дата закрытия: {o.ClosedDate,0:d}";

                if (o.ProviderID != null)
                    result += $" Поставщик: {o.Provider.EnterpriseDesc}";

                if (o.SFNumber != null)
                    result += $" № СФ: {o.SFNumber}";

                if (o.SFDate != null)
                    result += $" Дата СФ: {o.SFDate,0:d}";

                if (o.GoodsCost != null)
                    result += $" Стоимость: {o.GoodsCost}";

                if (o.SC_ID != null)
                    result += $" СЦ: {o.SC.SCDesc}";

                if (o.ProduceDate != null)
                    result += $" Дата производства: {o.ProduceDate,0:d}";

                if (o.ProviderSaleDate != null)
                    result += $" Дата продажи: {o.ProviderSaleDate,0:d}";

                if (o.NumInvoiceOPT != null)
                    result += $" № опт: {o.NumInvoiceOPT}";

                if (o.DateInvoiceOPT != null)
                    result += $" Дата опт: {o.DateInvoiceOPT,0:d}";
            }
            return result;
        }

        // добавление данных
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Добавление новой заявки";

            OrderEditModel editModel = new OrderEditModel()
            {
                Order = new Order()
                {
                    DocDate = DateTime.Now
                },
                Note = new Note(),
                NoteViewModel = new NoteViewModel()
                {
                    Notes = repository.Notes.Where(p => p.DataID == 0)
                },
                CommonLogViewModel = new CommonLogViewModel()
                {
                    CommonLogs = repository.CommonLogs.Where(p => p.ActionID == 0)
                },
                Page = page,
                ReturnUrl = returnUrl,
                Sort = sort
            };

            FillSelectItems(editModel);

            return View("Edit", editModel);
        }

        // lookup для выбора товара
        [HttpGet]
        public JsonResult LookupGoods(LookupFilter filter)
        {
            GoodsLookup lookup = new GoodsLookup(context);
            lookup.Filter = filter;
            return Json(lookup.GetData());
        }

        // lookup для выбора предприятия
        [HttpGet]
        public JsonResult LookupEnterprises(LookupFilter filter)
        {
            EnterpriseLookup lookup = new EnterpriseLookup(context);
            lookup.Filter = filter;
            return Json(lookup.GetData());
        }

        // заполнение выпадающих списков
        public void FillSelectItems(OrderEditModel editModel)
        {
            editModel.Order.itemsSC = new SelectList(repository.SCs, "SC_ID", "SCDesc");
            editModel.Order.itemsStatus = new SelectList(repository.Statuses, "StatusID", "StatusDesc");
            editModel.Order.itemsCompType = new SelectList(repository.CompensationTypes, "TypeID", "TypeDesc");
            editModel.Order.itemsDep = new SelectList(repository.Departments, "DepartmentID", "DepartmentDesc");
        }

        // получить список всех комментариев к заявке
        [HttpGet]
        public ActionResult OrderNotes(int dataID)
        {
            NoteViewModel viewModel = new NoteViewModel()
            {
                Notes = repository.Notes.Where(p => p.DataID == dataID).OrderBy(p => p.NoteDate)
            };
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(viewModel);
            return View(viewModel); // здесь плохо понимаю - никогда это не должно вызываться
        }

        [HttpPost]
        public IActionResult OrderNotes(Note note)
        {
            // добавляем новую заметку
            try
            {
                noteRepository.Save(note);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка сохранения: {e.Message} {e.InnerException.Message}";
                return RedirectToAction("OrderNotes", new { dataID = note.DataID });
            }
            TempData["message"] = $"Запись сохранена!";

            return RedirectToAction("OrderNotes",new { dataID = note.DataID });
        }

        [HttpPost]
        public IActionResult DeleteNote(int noteID, int orderID)
        {
            Note delRec;
            try
            {
                delRec = noteRepository.Delete(noteID);
            }
            catch (Exception e)
            {
                TempData["message"] = $"Ошибка удаления: {e.Message}";
                return RedirectToAction("OrderNotes", new { dataID = orderID });
            }

            if (delRec != null)
                TempData["message"] = $"Запись успешно удалена!";
            return RedirectToAction("OrderNotes", new { dataID = orderID });
        }
    }
}

