namespace EventChart_Core
{
  public class event_data
  {

    ///////////////////////////////////////////////
    /// <summary>訂單總數</summary>
    public int order_total { get; set; }
    /// <summary>刷卡總數</summary>
    public int pay_total { get; set; }
    /// <summary>刷卡成功</summary>
    public int pay_success { get; set; }
    /// <summary>刷卡失敗</summary>
    public int pay_error { get; set; }
    /// <summary>合約成功</summary>
    public int apply_success { get; set; }
    /// <summary>合約失敗</summary>
    public int apply_error { get; set; }

    /// <summary>合約待審</summary>
    public int apply_none { get; set; }

    /// <summary>CRM成功</summary>
    public int crm_success { get; set; }
    /// <summary>CRM成功</summary>
    public int crm_error { get; set; }

    /// <summary>EIP成功</summary>
    public int eip_success { get; set; }
    /// <summary>EIP成功</summary>
    public int eip_error { get; set; }
    /// <summary>event01_total</summary>
    public int event01_total { get; set; }
    /// <summary>event02_total</summary>
    public int event02_total { get; set; }
    /// <summary>event03_total</summary>
    public int event03_total { get; set; }
  }
}