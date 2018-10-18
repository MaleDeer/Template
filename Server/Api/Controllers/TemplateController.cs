using CommonLibrary.Entitys;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using Model.ResultEntitys;
using Service;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 微信控制器
    /// </summary>
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class TemplateController : BaseController
    {
        /// <summary>
        /// 提供数据保护
        /// </summary>
        /// <param name="provider">数据保护提供者</param>
        public TemplateController(IDataProtectionProvider provider)
            : base(provider) { }


        #region 用户服务
        /// <summary>
        /// 用户的信息更新
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<string>> InsertUser(TUser user)
        {
            try
            {
                UserService service = new UserService();
                await service.InsertUser(user);
                return await OutDataAsync();
            }
            catch (Exception e)
            {
                return await OutErrorAsync<string>(e.Message);
            }
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="encryptId">用户信息Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultData<string>> DeleteUserById(string encryptId)
        {
            try
            {
                UserService service = new UserService();
                int id = int.Parse(_protector.Unprotect(encryptId));
                await service.DeleteUserById(id);
                return await OutDataAsync();
            }
            catch (Exception e)
            {
                return await OutErrorAsync<string>(e.Message);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ResultData<string>> UpdateUser(TUser user)
        {
            try
            {
                UserService service = new UserService();
                user.Id = int.Parse(_protector.Unprotect(user.EncryptId));
                await service.UpdateUser(user);
                return await OutDataAsync();
            }
            catch (Exception e)
            {
                return await OutErrorAsync<string>(e.Message);
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="encryptId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultData<TUser>> QueryUserById(string encryptId)
        {
            try
            {
                UserService service = new UserService();
                int id = int.Parse(_protector.Unprotect(encryptId));
                TUser user = await service.QueryUserById(id);
                return await OutDataAsync(user);

            }
            catch (Exception e)
            {
                return await OutErrorAsync<TUser>(e.Message);
            }
        }
        #endregion


        #region 用户密码服务
        /// <summary>
        /// 插入用户密码
        /// </summary>
        /// <param name="userPassWord">用户密码信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultData<string>> InsertUserPassWord(TUserPassWord userPassWord)
        {
            try
            {
                UserPassWordService service = new UserPassWordService();
                await service.InsertUserPassWord(userPassWord);
                return await OutDataAsync();
            }
            catch (Exception e)
            {
                return await OutErrorAsync<string>(e.Message);
            }
        }

        /// <summary>
        /// 删除用户密码
        /// </summary>
        /// <param name="encryptId">用户密码Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultData<string>> DeleteUserPassWordById(string encryptId)
        {
            try
            {
                UserPassWordService service = new UserPassWordService();
                int id = int.Parse(_protector.Unprotect(encryptId));
                await service.DeleteUserPassWordById(id);
                return await OutDataAsync();
            }
            catch (Exception e)
            {
                return await OutErrorAsync<string>(e.Message);
            }
        }

        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="userPassWord">用户密码信息</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ResultData<string>> UpdateUserPassWord(TUserPassWord userPassWord)
        {
            try
            {
                UserPassWordService service = new UserPassWordService();
                userPassWord.Id = int.Parse(_protector.Unprotect(userPassWord.EncryptId));
                await service.UpdateUserPassWord(userPassWord);
                return await OutDataAsync();
            }
            catch (Exception e)
            {
                return await OutErrorAsync<string>(e.Message);
            }
        }

        /// <summary>
        /// 查询用户密码
        /// </summary>
        /// <param name="encryptId">用户密码Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultData<TUserPassWord>> QueryUserPassWordById(string encryptId)
        {
            try
            {
                UserPassWordService service = new UserPassWordService();
                int id = int.Parse(_protector.Unprotect(encryptId));
                TUserPassWord userPassWord = await service.QueryUserPassWordById(id);
                return await OutDataAsync(userPassWord);
            }
            catch (Exception e)
            {
                return await OutErrorAsync<TUserPassWord>(e.Message);
            }
        }

        /// <summary>
        /// 查询用户密码
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultData<R_DataPaging<TUserPassWord>>> QueryUserPassWordPaging(int userId, int pageIndex, int pageSize)
        {
            try
            {
                UserPassWordService service = new UserPassWordService();
                var dataPaging = await service.QueryUserPassWordPaging(userId, pageIndex, pageSize);
                return await OutDataAsync(dataPaging);
            }
            catch (Exception e)
            {
                return await OutErrorAsync<R_DataPaging<TUserPassWord>>(e.Message);
            }
        }
        #endregion
    }
}
