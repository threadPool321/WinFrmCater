using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
  public  class MemberBll
    {
        Dal.MemberDal dal = new Dal.MemberDal();
        public List<Model.MemberInfo> GetList(Model.MemberInfo member)
        {
            return dal.GetList(member);
        }
        public bool Add(Model.MemberInfo obj)
        {
            return dal.Add(obj) > 0;
        }
        public bool Update(Model.MemberInfo obj)
        {
            return dal.Update(obj)>0;
        }
        public bool Delete(int id)
        {
            return dal.Delete(id)>0;
        }
    }
}
