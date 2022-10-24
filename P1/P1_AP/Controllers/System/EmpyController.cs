using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using P1_AP.Models;
using P1_AP.DAO;
using P1_Core;
using P1_AP.Common;
using Newtonsoft.Json; //序列化

namespace P1_AP.Controllers;

public class EmpyController : Controller
{
  private string folder_name = "/System/Empy";

  // 新增 C
  [HttpPost]
  public IActionResult Add(empy el)
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Add", "/System/Empy");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 新增作業   
    DAOEmpy DAOEmpy = new DAOEmpy();
    int num = DAOEmpy.insert(el);

    TempData["ExcuteStatus"] = (num > 0) ? "新增資料成功" : "新增資料失敗";
    return RedirectToAction("List", "Empy");
  }

  // 列表 
  [HttpGet]
  public IActionResult List(string empy_name = null, string account = null, int age = 0, string sex = null, string fav = null, int pageNum = 0)
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "List", "/System/Empy");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    //****************************************  
    // 1.查詢指定資料
    // 2.計算總筆數  
    // 3.取一頁幾筆  
    // 4.計算總頁數 
    // 5.設定預查頁數  
    // 6.計算從第幾筆開始 
    // 7.使用LinＱ 進行資料排序
    // 8.紀錄查詢狀態 (使用empy（）及 分頁資料) 
    //**************************************** 

    // 查詢 empy  全部資料
    // List<empy> empy_list = DAOEmpy.getAll();

    DAOEmpy DAOEmpy = new DAOEmpy();
    // 1.查詢指定資料
    List<dynamic> empy_list = DAOEmpy.getDataBySearch(empy_name, account, age, sex, fav);
    // 2.計算總筆數
    int listCount = empy_list.Count();
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
    empy_list = (from item in empy_list
                 where item.age >= 0
                 orderby item.empy_id descending
                 select item).Skip(beginNum).Take(pageDisplayNum).ToList();

    // 8.紀錄查詢狀態 (使用empy（）及 分頁資料) 
    ViewData["listCount"] = listCount;
    ViewData["pageTotal"] = pageTotal;
    ViewData["pageNum"] = pageNum;
    ViewData["pageDisplayNum"] = pageDisplayNum;

    ViewData["empy_name"] = empy_name;
    ViewData["account"] = account;
    ViewData["sex"] = sex;
    ViewData["fav"] = fav;

    // 注射至view方式一：使用session (先序列化) / 分頁使用
    HttpContext.Session.SetString("EmpyData", JsonConvert.SerializeObject(empy_list));

    // 注射至view方式二：使用model
    return View("~/Views" + folder_name + "/List.cshtml", empy_list);

  }
  // 修改 U
  [HttpPost]
  public IActionResult Edit(empy el)
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Edit", "/System/Empy");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 更新指定作業   
    DAOEmpy DAOEmpy = new DAOEmpy();
    int num = DAOEmpy.update(el);

    TempData["ExcuteStatus"] = (num > 0) ? ("empy_id:" + el.empy_id + "修改資料成功") : ("empy_id:" + el.empy_id + "修改資料失敗");
    return RedirectToAction("list", "Empy");
  }

  // 刪除 D
  [HttpGet]
  public IActionResult Del(string empy_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Del", "Empy");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 刪除指定作業   
    empy empy_core = new empy();
    empy_core.empy_id = Convert.ToInt16(empy_id);

    DAOEmpy DAOEmpy = new DAOEmpy();
    int num = DAOEmpy.delete(empy_core);

    TempData["ExcuteStatus"] = (num > 0) ? ("id:" + empy_id + "刪除資料成功") : ("id:" + empy_id + "刪除資料失敗");
    return RedirectToAction("list", "Empy");
  }

  // 明細頁 N
  [HttpGet]
  public IActionResult Detail(string act, string empy_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Detail", "/System/Empy");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================


    TempData["act"] = Convert.ToString(act);
    TempData["empy_id"] = Convert.ToString(empy_id);
    return View("~/Views" + folder_name + "/detail.cshtml");
  }

  // 查詢指定ID 
  [HttpGet]
  public IActionResult detailByID(string empy_id)
  {
    // 檢查登入者是否有權限執行

    // 查詢作業
    DAOEmpy DAOEmpy = new DAOEmpy();
    List<empy> empy_data = DAOEmpy.getDataByID(empy_id);
    return Content(JsonConvert.SerializeObject(empy_data), "application/json");
  }

  // 以下尚未運用
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
