using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventChart_AP.Models;
using EventChart_AP.DAO;
using EventChart_Core;
using EventChart_AP.Common;
using Newtonsoft.Json; //序列化

namespace EventChart_AP.Controllers;

public class LoginController : Controller
{
  private string viewsPath = "~/Views/System/Login";
  private string controllerName = "Login";
  private DAOLogin dataDao = new DAOLogin();

  // 新增 C
  [HttpPost]
  public IActionResult Add(login el)
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;

    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Add", "/System/Login");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 新增作業 
    int num = dataDao.insert(el);

    TempData["ExcuteStatus"] = (num > 0) ? "新增資料成功" : "新增資料失敗";
    return RedirectToAction("List", controllerName);
  }

  // 查詢 R
  public IActionResult List(string empy_name = null, int pageNum = 0)
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "List", "/System/Login");
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

    // 1.查詢指定資料
    List<dynamic> login_list = dataDao.getDataBySearch(empy_name);
    // 2.計算總筆數
    int listCount = login_list.Count();
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
    login_list = (from item in login_list
                  where item.login_id >= 0
                  orderby item.login_id descending
                  select item).Skip(beginNum).Take(pageDisplayNum).ToList();

    // 8.紀錄查詢狀態 (使用empy（）及 分頁資料) 
    ViewData["listCount"] = listCount;
    ViewData["pageTotal"] = pageTotal;
    ViewData["pageNum"] = pageNum;
    ViewData["pageDisplayNum"] = pageDisplayNum;

    ViewData["empy_name"] = empy_name;

    // 注射至view方式一：使用session (先序列化)
    HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(login_list));

    // 注射至view方式二：使用model
    return View(viewsPath + "/List.cshtml", login_list);
  }
  // 修改 U
  [HttpPost]
  public IActionResult Edit(login el) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;

    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Edit", "/System/Login");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 更新指定作業   
    int num = dataDao.update(el);

    TempData["ExcuteStatus"] = (num > 0) ? ("login_id:" + el.login_id + "修改資料成功") : ("login_id:" + el.login_id + "修改資料失敗");
    return RedirectToAction("list", controllerName);
  }

  // 刪除 D
  [HttpGet]
  public IActionResult Del(string login_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Del", "/System/Login");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    // 刪除指定作業  
    login login_core = new login();
    login_core.login_id = Convert.ToInt16(login_id);

    int num = dataDao.delete(login_core);

    TempData["ExcuteStatus"] = (num > 0) ? ("id:" + login_id + "刪除資料成功") : ("id:" + login_id + "刪除資料失敗");
    return RedirectToAction("list", controllerName);
  }
  // 明細頁 N
  [HttpGet]
  public IActionResult Detail(string act, string login_id) // 
  {
    //== 登入權限檢核作業  開始====================================
    // 1.檢查是否登入
    LoginInfo LoginInfo = new LoginInfo();
    var r = LoginInfo.checkLoginStatus(this);
    if (r != null) return r;
    // 2.是否否有權限執行
    empy LoginInfoData = JsonConvert.DeserializeObject<empy>(HttpContext.Session.GetString("LoginInfoData"));
    r = LoginInfo.checkRoleStatus(this, LoginInfoData.empy_id, "Detail", "/System/Login");
    if (r != null) return r;
    //== 登入權限檢核作業  結束====================================

    TempData["act"] = Convert.ToString(act);
    TempData["login_id"] = Convert.ToString(login_id);
    return View(viewsPath + "/detail.cshtml");
  }
  // 查詢指定ID 
  [HttpGet]
  public IActionResult detailByID(string login_id)
  {
    // 檢查登入者是否有權限執行
    // 待處理

    // 查詢作業
    List<login> ListData = dataDao.getDataByID(login_id);
    return Content(JsonConvert.SerializeObject(ListData), "application/json");
  }
  // 查詢全部資料
  [HttpGet]
  public IActionResult getDataByAjax()
  {
    // 檢查登入者是否有權限執行
    // 待處理

    // 查詢作業
    List<login> ListData = dataDao.getAll();
    return Content(JsonConvert.SerializeObject(ListData), "application/json");
  }

  // 登入頁面
  public IActionResult Index()
  {

    return View("~/Views/System/Login/login.cshtml");
  }

  // 無權限畫面
  public IActionResult NoRole()
  {
    TempData["login_note"] = "無權限使用";
    return View("~/Views/System/Login/login_note.cshtml");
  }
  // 登入成功畫面
  public IActionResult Success()
  {
    TempData["login_note"] = "登入成功";
    return View("~/Views/System/Login/login_note.cshtml");
  }

  // 登出作業
  public IActionResult LogOut()
  {
    HttpContext.Session.Remove("LoginInfoData");
    return RedirectToAction("index", "Home");
  }
  // 登入作業
  public IActionResult loginAction(string login, string password)
  {
    HttpContext.Session.Remove("LoginInfoData");

    // 取login資料
    DAOEmpy empyDAO = new DAOEmpy();
    List<empy> empy_data = empyDAO.getDataByAccount(login);

    // 宣告
    login el = new login();
    el.status = 0;
    el.empy_id = empy_data[0].empy_id;
    el.created_datetime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

    // 檢查狀態
    if (empy_data.Count != 0)
    {
      el.status = 3; // 帳號正確
      el.note = "帳號正確";
      if (Convert.ToString(empy_data[0].password) == Convert.ToString(password))
      {
        el.status = 5; //帳號密碼正確
        el.note = "帳號密碼正確,登入成功！";
      }
      else
      {
        el.status = 8;
        el.note = "密碼錯誤";
      }
    }
    else
    {
      el.status = 9;//
      el.note = "查無帳號";
    }

    //  新增登入資料
    DAOLogin DAOLogin = new DAOLogin();
    int num = DAOLogin.insert(el);
    // if(num==0){Exception 處理～}

    // 將登入資料放置HttpContext.Session.GetString["LoginInfoData"]
    if (el.status == 5)
    {
      HttpContext.Session.SetString("LoginInfoData", JsonConvert.SerializeObject(empy_data[0]));
    }

    // 回傳
    return Content(JsonConvert.SerializeObject(el), "application/json");
  }

  // 取登入狀態
  public IActionResult getLoginInfoData()
  {
    // 取登入狀態 
    LoginInfo LoginInfo = new LoginInfo();
    stdRespones res = LoginInfo.getLoginInfoData(this);

    // 回傳
    return Content(JsonConvert.SerializeObject(res), "application/json");
  }

  // 以下尚未運用
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
