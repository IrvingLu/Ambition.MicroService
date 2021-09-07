using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace Shared.Infrastructure.Core.Tools
{
    public static class Helper
    {
        /// <summary>
        /// 获取对象属性名称和值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>用&拼接的结果</returns>
        public static string GetPropertyNameAndValue(object obj)
        {
            if (obj == null) { return string.Empty; }
            StringBuilder result = new();
            try
            {
                PropertyInfo[] propertys = obj.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    object valueObj = pi.GetValue(obj, null);
                    if (valueObj == null) { continue; }
                    Type valueType = valueObj.GetType();
                    if (valueType != typeof(object) && Type.GetTypeCode(valueType) == TypeCode.Object)//判断是自定义类
                    {
                        PropertyInfo[] classProperty = valueType.GetProperties();
                        foreach (PropertyInfo property in classProperty)
                        {
                            object childValueObj = property.GetValue(valueObj, null);
                            if (childValueObj == null) { continue; }
                            result.Append(pi.Name + "." + property.Name + "=" + childValueObj.ToString() + "&");
                        }
                    }
                    else
                    {
                        result.Append(pi.Name + "=" + valueObj.ToString() + "&");
                    }
                }
            }
            catch (Exception)
            {
            }
            return result.ToString().TrimEnd('&');
        }
        /// <summary>
        /// 返回隐藏中间的字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>输出</returns>
        public static string GetAnonymousString(string input)
        {
            string output;
            switch (input.Length)
            {
                case 1:
                    output = "*";
                    break;
                case 2:
                    output = input[0] + "*";
                    break;
                case 0:
                    output = "";
                    break;
                default:
                    output = input.Substring(0, 1);
                    for (int i = 0; i < input.Length - 2; i++)
                    {
                        output += "*";
                    }
                    output += input.Substring(input.Length - 1, 1);
                    break;
            }
            return output;
        }


        /// <summary>
        /// 验证字符串是否为有效的IP地址
        /// </summary>
        /// <param name="ipAddress">IPAddress to verify</param>
        /// <returns>true if the string is a valid IpAddress and false if it's not</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out IPAddress _);
        }
        /// <summary>
        /// 生成随机数字代码
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }
        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            ASCIIEncoding ascii = new();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 算年龄
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            var age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age))
                age--;
            return age;
        }


    }
}
