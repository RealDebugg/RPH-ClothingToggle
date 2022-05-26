using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outfitToggler
{
    internal class Bags
    {
        public Dictionary<int, int> male { get; private set; } = new Dictionary<int, int>() 
        {
            {45, 44},
            {41, 40},
        };

        public Dictionary<int, int> female { get; private set; } = new Dictionary<int, int>()
        {
            {45, 44},
            {41, 40},
        };
    }
}
