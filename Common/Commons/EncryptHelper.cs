using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Commons
{
    public static class EncryptHelper
    {
        public static string SecurityKey = "MTIzQDEyM2FBQg==";

        public static string EncryptMD5(string context)
        {
            UTF8Encoding Unic = new UTF8Encoding();

            byte[] bytes = Unic.GetBytes(context);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(bytes);

            return BitConverter.ToString(result);

        }

        public static string EncryptRSA(object obj, RsaKeyParameters rsaKeyParameters)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new TickDateTimeConverter() }
            };

            var serialized = JsonConvert.SerializeObject(obj, settings);
            var payloadBytes = Encoding.UTF8.GetBytes(serialized);

            var cipher = GetAsymmetricBlockCipher(true, rsaKeyParameters);
            var encrypted = Process(cipher, payloadBytes);

            var encoded = Convert.ToBase64String(encrypted);
            return encoded;
        }

        public static T Decrypt<T>(string encryptedText, RsaKeyParameters rsaKeyParameters)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new TickDateTimeConverter() }
            };

            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

            var cipher = GetAsymmetricBlockCipher(false, rsaKeyParameters);
            var decrypted = Process(cipher, cipherTextBytes);

            var decoded = Encoding.UTF8.GetString(decrypted);

            return JsonConvert.DeserializeObject<T>(decoded, settings);
        }



        private static IAsymmetricBlockCipher GetAsymmetricBlockCipher(bool forEncryption, RsaKeyParameters rsaKeyParameters)
        {
            var cipher = new Pkcs1Encoding(new RsaEngine());
            cipher.Init(forEncryption, rsaKeyParameters);

            return cipher;
        }

        private static byte[] Process(IAsymmetricBlockCipher cipher, byte[] payloadBytes)
        {
            int length = payloadBytes.Length;
            int blockSize = cipher.GetInputBlockSize();

            var plainTextBytes = new List<byte>();
            for (int chunkPosition = 0; chunkPosition < length; chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, length - chunkPosition);
                plainTextBytes.AddRange(cipher.ProcessBlock(
                    payloadBytes, chunkPosition, chunkSize
                ));
            }

            return plainTextBytes.ToArray();
        }

        public static string AES_EncryptText(string plainText, string keyString)
        {
            byte[] cipherData;
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(keyString);
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }

                cipherData = ms.ToArray();
            }

            byte[] combinedData = new byte[aes.IV.Length + cipherData.Length];
            Array.Copy(aes.IV, 0, combinedData, 0, aes.IV.Length);
            Array.Copy(cipherData, 0, combinedData, aes.IV.Length, cipherData.Length);
            return Convert.ToBase64String(combinedData);
        }

        public static string AES_DecryptText(string combinedString, string keyString)
        {
            if (string.IsNullOrEmpty(combinedString))
            {
                return "";
            }
            string plainText;
            byte[] combinedData = Convert.FromBase64String(combinedString);
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherText = new byte[combinedData.Length - iv.Length];
            Array.Copy(combinedData, iv, iv.Length);
            Array.Copy(combinedData, iv.Length, cipherText, 0, cipherText.Length);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        plainText = sr.ReadToEnd();
                    }
                }

                return plainText;
            }
        }
    }

    public class TickDateTimeConverter : DateTimeConverterBase
    {
        private static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long tick = (long)reader.Value;

            return unixEpoch.AddMilliseconds(tick).ToLocalTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }
    }

}
