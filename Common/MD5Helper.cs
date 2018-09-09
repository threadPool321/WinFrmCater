using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MD5Helper
    {
        public static string GetMD5Str(string txt)
        {
            //创建md5对象
            MD5 md5 = MD5.Create();
            byte[] strByte = Encoding.UTF8.GetBytes(txt);   //这里一定要用utf-8编码方式
            byte[] newStr = md5.ComputeHash(strByte);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < newStr.Length; i++)
            {
                sb.Append(newStr[i].ToString("X2").ToLower());   //转成16进制
            }
            return sb.ToString();
        }
    }
}
