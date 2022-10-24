using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventChart_AP.Models;
using EventChart_AP.DAO;
using EventChart_Core;
using EventChart_AP.Common;
using Newtonsoft.Json; //序列化

namespace EventChart_AP.Controllers;

public class EmpyRoleController : Controller
{

  private string viewsPath = "~/Views/System/EmpyRole";
  private string controllerName = "EmpyRole";
  private DAOEmpyRole dataDao = new DAOEmpyRole();

  // 新增 C
  [HttpPost]
  public IActionResult Add(empy_role el)
  {
    //登入權限檢核作業====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否頁面功能權限檢核
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Add", "/System/EmpyRole");
    if (r != null) return r;
    //==============================================

    // 新增作業  
    int num = dataDao.insert(el);
    TempData["ExcuteStatus"] = (num > 0) ? "新增資料成功" : "新增資料失敗";
    return RedirectToAction("List", controllerName);
  }
  // 查詢 R
  public IActionResult List(string empy_name = null, int pageNum = 0)
  {
    //登入權限檢核作業====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否頁面功能權限檢核
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "List", "/System/EmpyRole");
    if (r != null) return r;
    //==============================================

    // 1.查詢指定資料
    List<empy_role> empy_role_list = dataDao.getDataBySearch(empy_name);
    // 2.計算總筆數
    int listCount = empy_role_list.Count();
    // 3.取一頁幾筆 $pagenum=50; 
    int pageDisplayNum = new ConfigsUtil().PageDisplayNum;
    // 4.計算總頁數$totalpages = intval(($num - 1) /$pagenum)+1;
    int pageTotal = ((listCount - 1) / pageDisplayNum) + 1;
    // 5.設定預查頁數
    if (pageNum == 0) pageNum = 1;
    if (pageNum > pageTotal) pageNum = pageTotal;

    // 6.計算從第幾筆開始 
    int beginNum = (pageNum - 1) * pageDisplayNum;

    // 7.使用LinＱ 進行資料排序及取分頁資料
    empy_role_list = (from item in empy_role_list
                      where item.empy_role_id >= 0
                      orderby item.empy_role_id descending
                      select item).ToList();

    // 8.紀錄查詢狀態 (使用empy（）及 分頁資料) 
    ViewData["listCount"] = listCount;
    ViewData["pageTotal"] = pageTotal;
    ViewData["pageNum"] = pageNum;
    ViewData["pageDisplayNum"] = pageDisplayNum;

    ViewData["empy_name"] = empy_name;

    // 注射至view方式一：使用session (先序列化)
    HttpContext.Session.SetString("EmpyRoleData", JsonConvert.SerializeObject(empy_role_list));

    // 注射至view方式二：使用model
    return View(viewsPath + "/List.cshtml", empy_role_list);
  }

  // 修改 U
  [HttpPost]
  public IActionResult Edit(empy_role el) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Edit", "/System/EmpyRole");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    int num = dataDao.update(el);

    TempData["ExcuteStatus"] = (num > 0) ? ("empy_role_id:" + el.empy_role_id + "修改資料成功") : ("empy_role_id:" + el.empy_role_id + "修改資料失敗");
    return RedirectToAction("list", controllerName);
  }

  // 刪除 D
  [HttpGet]
  public IActionResult Del(string empy_role_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Del", "EmpyRole");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 刪除指定作業  
    empy_role empy_role_core = new empy_role();
    empy_role_core.empy_role_id = Convert.ToInt16(empy_role_id);


    int num = dataDao.delete(empy_role_core);

    TempData["ExcuteStatus"] = (num > 0) ? ("id:" + empy_role_id + "刪除資料成功") : ("id:" + empy_role_id + "刪除資料失敗");
    return RedirectToAction("list", controllerName);
  }

  // 明細頁 N
  [HttpGet]
  public IActionResult Detail(string act, string empy_role_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Detail", "/System/EmpyRole");
    if (r != null) return r;
    //== 登入權限檢核作業  結束==================================== 

    DAOEmpy empyDao = new DAOEmpy();
    List<empy> empy_list = empyDao.getAll();

    // 7.使用LinＱ 進行資料排序
    empy_list = (from item in empy_list
                 where item.age >= 0
                 orderby item.account descending
                 select item).ToList();

    DAOChapter chapterDao = new DAOChapter();
    List<chapter> chapter_list = chapterDao.getAll();

    // 7.使用LinＱ 進行資料排序
    chapter_list = (from item in chapter_list
                    where item.chapter_id >= 0
                    orderby item.seq descending
                    select item).ToList();
    // EmpyData
    HttpContext.Session.SetString("EmpyData", JsonConvert.SerializeObject(empy_list));
    // ChapterData
    HttpContext.Session.SetString("ChapterData", JsonConvert.SerializeObject(chapter_list));

    TempData["act"] = Convert.ToString(act);
    TempData["empy_role_id"] = Convert.ToString(empy_role_id);
    return View(viewsPath + "/detail.cshtml");
  }

  // 查詢指定ID 
  [HttpGet]
  public IActionResult detailByID(string empy_role_id)
  {
    // 檢查登入者是否有權限執行
    // 待處理

    // 查詢作業
    List<empy_role> ListData = dataDao.getDataByID(empy_role_id);
    return Content(JsonConvert.SerializeObject(ListData), "application/json");
  }

  // getDataByAjax 
  [HttpGet]
  public IActionResult getDataByAjax()
  {
    // 檢查登入者是否有權限執行
    // 待處理

    // 查詢作業
    List<empy_role> ListData = dataDao.getAll();
    return Content(JsonConvert.SerializeObject(ListData), "application/json");

  }


  // 以下尚未運用
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
