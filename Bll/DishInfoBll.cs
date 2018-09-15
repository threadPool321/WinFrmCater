using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class DishInfoBll
    {
        private Dal.DishInfoDal dal = new Dal.DishInfoDal();
        public List<Model.DishInfo> GetDishInfos(Model.DishInfo obj)
        {
            return dal.GetDishInfos(obj);
        }
        public bool Add(Model.DishInfo di)
        {
            return dal.Add(di) > 0;
        }

        public bool Update(Model.DishInfo di)
        {
            return dal.Update(di) > 0;

        }
        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }

    }
}
