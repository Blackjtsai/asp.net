namespace P1_AP.Common;

public class ConfigsUtil
{
  /// <summary>系統代號</summary>
  private static string _SYSTEM_ID;

  /// <summary>取得系統代號</summary>
  static string SYSTEM_ID
  {
    get { return _SYSTEM_ID; }
  }

  /// <summary>系統名稱</summary>
  private static string _SYSTEM_Name;

  /// <summary>取得系統名稱</summary>
  string SYSTEM_Name
  {
    get { return _SYSTEM_Name; }
  }




  /// <summary>資料庫連線資訊</summary>
  private static string _Net6TestConn;
  /// <summary>取得資料庫連線資訊</summary>
  public string Net6TestConn
  {
    get { return _Net6TestConn; }
  }
  /// <summary>列表筆術數</summary>
  private static int _PageDisplayNum;

  /// <summary>取得列表筆術數</summary>
  public int PageDisplayNum
  {
    get { return _PageDisplayNum; }
  }
  /// <summary>創建init</summary>
  public ConfigsUtil()
  {
    if (_SYSTEM_ID == null)
    {
      reloadConfig();
    }
  }
  public void reloadConfig()
  {
    var builder = new ConfigurationBuilder()
    .SetBasePath(Directory
    .GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

    IConfigurationRoot configsettings = builder.Build();

    _SYSTEM_ID = configsettings["SYSTEM_ID"];
    _SYSTEM_Name = configsettings["SYSTEM_Name"];
    _PageDisplayNum = Convert.ToInt16(configsettings["PageDisplayNum"]);
    _Net6TestConn = configsettings.GetConnectionString("Net6TestConn");

  }
}

