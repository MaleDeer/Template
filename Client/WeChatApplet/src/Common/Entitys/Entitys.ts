import { Gender, PassWordState } from "./Enums";

/**
 * 从后台得到的数据
 * @export
 * @class GetData
 * @template T
 */
export class ResultData<T> {
  /**
   * 200为正确返回，300为未登录，400为错误返回
   * @type {number}
   * @memberof GetData
   */
  public code: number;

  /**
   * 错误说明， code!=200 有值
   * @type {string}
   * @memberof GetData
   */
  public message: string;

  /**
   * 正确返回时的返回数据
   * @type {T}
   * @memberof GetData
   */
  public result: T;
}

/*---------------微信实体 start------------------*/

/**
 * 登录的信息
 * @export
 * @class Wx_LoginInfo
 */
export class Wx_LoginInfo {
  /**
   * 用户信息
   * @type {Wx_EncryptedData}
   * @memberof Wx_LoginInfo
   */
  public userInfo: Wx_EncryptedData;

  /**
   * 登录凭证
   * @type {string}
   * @memberof Wx_LoginInfo
   */
  public encryptStr: string;
}

/**
 * 数字水印
 * @export
 * @class WaterMark
 */
export class Wx_WaterMark {
  public appid: string;
  public timestamp: number;
}

/**
 * 加密的数据
 * @export
 * @class EncryptedData
 */
export class Wx_EncryptedData {
  /**
   * OpenId
   * @type {string}
   * @memberof EncryptedData
   */
  public openId: string;

  /**
   * 昵称
   * @type {string}
   * @memberof EncryptedData
   */
  public nickName: string;

  /**
   * 性别
   * @type {Gender}
   * @memberof EncryptedData
   */
  public gender: Gender;

  /**
   * 语言
   * @type {string}
   * @memberof Wx_EncryptedData
   */
  public language: string;

  /**
   * 城市
   * @type {string}
   * @memberof EncryptedData
   */
  public city: string;

  /**
   * 省
   * @type {string}
   * @memberof EncryptedData
   */
  public province: string;

  /**
   * 故乡
   * @type {string}
   * @memberof EncryptedData
   */
  public country: string;

  /**
   * 头像Url
   * @type {string}
   * @memberof EncryptedData
   */
  public avatarUrl: string;

  /**
   * UnionId
   * @type {string}
   * @memberof EncryptedData
   */
  public unionId: string;

  /**
   * 数字水印
   * @type {WaterMark}
   * @memberof EncryptedData
   */
  public watermark: Wx_WaterMark;
}

/**
 * 用户信息
 * @export
 * @class UserInfo
 */
export class Wx_UserInfo {
  /**
   * 用户头像路径
   * @type {string}
   * @memberof UserInfo
   */
  public avatarUrl: string;

  /**
   * 用户所在城市
   * @type {string}
   * @memberof UserInfo
   */
  public city: string;

  /**
   * 用户故乡
   * @type {string}
   * @memberof UserInfo
   */
  public country: string;

  /**
   * 用户性别
   * @type {number}
   * @memberof UserInfo
   */
  public gender: Gender;

  /**
   * 语言
   * @type {string}
   * @memberof UserInfo
   */
  public language: string;

  /**
   * 用户昵称
   * @type {string}
   * @memberof UserInfo
   */
  public nickName: string;

  /**
   * 用户所在省
   * @type {string}
   * @memberof UserInfo
   */
  public province: string;
}

/**
 * 用户完整信息
 * @export
 * @class FullUserInfo
 */
export class Wx_FullUserInfo {
  /**
   * 加密的数据（包括敏感数据在内的完整用户信息的加密数据）
   * @type {string}
   * @memberof FullUserInfo
   */
  public encryptedData: string;

  /**
   * 错误的信息
   * @type {string}
   * @memberof FullUserInfo
   */
  public errMsg: string;

  /**
   * 加密算法的初始向量
   * @type {string}
   * @memberof FullUserInfo
   */
  public iv: string;

  /**
   * 不包括敏感信息的原始数据字符串，用于计算签名
   * @type {string}
   * @memberof FullUserInfo
   */
  public rawData: string;

  /**
   * 使用 sha1( rawData + sessionkey ) 得到字符串，用于校验用户信息
   * @type {string}
   * @memberof FullUserInfo
   */
  public signature: string;

  /**
   * 用户信息对象，不包含 openid 等敏感信息
   * @type {UserInfo}
   * @memberof FullUserInfo
   */
  public userInfo: Wx_UserInfo;
}

/*---------------微信实体 end------------------*/

/*---------------后端实体 start------------------*/

/**
 * 微信登录正确返回的数据
 * @export
 * @class WeChatLogin
 */
export class T_WeChatUserKey {
  /**
   * OpenId
   * @type {string}
   * @memberof WeChatLogin
   */
  public openid: string;

  /**
   * SessionKey
   * @type {string}
   * @memberof WeChatLogin
   */
  public session_key: string;

  /**
   * UnionId
   * @type {string}
   * @memberof WeChatLogin
   */
  public unionid: string;
}

/**
 * 用户信息
 * @export
 * @class T_User
 */
export class T_User {
  /**
   * Id
   * @type {number}
   * @memberof T_User
   */
  public Id: number;

  /**
   * 该Id为加密后的用户Id
   * @type {string}
   * @memberof T_User
   */
  public _Id: string;

  /**
   * OpenId 微信用户唯一标识
   * @type {string}
   * @memberof T_User
   */
  public OpenId: string;

  /**
   * UnionId 用户在微信开放平台的唯一标识
   * @type {string}
   * @memberof T_User
   */
  public UnionId: string;

  /**
   * AppId 小程序唯一标识
   * @type {string}
   * @memberof T_User
   */
  public AppId: string;

  /**
   * 用户昵称
   * @type {string}
   * @memberof T_User
   */
  public NickName: string;

  /**
   * 账户
   * @type {string}
   * @memberof T_User
   */
  public Account: string;

  /**
   * 手机号码
   * @type {string}
   * @memberof T_User
   */
  public PhoneNunmber: string;

  /**
   * 性别
   * @type {Sex}
   * @memberof T_User
   */
  public Gender: Gender;

  /**
   * 城市
   * @type {string}
   * @memberof T_User
   */
  public City: string;

  /**
   * 省
   * @type {string}
   * @memberof T_User
   */
  public Province: string;

  /**
   * 头像
   * @type {string}
   * @memberof T_User
   */
  public AvatarUrl: string;

  /**
   * 生日
   * @type {Date}
   * @memberof T_User
   */
  public Birthday: Date;

  /**
   * 创建时间
   * @type {Date}
   * @memberof T_User
   */
  public CreateTime: Date;

  /**
   * 用户的密码
   * @type {Array<T_UserPassWord>}
   * @memberof T_User
   */
  public UserPassWords: Array<T_UserPassWord>;

  /**
   * 用户账单
   * @type {Array<T_UserBill>}
   * @memberof T_User
   */
  public UserBills: Array<T_UserBill>;
}

/**
 * 用户账单
 * @export
 * @class T_UserBill
 */
export class T_UserBill {
  /**
   * Id
   * @type {number}
   * @memberof T_UserBill
   */
  public Id: number;

  /**
   * 该Id为加密后的用户账单Id
   * @type {string}
   * @memberof T_UserBill
   */
  public _Id: string;

  /**
   * 用户Id
   * @type {number}
   * @memberof T_UserBill
   */
  public UserId: number;

  /**
   * 账单类型
   * @type {number}
   * @memberof T_UserBill
   */
  public UserBillStateId: number;

  /**
   * 价格
   * @type {number}
   * @memberof T_UserBill
   */
  public Price: number;

  /**
   * 用户账单类型
   * @type {T_UserBillState}
   * @memberof T_UserBill
   */
  public UserBillState: T_UserBillState;

  /**
   * 用户
   * @type {T_User}
   * @memberof T_UserBill
   */
  public User: T_User;
}

/**
 * 用户账单类型
 * @export
 * @class T_UserBillState
 */
export class T_UserBillState {
  /**
   * Id
   * @type {number}
   * @memberof T_UserBillState
   */
  public Id: number;

  /**
   * 该Id为加密后的账单类型Id
   * @type {string}
   * @memberof T_UserBillState
   */
  public _Id: string;

  /**
   * PId
   * @type {number}
   * @memberof T_UserBillState
   */
  public PId: number;

  /**
   * 该PId为加密后的账单类型PId
   * @type {string}
   * @memberof T_UserBillState
   */
  public _PId:string;
  
  /**
   * 账单名称
   * @type {string}
   * @memberof T_UserBillState
   */
  public Name: string;

  /**
   * 图标
   * @type {string}
   * @memberof T_UserBillState
   */
  public IconFont: string;

  /**
   * 用户账单
   * @type {T_UserBill}
   * @memberof T_UserBillState
   */
  public UserBills: Array<T_UserBill>;
}

/**
 * 用户的密码
 * @export
 * @class T_UserPassWord
 */
export class T_UserPassWord {
  /**
   * Id
   * @type {number}
   * @memberof T_UserPassWord
   */
  public Id: number;

  /**
   * 该Id为加密后的用户的密码Id
   * @type {string}
   * @memberof T_UserPassWord
   */
  public _Id: string;

  /**
   * 用户Id
   * @type {number}
   * @memberof T_UserPassWord
   */
  public UserId: number;

  /**
   * 密码
   * @type {string}
   * @memberof T_UserPassWord
   */
  public PassWord: string;

  /**
   * 密码状态
   * @type {PassWordState}
   * @memberof T_UserPassWord
   */
  public State: PassWordState;

  /**
   * 用户信息
   * @type {T_User}
   * @memberof T_UserPassWord
   */
  public User: T_User;
}

/*---------------实体 end------------------*/
