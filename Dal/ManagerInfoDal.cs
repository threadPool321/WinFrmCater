using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Dal
{
    public class ManagerInfoDal
    {
        public List<ManagerInfo> GetList()
        {
            //执行查询，获取数据
            DataTable table = SqliteHelper.GetList("select * from managerInfo");
            //构建集合对象
            List<ManagerInfo> list = new List<ManagerInfo>();
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
    }
}
