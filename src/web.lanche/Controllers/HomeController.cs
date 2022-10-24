using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.lanche.Models;
using web.lanche.Repositories.Interfaces;
using web.lanche.ViewModels;

namespace web.lanche.Controllers;

public class HomeController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public HomeController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel{
            LanchesPreferidos = _lancheRepository.LanchesPreferidos
        };

        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
