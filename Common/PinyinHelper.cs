using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;

namespace Common
{
   public class PinyinHelper
    {
        public static string GetPinYin(string txt)
        {
            StringBuilder sb =new StringBuilder();
            foreach (char item in txt)
            {
                if (ChineseChar.IsValidChar(item))
                {
                    ChineseChar c = new ChineseChar(item);
                    sb.Append(c.Pinyins[0][0]);
                }
                else
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }
    }
}
