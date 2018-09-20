using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DishInfoDal
    {
        public List<Model.DishInfo> GetDishInfos(Model.DishInfo obj)
        {
            string sql = "SELECT di.*,dti.DTitle Tname from DishInfo di INNER JOIN DishTypeInfo dti on di.DTypeId=dti.DId where di.DIsDelete=0 ";
            List<System.Data.SQLite.SQLiteParameter> listPara = new List<System.Data.SQLite.SQLiteParameter>();
            if (!string.IsNullOrEmpty(obj.DTitle))
            {
                sql += "  and di.DTitle like @name";
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@name", "%" + obj.DTitle + "%"));
            }
            if (obj.DTypeId > 0)
            {
                sql += "  and di.DtypeId like @id";
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@id","%"+obj.DTypeId+"%"));            
            }
            if(!string.IsNullOrEmpty(obj.DChar))
            {
                sql += " and di.Dchar like @char";
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@char","%"+obj.DChar+"%"));
            }
            DataTable table = SqliteHelper.GetList(sql,listPara.ToArray());
            List<Model.DishInfo> dish = new List<Model.DishInfo>(); 
            foreach (DataRow item in table.Rows)
            {
                dish.Add(new Model.DishInfo() {
                    DId=Convert.ToInt32(item["DId"]),
                    DTitle=Convert.ToString(item["DTitle"]),
                    DPrice=Convert.ToDecimal(item["DPrice"]),
                    DChar=Convert.ToString(item["DChar"]),
                    DTypeName=Convert.ToString(item["Tname"])
                });
            }
            return dish;
        }
        public int Add(Model.DishInfo di)
        {
            string sql = "insert into dishinfo(dtitle,dprice,dtypeid,dchar,dIsDelete) values(@title,@price,@tid,@char,0)";
            List<System.Data.SQLite.SQLiteParameter> lisrPar = new List<System.Data.SQLite.SQLiteParameter>();
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@title",di.DTitle));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@price", di.DPrice));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@tid", di.DTypeId));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@char", di.DChar));
            return SqliteHelper.ExecuteNonQuery(sql, lisrPar.ToArray());
        }
        public int Update(Model.DishInfo di)
        {
            string sql = "update dishinfo set dtitle=@title,dprice=@price,dchar=@char,dtypeid=@tid where did=@id";
            List<System.Data.SQLite.SQLiteParameter> lisrPar = new List<System.Data.SQLite.SQLiteParameter>();
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@title", di.DTitle));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@price", di.DPrice));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@tid", di.DTypeId));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@char", di.DChar));
            lisrPar.Add(new System.Data.SQLite.SQLiteParameter("@id",di.DId));
            return SqliteHelper.ExecuteNonQuery(sql, lisrPar.ToArray());

        }
        public int Delete(int id)
        {
            string sql = "update dishinfo set dIsDelete=1 where did=@id";
            List<System.Data.SQLite.SQLiteParameter> list = new List<System.Data.SQLite.SQLiteParameter>()
            {
                new System.Data.SQLite.SQLiteParameter("@id",id)
            };
            return SqliteHelper.ExecuteNonQuery(sql, list.ToArray());
        }
    }
}
