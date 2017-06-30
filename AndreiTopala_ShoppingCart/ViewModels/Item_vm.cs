using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreiTopala_ShoppingCart.ViewModels
{
    public class ItemViewModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
    }

    public class ItemListViewModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
    }
}