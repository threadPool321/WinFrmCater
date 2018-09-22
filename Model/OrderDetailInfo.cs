using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderDetailInfo
    {
        //主键
        public int Oid { get; set; }
        //属于哪一个订单的
        public int OrderId { get; set; }
        //菜品的ID
        public int DishId { get; set; }
        //菜品的数量
        public int Count { get; set; }


        //菜品的名称
        public string DName { get; set; }
        //菜品的单价
        public decimal Dprice { get; set; }
    }
}
