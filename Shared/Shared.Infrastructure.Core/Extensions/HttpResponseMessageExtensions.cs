/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：HttpResponseMessageExtensions
*使用说明    ：HttpResponseMessageExtensions
***********************************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// 将结果转为json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        public async static Task<T> AsJson<T>(this HttpResponseMessage httpResponseMessage)
        {
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 将结果转为json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        public async static Task<JObject> AsJson(this HttpResponseMessage httpResponseMessage)
        {
            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            return JObject.Parse(json);
        }
    }
}
