namespace BTEAManual
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;

    public class Strings
    {
        const int iterations = 64000;
        static Encoding encoder = new UTF8Encoding();

        public static bool Contains(string source, string toCheck)
        {
            return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #region HASH - SHA2 Family
        private static string HashSHA256(string phrase)
        {
            if (phrase == null)
                return null;

            var sha256Hasher = new SHA256CryptoServiceProvider();
            var hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(phrase));

            return ByteArrayToHexString(hashedDataBytes);
        }

        private static string HashSHA384(string phrase)
        {
            if (phrase == null)
                return null;

            var sha384Hasher = new SHA384CryptoServiceProvider();
            var hashedDataBytes = sha384Hasher.ComputeHash(encoder.GetBytes(phrase));

            return ByteArrayToHexString(hashedDataBytes);
        }

        private static string HashSHA512(string phrase)
        {
            if (phrase == null)
                return null;

            var sha512Hasher = new SHA512CryptoServiceProvider();
            var hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(phrase));

            return ByteArrayToHexString(hashedDataBytes);
        }
        #endregion
        #region AES
        public static string EncryptAES(string phrase, string key, bool hashKey = true)
        {
            if (phrase == null || key == null)
                return null;

            var keyArray = HexStringToByteArray(hashKey ? HashSHA512(key) : key);

            // Trim Array Length From 32 Down To 24 To Match SHA256 Array Length
            var trimmedKeyArray = new byte[24];
            Buffer.BlockCopy(keyArray, 0, trimmedKeyArray, 0, 24);
            keyArray = trimmedKeyArray;

            var byteHash = Encoding.UTF8.GetBytes(phrase);
            byte[] result = null;

            using (var aes = new AesCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                var cTransform = aes.CreateEncryptor();
                try
                {
                    result = cTransform.TransformFinalBlock(byteHash, 0, byteHash.Length);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex.Message);
                }
                aes.Clear();
            }
            return ByteArrayToHexString(result);
        }

        public static string DecryptAES(string hash, string key, bool hashKey = true)
        {
            if (hash == null || key == null)
                return null;

            var keyArray = HexStringToByteArray(hashKey ? HashSHA512(key) : key);

            // Trim Array Length From 32 Down To 24 To Match SHA256 Array Length
            var trimmedKeyArray = new byte[24];
            Buffer.BlockCopy(keyArray, 0, trimmedKeyArray, 0, 24);
            keyArray = trimmedKeyArray;

            var byteHash = HexStringToByteArray(hash);
            byte[] result = null;

            using (var aes = new AesCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                var cTransform = aes.CreateDecryptor();
                try
                {
                    result = cTransform.TransformFinalBlock(byteHash, 0, byteHash.Length);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex.Message);
                }
                aes.Clear();
            }

            return Encoding.UTF8.GetString(result);
        }
        #endregion
        #region 3DES
        public static string EncryptTripleDES(string phrase, string key, bool hashKey = true)
        {
            var keyArray = HexStringToByteArray(hashKey ? HashSHA256(key) : key);

            // Trim Array Length From 32 Down To 24 To Match SHA256 Array Length
            var trimmedKeyArray = new byte[24];
            Buffer.BlockCopy(keyArray, 0, trimmedKeyArray, 0, 24);
            
            keyArray = trimmedKeyArray;

            var byteHash = Encoding.UTF8.GetBytes(phrase);
            byte[] result = null;

            using (var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                var cTransform = tdes.CreateEncryptor();
                try
                {
                    result = cTransform.TransformFinalBlock(byteHash, 0, byteHash.Length);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex.Message);
                }
                tdes.Clear();
            }
            return ByteArrayToHexString(result);
        }

        public static string DecryptTripleDES(string hash, string key, bool hashKey = true)
        {
            var keyArray = HexStringToByteArray(hashKey ? HashSHA256(key) : key);

            // Trim Array Length From 32 Down To 24 To Match SHA256 Array Length
            var trimmedKeyArray = new byte[24];
            Buffer.BlockCopy(keyArray, 0, trimmedKeyArray, 0, 24);
            keyArray = trimmedKeyArray;

            var byteHash = HexStringToByteArray(hash);
            byte[] result = null;

            using (var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            {
                var cTransform = tdes.CreateDecryptor();
                try
                {
                    result = cTransform.TransformFinalBlock(byteHash, 0, byteHash.Length);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex.Message);
                }
                tdes.Clear();
            }
            return Encoding.UTF8.GetString(result);
        }
        #endregion
        #region CONVERTER [BytesToHex <> HexToBytes]
        private static string ByteArrayToHexString(byte[] InputArray)
        {
            if (InputArray == null)
                return null;

            var sb = new StringBuilder("");
            for (var i = 0; i < InputArray.Length; i++)
                sb.Append(InputArray[i].ToString("X2"));

            return sb.ToString();
        }

        private static byte[] HexStringToByteArray(string InputString)
        {
            if (InputString == null)
                return null;

            if (InputString.Length == 0)
                return new byte[0];

            if (InputString.Length % 2 != 0)
                Logger.Write(string.Format("{0} Error: Hex Strings Have An Even Number Of Characters And You Have Got An Odd Number Of Characters!", MethodBase.GetCurrentMethod().Name));

            var num = InputString.Length / 2;
            var bytes = new byte[num];

            for (var i = 0; i < num; i++)
            {
                var x = InputString.Substring(i * 2, 2);
                try
                {
                    bytes[i] = Convert.ToByte(x, 16);
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("{0} Error: {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
                }
            }
            return bytes;
        }
        #endregion

        public static string FormatPhone(string source)
        {
            var chars = new string[] { "(", ")", "-", " " };

            return chars.Aggregate(source, (s, c) => s.Replace(c, ""));
        }

        public static string PascalCase(string source)
        {
            return Regex.Replace(source.ToLower(), @"\b[a-zA-Z]", s => s.Value.ToUpper());
        }

        public static string Replace(int index, char value, string source)
        {
            char[] strings = source.ToCharArray();
            strings[index] = value;

            return string.Join("", strings);
        }

        public static string PadCenter(string str, int len)
        {
            int spaces = len - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(len);
        }

        public static string Truncate(string str, int maxLen)
        {
            return str?.Substring(0, Math.Min(str.Length, maxLen));
        }

        public static string GetStringBetween(string fullStrings, string first, string second)
        {
            if (!fullStrings.Contains(first)) return "";

            var afterFirst = fullStrings.Split(new[] { first }, StringSplitOptions.None)[1];

            if (!afterFirst.Contains(second)) return "";

            var result = afterFirst.Split(new[] { second }, StringSplitOptions.None)[0];

            return result;
        }

        public static string DayWithSuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return $"{day}st";
                case 2:
                case 22:
                    return $"{day}nd";
                case 3:
                case 23:
                    return $"{day}rd";
                default:
                    return $"{day}th";
            }
        }
    }
}