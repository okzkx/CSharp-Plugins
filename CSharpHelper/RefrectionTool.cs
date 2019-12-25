using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OKZKX.Util
{
    public static class RefrectionTool
    {
        const BindingFlags ALL_FIELDS_FLAG = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void EachField(this object obj, Action<FieldInfo> action)
        {
            FieldInfo[] fieldInfos = obj.GetType().GetFields(ALL_FIELDS_FLAG);
            foreach (var fieldInfo in fieldInfos) action(fieldInfo);
        }

        public static string ParasToString(this object obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            obj.EachField((fi) =>
            {
                sb.Append($"{fi.Name}:{fi.GetValue(obj)},");
            });
            sb.Remove(sb.Length - 1, 1);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
