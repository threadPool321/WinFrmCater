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
            return dal.InsertOrderInfo(id) > 0;
        }
        public int GetOrderIdByTid(int id)
        {
            return dal.GetOrderIdByTid(id);
        }
        public bool OrderDish(int orderId, int dishId)
        {
            return dal.OrderDish(orderId, dishId) > 0;
        }
        public List<Model.OrderDetailInfo> GetOrderDetailInfos(int id)
        {
            return dal.GetOrderDetailInfos(id);
        }
        public bool UpdateCountOrder(int orderId, int count)
        {
            return dal.UpdateCountOrder(orderId, count) > 0;
        }
    }
}
