using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class TableState
    {
        public TableState(string index,string name)
        {
            StateId = index;
            StateName = name;     
        }

        public string StateId { get; set; }
        public string StateName { get; set; }

    }
}
