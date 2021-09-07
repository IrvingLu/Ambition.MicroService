using Aliyun.OSS;
using NMS.File.Web.Configuration;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace NMS.File.Web.Application
{
    public class OssService : IOssService
    {
        private readonly OssClient _ossClient;
        private readonly OssClientConfig _ossClientConfig;
        public OssService(OssClientConfig ossClientConfig)
        {
            _ossClientConfig = ossClientConfig;
            _ossClient = new OssClient(_ossClientConfig.EndPoint, _ossClientConfig.AccessKeyId, _ossClientConfig.AccessKeySecret);
        }
        public async Task<string> UploadAsync(byte[] data, string fileName, UploadType type)
        {
            using var requestContent = new MemoryStream(data);
            string filepath = "";
            switch (type)
            {
                case UploadType.avatar:
                    ///用户头像
                    filepath = $"user/avatar/{fileName}";
                    _ossClient.PutObject(_ossClientConfig.BucketName, filepath, requestContent);
                    break;
                default:
                    ///用户头像
                    filepath = $"other/{fileName}";
                    _ossClient.PutObject(_ossClientConfig.BucketName, filepath, requestContent);
                    break;
            }
            return await Task.FromResult("/" + filepath);

        }

    }

    public enum UploadType
    {
        [Description("用户头像")]
        avatar

    }
}
