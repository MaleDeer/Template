using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using Model.ResultEntitys;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 用户密码服务
    /// </summary>
    public class UserPassWordService : BaseService
    {
        /// <summary>
        /// 插入用户密码
        /// </summary>
        /// <param name="userPassWord"></param>
        /// <returns></returns>
        public async Task InsertUserPassWord(TUserPassWord userPassWord)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    await context.UserPassWords.AddAsync(userPassWord);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "插入用户密码失败");
            }
        }

        /// <summary>
        /// 删除用户密码
        /// </summary>
        /// <param name="id">用户密码Id</param>
        /// <returns></returns>
        public async Task DeleteUserPassWordById(int id)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    TUserPassWord userPassWord = await context.UserPassWords.FirstOrDefaultAsync(o => o.Id == id);
                    if (userPassWord != null)
                    {
                        context.UserPassWords.Remove(userPassWord);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "删除用户密码失败");
            }
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userPassWord"></param>
        /// <returns></returns>
        public async Task UpdateUserPassWord(TUserPassWord userPassWord)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    context.UserPassWords.Update(userPassWord);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "更新用户密码失败");
            }
        }

        /// <summary>
        /// 查询用户密码
        /// </summary>
        /// <param name="id">用户密码Id</param>
        /// <returns></returns>
        public async Task<TUserPassWord> QueryUserPassWordById(int id)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    return await context.UserPassWords.FirstOrDefaultAsync(o => o.Id == id);
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "查询用户密码失败");
            }
        }

        /// <summary>
        /// 查询用户密码
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public async Task<R_DataPaging<TUserPassWord>> QueryUserPassWordPaging(int userId, int pageIndex, int pageSize)
        {
            try
            {
                using (TemplateContext context = new TemplateContext())
                {
                    DbSet<TUserPassWord> userPassWords = context.UserPassWords;
                    R_DataPaging<TUserPassWord> dataPaging = new R_DataPaging<TUserPassWord>()
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        TotalCount = await userPassWords.CountAsync(),
                        ResultData = await userPassWords.Where(o => o.Id == userId)
                            .OrderByDescending(o => o.Id).Skip(pageIndex - 1)
                            .Take(pageSize).ToListAsync()
                    };
                    dataPaging.TotalPage = (int)Math.Ceiling((decimal)dataPaging.TotalCount / pageSize);
                    return dataPaging;
                }
            }
            catch (Exception e)
            {
                throw ThrowException(e, "查询用户密码失败");
            }
        }
    }
}
