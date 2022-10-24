using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Dapper;
using System.Data.Common;

namespace EventChart_AP.DAO;

public class DBFactory
{

  protected string DBType = ""; //ORACLE

  //private System.Data.OleDb.OleDbConnection connOracle;
  //private System.Data.OracleClient.OracleConnection connOracle;
  //private System.Data.SqlClient.SqlConnection connSQL;

  protected System.Data.Common.DbConnection connDB;
  //protected System.Data.Common.DbTransaction tranSation;

  protected string tableName = "";
  protected string strSQL = "";
  protected string strSQL_NoLock = "";
  protected string orderBy = "";

  public DBFactory(string type, string connString)
  {
    //DBType = (String)System.Web.HttpContext.Current.Application["DATABASE"];
    DBType = type;
    if (DBType == "MSSQL")
    {
      connDB = new System.Data.SqlClient.SqlConnection();
    }
    else if (DBType == "MYSQL")
    {
      connDB = new MySql.Data.MySqlClient.MySqlConnection();
    }
    //else
    //{
    //connDB = new System.Data.OracleClient.OracleConnection();
    //connDB = new Oracle.ManagedDataAccess.Client.OracleConnection();
    //}
    connDB.ConnectionString = connString;

  }
  public System.Data.Common.DbConnection getConntion()
  {
    return connDB;
  }
  ~DBFactory()
  {
    CloseConnect();
    connDB.Dispose();
  }
  public void OpenConnect()
  {
    if (connDB.State != ConnectionState.Open)
    {
      connDB.Open();
      return;
    }
  }

  public void CloseConnect()
  {
    if (connDB.State == ConnectionState.Open)
    {
      try
      {
        connDB.Close();
      }
      catch
      {
        return;
      }
      return;
    }
  }

  public List<T> Query<T>(String strSql, object pObj = null)
  {

    List<T> rList = new List<T>();

    try
    {
      OpenConnect();
      rList = connDB.Query<T>(strSql, pObj).ToList();
    }
    catch (Exception e)
    {
      throw e;
    }
    finally
    {
      CloseConnect();
    }

    return rList;
  }

  public List<dynamic> Query(String strSql, object pObj = null)
  {
    var rList = new List<dynamic>();

    try
    {
      OpenConnect();
      rList = connDB.Query(strSql, pObj).ToList();
    }
    catch (Exception e)
    {
      throw e;
    }
    finally
    {
      CloseConnect();
    }

    return rList;
  }

  public int Execute(String strSql, object pObj = null)
  {
    int n1 = 0;

    try
    {
      OpenConnect();
      n1 = connDB.Execute(strSql, pObj);
    }
    catch (Exception e)
    {
      throw e;
    }
    finally
    {
      CloseConnect();
    }

    return n1;
  }

}
