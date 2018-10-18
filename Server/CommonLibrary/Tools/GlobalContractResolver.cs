using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CommonLibrary.Tools
{
    public class GlobalContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 合同解析器
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName;

            //传递给前端的数据key都转换为大写
            //return propertyName.ToUpper();
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            IsoDateTimeConverter iso = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            IList<JsonProperty> listWithConver = new List<JsonProperty>();
            foreach (var item in list)
            {
                if (item.PropertyType.ToString().Contains("System.DateTime"))
                {
                    item.Converter = iso;
                }
                item.PropertyName = item.UnderlyingName.ToUpper();
                listWithConver.Add(item);
            }
            return listWithConver;
        }
    }
}
