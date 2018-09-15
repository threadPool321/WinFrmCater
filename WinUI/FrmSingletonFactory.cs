using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI
{
    public class FrmSingletonFactory
    {
        private static WinUI.MemberTypeInfo memberTypeObj = null;
        private static WinUI.MemberInfoList memberInfoOjb = null;
        private static WinUI.ManagerInfoList managetListObj = null;
        private static WinUI.DishInfo dishObj = null;
        public static DishInfo CreateInstacne()
        {
            if(dishObj==null||dishObj.IsDisposed)
            {
                dishObj = new DishInfo();
            }

            return dishObj;
        }
        public static ManagerInfoList CreateInstance()
        {
            if(managetListObj==null||managetListObj.IsDisposed)
            {
                managetListObj = new ManagerInfoList();
            }
            return managetListObj;
        }
        public static MemberTypeInfo CreateMemberTypeInstance()
        {
            if(memberTypeObj==null|| memberTypeObj.IsDisposed)
            {
                memberTypeObj = new MemberTypeInfo();
            }
            return memberTypeObj;
        }
        public static MemberInfoList CreateMemberInstance()
        {
            if (memberInfoOjb == null||memberInfoOjb.IsDisposed)
            {
                memberInfoOjb = new MemberInfoList();
            }
            return memberInfoOjb;
        }
    }
}
