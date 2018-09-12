using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class MemberDal
    {
        /// <summary>
        /// 查询会员
        /// </summary>
        /// <returns></returns>
        public List<Model.MemberInfo> GetList(Model.MemberInfo manager)
        {
            string sql = "SELECT mi.*,mti.MTitle FROM MemberInfo mi inner JOIN MemberTypeInfo mti ON mi.MTypeId=mti.MId where mi.MIsDelete=0 ";
            List<System.Data.SQLite.SQLiteParameter> para = new List<System.Data.SQLite.SQLiteParameter>();
            if(!string.IsNullOrEmpty(manager.MName))
            {
                sql += " and mi.MName like @names ";
                para.Add(new System.Data.SQLite.SQLiteParameter("@names", "%" + manager.MName + "%"));
            }
            if(!string.IsNullOrEmpty(manager.MPhone))
            {
                sql += " and mi.MPhone like @phone ";
                para.Add(new System.Data.SQLite.SQLiteParameter("@phone","%"+manager.MPhone+"%"));
            }
            System.Data.DataTable table = SqliteHelper.GetList(sql,para.ToArray());
            List<Model.MemberInfo> list = new List<Model.MemberInfo>();
            foreach (System.Data.DataRow item in table.Rows)
            {
                list.Add(new Model.MemberInfo()
                {
                    MId = Convert.ToInt32(item["MId"]),
                    MName = Convert.ToString(item["MName"]),
                    TypeTitle = Convert.ToString(item["MTitle"]),
                    MMoney = Convert.ToDecimal(item["MMoney"]),
                    MPhone = Convert.ToString(item["MPhone"])
                });
            }
            return list;
        }
        public int Add(Model.MemberInfo obj)
        {
            string sql = "insert into MemberInfo(MTypeId,MName,MPhone,MMoney,MIsDelete) values(@type,@name,@phone,@money,0);";
            List<System.Data.SQLite.SQLiteParameter> parList = new List<System.Data.SQLite.SQLiteParameter>();
            if(obj!=null)
            {
                parList.Add(new System.Data.SQLite.SQLiteParameter("@type",obj.MTypeId));
                parList.Add(new System.Data.SQLite.SQLiteParameter("@name", obj.MName));
                parList.Add(new System.Data.SQLite.SQLiteParameter("@phone",obj.MPhone));
                parList.Add(new System.Data.SQLite.SQLiteParameter("money", obj.MMoney));
                
            }
            return SqliteHelper.ExecuteNonQuery(sql, parList.ToArray());
        }
        public int Update(Model.MemberInfo obj)
        {
            string sql = "UPDATE MemberInfo set Mtypeid=@typeId,MName=@name,MPhone=@phone,MMoney=@money where MId=@id; ";
            List<System.Data.SQLite.SQLiteParameter> listPara = new List<System.Data.SQLite.SQLiteParameter>();
            if(obj!=null)
            {
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@typeId",obj.MTypeId));
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@name",obj.MName));
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@phone",obj.MPhone));
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@money",obj.MMoney));
                listPara.Add(new System.Data.SQLite.SQLiteParameter("@id",obj.MId));
            }
            return SqliteHelper.ExecuteNonQuery(sql, listPara.ToArray());
        }
        public int Delete(int id)
        {
            string sql = "UPDATE MemberInfo set MIsDelete=1 where Mid=@id;";
            System.Data.SQLite.SQLiteParameter par = new System.Data.SQLite.SQLiteParameter("@id",id);
            return SqliteHelper.ExecuteNonQuery(sql, par);
        }
    }
}
