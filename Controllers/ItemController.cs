using Microsoft.AspNetCore.Mvc;
using MyShop.Models; // connects to Model-folder to retrieve data models

namespace MyShop.Controllers;

public class ItemController : Controller 
{
    // actions are Controller methods handling specific requests. 
    // each action typically corresponding to a user interaction - e.g. view list, displau item detail, submit form
    // usually returns an IActionResult - View/JSON/rediret or other
    public IActionResult Table ()
    {
        var items = new List<Item>(); // create list of items
        var item1 = new Item(); // create item (hardcoded - next week: with db)
        item1.ItemId = 1;
        item1.Name = "Pizza";
        item1.Price = 60;

        // preferred way with {} to add an instance to a class
        var item2 = new Item
        {
            ItemId = 2,
            Name = "Fried Chicken Leg",
            Price = 15
        };

        // add items to list
        items.Add(item1);
        items.Add(item2);

        // name and return - to display view
        ViewBag.CurrentViewName = "List of Shop Items";
        return View(items);
    }
}