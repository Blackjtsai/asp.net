using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventChart_Core;

namespace EventChart_AP.DAO;
/// <summary>EventData存取物件</summary>
public class DAOEventData : DBTBNet6TESTFactory
{
  /// <summary>取訂單總數</summary>
  public List<dynamic> getOrderTotal()
  {
    string strSQL;
    strSQL = @"select count(*) as count from event_data ";

    return Query(strSQL, null);
  }
  /// <summary>取刷卡總數</summary>
  public List<dynamic> getPayTotal()
  {
    string strSQL;
    strSQL = @"select count(*) as count  from event_data where pay_status=5";
    return Query(strSQL, null);
  }
}