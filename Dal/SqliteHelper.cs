using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Dal
{
    public class SqliteHelper
    {
        //连接字符串
        private static readonly string _SqlConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;


        public static List<Model.ManagerInfo> SqliteDataList(string sql)
        {
            //考虑到GC问题，这个是必须要关闭的，其他的是在托管资源所以是要不需要的
            using (System.Data.SQLite.SQLiteConnection conn=new System.Data.SQLite.SQLiteConnection(_SqlConn))
            {
                SQLiteCommand com = new SQLiteCommand(sql,conn);
                conn.Open();
                SQLiteDataReader reader= com.ExecuteReader();
                List<Model.ManagerInfo> managerInfos = new List<Model.ManagerInfo>();
                while(reader.Read())
                {
                    managerInfos.Add(new Model.ManagerInfo() {
                        MId = Convert.ToInt32(reader["MId"]),
                        MName = Convert.ToString(reader["MName"]),
                        MPwd = Convert.ToString(reader["MPwd"]),
                        MType=Convert.ToInt32(reader["MType"])
                    });
                }
                return managerInfos;
            }
                
        }
        public static DataTable GetList(string sql)
        {
            //构建连接对象
            using (SQLiteConnection conn=new SQLiteConnection(_SqlConn))
            {
                //SQLiteCommand comm = new SQLiteCommand(sql,conn);
                //conn.Open();
                //构建桥接器
                SQLiteDataAdapter dap = new SQLiteDataAdapter(sql,conn);
                //构建表对象
                DataTable dt = new DataTable();
                dap.Fill(dt);
                return dt;
            }
                
        }
        public static int ExecuteNonQuery(string sql,params SQLiteParameter[] parm)
        {
            using(SQLiteConnection conn=new SQLiteConnection(_SqlConn))
            {
                SQLiteCommand comm = new SQLiteCommand(sql,conn);
                comm.Parameters.AddRange(parm);
                conn.Open();
                int execResult = comm.ExecuteNonQuery();
                return execResult;
            }
        }
    }
}
