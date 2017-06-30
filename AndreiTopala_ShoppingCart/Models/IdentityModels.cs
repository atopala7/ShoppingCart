using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AndreiTopala_ShoppingCart.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            /*
            var itemList = new System.Collections.Generic.List<Item>
            {
                new Item
                {
                    name = "Book",
                    description = "This is a book.",
                    price = 10.99m,
                    stock = 100
                },
                new Item
                {
                    name = "Phone",
                    description = "It is a brand new phone.",
                    price = 100.99m,
                    stock = 10
                },
                new Item
                {
                    name = "Car",
                    description = "It's a shiny car.",
                    price = 10000.99m,
                    stock = 5
                }
            };

            foreach (var item in itemList)
            {
                items.Add(item);
            }

            SaveChanges();
            */
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Item> items { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }

        //public System.Data.Entity.DbSet<AndreiTopala_ShoppingCart.ViewModels.ItemViewModel> ItemViewModels { get; set; }

        //public System.Data.Entity.DbSet<AndreiTopala_ShoppingCart.ViewModels.CartItemViewModel> CartItemViewModels { get; set; }

        //public System.Data.Entity.DbSet<AndreiTopala_ShoppingCart.ViewModels.ItemListViewModel> ItemListViewModels { get; set; }

        //public System.Data.Entity.DbSet<AndreiTopala_ShoppingCart.ViewModels.ItemViewModel> ItemViewModels { get; set; }

        //public System.Data.Entity.DbSet<AndreiTopala_ShoppingCart.ViewModels.CartItemViewModel> CartItemViewModels { get; set; }
    }
}