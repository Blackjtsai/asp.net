using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P1_Core;

namespace P1_AP.DAO;
/// <summary>Empy存取物件</summary>
public class DAOEmpyRole : DBTBNet6TESTFactory
{
  /// <summary>新增Empy資料</summary>
  public int insert(empy_role empy_roleObj)
  {
    string strSQL;
    strSQL = @"insert into empy_role (empy_id, controller_name, action_name,note) values (@empy_id, @controller_name, @action_name,@note)";
    return Execute(strSQL, empy_roleObj);
  }

  /// <summary>刪除指定Empy資料</summary>
  public int delete(empy_role empy_roleObj)
  {
    string strSQL;
    strSQL = @"delete from empy_role where empy_role_id = @empy_role_id ";
    return Execute(strSQL, empy_roleObj);
  }
  /// <summary>更新Empy資料</summary>
  public int update(empy_role empy_roleObj)
  {
    string strSQL;

    strSQL = @"Update empy_role set 
      controller_name = @controller_name
      , action_name = @action_name
      , empy_id = @empy_id 
      , note = @note 
      where empy_role_id = @empy_role_id";

    return Execute(strSQL, empy_roleObj);

  }

  /// <summary>取Empy最大ID</summary>
  public dynamic getMaxID()
  {
    string strSQL;
    strSQL = @"select max(empy_role_id) as id from empy_role ";
    return Query(strSQL, null)[0];
  }

  /// <summary>查詢所有Empy資料 (join empy)</summary>
  public List<empy_role> getAll()
  {
    string strSQL;
    strSQL = @"select a.* , b.empy_name 
    from empy_role as a 
    inner join  empy as b 
    using(empy_id) ";
    return Query<empy_role>(strSQL, null);
  }

  /// <summary>依照empy_id取資料</summary>
  public List<empy_role> getDataByEmpyId(int empy_id, string action_name, string controller_name)
  {
    string strSQL;
    action_name = "%" + action_name + "%";
    strSQL = @"select * from empy_role where empy_id = @empy_id and controller_name = @controller_name and action_name like @action_name ";
    return Query<empy_role>(strSQL, new { empy_id = empy_id, controller_name = controller_name, action_name = action_name });
  }
  /// <summary>依照empy_id取資料</summary>
  public List<empy_role> getDataByID(string empy_role_id)
  {
    string strSQL;

    strSQL = @"select * from empy_role where empy_role_id = @empy_role_id";
    return Query<empy_role>(strSQL, new { empy_role_id = empy_role_id });

  }

  /// <summary>分頁及查詢</summary>
  public List<empy_role> getDataBySearch(string empy_name)
  {
    string strSQL = @"select a.* ,b.empy_name
    from empy_role  as a 
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
    return Query<empy_role>(strSQL, new { empy_name = empy_name });

  }



}