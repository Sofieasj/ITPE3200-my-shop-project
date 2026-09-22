using Microsoft.AspNetCore.Mvc;
using MyShop.Models; // connects to Model-namespace to retrieve data models
using MyShop.ViewModels; // connets to ViewModels-namespace - if a type name is used that can't be resolved locally, check here
namespace MyShop.Controllers;

public class ItemController : Controller 
{
    // actions are Controller methods handling specific requests. 
    // each action typically corresponding to a user interaction - e.g. view list, displau item detail, submit form
    // usually returns an IActionResult - View/JSON/rediret or other

    public IActionResult Table()
    {
        var items = GetItems();
        var itemsViewModel = new ItemsViewModel(items, "Table");
        return View(itemsViewModel);
    }

    public IActionResult Grid()
    {
        var items = GetItems();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }
   
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