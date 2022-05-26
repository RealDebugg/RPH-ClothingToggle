using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outfitToggler
{
    internal class Visor
    {
        public Dictionary<int, int> male { get; private set; } = new Dictionary<int, int>()
        {
            {9, 10},
            {18, 67},
            {82, 67},
            {44, 45},
            {50, 68},
            {51, 69},
            {52, 70},
            {53, 71},
            {62, 72},
            {65, 66},
            {73, 74},
            {76, 77},
            {79, 78},
            {80, 81},
            {91, 92},
            {104, 105},
            {109, 110},
            {116, 117},
            {118, 119},
            {123, 124},
            {125, 126},
            {127, 128},
            {130, 131},
        };

        public Dictionary<int, int> female { get; private set; } = new Dictionary<int, int>()
        {
            {43, 44},
            {49, 67},
            {64, 65},
            {65, 64},
            {51, 69},
            {50, 68},
            {52, 70},
            {62, 71},
            {72, 73},
            {75, 76},
            {78, 77},
            {79, 80},
            {18, 66},
            {66, 81},
            {81, 66},
            {86, 84},
            {90, 91},
            {103, 104},
            {108, 109},
            {115, 116},
            {117, 118},
            {122, 123},
            {124, 125},
            {126, 127},
            {129, 130},
        };
    }
}
