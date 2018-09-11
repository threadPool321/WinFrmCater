using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    //会员类型
    public class MemberTypeInfoBll
    {
        private Dal.MemberInfoDal dal = new Dal.MemberInfoDal();
        public List<Model.MemberTypeInfo> GetMemberTypeInfos()
        {
            return dal.GetMemberTypeInfos();
        }
        public bool Insert(Model.MemberTypeInfo mtf)
        {
            return dal.Insert(mtf)>0;
        }
        public bool Update(Model.MemberTypeInfo mtf)
        {
            return dal.Update(mtf) > 0;
        }
        public bool Delete(int id)
        {
            return dal.Delete(id)>0;
        }
    }
}
