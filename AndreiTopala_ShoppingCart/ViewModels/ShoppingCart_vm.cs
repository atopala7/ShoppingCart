using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreiTopala_ShoppingCart.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        public virtual ICollection<CartItemViewModel> cartItems { get; set; }
    }

    public class CartItemViewModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int shoppingCartId { get; set; }
        [HiddenInput]
        public int itemId { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}