using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using P1_AP.Models;
using P1_AP.DAO;
using P1_Core;
using Newtonsoft.Json; //序列化


namespace P1_AP.Common;

/// <summary>登入檢查相關函式</summary>
public class LoginInfo
{
  /// <summary>確認是否登入</summary>
  public RedirectToActionResult checkLoginStatus(Controller cl)
  {
    if (cl.HttpContext.Session.GetString("LoginInfoData") == null)
    {
      return cl.RedirectToAction("Index", "Login");
    }
    return null;
  }

  /// <summary>頁面權限判斷</summary>
  public RedirectToActionResult checkRoleStatus(Controller cl, int empy_id, string action_name, string controller_name)
  {
    // 檢核頁面功能權限作業
    DAOEmpyRole DAOEmpyRole = new DAOEmpyRole();
    List<empy_role> empy_role_list = DAOEmpyRole.getDataByEmpyId(empy_id, action_name, controller_name);

    if (empy_role_list.Count == 0)
    {
      return cl.RedirectToAction("NoRole", "Login");
    }
    return null;
  }

  /// <summary>取得 getLoginInfoData</summary>
  public stdRespones getLoginInfoData(Controller cl)
  {
    stdRespones res = new stdRespones();
    if (cl.HttpContext.Session.GetString("LoginInfoData") == null)
    {
      res.resCode = "9999";
      res.resNote = "尚未登入";
      res.resData = null;
      return res;
    }
    else
    {
      res.resCode = "0000";
      res.resNote = "已登入";
      res.resData = JsonConvert.DeserializeObject<empy>(cl.HttpContext.Session.GetString("LoginInfoData"));
      return res;
    }
  }
}




