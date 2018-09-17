using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
  public  class HillInfoDal
    {
        public List<Model.HallInfo> GetDishInfos()
        {
            string sql = "select * from HallInfo where HIsDelete=0";
            List<Model.HallInfo> hallInfos = new List<Model.HallInfo>();
            DataTable table = SqliteHelper.GetList(sql);
            foreach (DataRow item in table.Rows)
            {
                hallInfos.Add(new Model.HallInfo()
                {
                    Hid=Convert.ToInt32(item["HId"]),
                    HTitle=Convert.ToString(item["HTitle"])
                });
            }
            return hallInfos;
        }
        public int Add(Model.HallInfo hi)
        {
            string sql = "insert into HallInfo(HTitle,HisDelete) values(@title,0);";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@title",hi.HTitle));
            return SqliteHelper.ExecuteNonQuery(sql, listPar.ToArray());
        }
        public int Update(Model.HallInfo hi)
        {
            string sql = "update HallInfo set Htitle=@title where Hid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@title", hi.HTitle));
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",hi.Hid));
            return SqliteHelper.ExecuteNonQuery(sql, listPar.ToArray());
        }
        public int Delete(int id)
        {
            string sql = "update HallInfo set HisDelete=1 where Hid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",id));
            return SqliteHelper.ExecuteNonQuery(sql,listPar.ToArray());
        }
    }
}
