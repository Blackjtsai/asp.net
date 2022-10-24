﻿using P1_AP.Common;

namespace P1_AP.DAO;
public class DBTBNet6TESTFactory : DBFactory
{
  public DBTBNet6TESTFactory()
      : base("MYSQL", new ConfigsUtil().Net6TestConn)
  {

  }
}

