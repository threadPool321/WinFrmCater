using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class HillInfoBll
    {
        private Dal.HillInfoDal dal = new Dal.HillInfoDal();
        public List<Model.HallInfo> GetDishInfos()
        {
            return dal.GetDishInfos();
        }
        public bool Add(Model.HallInfo hi)
        {
            
            
            return dal.Add(hi)>0;
        }
        public bool Update(Model.HallInfo hi)
        {
           
            return dal.Update(hi)>0;
        }
        public bool Delete(int id)
        {
            
            return dal.Delete(id)>0;
        }
    }
}
