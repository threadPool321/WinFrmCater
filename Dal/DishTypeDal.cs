using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    //菜品
   public class DishTypeDal
    {
        public List<Model.DishTypeModel> GetListDishType()
        {
            string sql = "select * from DishTypeInfo dti where dti.DIsDelete=0;";
            System.Data.DataTable dt = SqliteHelper.GetList(sql);
            List<Model.DishTypeModel> list = new List<Model.DishTypeModel>();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(new Model.DishTypeModel() {
                    Did = Convert.ToInt32(item["DId"]),
                    DTitle = Convert.ToString(item["DTitle"])
                });
            }
            return list;
        }
        public int Add(Model.DishTypeModel disObj)
        {
            string sql = "insert into DishTypeInfo (DTitle,DIsDelete) VALUES(@title,0);";
            List<System.Data.SQLite.SQLiteParameter> paraList = new List<System.Data.SQLite.SQLiteParameter>();
            paraList.Add(new System.Data.SQLite.SQLiteParameter("@title",disObj.DTitle));
            return SqliteHelper.ExecuteNonQuery(sql, paraList.ToArray());
        }
        public int Update(Model.DishTypeModel disObj)
        {
            string sql = "update DishTypeInfo set DTitle=@title where DId=@id";
            List<System.Data.SQLite.SQLiteParameter> paraList = new List<System.Data.SQLite.SQLiteParameter>();
            paraList.Add(new System.Data.SQLite.SQLiteParameter("@title",disObj.DTitle));
            paraList.Add(new System.Data.SQLite.SQLiteParameter("@id",disObj.Did));
            return SqliteHelper.ExecuteNonQuery(sql, paraList.ToArray());

        }
        public int Delete(int id)
        {
            string sql = "delete from DishTypeInfo where DId=@id;";
            System.Data.SQLite.SQLiteParameter para = new System.Data.SQLite.SQLiteParameter("@id",id);
            return SqliteHelper.ExecuteNonQuery(sql, para);
        }
    }
}
