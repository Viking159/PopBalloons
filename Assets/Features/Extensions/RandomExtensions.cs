using UnityEngine;

namespace Features.Extensions
{

    public static class RandomExtensions
    {
        public static float RandomPair(RandomPair pair) => Random.Range(pair.MinValue, pair.MaxValue);

        public static bool RandomBool() => Random.Range(0, 2) == 0;
    }
}
