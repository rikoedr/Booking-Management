using API.DataTransferObjects;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        this.repository = repository;
    }
    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var listEmployee = new List<EmployeeDTO>();
        if (result != null)
        {
            listEmployee = result.Data.ToList();
        }

        return View(listEmployee);
    }
}
