using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using P1_AP.Models;

namespace P1_AP.Controllers;

public class NewsController : Controller
{

  private string folder_name = "MenuType01/News/";


  private readonly ILogger<NewsController> _logger;

  public NewsController(ILogger<NewsController> logger)
  {
    _logger = logger;
  }

  public IActionResult news()
  {
    return View("~/Views/" + folder_name + "news.cshtml");
  }



  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
