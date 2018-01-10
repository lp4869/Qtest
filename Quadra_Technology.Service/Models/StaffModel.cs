using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadra_Technology.Service.Models
{
    public class StaffModel
    {
        [DisplayName("แผนก")]
        public string department { get; set; }
        [DisplayName("ชื่อ")]
        public string name { get; set; }
        [DisplayName("นามสกุล")]
        public string lastname { get; set; }
        [DisplayName("วันเกิด")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? birthdate { get; set; }
        public String birthdateString
        {
            get
            {
                if (birthdate != null)
                {
                    return birthdate.Value.ToShortDateString();
                }
                return String.Empty;
            }
        }
        [DisplayName("ที่อยู่")]
        public string address { get; set; }
        [DisplayName("เบอร์โทร")]
        public string phone { get; set; }
        [DisplayName("อีเมล์")]
        public string email { get; set; }
        [DisplayName("ตำแหน่ง")]
        public string position { get; set; }
        [DisplayName("วันเริ่มงาน")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? workingday { get; set; }
        public DateTime? createdOn { get; set; }
        public String workingdayString
        {
            get
            {
                if (workingday != null)
                {
                    return workingday.Value.ToShortDateString();
                }
                return String.Empty;
            }
        }
        public Guid guid { get; set; }
    }
}
