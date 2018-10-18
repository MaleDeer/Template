using System.Collections.Generic;

namespace Model.ResultEntitys
{
    /// <summary>
    /// 分页时返回的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class R_DataPaging<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总的页面数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 总的记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public List<T> ResultData { get; set; }
    }
}
