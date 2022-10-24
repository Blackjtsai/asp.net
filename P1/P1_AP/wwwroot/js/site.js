//  選單資料
Vue.createApp({
  data() {
    return {
      menu_items: []
      , login_status: false
      , login_status_data: null
      // ,account: ""
      // ,password: ""
    }
  },
  mounted() {
    // 取nav
    var that = this;
    $.getJSON("/System/Chapter/getDataByAjax", '',
      function (result) {
        that.menu_items = result;
      });
    // 取登入狀態及登入資料
    $.getJSON("/System/Login/getLoginInfoData", '',
      function (result) {
        if (result.resCode == "0000") {
          that.login_status = true;
          that.login_status_data = result.resData;
        }
      });
  },
  computed: {
    // 選單資料
    now_menu() {
      return this.menu_items;
    },
    // 登入狀態
    isLogin() {
      return this.login_status;
    },
    // 登入資料
    isLoginData() {
      return this.login_status_data;
    },
  },
  methods: {
    //登入按鈕
    loginAction() {
      let url = "/System/Login/Index";
      window.location.href = url;
    },
    //登出按鈕
    logoutAction() {
      if (confirm("確定要登出？")) {
        this.login_status = false;
        let url = "/System/Login/LogOut";
        window.location.href = url;
      }
    },
  }
}).mount('#app-nav');

