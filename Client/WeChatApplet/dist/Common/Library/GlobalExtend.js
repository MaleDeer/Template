"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.IsNull = function (value) {
    if (value == null || value == undefined)
        return true;
    else
        return false;
};
function CompareVersion(otherV) {
    var thisV = wx.getSystemInfoSync().SDKVersion;
    var thisVSplit = thisV.split(".");
    var otherVSplit = otherV.split(".");
    (thisV = ""), (otherV = "");
    for (var i = 0; i < thisVSplit.length; i++) {
        if (i == 0) {
            thisVSplit[i] = thisVSplit[i] + ".";
        }
        thisV += thisVSplit[i];
    }
    for (var i = 0; i < otherVSplit.length; i++) {
        if (i == 0) {
            otherVSplit[i] = otherVSplit[i] + ".";
        }
        otherV += otherVSplit[i];
    }
    var thisVNum = parseFloat(thisV);
    var otherVNum = parseFloat(otherV);
    return thisVNum >= otherVNum ? true : false;
}
exports.CompareVersion = CompareVersion;
