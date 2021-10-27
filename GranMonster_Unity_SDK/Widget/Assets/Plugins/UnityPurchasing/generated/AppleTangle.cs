#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("fM5I93zOT+/sT05NTk5NTnxBSkVKTxlRQkhaSFhnnCUL2DpFsrgnwWBsLyk+OCUqJS8tOClsPCMgJS81+1fx3w5oXmaLQ1H6AdASL4QHzFt61QBhNPuhwNeQvzvXvjqeO3wDjURnSk1JSUtOTVpSJDg4PD92Y2M7OCQjPiU4NX1afFhKTxlIT19BDTxEEnzOTV1KTxlRbEjOTUR8zk1IfFPJz8lX1XELe77l1wzCYJj93F6UbCMqbDgkKWw4JCkibC08PCAlLy01bC0/PzkhKT9sLS8vKTw4LSIvKUPRcb9nBWRWhLKC+fVClRJQmodx5JAybnmGaZmVQ5onmO5ob1277eA+LS84JS8pbD84LTgpISkiOD9ifEp8Q0pPGVFfTU2zSEl8T01Ns3xRjC9/O7t2S2Aap5ZDbUKW9j9VA/nn7z3eCx8ZjeNjDf+0t688garvADMN5NS1nYYq0GgnXZzv96hXZo9TlXozjcsZlevV9X4Ot5SZPdIy7R5qfGhKTxlIR19RDTw8IClsDyk+OHl+fXh8f3oWW0F/eXx+fHV+fXh8aK6nnfs8k0MJrWuGvSE0oav5W1tBSkVmygTKu0FNTUlJTE/OTU1MECApbAUiL2J9anxoSk8ZSEdfUQ08+XbhuENCTN5H/W1aYjiZcEGXLlo2fM5NOnxCSk8ZUUNNTbNISE9OTTs7Yi08PCApYi8jIWMtPDwgKS8tOCUqJS8tOClsLjVsLSI1bDwtPjjyuD/Xop4oQ4c1A3iU7nK1NLMnhGwPDXzOTW58QUpFZsoEyrtBTU1NcWorbMZ/JrtBzoOSp+9jtR8mFyhsLSIobC8pPjglKiUvLTglIyJsPBXrSUUwWwwaXVI4n/vHb3cL75kjzk1MSkVmygTKuy8oSU18zb58ZkpLoDF1z8cfbJ90iP3z1gNGJ7NnsFPdl1ILHKdJoRI1yGGneu4bABmgzFhnnCUL2DpFsrgnwWIM6rsLATNiDOq7CwEzRBJ8U0pPGVFvSFR8WoVVPrkRQpkzE9e+aU/2GcMBEUG9LiApbD84LSIoLT4obDgpPiE/bC3DP80silcXRWPe/rQIBLwsdNJZuQWUOtN/WCntO9iFYU5PTUxN785NIihsLyMiKCU4JSMiP2wjKmw5PylISl9OGR99X3xdSk8ZSEZfRg08PDwgKWweIyM4bA8NfFJbQXx6fHh+ZsoEyrtBTU1JSUx8Ln1HfEVKTxlafFhKTxlIT19BDTw8IClsHiMjOCvDRPhsu4fgYGwjPPpzTXzA+w+DHikgJS0iLylsIyJsOCQlP2wvKT4lKiUvLTglIyJsDTk4JCM+JTg1fX96FnwufUd8RUpPGUhKX04ZH31f/XwUoBZIfsAk/8NRkik/sysSKfAJMlMAJxzaDcWIOC5HXM8Ny3/GzcdVxZK1ByC5S+dufE6kVHK0HEWfY3zNj0pEZ0pNSUlLTk58zfpWzf98XUpPGUhGX0YNPDwgKWwFIi9ifSh5b1kHWRVR/9i7utDSgxz2jRQcSUxPzk1DTHzOTUZOzk1NTKjd5UXZ0jZA6AvHF5hae3+HiEMBglglnTwgKWwPKT44JSolLy04JSMibA05HObGmZaosJxFS3v8OTlt");
        private static int[] order = new int[] { 0,30,44,59,48,35,35,33,49,41,40,22,54,46,29,57,24,17,58,40,33,53,47,59,59,28,36,39,50,46,56,45,52,34,51,57,52,45,40,42,55,42,43,44,57,56,54,53,55,53,53,52,53,59,56,57,59,59,58,59,60 };
        private static int key = 76;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
