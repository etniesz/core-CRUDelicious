using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class DishController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public DishController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("New");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View("New");
        }
    }

    [HttpGet("dishes/{dishId}")]
    public IActionResult ShowDish(int dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
        return View("Show", dish);
    }

    [HttpPost("dishes/{DishId}/destroy")]
    public IActionResult DestroyDish(int DishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
        _context.Remove(dish);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("dishes/{DishId}/edit")]
    public IActionResult EditDish(int DishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
        return View("Edit", dish);
    }

    [HttpPost("dishes/{DishId}/update")]
    public IActionResult UpdateDish(int DishId, Dish dish)
    {
        Dish? dishToUpdate = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
        dishToUpdate.Name = dish.Name;
        dishToUpdate.Chef = dish.Chef;
        dishToUpdate.Calories = dish.Calories;
        dishToUpdate.Tastiness = dish.Tastiness;
        dishToUpdate.Description = dish.Description;
        dishToUpdate.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}
