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
            string sql = "select ti.*,hi.HTitle from TableInfo ti inner JOIN HallInfo hi on hi.Hid=ti.Thallid  where TIsDelete=0;";
            DataTable table = SqliteHelper.GetList(sql);


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
        //public Add(Model.TableInfo)
        //{
        //    string sql = "insert into TableInfo() values()";
        //}
    }
}
