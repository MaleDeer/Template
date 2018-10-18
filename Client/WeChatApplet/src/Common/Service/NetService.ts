import { T_User, Wx_LoginInfo } from "../Entitys/Entitys";
import { Config } from "../../Config";
import * as NSBase from "./NetServiceBase";

/**
 * 用户登录
 * @param code 用户临时登录凭证
 * @param fullUserInfoStr 用户数据
 */
export var Wx_UserLogin = (
  code: string,
  fullUserInfoStr: string
): Promise<Wx_LoginInfo> => {
  return new Promise<Wx_LoginInfo>(async (resolve, reject) => {
    try {
      let data = { code, fullUserInfoStr };
      let LoginInfo: Wx_LoginInfo = await NSBase.Wx_Request<Wx_LoginInfo>(
        Config.Wx_UserLogin,
        data,
        "POST"
      );
      resolve(LoginInfo);
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 插入一条用户数据
 * @param User 用户数据
 */
export var InsertUser = (User: T_User): Promise<void> => {
  return new Promise<void>(async (resolve, reject) => {
    try {
      await NSBase.Wx_Request(Config.InsertUser, User);
      resolve();
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 删除单个用户
 * @param UserId 用户Id
 */
export var DeleteUserById = (UserId: string): Promise<void> => {
  return new Promise<void>(async (resolve, reject) => {
    try {
      await NSBase.Wx_Request(Config.DeleteUserById, UserId);
      resolve();
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 更新单个用户数据
 * @param User 用户数据
 */
export var UpdateUser = (User: T_User): Promise<void> => {
  return new Promise<void>(async (resolve, reject) => {
    try {
      await NSBase.Wx_Request(Config.UpdateUser, User);
      resolve();
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 查询单个用户信息
 * @param UserId 用户Id
 */
export var QueryUserById = (UserId: string): Promise<T_User> => {
  return new Promise<T_User>(async (resolve, reject) => {
    try {
      let user: T_User = await NSBase.Wx_Request<T_User>(
        Config.QueryUserById,
        UserId
      );
      resolve(user);
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 查询全部用户信息
 */
export var QueryUsersAll = (): Promise<Array<T_User>> => {
  return new Promise<Array<T_User>>(async (resolve, reject) => {
    try {
      let users: Array<T_User> = await NSBase.Wx_Request<Array<T_User>>(
        Config.QueryUsersAll
      );
      resolve(users);
    } catch (e) {
      reject(e);
    }
  });
};
