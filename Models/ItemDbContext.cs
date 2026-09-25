// there would normally be several db's in a project, 
// but for this project all entities are managed from here

using Microsoft.EntityFrameworkCore; // functionality for db operations

namespace MyShop.Models;

public class ItemDbContext : DbContext // defines inheritance from DbContext (represents a session with the db)
{
    // constructor
    public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
    {
        // creates empty db (with schema) based on current model i DbContext, in case no db exists 
//        Database.EnsureCreated();// for early prototyping only, remove when switching to EF core migrations
    }

    // represents the Item entities in the db + methods for querying and saving instances of Item
    public DbSet<Item> Items {get; set;}
    public DbSet<Customer> Customers {get; set; }
    public DbSet<Order> Orders {get; set; }
    public DbSet<OrderItem> OrderItems {get; set; }
}