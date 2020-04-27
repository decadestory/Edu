using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Lib.Collection
{
    public class ListConverter
    {
       /// <summary>
       /// 字符串传int List
       /// </summary>
       /// <param name="str"></param>
       /// <param name="splitor"></param>
       /// <returns></returns>
        public static List<int> StringToListInt(string str,char splitor)
        {
            var arr = str.Split(splitor).ToList();
            var res = new List<int>();
            arr.ForEach(t=>res.Add(Convert.ToInt32(t)));
            return res;
        }
    }
}
