using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pet.User.Web.Application.Wechat
{
    /// <summary>
    /// 功能描述    ：微信相关服务
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/29 15:20:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/29 15:20:23 
    /// </summary>
    public class WechatService : IWechatService
    {
        private readonly IConfiguration _configuration;
        private readonly string appId;
        private readonly string secret;
        public WechatService(IConfiguration configuration)
        {
            _configuration = configuration;
            appId = _configuration.GetSection("ApplicationConfiguration").GetSection("WechatConfig").GetSection("AppId").Value;
            secret = _configuration.GetSection("ApplicationConfiguration").GetSection("WechatConfig").GetSection("AppSecret").Value;
        }
        /// <summary>
        /// AES解密：从小程序中 getPhoneNumber 返回值中，解析手机号码
        /// </summary>
        /// <param name="encryptedData">包括敏感数据在内的完整用户信息的加密数据，详细见加密数据解密算法</param>
        /// <param name="IV">加密算法的初始向量</param>
        /// <param name="Session_key"></param>
        /// <returns>手机号码</returns>
        public string GetPhoneNumber(string encryptedData, string IV, string Session_key)
        {
           
            var encryptedDatas = Convert.FromBase64String(encryptedData); // strToToHexByte(text);
            RijndaelManaged rijndaelCipher = new RijndaelManaged
            {
                Key = Convert.FromBase64String(Session_key), // Encoding.UTF8.GetBytes(AesKey);
                IV = Convert.FromBase64String(IV),// Encoding.UTF8.GetBytes(AesIV);
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedDatas, 0, encryptedDatas.Length);
            string results = Encoding.Default.GetString(plainText);
            //序列化获取手机号码
            dynamic model = JToken.Parse(results) as dynamic;
            return model.phoneNumber;
        }
        /// <summary>
        /// 获取小程序基础信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<OAuthAccessTokenResult> GetOpenInfoAsync(string code)
        {
            var result = await OAuthApi.GetAccessTokenAsync(appId, secret, code);
            return result;
        }

    }
}
