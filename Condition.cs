using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTradingSystem
{
    public class Condition
    {
        public int Index;
        public string Name;
        public Condition(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}
