using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outfitToggler
{
    internal class Jackets
    {
        public Dictionary<int, int> male { get; private set; } = new Dictionary<int, int>()
        {
            {29, 30},
            {31, 32},
            {42, 43},
            {68, 69},
            {74, 75},
            {87, 88},
            {99, 100},
            {101, 102},
            {103, 104},
            {126, 127},
            {129, 130},
            {184, 185},
            {188, 189},
            {194, 195},
            {196, 197},
            {198, 199},
            {200, 203},
            {202, 205},
            {206, 207},
            {210, 211},
            {217, 218},
            {229, 230},
            {232, 233},
            {251, 253},
            {256, 261},
            {262, 263},
            {265, 266},
            {267, 268},
            {279, 280},
        };
        public Dictionary<int, int> female { get; private set; } = new Dictionary<int, int>()
        {
            {53, 52},
            {57, 58},
            {62, 63},
            {90, 91},
            {92, 93},
            {94, 95},
            {187, 186},
            {190, 191},
            {196, 197},
            {198, 199},
            {200, 201},
            {202, 205},
            {204, 207},
            {210, 211},
            {214, 215},
            {227, 228},
            {239, 240},
            {242, 243},
            {259, 261},
            {265, 270},
            {271, 272},
            {274, 275},
            {276, 277},
            {292, 293},
        };
    }
}
