using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    // each action typically corresponding to a user interaction - e.g. view list, display item details, submit form
    // usually returns an IActionResult - View/JSON/rediret or other

    public async Task<IActionResult> Table() // Table-view-controller
    {
        List<Item> items = await _itemDbContext.Items.ToListAsync(); // converts db records to list
        var itemsViewModel = new ItemsViewModel(items, "Table"); // create object
        // VIEWMODEL - strongly typed, autocomplete support, easier to scale and maintain
        return View(itemsViewModel);
    }

    // Grid-view-controller - async/await to ensure the system does not pause during wait
    public async Task<IActionResult> Grid()
    {
        List<Item> items = await _itemDbContext.Items.ToListAsync();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    // Show item details-view
    public async Task<IActionResult> Details(int id)
    {
        List<Item> items = await _itemDbContext.Items.ToListAsync();
        var item = items.FirstOrDefault(i => i.ItemId == id); // for each i, check if i.ItemId == id (parameter)
        if (item == null)
            return NotFound();
        return View(item);
    }

    [HttpGet] // GET to display the form (in create view) to create a new item
    public IActionResult Create()
    { // no db-communication, so no async/await
        return View();
    }

    [HttpPost] // POST to handle the creation form submission
    public async Task<IActionResult> Create(Item item) // creates a new Item object
    {
        if (ModelState.IsValid) // checks if form data passed validation
        {
            _itemDbContext.Items.Add(item); // add item
            await _itemDbContext.SaveChangesAsync(); // update/save in db
            return RedirectToAction(nameof(Table)); // redirect to table to show all items
        }
        return View(item);
    }

    [HttpGet] // display update form
    public async Task<IActionResult> Update(int id)
    {
        var item = await _itemDbContext.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost] // update the item in db
    public async Task<IActionResult> Update(Item item)
    {
        if (ModelState.IsValid)
        {
            _itemDbContext.Items.Update(item);
            await _itemDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
        return View(item);
    }

    [HttpGet] // view deletion-form
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemDbContext.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost] // delete item for db
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _itemDbContext.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _itemDbContext.Items.Remove(item);
        await _itemDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Table));
    }
}