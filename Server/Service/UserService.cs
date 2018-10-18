using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using System;
using System.Threading.Tasks;
using WeChatLibrary.Entitys;

namespace Service
{
    /// <summary>
    /// 用户信息服务
    /// </summary>
    public class UserService : BaseService
    {
        /// <summary>
        /// 用户的信息更新
        /// </summary>
        /// <returns>解密后的用户数据</returns>
        public async Task Wx_UserUpdate(EncryptedData userData)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    TUser user = await context.Users.SingleOrDefaultAsync(o => o.OpenId == userData.openId);
                    if (user == null)
                    {
                        user = new TUser
                        {
                            OpenId = userData.openId,
                            NickName = userData.nickName,
                            Gender = userData.gender,
                            City = userData.city,
                            Province = userData.province,
                            Country = userData.country,
                            AvatarUrl = userData.avatarUrl,
                            UnionId = userData.unionId
                        };
                        await context.Users.AddAsync(user);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(userData.openId) && user.OpenId != userData.openId)
                            user.OpenId = userData.openId;
                        if (string.IsNullOrWhiteSpace(userData.nickName) && user.NickName != userData.nickName)
                            user.NickName = userData.nickName;
                        if (user.Gender != userData.gender)
                            user.Gender = userData.gender;
                        if (string.IsNullOrWhiteSpace(userData.city) && user.City != userData.city)
                            user.City = userData.city;
                        if (string.IsNullOrWhiteSpace(userData.province) && user.Province != userData.province)
                            user.Province = userData.province;
                        if (string.IsNullOrWhiteSpace(userData.country) && user.Country != userData.country)
                            user.Country = userData.country;
                        if (string.IsNullOrWhiteSpace(userData.avatarUrl) && user.AvatarUrl != userData.avatarUrl)
                            user.AvatarUrl = userData.avatarUrl;
                        if (string.IsNullOrWhiteSpace(userData.unionId) && user.UnionId != userData.unionId)
                            user.UnionId = userData.unionId;
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "用户数据对比失败");
            }
        }



        /// <summary>
        /// 插入用户数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task InsertUser(TUser user)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "插入用户失败");
            }
        }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public async Task DeleteUserById(int id)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    TUser user = await context.Users.FirstOrDefaultAsync(o => o.Id == id);
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "删除用户信息失败");
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateUser(TUser user)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "更新用户信息失败");
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TUser> QueryUserById(int id)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    return await context.Users.FirstOrDefaultAsync(o => o.Id == id);
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "查询用户数据失败");
            }
        }
    }
}
