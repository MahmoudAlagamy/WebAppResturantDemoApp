using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppResturantDemoApp.Repositories;
using WebAppResturantDemoApp.Models;
using WebAppResturantDemoApp.ViewModel;

namespace WebAppResturantDemoApp.Controllers
{
    public class HomeController : Controller
    {
        readonly RestrurantDBEntities objRestrurantDBEntities;
        public HomeController()
        {
            objRestrurantDBEntities = new RestrurantDBEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            CustomerRepository objCustomerRepository = new CustomerRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();

            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCustomerRepository.GetAllCustomers(), objItemRepository.GetAllItems(), objPaymentTypeRepository.GetAllPaymentType());
            return View(objMultipleModels);
        }
        [HttpGet]
        public JsonResult GetItemUnitPrice(int itemId)
        {
            decimal UnitPrice = objRestrurantDBEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(UnitPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);
            return Json("Your Orser has been Successfully Placed.", JsonRequestBehavior.AllowGet);
        }
    }
}