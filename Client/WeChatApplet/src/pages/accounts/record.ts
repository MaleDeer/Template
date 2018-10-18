/*===============================================================
* Copyright (C) 2018 SangFor Ltd. All rights reserved.
*
* 文件名称 : record.ts
* 创建者 : MaJunCheng
* 创建时间 : 2018.10.03
* 描述 : none;
*
===============================================================*/

/// <reference path="../../wxAPI.d.ts" />

import { Wx_PageData, Wx_Page } from "../../Common/Entitys/EntitysBase";
import { T_UserBillState } from "../../Common/Entitys/Entitys";

/**
 * 页面的数据
 * @class IndexPageData
 */
class IndexPageData extends Wx_PageData {
  /**
   * 账单类型
   * @type {T_UserBillState}
   * @memberof IndexPageData
   */
  public BillState:T_UserBillState = new T_UserBillState();
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
   * 页面加载触发
   * @param {*} option
   * @memberof IndexPage
   */
  public async onLoad(option: any) {
      
  }
}

// 注册页面
Page(new IndexPage());
