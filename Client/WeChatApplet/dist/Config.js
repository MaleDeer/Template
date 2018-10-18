"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var host = "https://localhost:6001";
var app = "Lemon/";
var WeChat = "WeChat/";
exports.Config = {
    InsertUser: host + "/" + app + "InsertUser",
    DeleteUserById: host + "/" + app + "DeleteUserById",
    UpdateUser: host + "/" + app + "UpdateUser",
    QueryUserById: host + "/" + app + "QueryUserById",
    QueryUsersAll: host + "/" + app + "QueryUsersAll",
    GetWxUserIdentity: host + "/" + WeChat + "GetWxUserIdentity",
    Wx_UserLogin: host + "/" + WeChat + "Wx_UserLogin"
};
