using MyShop.Models; // lets the file reference types defined here w/o writing MyShop.Models.Item

namespace MyShop.ViewModels // similar to java packages
{
    // package the view (razor) to be rendered
    public class ItemsViewModel
    {
        public IEnumerable<Item> Items; // to hold a sequence of Items that can be iterated over (flexible type)
        public string? CurrentViewName;

        // constructor - takes a colletion of items + optionally a view name
        public ItemsViewModel(IEnumerable<Item> items, string? currentViewName)
        {
            Items = items;
            CurrentViewName = currentViewName;
        }
    }
}