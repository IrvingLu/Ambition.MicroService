using NMS.Patient.Domain.Patient.Events;
using Shared.Domain.Abstractions;

namespace NMS.Patient.Domain.Patient
{
    /// <summary>
    /// 功能描述    ：患者 
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2021/09/03 13:48:53 
    /// 最后修改者  ：鲁岩奇
    /// 最后修改日期：2021/09/03 13:48:53 
    /// </summary>
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
