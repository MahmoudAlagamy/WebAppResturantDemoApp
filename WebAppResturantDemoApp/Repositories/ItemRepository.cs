using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppResturantDemoApp.Models;

namespace WebAppResturantDemoApp.Repositories
{
    public class ItemRepository
    {
        readonly RestrurantDBEntities objRestrurantDBEntities;
        public ItemRepository()
        {
            objRestrurantDBEntities = new RestrurantDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllItems()
        {
            var objSelectListItems = new List<SelectListItem>();
            objSelectListItems = (from obj in objRestrurantDBEntities.Items
                                  select new SelectListItem()
                                  {
                                      Text = obj.ItemName,
                                      Value = obj.ItemId.ToString(),
                                      Selected = false
                                  }).ToList();
            return objSelectListItems;
        }
    }
}