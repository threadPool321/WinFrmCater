using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
   public class TableInfoDal
    {
        public List<Model.TableInfo> GetTableInfos (Model.TableInfo tables)
        {
            string sql = "select ti.*,hi.HTitle from TableInfo ti inner JOIN HallInfo hi on hi.Hid=ti.Thallid  where TIsDelete=0 ";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            //根据前台设定的参数开始查询
            if(tables.THallId>0)
            {
                sql += " and ti.ThallId=@hallId ";
                listPar.Add(new System.Data.SQLite.SQLiteParameter("@hallId",tables.THallId));
            }
            if(tables.TIsFree>-1)
            {
                sql += " and ti.TIsFree=@FreeId";
                listPar.Add(new System.Data.SQLite.SQLiteParameter("@FreeId",tables.TIsFree));
            }
            DataTable table = SqliteHelper.GetList(sql,listPar.ToArray());


            List<Model.TableInfo> list = new List<Model.TableInfo>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(new Model.TableInfo() {
                    TId=Convert.ToInt32(item["TId"]),
                    TTitle=Convert.ToString(item["TTitle"]),
                    THallId=Convert.ToInt32(item["THallid"]),
                    TIsFree=Convert.ToInt32(item["TIsFree"]),
                    TIsDelete=Convert.ToInt32(item["TIsDelete"]),
                    TypeTitle=Convert.ToString(item["HTitle"])
                });
            }
            return list;
        }
        public int Add(Model.TableInfo table)
        {
            string sql = "insert into TableInfo(TTitle,ThallId,TisFree,TisDelete) values(@title,@hallid,@isFree,0)";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@title",table.TTitle));
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@hallid",table.THallId));
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@isFree", table.TIsFree));   

            return SqliteHelper.ExecuteNonQuery(sql,listPar.ToArray());

        }
        public int Update(Model.TableInfo table)
        {
            string sql = "update TableInfo set TTitle=@title,ThallId=@hallid,TisFree=@isFree where Tid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@title", table.TTitle));
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@hallid", table.THallId));
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@isFree", table.TIsFree));            
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id",table.TId));
            return SqliteHelper.ExecuteNonQuery(sql,listPar.ToArray());
        }
        public int Delete(int id)
        {
            string sql = "update TableInfo set TIsDelete=1 where Tid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            listPar.Add(new System.Data.SQLite.SQLiteParameter("@id", id));
            return SqliteHelper.ExecuteNonQuery(sql, listPar.ToArray());
        }

    }
}
