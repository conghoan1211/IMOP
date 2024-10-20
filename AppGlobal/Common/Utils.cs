using System;
using System.Collections.Generic;
using System.Linq;
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

        //add more utils here ...

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
