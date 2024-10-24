using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Common
{
    public static class Utils
    {
        public static string ObjToString(this object obj)
        {
            if (obj == null)
            {
                return "";
            }

            return obj.ToString();
        }
        public static int Generate6Number()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
        public static string Generate6Character()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool AreObjectsDifferent<T1, T2>(this T1 obj1, T2 obj2)
        {
            if (obj1 == null || obj2 == null)
                return true; // If either object is null, treat them as different

            var properties1 = typeof(T1).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var properties2 = typeof(T2).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Dictionary of properties from obj2 for fast lookup
            var properties2Dict = properties2.ToDictionary(p => p.Name, p => p);

            foreach (var property1 in properties1)
            {
                if (!properties2Dict.TryGetValue(property1.Name, out var property2) ||
                    property1.PropertyType != property2.PropertyType)
                {
                    return true; // If property names or types don't match, they are considered different
                }

                var value1 = property1.GetValue(obj1);
                var value2 = property2.GetValue(obj2);

                if (!Equals(value1, value2))
                {
                    return true; // Return true if any property values are different
                }
            }

            return false; // Return false if no differences are found
        }


    }

    public static class GuidHelper
    {
        public static Guid ToGuid(this string guid)
        {
            return string.IsNullOrEmpty(guid) ? Guid.Empty : Guid.Parse(guid);
        }
        public static string ToString(this Guid? guid)
        {
            // Kiểm tra nếu Guid là null, trả về chuỗi rỗng
            return guid?.ToString() ?? string.Empty;
        }
        public static bool IsGuidEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }
        public static bool IsGuid(string guid)
        {
            return Guid.TryParse(guid, out _); // Trả về true nếu chuỗi là GUID hợp lệ, ngược lại trả về false
        }
    }

    //add more class helper here ...

}
