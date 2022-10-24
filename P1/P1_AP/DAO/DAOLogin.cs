using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P1_Core;

namespace P1_AP.DAO;
/// <summary>Login存取物件</summary>
public class DAOLogin : DBTBNet6TESTFactory
{
  /// <summary>新增Login資料</summary>
  public int insert(login loginObj)
  {
    string strSQL;
    strSQL = @"insert into login (login_id, empy_id, created_datetime, status,note) values (@login_id, @empy_id, @created_datetime, @status,@note)";
    return Execute(strSQL, loginObj);
  }

  /// <summary>刪除指定Login資料</summary>
  public int delete(login loginObj)
  {
    string strSQL;

    strSQL = @"delete from login where login_id = @login_id";

    return Execute(strSQL, loginObj);

  }

  /// <summary>更新Login資料</summary>
  public int update(login loginObj)
  {
    string strSQL;

    strSQL = @"Update login set 
      login_name = @login_name
      , empy_id = @empy_id
      , created_datetime = @created_datetime 
      , status = @status 
      where login_id = @login_id";

    return Execute(strSQL, loginObj);

  }

  /// <summary>取Login最大ID</summary>
  public dynamic getMaxID()
  {
    string strSQL;
    strSQL = @"select max(login_id) as id from login ";
    return Query(strSQL, null)[0];
  }

  /// <summary>查詢所有Login資料</summary>
  public List<login> getAll()
  {
    string strSQL;
    strSQL = @"select * from login ";
    return Query<login>(strSQL, null);
  }

  /// <summary>依照login_id取資料</summary>
  public List<login> getDataByID(string login_id)
  {
    string strSQL;

    strSQL = @"select a.* , b.empy_name 
    from login  as a 
    inner join empy as b 
    on a.empy_id = b.empy_id 
    where a.login_id = @login_id";
    return Query<login>(strSQL, new { login_id = login_id });

  }
  /// <summary>分頁及查詢</summary>
  public List<dynamic> getDataBySearch(string empy_name)
  {

    string strSQL = @"select a.* ,b.empy_name
    from login  as a 
    inner join empy as b 
    on a.empy_id = b.empy_id
    where 1 = 1 ";
    string strWhere = "";
    if (!String.IsNullOrEmpty(empy_name))
    {
      empy_name = "%" + empy_name + "%";
      strWhere += "and b.empy_name like @empy_name ";
    }

    strSQL += strWhere;
    return Query<dynamic>(strSQL, new { empy_name = empy_name });

  }

}