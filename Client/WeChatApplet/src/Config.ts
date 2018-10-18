let host = "https://localhost:6001";
let app = "Lemon/";
let WeChat = "WeChat/";

export let Config = {
  //插入一条用户数据
  InsertUser: `${host}/${app}InsertUser`,
  //删除单个用户
  DeleteUserById: `${host}/${app}DeleteUserById`,
  //更新单个用户数据
  UpdateUser: `${host}/${app}UpdateUser`,
  //查询单个用户信息
  QueryUserById: `${host}/${app}QueryUserById`,
  //查询全部用户信息
  QueryUsersAll: `${host}/${app}QueryUsersAll`,



  //获取用户凭证
  GetWxUserIdentity: `${host}/${WeChat}GetWxUserIdentity`,
  //微信登录
  Wx_UserLogin: `${host}/${WeChat}Wx_UserLogin`  
};
