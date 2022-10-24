using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using P1_AP.Models;
using P1_AP.DAO;
using P1_Core;
using P1_AP.Common;
using Newtonsoft.Json; //序列化

namespace P1_AP.Controllers;

public class EventDataController : Controller
{

  private string viewsPath = "~/Views/System/EventData";
  private string controllerName = "EventData";

  // 查詢 R
  public IActionResult Chart()
  {
    return View(viewsPath + "/chart.cshtml");
  }
  public IActionResult List()
  {
    return View(viewsPath + "/list.cshtml");
  }
  // getDataByAjax 
  [HttpGet]
  public IActionResult getDataByAjax()
  {
    DAOEventData eventDataDao = new DAOEventData();
    event_data event_data = new event_data();

    // 查詢order_total作業
    event_data.order_total = Convert.ToInt16(eventDataDao.getOrderTotal()[0].count);
    // 查詢 pay_total 作業
    event_data.pay_total = Convert.ToInt16(eventDataDao.getPayTotal()[0].count);

    event_data.pay_success = 50;
    event_data.pay_error = 50;
    event_data.apply_success = 50;
    event_data.apply_error = 50;
    event_data.apply_none = 50;
    event_data.crm_success = 50;
    event_data.crm_error = 50;
    event_data.eip_success = 50;
    event_data.eip_error = 50;
    return Content(JsonConvert.SerializeObject(event_data), "application/json");

  }

  // getDataByAjax2 
  [HttpGet]
  public IActionResult getDataByAjax2()
  {
    DAOEventDocData eventDocDataDao = new DAOEventDocData();
    event_data event_data = new event_data();

    // 查詢進店登記作業

    event_data.event01_total = Convert.ToInt16(eventDocDataDao.getEvent03Total()[0].count);
    // 查詢專人聯繫作業
    event_data.event02_total = Convert.ToInt16(eventDocDataDao.getEvent03Total()[0].count);
    // 查詢直撥抽獎登記作業
    event_data.event03_total = Convert.ToInt16(eventDocDataDao.getEvent03Total()[0].count);

    return Content(JsonConvert.SerializeObject(event_data), "application/json");

  }


  // 以下尚未運用
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
