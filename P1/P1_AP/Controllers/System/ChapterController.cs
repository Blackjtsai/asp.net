using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using P1_AP.Models;
using P1_AP.DAO;
using P1_Core;
using P1_AP.Common;
using Newtonsoft.Json; //序列化

namespace P1_AP.Controllers;

public class ChapterController : Controller
{

  private string viewsPath = "~/Views/System/Chapter";
  private string controllerName = "Chapter";
  private DAOChapter dataDao = new DAOChapter();


  // 新增 C
  [HttpPost]
  public IActionResult Add(chapter el)
  {
    //登入權限檢核作業====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否頁面功能權限檢核
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Add", "/System/Chapter");
    if (r != null) return r;
    //==============================================

    // 新增作業   
    int num = dataDao.insert(el);
    TempData["ExcuteStatus"] = (num > 0) ? "新增資料成功" : "新增資料失敗";
    return RedirectToAction("List", controllerName);
  }

  // 查詢 R
  public IActionResult List()
  {
    //登入權限檢核作業====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否頁面功能權限檢核
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "List", "/System/Chapter");
    if (r != null) return r;
    //==============================================

    // 查詢全部資料
    List<chapter> chapter_list = dataDao.getAll();

    chapter_list = (from item in chapter_list
                    where item.chapter_id >= 0
                    orderby item.seq
                    select item).ToList();

    // 注射至view方式一：使用session (先序列化)
    HttpContext.Session.SetString("ChapterData", JsonConvert.SerializeObject(chapter_list));

    // 注射至view方式二：使用model
    return View(viewsPath + "/List.cshtml", chapter_list);
  }

  // 修改 U
  [HttpPost]
  public IActionResult Edit(chapter el) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Edit", "/System/Chapter");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 更新指定作業   

    int num = dataDao.update(el);

    TempData["ExcuteStatus"] = (num > 0) ? ("chapter_id:" + el.chapter_id + "修改資料成功") : ("chapter_id:" + el.chapter_id + "修改資料失敗");
    return RedirectToAction("list", controllerName);
  }

  // 刪除 D
  [HttpGet]
  public IActionResult Del(string chapter_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Del", "/System/Chapter");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 刪除指定作業  
    chapter chapter_core = new chapter();
    chapter_core.chapter_id = Convert.ToInt16(chapter_id);

    int num = dataDao.delete(chapter_core);

    TempData["ExcuteStatus"] = (num > 0) ? ("id:" + chapter_id + "刪除資料成功") : ("id:" + chapter_id + "刪除資料失敗");
    return RedirectToAction("list", controllerName);
  }

  // 明細頁 N
  [HttpGet]
  public IActionResult Detail(string act, string chapter_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Detail", "/System/Chapter");
    if (r != null) return r;
    //== 登入權限檢核作業  結束==================================== 

    TempData["act"] = Convert.ToString(act);
    TempData["chapter_id"] = Convert.ToString(chapter_id);
    return View(viewsPath + "/detail.cshtml");
  }

  // 查詢指定ID 
  [HttpGet]
  public IActionResult detailByID(string chapter_id)
  {
    // 檢查登入者是否有權限執行
    // 待處理

    // 查詢作業
    List<chapter> chapter_list = dataDao.getDataByID(chapter_id);
    return Content(JsonConvert.SerializeObject(chapter_list), "application/json");

  }

  // getDataByAjax 
  [HttpGet]
  public IActionResult getDataByAjax()
  {
    // 檢查登入者是否有權限執行
    // 待處理


    // 查詢作業
    List<chapter> chapter_list = dataDao.getAll();

    // 7.使用LinＱ 進行資料排序及取分頁資料
    chapter_list = (from item in chapter_list
                    where item.chapter_id >= 0
                    orderby item.seq
                    select item).ToList();

    return Content(JsonConvert.SerializeObject(chapter_list), "application/json");

  }


  // 以下尚未運用
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
