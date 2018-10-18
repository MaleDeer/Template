using CommonLibrary.Entitys;
using CommonLibrary.Helpers;
using CommonLibrary.Tools;
using LitJson;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WeChatLibrary.Entitys;
using WeChatLibrary.Helpers;

namespace Api.Controllers
{
    /// <summary>
    /// 微信控制器
    /// </summary>
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class WeChatController : BaseController
    {
        /// <summary>
        /// 提供数据保护
        /// </summary>
        /// <param name="provider">数据保护提供者</param>
        public WeChatController(IDataProtectionProvider provider)
            : base(provider) { }


        /// <summary>
        /// 获取用户凭证
        /// </summary>
        /// <param name="code">临时登录凭证code</param>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)] //swagger 文档中不显示这个接口
        public async Task<ActionResult<ResultData<UserKey>>> GetWxUserIdentity(string code)
        {
            try
            {
                UserKey UserProof = new WeChatHelper().GetWxUserIdentity(code);
                //为了数据安全，不对外提供这个密钥
                UserProof.session_key = null;
                return await OutDataAsync(UserProof);
            }
            catch (Exception e)
            {
                return await OutErrorAsync<UserKey>(e.Message);
            }
        }

        /// <summary>
        /// 微信用户登录
        /// </summary>
        /// <param name="code">临时登录凭证code</param>
        /// <param name="fullUserInfoStr">微信用户信息</param>
        /// <returns></returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResultData<LoginInfo>> Wx_UserLogin(string code, string fullUserInfoStr)
        {
            FullUserInfo fillUserInfo = JsonHelper.ParseFormJson<FullUserInfo>(fullUserInfoStr);
            JsonData WeChatConfig = AppConfig.Configs["PrjectConfig"]["WeChat"];
            string AppId = WeChatConfig["AppID"].ToString();
            string AppSecret = WeChatConfig["AppSecret"].ToString();
            string EncryptSky = WeChatConfig["EncryptStr"].ToString();
            WeChatHelper wxHelper = new WeChatHelper(AppId, AppSecret);
            //用户标识
            UserKey userKey = wxHelper.GetWxUserIdentity(code);
            bool vaildateUser = wxHelper.VaildateUserInfo(fillUserInfo.rawData, fillUserInfo.signature, userKey.session_key);
            if (vaildateUser)
            {
                //解密后的用户数据
                EncryptedData userData = wxHelper.DecryptEncryptedData(fillUserInfo.encryptedData, userKey.session_key, fillUserInfo.iv);
                UserService service = new UserService();
                //对比用户数据，无则增，有则改
                await service.Wx_UserUpdate(userData);

                string MD5Encrypt;
                using (MD5 md5Hash = MD5.Create())
                {
                    string EncryptStr = EncryptSky + (string.IsNullOrWhiteSpace(userData.unionId) ? userData.openId : userData.unionId);
                    // 获取 EncryptStr 的 MD5 哈希值
                    MD5Encrypt = Md5Helper.GetMd5Hash(md5Hash, EncryptStr);
                }
                if (string.IsNullOrWhiteSpace(userData.unionId))
                    userData.unionId = userData.openId;
                LoginInfo loginInfo = new LoginInfo
                {
                    UserInfo = userData,
                    EncryptStr = MD5Encrypt
                };
                return await OutDataAsync(loginInfo);
            }
            else
            {
                return await OutErrorAsync<LoginInfo>("登陆失败");
            }
        }
    }
}
