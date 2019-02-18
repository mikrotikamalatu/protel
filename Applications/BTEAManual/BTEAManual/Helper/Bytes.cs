namespace BTEAManual
{
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;

    public class Bytes
    {
        public static string GetSize(byte[] bytes)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
            int length = bytes.Length;
            int order = 0;

            while (length >= 1024 && order < sizes.Length - 1)
            {
                order++;
                length = length / 1024;
            }

            return string.Format("{0:0.##}{1}", length, sizes[order]);
        }

        public static string GetSize(double len)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0.##}{1}", len, sizes[order]);
        }

        public static byte[] Encrypt(byte[] clearBytes)
        {
            const string pass = "!@#$%^&*()_+";
            const string salt = "+_)(*&^%$#@!";

            byte[] passBytes = Encoding.UTF8.GetBytes(pass);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            var key = new Rfc2898DeriveBytes(passBytes, saltBytes, 32768);

            using (var aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.Padding = PaddingMode.Zeros;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }

                    return ms.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] encryptedBytes)
        {
            const string pass = "!@#$%^&*()_+";
            const string salt = "+_)(*&^%$#@!";

            byte[] passBytes = Encoding.UTF8.GetBytes(pass);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            var key = new Rfc2898DeriveBytes(passBytes, saltBytes, 32768);

            using (var aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.Padding = PaddingMode.Zeros;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }

                    return ms.ToArray();
                }
            }
        }
    }
}