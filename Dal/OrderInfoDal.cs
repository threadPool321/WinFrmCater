using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class OrderInfoDal
    {
        /// <summary>
        /// 开单进行
        /// </summary>
        /// <returns></returns>
        public int InsertOrderInfo(int id)
        {
            string sql = "insert into OrderInfo (ODate,IsPay,Tableid) values(datetime('now', 'localtime'),0,@id);" +
                "update tableinfo set tIsFree=0 where tid=@id";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",id));
            return SqliteHelper.ExecuteNonQuery(sql,listPar.ToArray());
        }
        public int GetOrderIdByTid(int id)
        {
            string sql = "select OId from OrderInfo where TableId=@id and isPay=0; ";
            System.Data.SQLite.SQLiteParameter par = new System.Data.SQLite.SQLiteParameter("@id",id);
            return Convert.ToInt32(SqliteHelper.ExecuteScaler(sql, par));
        }
        //进行点菜功能
        public int OrderDish(int orderId,int dishId)
        {
            //做一个判断，如果已经存在了那就在原先的基础上在加一
            string sqlSlected = "select count(*) from OrderDetailInfo where OrderId=@orderId and Dishid=@dishId;";
            List<System.Data.SQLite.SQLiteParameter> listPara = new List<System.Data.SQLite.SQLiteParameter>();
            listPara.Add(new System.Data.SQLite.SQLiteParameter("@orderId", orderId));
            listPara.Add(new System.Data.SQLite.SQLiteParameter("@dishId", dishId));
            int count = Convert.ToInt32(SqliteHelper.ExecuteScaler(sqlSlected, listPara.ToArray()));
            string sql = "";
            if(count==0)
            {
                sql = "insert into OrderDetailInfo(OrderId,Dishid,count) values(@orderId,@dishId,1);";

            }
            else
            {
                sql = "update OrderDetailInfo set count=count+1 where OrderId=@orderId and Dishid=@dishId;";
            }
            return SqliteHelper.ExecuteNonQuery(sql,listPara.ToArray());
        }
        public List<Model.OrderDetailInfo> GetOrderDetailInfos(int orderId)
        {
            string sql = "select odi.*,di.DTitle,di.Dprice from OrderDetailInfo odi left join DishInfo di on odi.DishId=di.DId where OrderId=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPara = new List<System.Data.SQLite.SQLiteParameter>();
            listPara.Add(new System.Data.SQLite.SQLiteParameter("@id",orderId));
            var table = SqliteHelper.GetList(sql, listPara.ToArray());
            List<Model.OrderDetailInfo> list =  new List<Model.OrderDetailInfo>();
            foreach (DataRow item in table.Rows)
            {
                
                list.Add(new Model.OrderDetailInfo() {
                    Oid=Convert.ToInt32(item["Oid"]),
                    OrderId=Convert.ToInt32(item["OrderId"]),
                    DishId=Convert.ToInt32(item["DishId"]),
                    Count=Convert.ToInt32(item["Count"]),
                    DName=Convert.ToString(item["DTitle"]),
                    Dprice=Convert.ToDecimal(item["Dprice"])
                });
            }
            return list;
        }
        public int UpdateCountOrder(int orderId,int count)
        {
            string sql = "UPDATE OrderDetailInfo set count=@count where oid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@count",count));
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",orderId));
            return SqliteHelper.ExecuteNonQuery(sql, listPar.ToArray());
        }
        //删除订单
        public int DeleteOrderDetail(int id)
        {
            string sql = "delete from  OrderDetailInfo where oid=@id";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",id));
            return SqliteHelper.ExecuteNonQuery(sql, listPar.ToArray());
        }
        //更新价格到订单中
        public int UpdateMoney(int oId,decimal money)
        {
            string sql = "update OrderInfo set oMoney=@money where oid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPara = new List<System.Data.SQLite.SQLiteParameter>();
            listPara.Add(new System.Data.SQLite.SQLiteParameter("@money",money));
            listPara.Add(new System.Data.SQLite.SQLiteParameter("@id",oId));
            return SqliteHelper.ExecuteNonQuery(sql,listPara.ToArray());
        }
    }
}
