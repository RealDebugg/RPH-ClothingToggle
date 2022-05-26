using System.Collections.Generic;

namespace outfitToggler
{
    internal class Hair
    {
        public int _lastHairDraw { get; set; }
        public int _lastHairText { get; set; }
        public Dictionary<int, int> male { get; private set; } = new Dictionary<int, int>()
        {
            {7, 15},
            {9, 43},
            {11, 43},
            {16, 43},
            {17, 43},
            {20, 43},
            {22, 43},
            {45, 43},
            {47, 43},
            {49, 43},
            {51, 43},
            {52, 43},
            {53, 43},
            {56, 43},
            {58, 43},
        };
        public Dictionary<int, int> female { get; private set; } = new Dictionary<int, int>()
        {
            {1, 49},
            {2, 49},
            {7, 49},
            {9, 49},
            {10, 49},
            {11, 48},
            {14, 53},
            {15, 42},
            {21, 42},
            {23, 42},
            {39, 49},
            {40, 49},
            {45, 49},
            {54, 55},
            {59, 42},
            {68, 53},
            {76, 48},
        };
    }
}
