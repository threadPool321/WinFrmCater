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
        public decimal GetOrderMoneyByTid(int id)
        {
            string sql = "select OMoney from OrderInfo where TableId=@id and isPay=0; ";
            System.Data.SQLite.SQLiteParameter par = new System.Data.SQLite.SQLiteParameter("@id",id);
            return Convert.ToDecimal(SqliteHelper.ExecuteScaler(sql,par));
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
        //开始结账功能
        public int SettleAccounts(int tableId,int memberId,decimal discount,decimal payMoney)
        {
            //根据连接对象开始事务
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();
                System.Data.SQLite.SQLiteTransaction tran= conn.BeginTransaction();
                int count = 0;
                try
                {
                    System.Data.SQLite.SQLiteCommand comm = new System.Data.SQLite.SQLiteCommand();
                    comm.Transaction = tran;

                    //1、更改订单状态,如果是会员则记录会员信息
                    string sql = "update OrderInfo  set ispay=1 ";
                    if(memberId>0)  //如果是会员则记录下来，不是会员就直接是discount=1;
                    {
                        sql += ",memberId="+memberId+",discount="+discount;
                    }
                    else
                    {
                        sql += ",discount=1";
                    }                  
                    sql += " where tableId="+tableId+";";
                    

                    comm.CommandText = sql;
                    count+=comm.ExecuteNonQuery();
                    //2.将餐桌更改为空闲 1
                    string sql2 = "update TableInfo set Tisfree=1 where tid="+tableId;
                    comm.CommandText = sql2;
                    count += comm.ExecuteNonQuery();
                    //3.如果使用余额结账，那么就要更新会员的余额
                    if(payMoney>0)
                    {
                        string sql3 = "update MemberInfo set Mmoney=Mmoney-" + payMoney + " where MId=" + memberId;
                        comm.CommandText = sql3;
                        count += comm.ExecuteNonQuery();
                    }                    
                    //执行成功后开始提交
                    tran.Commit();
                    return count;
                }
                catch (Exception)
                {
                    //执行错误后执行回滚
                    count = 0;
                    tran.Rollback();
                    return count;              
                }
            }
        }
    }
}
