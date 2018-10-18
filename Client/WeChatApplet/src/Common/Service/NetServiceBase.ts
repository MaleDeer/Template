import { IsNull } from "../Library/GlobalExtend";
import { ResultData } from "../Entitys/Entitys";

/**
 * 转换为可以接收List<T>的格式
 * @param data
 */
export var ObjParseParam = (data: any) => {
  if (typeof data != "object") {
    return "";
  }
  var urlEncode = (param: any, key?: any, encode?: any) => {
    if (param == null) {
      return "";
    }
    let paramStr: string = "";
    let t = typeof param;
    if (t == "string" || t == "number" || t == "boolean") {
      paramStr += `&${key}=${
        encode == null || encode ? encodeURIComponent(param) : param
      }`;
    } else {
      for (let i in param) {
        let k =
          key == null
            ? i
            : key + (param instanceof Array ? `[${i}]` : `[${i}]`);
        paramStr += urlEncode(param[i], k, encode);
      }
    }
    return paramStr;
  };
  return urlEncode(data).substring(1);
};

/**
 * 发起网络请求
 * @param url 请求的URL
 * @param data 请求的数据
 * @param method Get请求或者Post请求
 */
export var Wx_Request = <T>(
  url: string,
  data?: any,
  method: string = "GET"
): Promise<T> => {
  return new Promise<T>((resolve, reject) => {
    try {
      wx.request({
        url: url,
        data: data,
        method: method,
        header: {
          "content-type": "application/x-www-form-urlencoded",
          EncryptStr: wx.getStorageSync("EncryptStr") || "",
          UnionId: wx.getStorageSync("UnionId") || ""
        },
        success: async (res: any) => {
          console.log("得到的数据:",res);
          let data: ResultData<T> = res.data;
          if (data.code == 200) {
            let resultData: T = data.result;
            resolve(resultData);
          } else {
            reject("获取服务器数据错误" + data.code);
          }
        },
        fail: res => {
          reject(res.errMsg);
        }
      });
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 查询用户是否已经授权
 */
export var Wx_GetSetting = (): Promise<boolean> => {
  return new Promise<boolean>((resolve, reject) => {
    try {
      // 查看是否授权
      wx.getSetting({
        success: function(res) {
          if (res.authSetting["scope.userInfo"]) {
            resolve(true);
          } else {
            resolve(false);
          }
        }
      });
    } catch (e) {
      reject(e);
    }
  });
};

/**
 * 获取登录凭证
 */
export var Wx_GetLoginCode = (): Promise<string> => {
  return new Promise<string>((resolve, reject) => {
    try {
      wx.login({
        success: res => {
          if (res.code) {
            resolve(res.code);
          } else {
            reject("登录失败！");
          }
        }
      });
    } catch (e) {
      reject(e);
    }
  });
};
