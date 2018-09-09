using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class ManagerInfoBll
    {
        ManagerInfoDal managerInfoDal = new ManagerInfoDal();
        public List<ManagerInfo> GetManagerInfos()
        {
            return managerInfoDal.GetList();
        }
        public bool InserData(ManagerInfo managerInfo)
        {
            return managerInfoDal.InserData(managerInfo)>0;
        }
        public bool Delete(int id)
        {
            return managerInfoDal.Delete(id)>0;
        }
        public bool Update(ManagerInfo mi)
        {
            return managerInfoDal.Update(mi) > 0;
        }
    }
}
