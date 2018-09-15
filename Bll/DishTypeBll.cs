using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
   public class DishTypeBll
    {
        private Dal.DishTypeDal dal = new Dal.DishTypeDal();
        public List<Model.DishTypeModel> GetDishTypes()
        {
            if(dal.GetListDishType().Count>0)
            {
                return dal.GetListDishType();
            }
            return null;
        }
        public bool Add(Model.DishTypeModel obj)
        {
            return dal.Add(obj) > 0;
        }
        public bool update(Model.DishTypeModel obj)
        {
            return dal.Update(obj)>0;
        }
        public bool Delete(int id)
        {
            return dal.Delete(id)>0;
        }
    }
}
