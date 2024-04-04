using Microsoft.VisualBasic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Extensions
{
    public static class Extensions
    {
        private static string[] dateFormats = new string[] {
            "yyyyMMddHHmmss",
            "yyyy-MM-dd",
            "yyyy-MM-dd HHmmss",
            "yyyyMMdd",
            "yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'ffffff",
            "yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'ffffff",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
            "yyyyMMdd HH:mm:ss",
            "yyyy/MM/dd",
            "MM/dd/yyyy HH:mm:ss",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z",
            "yyyy-MM-ddTHH:mm:ssZ",
            "yyyy-MM-ddTHH:mm:ss.fffZ"
        };

        public static string StringToDateTimeFormat(this string dateString)
        {
            DateTime dateTime = DateTime.ParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
        }

        public static bool IsStringDateTimeFormat(this string inputString)
        {
            var isParsed = DateTime.TryParseExact(inputString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);

            if (isParsed)
            {
                return true;
            }

            return false;
        }

        public static string StringToDateFormat(this string dateString)
        {
            DateTime dateTime = DateTime.ParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string Encrypt(this string inputString)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("Dp(I82=jYzak#3Mq");
                aes.IV = new byte[16] { 145, 249, 126, 218, 15, 17, 5, 94, 92, 84, 10, 34, 14, 86, 197, 132 }; // IV (Initialization Vector) should be random and unique for each encryption



                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] passwordBytes = Encoding.UTF8.GetBytes(inputString);

                using (var ms = new System.IO.MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(passwordBytes, 0, passwordBytes.Length);
                    cs.FlushFinalBlock();
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    return base64;
                }
            }
        }

        public static string Decrypt(this string inputString)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("Dp(I82=jYzak#3Mq");
                aes.IV = new byte[16] { 145, 249, 126, 218, 15, 17, 5, 94, 92, 84, 10, 34, 14, 86, 197, 132 }; // IV should match the one used during encryption

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] encryptedPasswordBytes = Convert.FromBase64String(inputString);

                using (var ms = new System.IO.MemoryStream())
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                    cs.FlushFinalBlock();
                    var decodedBaseString = Encoding.UTF8.GetString(ms.ToArray());
                    return decodedBaseString;
                }
            }
        }

        public static int GetRoleId(this JwtSecurityToken token)
        {
            var roleId = token.Claims.First(c => c.Type == "Role").Value;

            return int.Parse(roleId);
        }

    }
}
