using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P1_Core;

namespace P1_AP.DAO;
/// <summary>EventData存取物件</summary>
public class DAOEventDocData : DBTBNet6TESTFactory
{
  /// <summary>進店登記</summary>
  public List<dynamic> getEvent01Total()
  {
    string strSQL;
    strSQL = @"select count(*) as count from event_data ";

    return Query(strSQL, null);
  }
  /// <summary>取專人聯繫數量</summary>
  public List<dynamic> getEvent02Total()
  {
    string strSQL;
    strSQL = @"select count(*) as count  from event_data where pay_status=5";
    return Query(strSQL, null);
  }
  /// <summary>取直播抽獎數量</summary>
  public List<dynamic> getEvent03Total()
  {
    string strSQL;
    strSQL = @"select count(*) as count  from event_data where pay_status=5";
    return Query(strSQL, null);
  }
}