﻿/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：实体Dto对象
*使用说明    ：需要用到Id的DTO对象
***********************************************************************/

using System;

namespace Shared.Infrastructure.Core.BaseDto
{
    public  class EntityDto
    {
        public Guid Id { get; set; }
    }
}
