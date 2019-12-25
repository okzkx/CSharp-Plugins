/*********************************************************************
* JTObject 只有4种字段
* 1. string
* 2. float
* 3. List<object>
* 4. Dictionary<object>
* 
* JTClass 只有4种字段
* 1. string
* 2. float
* 3. List<object>
* 4. object : JTClass
**********************************************************************/

using OKZKX.Util;
using System;
using System.Collections.Generic;

namespace OKZKX.ZJson
{

    /// <summary>
    /// ZJsonObject : ZJsonTranslate
    /// </summary>
    public partial class ZJson
    {
        public const string NULL = "null";

        #region ZJson <=> JTObject

        public static ZJson Parse(object jtobj)
        {
            switch (jtobj)
            {
                case null:
                    return new ZJson(NULL);
                case float a:
                    return new ZJson(a);
                case string a:
                    return new ZJson(a);
                case List<object> a:
                    List<ZJson> list = new List<ZJson>();
                    foreach (var item in a) list.Add(Parse(item));
                    return new ZJson(list);
                case Dictionary<string, object> a:
                    Dictionary<string, ZJson> dict = new Dictionary<string, ZJson>();
                    foreach (var item in a) dict.Add(item.Key, Parse(item.Value));
                    return new ZJson(dict);
                default:
                    return new ZJson(NULL);
            }
        }

        public object ToJTObject()
        {
            switch (Type)
            {
                case JsonType.String:
                    if (Value.ToString() == NULL)
                        return null;
                    return (string)this;
                case JsonType.Number:
                    return (float)this;
                case JsonType.List:
                    List<ZJson> zlist = this;
                    List<object> list = new List<object>();
                    zlist.ForEach((item) => list.Add(item.ToJTObject()));
                    return list;
                case JsonType.Dictionary:
                    Dictionary<string, ZJson> zDict = this;
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    foreach (var item in zDict) dict.Add(item.Key, item.Value.ToJTObject());
                    return dict;
                default:
                    return null;
            }
        }

        #endregion

        #region JTObject <=> JTClass

        /// <summary>
        /// jto 是Dictionary 为新类赋值,
        /// jto 是List 为其中的所有项赋值,
        /// 其他类型返回 jto
        /// </summary>
        /// <param name="jto"></param>
        /// <returns></returns>
        public static object JTObjectToJTClass(object jto, Type jtcType)
        {
            // list
            if (jto is List<object> jtos)
            {
                List<object> jtcs = new List<object>();
                for (int i = 0; i < jtos.Count; i++)
                    jtcs[i] = JTObjectToJTClass(jtos[i], jtos[i].GetType());
                return jtcs;
            }

            // dict
            if (jto is Dictionary<string, object> jtodict)
            {
                object jtcclass = Activator.CreateInstance(jtcType);
                foreach (var item in jtodict)
                {
                    var fi = jtcType.GetField(item.Key);
                    fi.SetValue(jtcclass, JTObjectToJTClass(item.Value, fi.FieldType));
                }
                return jtcclass;
            }

            // string || value
            return jto;
        }

        public static object JTClassToJTObject(object jtc)
        {
            // string
            if (jtc is string)
                return jtc;

            // list
            if (jtc is List<object> c_list)
            {
                List<object> list = new List<object>();
                foreach (var item in c_list)
                    list.Add(JTClassToJTObject(item));
                return list;
            }

            // value
            if (jtc.GetType().IsValueType)
                return jtc;

            // dict
            Dictionary<string, object> dict = new Dictionary<string, object>();
            jtc.EachField((fi) =>
            {
                dict.Add(fi.Name, JTClassToJTObject(fi.GetValue(jtc)));
            });
            return dict;
        }

        #endregion

        #region ZJson <=> JTClass

        public static ZJson ParseJTClass(object jtc) => Parse(JTClassToJTObject(jtc));

        public object ToJTClass(Type type) => JTObjectToJTClass(ToJTObject(), type);

        #endregion
    }
}
