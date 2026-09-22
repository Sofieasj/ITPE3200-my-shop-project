using Microsoft.AspNetCore.Mvc;
using MyShop.Models; // connects to Model-namespace to retrieve data models
using MyShop.ViewModels; // connets to ViewModels-namespace - if a type name is used that can't be resolved locally, check here
namespace MyShop.Controllers;

public class ItemController : Controller 
{
    // actions are Controller methods handling specific requests. 
    // each action typically corresponding to a user interaction - e.g. view list, displau item detail, submit form
    // usually returns an IActionResult - View/JSON/rediret or other

    // VIEWMODEL - strongly typed, autocomplete support, easier to scale and maintain
    // Table-view-controller
    public IActionResult Table()
    {
        var items = GetItems(); // input all the items
        var itemsViewModel = new ItemsViewModel(items, "Table"); // create object
        return View(itemsViewModel);
    }

    // Grid-view-controller
    public IActionResult Grid()
    {
        var items = GetItems();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    // Details-view-controller
    public IActionResult Details(int id)
    {
        var items = GetItems();
        var item = items.FirstOrDefault(i => i.ItemId == id); // for each i, check if i.ItemId == id (parameter)
        if (item == null)
            return NotFound();
        return View(item);
    }
   
   // VIEWBAG - loosely typed, less support and messy to work with on big projects
    // public IActionResult Table ()
    // {

    //     var items = GetItems();
    //     ViewBag.CurrentViewName = "Table";
    //     return View(items);
    // }

    // public IActionResult Grid()
    // {
    //     var items = GetItems();
    //     ViewBag.CurrentViewName = "Grid";
    //     return View(items);
    // }

    public List<Item> GetItems()
    {
        var items = new List<Item>();
        var item1 = new Item
        {
            ItemId = 1,
            Name = "Pizza",
            Price = 150,
            Description = "Delicious italian dish",
            ImageUrl = "/images/pizza.jpg"
        };

        var item2 = new Item
        {
            ItemId = 1,
            Name = "Fish and chips",
            Price = 150,
            Description = "Savoury treat",
            ImageUrl = "/images/fishandchips.jpg"
        };

        var item3 = new Item
        {
            ItemId = 1,
            Name = "Tacos",
            Price = 150,
            Description = "Delicious mexican dish",
            ImageUrl = "/images/tacos.jpg"
        };

        items.Add(item1);
        items.Add(item2);
        items.Add(item3);

        return items;
    }
}