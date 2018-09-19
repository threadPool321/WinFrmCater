using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
   public class OrderInfoBll
    {
        Dal.OrderInfoDal dal = new Dal.OrderInfoDal();
        public bool InsertOrder(int id)
        {
            return dal.InsertOrderInfo(id)>0;
        }
    }
}
