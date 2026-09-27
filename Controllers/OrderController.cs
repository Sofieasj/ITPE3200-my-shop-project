using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.DAL;
using MyShop.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyShop.Controllers;

public class OrderController : Controller
{
    // dependency injection of BOTH orders and items - orders consist of items
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;

    public OrderController(IOrderRepository orderRepository, IItemRepository itemRepository)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
    }

    public async Task<IActionResult> Table()
    {
        var orders = await _orderRepository.GetAll();
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> CreateOrderItem()
    {
        var items = await _itemRepository.GetAll();
        var orders = await _orderRepository.GetAll();
        var createOrderItemViewModel = new CreateOrderItemViewModel
        {
            OrderItem = new OrderItem(),

            ItemSelectList = items.Select(item => new SelectListItem
            {
                Value = item.ItemId.ToString(),
                Text = item.ItemId.ToString() + ": " + item.Name
            }).ToList(),

            OrderSelectList = orders.Select(order => new SelectListItem
            {
                Value = order.OrderId.ToString(),
                Text = "Order" + order.OrderId.ToString() + ", Date: " + order.OrderDate + ", Customer: " + order.Customer.Name
            }).ToList(),
        };
        return View(createOrderItemViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderItem(OrderItem orderItem)
    {
        try
        {
            var newItem = await _itemRepository.GetItemById(orderItem.ItemId);
            var newOrder = await _orderRepository.GetOrderById(orderItem.OrderId);

            if (newItem == null || newOrder == null)
            {
                return BadRequest("Item or Order not found.");
            }

            var newOrderItem = new OrderItem
            {
                ItemId = orderItem.ItemId,
                Item = newItem,
                Quantity = orderItem.Quantity,
                OrderId = orderItem.OrderId,
                Order = newOrder,
            };
            newOrderItem.OrderItemPrice = orderItem.Quantity * newOrderItem.Item.Price;

            newOrder.OrderItems ??= new List<OrderItem>();
            newOrder.OrderItems.Add(newOrderItem);
            await _orderRepository.Create(newOrder);
            return RedirectToAction(nameof(Table));
        }
        catch
        {
            return BadRequest("OrderItem creation failed.");
        }
    }    
}