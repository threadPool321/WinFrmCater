using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TableInfo
    {
        public int TId { get; set; }
        public string TTitle { get; set; }
        public int THallId { get; set; }
        public int TIsFree { get; set; }
        public int TIsDelete { get; set; }
        public string TypeTitle { get; set; }
    }
}
