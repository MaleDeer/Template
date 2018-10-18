/// <reference path="../../wxAPI.d.ts" />

import { CompareVersion } from "../Library/GlobalExtend";

/**
 * 页面数据基类
 * @export
 * @class Wx_PageData
 * @implements {WXPageDataObj}
 */
export class Wx_PageData implements WXPageDataObj {}

/**
 * 页面基类
 * @export
 * @class Wx_Page
 * @implements {WXPageObj}
 */
export class Wx_Page implements WXPageObj {

  /**
   * 设置页面数据
   * @param {*} data
   * @returns {Promise<void>}
   * @memberof Wx_Page
   */
  public PageSetData(data: any): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      try {
        if (CompareVersion("1.5.0")) {
          (this as any).setData(data, () => {
            resolve();
          });
        } else {
          (this as any).setData(data);
          resolve();
        }
      } catch (e) {
        reject(e);
      }
    });
  }

  /**
   * 应用更改
   * @param {Wx_PageData} data
   * @memberof Wx_Page
   */
  public async ApplyChange(Data: Wx_PageData) {
    let data = (this as any).data;
    let _data = {};
    for (const key in Data) {
      if (data.hasOwnProperty(key)) {
        if (data[key] != Data[key]) {
          _data[key] = Data[key];
        }
      } else {
        _data[key] = Data[key];
      }
    }
    await this.PageSetData(_data);
  }
}

