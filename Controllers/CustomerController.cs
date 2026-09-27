using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.DAL;
using MyShop.ViewModels;

namespace MyShop.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IActionResult> Table()
    {
        var customers = await _customerRepository.GetAll();
        return View(customers);
    }
}