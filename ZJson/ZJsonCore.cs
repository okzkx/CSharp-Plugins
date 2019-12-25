using System.Collections.Generic;
using System.Text;

namespace OKZKX.ZJson
{
    public enum JsonType
    {
        String,
        Number,
        List,
        Dictionary
    }

    public partial class ZJson
    {
        #region Consts

        static readonly List<char> CONTROL_CHAR = new List<char>() { '{', '}', '[', ']', ',', ':', '"' };

        #endregion

        #region Fields

        public readonly JsonType Type;
        public readonly object Value;

        #endregion

        #region Constructor

        private ZJson(object value, JsonType type)
        {
            Type = type;
            Value = value;
        }
        private ZJson(float arg) : this(arg, JsonType.Number) { }
        private ZJson(string arg) : this(arg, JsonType.String) { }
        private ZJson(List<ZJson> arg) : this(arg, JsonType.List) { }
        private ZJson(Dictionary<string, ZJson> arg) : this(arg, JsonType.Dictionary) { }

        #endregion

        #region Serialize
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            switch (Type)
            {
                case JsonType.String:
                    sb.Append($"\"{(string)Value}\"");
                    break;
                case JsonType.Number:
                    sb.Append((float)Value);
                    break;
                case JsonType.List:
                    List<ZJson> list = Value as List<ZJson>;
                    sb.Append('[');
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                            sb.Append($"{item.ToString()},");
                        sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append(']');
                    break;
                case JsonType.Dictionary:
                    Dictionary<string, ZJson> dict = Value as Dictionary<string, ZJson>;
                    sb.Append('{');
                    if (dict.Count > 0)
                    {
                        foreach (var item in dict)
                            sb.Append($"\"{item.Key}\":{item.Value},");
                        sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append('}');
                    break;
                default:
                    break;
            }

            return sb.ToString();
        }

        #endregion

        #region DeSerialize

        public static ZJson Parse(string json)
        {
            int cursor = 0;
            if (TryJsonFormat(json, out string jsonFormat))
                if (TryCreateFromJson(jsonFormat, ref cursor, out ZJson zJson))
                    return zJson;
            return null;
        }
        private static bool TryJsonFormat(string json, out string formatJson)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in json)
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            formatJson = sb.ToString();
            return true;
        }
        private static bool TryCreateFromJson(string json, ref int cursor, out ZJson zJson)
        {
            zJson = null;
            JsonType jsonType;
            object jsonValue = null;

            #region Set Type

            char first_char = json[cursor];
            switch (first_char)
            {
                case '{':
                    jsonType = JsonType.Dictionary;
                    break;
                case '[':
                    jsonType = JsonType.List;
                    break;
                case '"':
                    jsonType = JsonType.String;
                    break;
                default:
                    if (char.IsDigit(first_char))
                        jsonType = JsonType.Number;
                    else
                        return false;
                    break;
            }

            #endregion

            #region Set Value

            switch (jsonType)
            {
                case JsonType.String:
                    {
                        int start = ++cursor;
                        while (json[cursor++] != '"') continue;
                        jsonValue = json.Substring(start, cursor - 1 - start);
                    }
                    break;
                case JsonType.Number:
                    {
                        int start = cursor;
                        while (!CONTROL_CHAR.Contains(json[++cursor])) continue;
                        jsonValue = float.Parse(json.Substring(start, cursor - start));
                    }
                    break;
                case JsonType.List:
                    List<ZJson> list = new List<ZJson>();
                    while (json[cursor++] != ']')
                        if (TryCreateFromJson(json, ref cursor, out var zzJson))
                            list.Add(zzJson);
                    jsonValue = list;
                    break;
                case JsonType.Dictionary:
                    Dictionary<string, ZJson> dict = new Dictionary<string, ZJson>();
                    while (json[cursor++] != '}')
                        if (TryCreateFromJson(json, ref cursor, out var key))
                        {
                            cursor++;
                            if (TryCreateFromJson(json, ref cursor, out var value))
                                dict.Add(key.Value.ToString(), value);
                        }
                    jsonValue = dict;
                    break;
                default:
                    break;
            }
            if (jsonValue == null) return false;

            #endregion

            #region Create ZJson

            switch (jsonType)
            {
                case JsonType.String:
                    zJson = new ZJson((string)jsonValue);
                    break;
                case JsonType.Number:
                    zJson = new ZJson((float)jsonValue);
                    break;
                case JsonType.List:
                    zJson = new ZJson((List<ZJson>)jsonValue);
                    break;
                case JsonType.Dictionary:
                    zJson = new ZJson((Dictionary<string, ZJson>)jsonValue);
                    break;
                default:
                    break;
            }

            #endregion

            return true;
        }

        #endregion
    }
}