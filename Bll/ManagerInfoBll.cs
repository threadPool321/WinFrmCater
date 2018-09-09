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
    }
}
