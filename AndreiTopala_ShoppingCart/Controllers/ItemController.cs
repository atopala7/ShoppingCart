using AndreiTopala_ShoppingCart.Models;
using AndreiTopala_ShoppingCart.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AndreiTopala_ShoppingCart.Controllers
{
    public class ItemController : Controller
    {
        Models.Manager man = new Models.Manager();

        // GET: Item
        // Display a list of all the items available for purchase
        public ActionResult Index()
        {
            List<Item> items = new List<Item>();
            foreach (var dbItem in man.dc.items)
            {
                items.Add(dbItem);
            }
            return View(AutoMapper.Mapper.Map<List<Item>, List<ItemListViewModel>>(items));
        }

        // GET: Item/Details/5
        // Display the details of the given item, if it exists
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var item = man.dc.items.FirstOrDefault(i => i.Id == id);

                if (item == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(AutoMapper.Mapper.Map<Item, ItemViewModel>(item));
                }
            }
        }
    }
}