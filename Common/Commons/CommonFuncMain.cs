using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Common.Commons
{
    public static class CommonFuncMain
    {
        public static bool IsUnicode(string input)
        {
            return Encoding.ASCII.GetByteCount(input) != Encoding.UTF8.GetByteCount(input);
        }
        public static string GenerateCoupon()
        {
            Random random = new Random();
            int length = random.Next(50, 80);
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
        public static T ToObject<T>(Object fromObject)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(fromObject, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
        }
        public static List<T> ToObjectList<T>(Object fromObject)
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(fromObject, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
        }
        static public object GetValueObject(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }
        public static string utf8Convert3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string Encrypt(string data)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(data);
            return Convert.ToBase64String(toEncodeAsBytes);
        }
        public static string Decrypt(string data)
        {
            return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(data));
        }
      
    }
}
