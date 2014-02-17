using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Common
{
    public class LstSort
    {
        //大きい順ソート
        public static int intToIntDesc(
          KeyValuePair<int, int> kvp1,
          KeyValuePair<int, int> kvp2)
        {
            return kvp2.Value - kvp1.Value;
        }
        //小さい順ソート
        public static int intToIntAsc(
          KeyValuePair<int, int> kvp1,
          KeyValuePair<int, int> kvp2)
        {
            return kvp1.Value - kvp2.Value;
        }
    }
}
