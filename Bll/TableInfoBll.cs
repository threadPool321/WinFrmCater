using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class TableInfoBll
    {
        private Dal.TableInfoDal dal = new Dal.TableInfoDal();
        public List<Model.TableInfo> GetTableInfos(Model.TableInfo table)
        {
            return dal.GetTableInfos(table);
        }
        public bool Add(Model.TableInfo table)
        {


            return dal.Add(table) > 0;

        }
        public bool Update(Model.TableInfo table)
        {

            return dal.Update(table)>0;
        }
        public bool Delete(int id)
        {

            return dal.Delete(id)>0;
        }
    }
}
