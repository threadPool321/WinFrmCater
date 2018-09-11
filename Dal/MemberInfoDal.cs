using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class MemberInfoDal
    {
        /// <summary>
        /// 获取会员类型列表
        /// </summary>
        /// <returns></returns>
        public List<Model.MemberTypeInfo> GetMemberTypeInfos()
        {
            List<Model.MemberTypeInfo> list = new List<Model.MemberTypeInfo>();
            string strSql = "select * from MemberTypeInfo;";
            DataTable table = SqliteHelper.GetList(strSql);
            foreach (DataRow item in table.Rows)
            {
                list.Add(new Model.MemberTypeInfo() {
                    Mid = Convert.ToInt32(item["MId"]),
                    MTitle = Convert.ToString(item["MTitle"]),
                    MDiscount = Convert.ToDecimal(item["MDiscount"])
                });
            }
            return list;
        }
        public int Insert(Model.MemberTypeInfo mtf)
        {
            string sql = "INSERT into MemberTypeInfo(MTitle,MDiscount,MIsDelete) values(@title,@discount,0)";
            List<System.Data.SQLite.SQLiteParameter> listPar = new List<System.Data.SQLite.SQLiteParameter>();
            if(mtf!=null)
            {
                listPar.Add(new System.Data.SQLite.SQLiteParameter("@title",mtf.MTitle));
                listPar.Add(new System.Data.SQLite.SQLiteParameter("@discount",mtf.MDiscount));
            }
            return SqliteHelper.ExecuteNonQuery(sql, listPar.ToArray());
        }
        public int Update(Model.MemberTypeInfo mtf)
        {
            string sql = "update MemberTypeInfo  set Mtitle=@title,Mdiscount=@discount where Mid=@id;";
            List<System.Data.SQLite.SQLiteParameter> listPara = new List<System.Data.SQLite.SQLiteParameter>();
            if(mtf!=null)
            {
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@title",mtf.MTitle));
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@discount",mtf.MDiscount));
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@id", mtf.Mid));
            }
            return SqliteHelper.ExecuteNonQuery(sql,listPara.ToArray());
        }
        public int Delete(int id)
        {
            string str = "update MemberTypeInfo  set DIsdelete=1where Mid=@id;";
            System.Data.SQLite.SQLiteParameter[] para = { new System.Data.SQLite.SQLiteParameter("@id", id) };
            return SqliteHelper.ExecuteNonQuery(str,para);
        }
    }
}
