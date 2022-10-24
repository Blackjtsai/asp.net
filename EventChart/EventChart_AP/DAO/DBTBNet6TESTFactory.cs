using EventChart_AP.Common;

namespace EventChart_AP.DAO;
public class DBTBNet6TESTFactory : DBFactory
{
  public DBTBNet6TESTFactory()
      : base("MYSQL", new ConfigsUtil().Net6TestConn)
  {

  }
}

