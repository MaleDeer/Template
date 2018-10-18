"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var _this = this;
Object.defineProperty(exports, "__esModule", { value: true });
exports.ObjParseParam = function (data) {
    if (typeof data != "object") {
        return "";
    }
    var urlEncode = function (param, key, encode) {
        if (param == null) {
            return "";
        }
        var paramStr = "";
        var t = typeof param;
        if (t == "string" || t == "number" || t == "boolean") {
            paramStr += "&" + key + "=" + (encode == null || encode ? encodeURIComponent(param) : param);
        }
        else {
            for (var i in param) {
                var k = key == null
                    ? i
                    : key + (param instanceof Array ? "[" + i + "]" : "[" + i + "]");
                paramStr += urlEncode(param[i], k, encode);
            }
        }
        return paramStr;
    };
    return urlEncode(data).substring(1);
};
exports.Wx_Request = function (url, data, method) {
    if (method === void 0) { method = "GET"; }
    return new Promise(function (resolve, reject) {
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
                success: function (res) { return __awaiter(_this, void 0, void 0, function () {
                    var data, resultData;
                    return __generator(this, function (_a) {
                        console.log("得到的数据:", res);
                        data = res.data;
                        if (data.code == 200) {
                            resultData = data.result;
                            resolve(resultData);
                        }
                        else {
                            reject("获取服务器数据错误" + data.code);
                        }
                        return [2];
                    });
                }); },
                fail: function (res) {
                    reject(res.errMsg);
                }
            });
        }
        catch (e) {
            reject(e);
        }
    });
};
exports.Wx_GetSetting = function () {
    return new Promise(function (resolve, reject) {
        try {
            wx.getSetting({
                success: function (res) {
                    if (res.authSetting["scope.userInfo"]) {
                        resolve(true);
                    }
                    else {
                        resolve(false);
                    }
                }
            });
        }
        catch (e) {
            reject(e);
        }
    });
};
exports.Wx_GetLoginCode = function () {
    return new Promise(function (resolve, reject) {
        try {
            wx.login({
                success: function (res) {
                    if (res.code) {
                        resolve(res.code);
                    }
                    else {
                        reject("登录失败！");
                    }
                }
            });
        }
        catch (e) {
            reject(e);
        }
    });
};
