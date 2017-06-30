using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndreiTopala_ShoppingCart.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
    }

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            cartItems = new List<CartItem>();
        }
        public int Id { get; set; }
        public string userId { get; set; }
        public virtual ICollection<CartItem> cartItems { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int shoppingCartId { get; set; }
        public int itemId { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}