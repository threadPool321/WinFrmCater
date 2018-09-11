using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Dal
{
    public class ManagerInfoDal
    {
        /// <summary>
        /// 查询/单个查询，如果传进来的对象是null,那就是查询所有的
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList(ManagerInfo mi)
        {
            //构建集合对象
            List<ManagerInfo> list = new List<ManagerInfo>();
            //SQLiteParameter[] para = new SQLiteParameter[2]; 数组（固定长度）和集合（不固定长度）
            List<SQLiteParameter> listpa = new List<SQLiteParameter>();
            //查询sql 语句
            string str = "select * from managerInfo";
            if(mi!=null)
            {
                str += " where Mname=@name and Mpwd=@pwd";
                listpa.Add(new SQLiteParameter("@name", mi.MName));
                listpa.Add(new SQLiteParameter("@pwd",Common.MD5Helper.GetMD5Str(mi.MPwd)));
            }
            //执行查询，获取数据
            DataTable table = SqliteHelper.GetList(str, listpa.ToArray());
           
            //遍历数据表，将数据转存到集合中
            foreach (DataRow item in table.Rows)
            {
                list.Add(new ManagerInfo()
                {
                    MId = Convert.ToInt32(item["MId"]),
                    MName = item["MName"].ToString(),
                    MPwd = item["MPwd"].ToString(),
                    MType = Convert.ToInt32(item["MType"])
                });
            }
            return list;
        }
        public int InserData(ManagerInfo managerInfo)
        {
            //构造sql语句
            string sql = "insert into ManagerInfo(MName,MPwd,MType) VALUES(@MName,@MPwd,@MType)";
            SQLiteParameter[] sqlParameters = new SQLiteParameter[] {
                new SQLiteParameter("@MName",managerInfo.MName),
               new SQLiteParameter("@MPwd", Common.MD5Helper.GetMD5Str(managerInfo.MPwd)),
                new SQLiteParameter("@MType",managerInfo.MType)
            };
            return SqliteHelper.ExecuteNonQuery(sql, sqlParameters);
        }
        public int Delete(int id)
        {
            string sql = "DELETE from ManagerInfo where Mid=@id";
            SQLiteParameter para = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, para);
        }
        public int Update(ManagerInfo mi)
        {
            //这里参数是不固定的嗯所以就用集合来装
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            string sql = "update ManagerInfo set MName=@name, ";
            list.Add(new SQLiteParameter("@name",mi.MName));
            if (!mi.MPwd.Equals("******"))  //这里就是作者修改过密码
            {
                sql += "MPwd=@pwd, ";
                list.Add(new SQLiteParameter("@pwd",Common.MD5Helper.GetMD5Str(mi.MPwd)));
            }            
            sql += " MType=@type WHERE MId=@id";
            list.Add(new SQLiteParameter("@type",mi.MType));
            list.Add(new SQLiteParameter("@id",mi.MId));
            return SqliteHelper.ExecuteNonQuery(sql,list.ToArray());

        }

    }
}
