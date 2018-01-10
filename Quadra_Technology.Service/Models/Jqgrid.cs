using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadra_Technology.Service.Models
{
    public class JqGridResult<T>
    {
        /// <summary>
        /// list of data
        /// </summary>
        public IEnumerable<T> rows { get; set; }

        /// <summary>
        /// current page
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Total page
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// no# of records founds
        /// </summary>
        public int records { get; set; }
    }
    public class Jqgrid
    {

    }
    public class datatable
    {
        public dataClass setdata { get; set; }
    }
    public class dataClass
    {
        public List<StaffModel> data { get; set; }
    }
}
