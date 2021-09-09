/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：内部异常
*使用说明    ：内部异常
***********************************************************************/

using System;

namespace Shared.Infrastructure.Core.Core
{
    public  class InternalException: Exception
    {
        public InternalException(string msg):base (msg)
        {

        }
    }
}
