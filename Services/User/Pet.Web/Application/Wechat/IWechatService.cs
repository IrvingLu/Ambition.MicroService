using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System.Threading.Tasks;

namespace Pet.User.Web.Application.Wechat
{
    public interface IWechatService
    {
        Task<OAuthAccessTokenResult> GetOpenInfoAsync(string code);
        string GetPhoneNumber(string encryptedData, string IV, string Session_key);
    }
}
