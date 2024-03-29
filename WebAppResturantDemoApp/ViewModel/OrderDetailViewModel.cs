﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppResturantDemoApp.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Descount { get; set; }
        public decimal Total { get; set; }
    }
}