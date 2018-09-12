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
