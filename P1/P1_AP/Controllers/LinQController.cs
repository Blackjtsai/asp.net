using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using P1_AP.Models;

namespace P1_AP.Controllers;

public class LinQController : Controller
{
  private readonly ILogger<LinQController> _logger;

  public LinQController(ILogger<LinQController> logger)
  {
    _logger = logger;
  }

  public IActionResult linq()
  {
    // The Three Parts of a LINQ Query:
    // 1. Data source.
    int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

    // 2. Query creation.
    // numQuery is an IEnumerable<int>
    var numQuery =
        from num in numbers
        where (num % 2) == 0
        select num;

    // 3. Query execution.
    foreach (int num in numQuery)
    {
      Console.Write("{0,1} ", num);
    }

    return View();
  }



  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
