using System;
using System.Linq;
using System.Security.Cryptography;

using System.Threading.Tasks;

namespace XamarinFrontEnd.Helpers
{
    public static class TOTPHelper
    {

        private const string allowedCharacters = "abcdefghijklmnopqrstuv0123456789"; // Due to Base32 encoding; https://code.google.com/p/vellum/wiki/GoogleAuthenticator
        private static int validityPeriodSeconds = 30; // RFC6238 4.1; X represents the time step in seconds (default value X = 30 seconds) and is a system parameter.
        private static int futureIntervals = 1; // How much time in the future can the client be; in validityPeriodSeconds intervals.
        private static int pastIntervals = 1; // How much time in the past can the client be; in validityPeriodSeconds intervals.
        private static int secretKeyLength = 16; // Must be a multiple of 8, iPhones accept up to 16 characters (apparently; didn't test it; don't own an iPhone)
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Beginning of time, according to Unix

        private static long GetInterval(DateTime dateTime)
        {
            TimeSpan elapsedTime = dateTime.ToUniversalTime() - unixEpoch;
            return (long)elapsedTime.TotalSeconds / validityPeriodSeconds;
        }
        public static  string GetCode(string secretKey)
        {
            return GetCode(secretKey, DateTime.Now);
        }
        public static string GetCode(string secretKey, DateTime when)
        {
            return GetCode(secretKey, GetInterval(when));
        }
        private static string GetCode(string secretKey, long timeIndex)
        {
            var secretKeyBytes = Base32Encode(secretKey);
            for(int i = secretKey.Length; i < secretKeyBytes.Length; i++) { secretKeyBytes[i] = 0; }
            HMACSHA1 hmac = new HMACSHA1(secretKeyBytes);
            byte[] challenge = BitConverter.GetBytes(timeIndex);
            if(BitConverter.IsLittleEndian) Array.Reverse(challenge);
            byte[] hash = hmac.ComputeHash(challenge);
            int offset = hash[19] & 0xf;
            int truncatedHash = hash[offset] & 0x7f;
            for(int i = 1; i < 4; i++)
            {
                truncatedHash <<= 8;
                truncatedHash |= hash[offset + i] & 0xff;
            }
            truncatedHash %= 1000000;
            return truncatedHash.ToString("D6");
        }
        private static byte[] Base32Encode(string source)
        {
            var bits = source.ToUpper().ToCharArray().Select(c => Convert.ToString(allowedCharacters.IndexOf(c), 2).PadLeft(5, '0')).Aggregate((a, b) => a + b);
            return Enumerable.Range(0, bits.Length / 8).Select(i => Convert.ToByte(bits.Substring(i * 8, 8), 2)).ToArray();
        }
        private static bool ConstantTimeEquals(string a, string b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;

            for(int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)a[i] ^ (uint)b[i];
            }

            return diff == 0;
        }
    }
}
