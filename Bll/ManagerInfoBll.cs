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
            return managerInfoDal.GetList(null);
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
        public bool Login(ManagerInfo mi)
        {
            var returnResult=managerInfoDal.GetList(mi);
            if(returnResult.Count>0)
            {
                return true;
            }
            return false;
        }
    }
}
