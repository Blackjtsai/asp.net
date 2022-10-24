using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P1_Core;

namespace P1_AP.DAO;
/// <summary>Empy存取物件</summary>
public class DAOEmpy : DBTBNet6TESTFactory
{
  /// <summary>新增Empy資料</summary>
  public int insert(empy empyObj)
  {
    string strSQL;
    strSQL = @"insert into empy (empy_id, empy_name, account, password,age,sex,fav) values (@empy_id, @empy_name, @account, @password,@age,@sex,@fav)";
    return Execute(strSQL, empyObj);
  }

  /// <summary>刪除指定Empy資料</summary>
  public int delete(empy empyObj)
  {
    string strSQL;

    strSQL = @"delete from empy where empy_id = @empy_id";

    return Execute(strSQL, empyObj);

  }

  /// <summary>更新Empy資料</summary>
  public int update(empy empyObj)
  {
    string strSQL;

    strSQL = @"Update empy set 
      empy_name = @empy_name
      , account = @account
      , password = @password 
      , sex = @sex 
      , fav = @fav 
      , age = @age 
      where empy_id = @empy_id";

    return Execute(strSQL, empyObj);

  }

  /// <summary>取Empy最大ID</summary>
  public dynamic getMaxID()
  {
    string strSQL;
    strSQL = @"select max(empy_id) as id from empy ";
    return Query(strSQL, null)[0];
  }

  /// <summary>查詢所有Empy資料</summary>
  public List<empy> getAll()
  {
    string strSQL;
    strSQL = @"select * from empy ";
    return Query<empy>(strSQL, null);
  }

  /// <summary>依照empy_id取資料</summary>
  public List<empy> getDataByID(string empy_id)
  {
    string strSQL;
    strSQL = @"select * from empy where empy_id = @empy_id";
    return Query<empy>(strSQL, new { empy_id = empy_id });

  }

  /// <summary>依照empy_id取資料</summary>
  public List<empy> getDataByAccount(string account)
  {
    string strSQL;
    strSQL = @"select * from empy where account = @account";
    return Query<empy>(strSQL, new { account = account });

  }

  /// <summary>分頁及查詢</summary>
  public List<dynamic> getDataBySearch(string empy_name, string account, int age, string sex, string fav)
  {
    string strSQL = " select * from empy where 1=1 ";
    string strWhere = "";
    if (!String.IsNullOrEmpty(empy_name))
    {
      empy_name = "%" + empy_name + "%";
      strWhere += "and empy_name like @empy_name ";
    }
    if (!String.IsNullOrEmpty(account))
    {
      account = "%" + account + "%";
      strWhere += "and account like @account ";
    }
    if (age > 0)
    {
      strWhere += "and age = @age ";
    }
    if (!String.IsNullOrEmpty(sex))
    {
      sex = "%" + sex + "%";
      strWhere += "and sex like @sex ";
    }
    if (!String.IsNullOrEmpty(fav))
    {
      fav = "%" + fav + "%";
      strWhere += "and fav like @fav ";
    }
    strSQL += strWhere;
    return Query<dynamic>(strSQL, new { empy_name = empy_name, account = account, age = age, sex = sex, fav = fav });



    // string strSQL;
    // strSQL = @"select count(*) as num from empy ";
    // strSQL += " where account = @account";
    // List<dynamic> arr = Query(strSQL, empyObj);

    // strSQL = @"select * from empy ";
    // strSQL += " where account = @account";

    // public List<empy> getDataBySearch(dynamic obj1)
    // {
    //   List<string> pNames = new List<string>();
    //   foreach (KeyValuePair<string, object> kvp in (IDictionary<string, object>)obj1)
    //   {
    //     pNames.Add(kvp.Key);
    //   }
    // }
  }

}