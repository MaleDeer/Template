export var IsNull = (value: any): boolean => {
  if (value == null || value == undefined) return true;
  else return false;
};

/**
 * 微信SDK当前版本号是否大于某个版本号
 * @export
 * @param {string} otherV 其他版本
 * @returns {boolean}
 */
export function CompareVersion(otherV: string): boolean {
  let thisV = wx.getSystemInfoSync().SDKVersion;
  let thisVSplit = thisV.split(".");
  let otherVSplit = otherV.split(".");
  (thisV = ""), (otherV = "");
  for (let i = 0; i < thisVSplit.length; i++) {
    if (i == 0) {
      thisVSplit[i] = thisVSplit[i] + ".";
    }
    thisV += thisVSplit[i];
  }
  for (let i = 0; i < otherVSplit.length; i++) {
    if (i == 0) {
      otherVSplit[i] = otherVSplit[i] + ".";
    }
    otherV += otherVSplit[i];
  }
  let thisVNum = parseFloat(thisV);
  let otherVNum = parseFloat(otherV);
  return thisVNum >= otherVNum ? true : false;
}
