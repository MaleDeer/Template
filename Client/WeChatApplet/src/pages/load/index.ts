/*===============================================================
* Copyright (C) 2018 SangFor Ltd. All rights reserved.
*
* 文件名称 : index.ts
* 创建者 : MaJunCheng
* 创建时间 : 2018.09.16
* 描述 : none;
*
===============================================================*/

/// <reference path="../../wxAPI.d.ts" />

import { Wx_Page, Wx_PageData } from "../../Common/Entitys/EntitysBase";
import {
  Wx_GetSetting,
  Wx_GetLoginCode
} from "../../Common/Service/NetServiceBase";
import { Wx_FullUserInfo, Wx_LoginInfo } from "../../Common/Entitys/Entitys";
import { Wx_UserLogin } from "../../Common/Service/NetService";

/**
 * 页面的数据
 * @class IndexPageData
 */
class IndexPageData extends Wx_PageData {
  public canIUse: any = wx.canIUse("button.open-type.getUserInfo");

  /**
   * 判断是否授权
   * @type {boolean}
   * @memberof IndexPageData
   */
  public Authorized: boolean = true;
}

/**
 * 页面 Page
 * @class IndexPage
 * @extends {IndexPageBase}
 */
class IndexPage extends Wx_Page {
  /**
   * 构造函数
   * @memberof IndexPage
   */
  constructor() {
    super();
    (this as any).data = this.Data;
  }

  /**
   * 页面数据
   * @type {IndexPageData}
   * @memberof IndexPage
   */
  public Data: IndexPageData = new IndexPageData();

  /**
   * 页面加载时触发
   * @memberof IndexPage
   */
  public async onLoad(options: any) {
    this.Data.Authorized = await Wx_GetSetting();
    await setTimeout(() => {
      if (this.Data.Authorized) {
        /** 已经授权 */
        wx.reLaunch({
          url: "../index/index"
        });
      } else {
        /** 等待授权 */
        this.ApplyChange(this.Data);
      }
    }, 600);
  }

  /**
   * 获得用户信息
   * @param options 用户信息
   */
  public async bindGetUserInfo(options: any) {
    console.log(options);
    // 获取登录凭证
    let code: string = await Wx_GetLoginCode();
    // 用户授权后获取的详细信息
    let fullUserInfo: Wx_FullUserInfo = options.detail as Wx_FullUserInfo;
    // 将用户数据转换为Json格式的字符串
    let fullUserInfoStr: string = JSON.stringify(fullUserInfo);

    // 获取登录凭证 和 用户数据
    let UserInfo: Wx_LoginInfo = await Wx_UserLogin(code, fullUserInfoStr);

    wx.setStorageSync("EncryptStr", UserInfo.encryptStr);
    wx.setStorageSync("UnionId", UserInfo.userInfo.unionId);

    //用户是否已经授权
    let Authorized: boolean = await Wx_GetSetting();
    if (Authorized) {
      wx.reLaunch({
        url: "../index/index"
      });
    }
  }
}

// 注册页面
Page(new IndexPage());