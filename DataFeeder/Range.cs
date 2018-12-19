using System.Collections.Generic;

namespace DataFeeder
{
    public static class Range
    {
        public static List<int> Int(int start, int end)
        {
            var result = new List<int>();
            for (var i = start; i < end; i++)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
