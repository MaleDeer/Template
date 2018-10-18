/*===============================================================
* Copyright (C) 2018 SangFor Ltd. All rights reserved.
*
* 文件名称 : index.ts
* 创建者 : MaJunCheng
* 创建时间 : 2018.10.14
* 描述 : none;
*
===============================================================*/

/// <reference path="../../wxAPI.d.ts" />

import { Wx_Page, Wx_PageData } from "../../Common/Entitys/EntitysBase";

/**
 * 页面的数据
 * @class IndexPageData
 */
class IndexPageData extends Wx_PageData {}

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
  public async onLoad(options: any) {}

  /**
   * 跳转到记账页面
   * @param {*} option
   * @memberof IndexPage
   */
  public async bindNavigateRecord(option: any) {
    wx.navigateTo({
      url: "../accounts/record"
    });
  }
}

// 注册页面
Page(new IndexPage());