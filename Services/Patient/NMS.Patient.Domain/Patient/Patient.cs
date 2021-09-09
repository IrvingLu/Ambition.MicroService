/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：患者实体，聚合根
*使用说明    ：患者实体
***********************************************************************/

using NMS.Patient.Domain.Patient.Events;
using Shared.Domain.Abstractions;

namespace NMS.Patient.Domain.Patient
{
    public class Patient : Entity, IAggregateRoot
    {
        /// <summary>
        /// 患者名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }

        public Patient(string name, string phone = null)
        {
            Name = name;
            Phone = phone;
            AddDomainEvent(new CreatePatientEvent(this));//举例
        }
    }
}
