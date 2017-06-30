using AndreiTopala_ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndreiTopala_ShoppingCart.Models
{
    public class Manager
    {
        public Manager()
        {
            dc = new ApplicationDbContext();
            dc.Configuration.ProxyCreationEnabled = false;
            dc.Configuration.LazyLoadingEnabled = false;
        }

        public ApplicationDbContext dc;
    }
}