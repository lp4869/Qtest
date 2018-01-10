using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadra_Technology.Service.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string DepartmentIcon { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
