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
        public decimal GetOrderMoneyByTid(int id)
        {
            return dal.GetOrderMoneyByTid(id);
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
        public bool DeleteOrderDetail(int id)
        {
            return dal.DeleteOrderDetail(id) > 0;
        }
        public bool UpdateMoney(int oid, decimal money)
        {
            return dal.UpdateMoney(oid, money) > 0;
        }
        //结账
        public bool SettleAccounts(int tableId, int memberId, decimal discount, decimal payMoney)
        {
            return dal.SettleAccounts(tableId,memberId,discount,payMoney)>0;
        }
    }
}
