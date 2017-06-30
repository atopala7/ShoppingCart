using AndreiTopala_ShoppingCart.Models;
using AndreiTopala_ShoppingCart.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AndreiTopala_ShoppingCart.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        Models.Manager man = new Models.Manager();

        // GET: ShoppingCart
        // Returns a list of all the items in the user's shopping cart
        public ActionResult Index()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<CartItem> items = new List<CartItem>();
            var cart = man.dc.shoppingCarts.Include("cartItems").FirstOrDefault(c => c.userId == userId);
            if (cart != null)
            {
                decimal totalPrice = 0;
                int totalQuantity = 0;
                foreach (var cartItem in cart.cartItems)
                {
                    items.Add(cartItem);
                    totalPrice += cartItem.price * cartItem.quantity;
                    totalQuantity += cartItem.quantity;
                }
                ViewBag.totalPrice = totalPrice;
                ViewBag.totalQuantity = totalQuantity;
            }
            return View(AutoMapper.Mapper.Map<List<CartItem>, List<CartItemViewModel>>(items));
        }

        // GET: ShoppingCart/AddItem/5
        // Adds the given item to the user's shopping cart, if possible
        public ActionResult AddItem(int id)
        {
            // Check if the id corresponds to a valid item
            Item item = man.dc.items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else if (item.stock == 0)
            {
                ViewBag.ErrorMessage = item.name + " is out of stock.";
                return View("Error");
            }

            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            // Get the shopping cart that belongs to the user, if any
            var cart = man.dc.shoppingCarts.FirstOrDefault(c => c.userId == userId);

            // If the user doesn't have a shopping cart, create one
            if (cart == null)
            {
                ShoppingCart newUserCart = new ShoppingCart();
                newUserCart.userId = userId;
                man.dc.shoppingCarts.Add(newUserCart);
                man.dc.SaveChanges();
            }
            // If a shopping cart already exists for the user, check if the item is already in the cart, and, if it is, increase its quantity
            else
            {
                var cartItem = man.dc.shoppingCarts.Include("CartItems").FirstOrDefault(c => c.userId == userId).cartItems.FirstOrDefault(ci => ci.itemId == id);
                if (cartItem != null)
                {
                    int cartItemId = cartItem.Id;
                    man.dc.cartItems.FirstOrDefault(ci => ci.Id == cartItemId).quantity++;
                    man.dc.items.FirstOrDefault(i => i.Id == id).stock--;
                    man.dc.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            // By this point, the cart exists but the item is not in the cart, so we add it to the cart
            int cartId = man.dc.shoppingCarts.FirstOrDefault(c => c.userId == userId).Id;
            CartItem newItem = new CartItem();
            newItem.itemId = id;
            newItem.quantity = 1;
            newItem.name = item.name;
            newItem.price = item.price;
            newItem.shoppingCartId = cartId;
            man.dc.shoppingCarts.FirstOrDefault(c => c.userId == userId).cartItems.Add(newItem);
            man.dc.items.FirstOrDefault(i => i.Id == id).stock--;
            man.dc.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ShoppingCart/Details/5
        // Display the details of an item in the user's shopping cart
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Check if the cartItem exists
                var cartItem = man.dc.cartItems.FirstOrDefault(i => i.Id == id);

                if (cartItem == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    // Check if the cartItem is in the current user's shopping cart
                    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var shoppingCart = man.dc.shoppingCarts.FirstOrDefault(c => c.userId == userId);
                    // If the user doesn't have a shopping cart, or if the item is not in the user's cart, something is wrong, so we return an error message
                    if (shoppingCart == null || shoppingCart.Id != cartItem.shoppingCartId)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        // Get the item to which the cartItem corresponds and display its details
                        var item = man.dc.items.FirstOrDefault(i => i.Id == cartItem.itemId);
                        return View(AutoMapper.Mapper.Map<Item, ItemViewModel>(item));
                    }
                }
            }
        }

        // GET: ShoppingCart/RemoveItem/5
        // Remove an item from the shopping cart
        public ActionResult RemoveItem(int id)
        {
            CartItem cartItem = man.dc.cartItems.FirstOrDefault(i => i.Id == id);
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var cart = man.dc.shoppingCarts.FirstOrDefault(c => c.userId == userId);
            // If the given id is invalid, or if the user doesn't have a cart, or if the given item doesn't exist in the user's cart, return an error
            if (cartItem == null || cart == null || cartItem.shoppingCartId != cart.Id)
            {
                return HttpNotFound();
            }
            // Otherwise, check if the item is already in the cart, and, if it is, decrease its quantity
            else
            {
                var userCartItem = man.dc.shoppingCarts.Include("CartItems").FirstOrDefault(c => c.userId == userId).cartItems.FirstOrDefault(ci => ci.Id == id);
                if (userCartItem != null)
                {
                    int cartItemId = cartItem.Id;
                    man.dc.items.FirstOrDefault(i => i.Id == cartItem.itemId).stock++;
                    // If there the quantity is greater than one, decrease it
                    if (userCartItem.quantity > 1)
                    {
                        man.dc.cartItems.FirstOrDefault(ci => ci.Id == cartItemId).quantity--;// = man.dc.cartItems.FirstOrDefault(ci => ci.Id == cartItemId).quantity + 1;
                        man.dc.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    // Otherwise remove the item entirely
                    else
                    {
                        man.dc.shoppingCarts.Include("CartItems").FirstOrDefault(c => c.userId == userId).cartItems.Remove(userCartItem);
                        man.dc.cartItems.Remove(userCartItem);
                        man.dc.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
    }
}