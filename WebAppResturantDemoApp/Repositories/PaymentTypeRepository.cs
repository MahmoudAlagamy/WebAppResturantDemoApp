using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppResturantDemoApp.Models;

namespace WebAppResturantDemoApp.Repositories
{
    public class PaymentTypeRepository
    {
        private RestrurantDBEntities objRestrurantDBEntities;
        public PaymentTypeRepository()
        {
            objRestrurantDBEntities = new RestrurantDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllPaymentType()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestrurantDBEntities.PaymentTypes
                                  select new SelectListItem()
                                  {
                                      Text = obj.PaymentTypeName,
                                      Value = obj.PaymentTypeId.ToString(),
                                      Selected = true
                                  }).ToList();
            return objSelectListItems;
        }
    }
}