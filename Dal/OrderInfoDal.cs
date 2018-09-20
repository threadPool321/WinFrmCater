using System;
using System.Collections.Generic;
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
            string sql = "insert into OrderInfo (ODate,IsPay,Tableid) values(datetime('now', 'localtime'),0,@id);";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",id));
            return SqliteHelper.ExecuteNonQuery(sql,listPar.ToArray());
        }
        public int GetOrderIdByTid(int id)
        {
            string sql = "select OId from OrderInfo where TableId=@id ";
            System.Data.SQLite.SQLiteParameter par = new System.Data.SQLite.SQLiteParameter("@id",id);
            return Convert.ToInt32(SqliteHelper.ExecuteScaler(sql, par));
        }
    }
}
