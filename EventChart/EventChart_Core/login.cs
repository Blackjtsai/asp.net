namespace EventChart_Core
{
  public class login
  {
    /// <summary>login流水號</summary>
    public int login_id { get; set; }
    /// <summary>登入ID</summary>
    public int empy_id { get; set; }
    /// <summary>建立時間</summary>
    public string created_datetime { get; set; }
    /// <summary>登入狀況</summary>
    public int status { get; set; }
    /// <summary>登入說明</summary>
    public string note { get; set; }

    //=================其他==================
    /// <summary>登入帳號</summary>
    public string empy_name { get; set; }

  }
}
