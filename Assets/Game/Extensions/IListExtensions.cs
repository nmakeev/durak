using System.Collections.Generic;

namespace Game.Extensions
{
    public static class IListExtensions
    {
        //https://discussions.unity.com/t/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code/535113/2
        public static void Shuffle<T>(this IList<T> self) {
            var count = self.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                (self[i], self[r]) = (self[r], self[i]);
            }
        }
    }
}