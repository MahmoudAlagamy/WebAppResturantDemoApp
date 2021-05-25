using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppResturantDemoApp.Models;
using WebAppResturantDemoApp.ViewModel;

namespace WebAppResturantDemoApp.Repositories
{
    public class OrderRepository
    {
        private RestrurantDBEntities objRestrurantDBEntities;
        public OrderRepository()
        {

            objRestrurantDBEntities = new RestrurantDBEntities();

        }

        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            Order objOrder = new Order();
            objOrder.CustomerId = objOrderViewModel.CustomerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = string.Format("{0:ddmmmyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objRestrurantDBEntities.Orders.Add(objOrder);
            objRestrurantDBEntities.SaveChanges();

            int OrderId = objOrder.OrderId;
            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Descount = item.Descount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = item.Quantity;
                objRestrurantDBEntities.OrderDetails.Add(objOrderDetail);
                objRestrurantDBEntities.SaveChanges();

                Transaction objTransaction = new Transaction();
                objTransaction.ItemId = item.ItemId;
                objTransaction.Quantity = (-1) * item.Quantity;
                objTransaction.TransactionDate = DateTime.Now;
                objTransaction.TypeId = 2;
                objRestrurantDBEntities.Transactions.Add(objTransaction);
                objRestrurantDBEntities.SaveChanges();

            }

            return true;
        }
    }
}