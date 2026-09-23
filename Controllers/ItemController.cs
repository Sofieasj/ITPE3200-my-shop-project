using Microsoft.AspNetCore.Mvc;
using MyShop.Models; // connects to Model-namespace to retrieve data models
using MyShop.ViewModels; // connets to ViewModels-namespace - if a type name is used that can't be resolved locally, check here

namespace MyShop.Controllers;

public class ItemController : Controller 
{
    private readonly ItemDbContext _itemDbContext;

    public ItemController(ItemDbContext itemDbContext) // constructor - dependency injection
    {
        _itemDbContext = itemDbContext;
    }

    // actions are Controller methods handling specific requests. 
    // each action typically corresponding to a user interaction - e.g. view list, displau item detail, submit form
    // usually returns an IActionResult - View/JSON/rediret or other

    // VIEWMODEL - strongly typed, autocomplete support, easier to scale and maintain
    // Table-view-controller
    public IActionResult Table()
    {
        List<Item> items = _itemDbContext.Items.ToList(); // converts db records to list
        var itemsViewModel = new ItemsViewModel(items, "Table"); // create object
        return View(itemsViewModel);
    }

    // Grid-view-controller
    public IActionResult Grid()
    {
        List<Item> items = _itemDbContext.Items.ToList();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    // Details-view-controller
    public IActionResult Details(int id)
    {
        List<Item> items = _itemDbContext.Items.ToList();
        var item = items.FirstOrDefault(i => i.ItemId == id); // for each i, check if i.ItemId == id (parameter)
        if (item == null)
            return NotFound();
        return View(item);
    }

    [HttpGet] // GET to display the form (in create view) to create a new item
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost] // POST to handle the creation form submission
    public IActionResult Create(Item item) // creates a new Item object
    {
        if (ModelState.IsValid) // checks if form data passed validation
        {
            _itemDbContext.Items.Add(item); // add item
            _itemDbContext.SaveChanges(); // update/save in db
            return RedirectToAction(nameof(Table)); // redirect to table to show all items
        }
        return View(item);
    }

    [HttpGet] // display update form
    public IActionResult Update(int id)
    {
        var item = _itemDbContext.Items.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost] // update the item in db
    public IActionResult Update(Item item)
    {
        if (ModelState.IsValid)
        {
            _itemDbContext.Items.Update(item);
            _itemDbContext.SaveChanges();
            return RedirectToAction(nameof(Table));
        }
        return View(item);
    }

    [HttpGet] // view deletion-form
    public IActionResult Delete(int id)
    {
        var item = _itemDbContext.Items.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost] // delete item for db
    public IActionResult DeleteConfirmed(int id)
    {
        var item = _itemDbContext.Items.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        _itemDbContext.Items.Remove(item);
        _itemDbContext.SaveChanges();
        return RedirectToAction(nameof(Table));
    }
}