#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("FdbG1MCKT4+vdS4b/yBWN3pXmh5ojqKpw2uKfQNfuf+OsizBVM3pwlPWu/UFvO78FrDNg0PSgXvHwUS3fv3z/Mx+/fb+fv39/EoR/QpFJgVhi8VDmGeXS+MGqHMLRd7ifNWsG/mIpjJAH0uOs1LlZFPnqWYQshkqIslTbUTVWvTDKD5wBZJAqiV+PpAaLmNq4hPqQthxJ8WCw8ZZb+wLJYXkqSAxy77mP9PGAleqaO31rFbIWNeA2/cUZjfflquria6yV2RMw9gyMd1nRxtpRx9SQlbh2Bu6GJ4m/07uUSr9Q/mIXgSqvBPzRf13e0gczH793szx+vXWerR6C/H9/f35/P/OW4cPXl5xaTZdpvOk8hAzGbpCZxwW3Dr/qU0mL/7//fz9");
        private static int[] order = new int[] { 3,11,4,11,13,5,7,13,12,10,13,13,13,13,14 };
        private static int key = 252;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
