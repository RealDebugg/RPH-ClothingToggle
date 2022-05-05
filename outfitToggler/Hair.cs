using System.Collections.Generic;

namespace outfitToggler
{
    internal class Hair
    {
        public int _lastHairDraw { get; set; }
        public int _lastHairText { get; set; }
        public Dictionary<int, int> male { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> female { get; set; } = new Dictionary<int, int>();
    }
}
