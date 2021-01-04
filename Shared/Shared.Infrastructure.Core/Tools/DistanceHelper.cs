using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Tools
{
    /// <summary>
    /// 功能描述    ：计算两点间的距离  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/31 15:12:26 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/31 15:12:26 
    /// </summary>
    public class DistanceHelper
    {
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            var a = new Coordinate(lat1, lon1);
            var b = new Coordinate(lat2, lon2);
            if (lat1 == lat2 && lon1 == lon2)
            {
                return 0;
            }

            a.lon = VC(a.lon, -180, 180);
            a.lat = aD(a.lat, -74, 74);
            b.lon = VC(b.lon, -180, 180);
            b.lat = aD(b.lat, -74, 74);
            double ret = JF(JK(a.lon), JK(b.lon), JK(a.lat), JK(b.lat));
            return ret;
        }
        public static double aD(double a, double b, double c)
        {
            if (b != null)
            {
                a = Math.Max(a, b);
            }
            if (b != null)
            {
                a = Math.Min(a, c);
            }
            return a;
        }
        public static double VC(double a, double b, double c)
        {
            if (a > c)
            {
                a -= c - b;
            }
            if (a < b)
            {
                a += c - b;
            }
            return a;
        }
        public static double JK(double a)
        {
            double ret = Math.PI * a / 180.0;
            return ret;
        }
        public static double JF(double a, double b, double c, double e)
        {
            const double Ou = 6370996.81;
            return Ou * Math.Acos(Math.Sin(c) * Math.Sin(e) + Math.Cos(c) * Math.Cos(e) * Math.Cos(b - a));
        }

        public class Coordinate
        {
            public Coordinate(double lat, double lon)
            {
                this.lat = lat;
                this.lon = lon;
            }
            /// <summary>
            /// 纬度
            /// </summary>
            public double lat { get; set; }
            /// <summary>
            /// 经度
            /// </summary>
            public double lon { get; set; }
        }

    }
}
