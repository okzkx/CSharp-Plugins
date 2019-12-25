using System.Collections.Generic;

namespace OKZKX.ZJson
{
    /// <summary>
    /// ZJson Translate : ZJsonCore
    /// </summary>
    public partial class ZJson
    {
        public ZJson this[int index] => ((List<ZJson>)this)?[index];
        public ZJson this[string key] => ((Dictionary<string, ZJson>)this)?[key];

        public static implicit operator float(ZJson zjson)
            => zjson.Type == JsonType.Number ? (float)zjson.Value : float.NaN;
        public static implicit operator string(ZJson zjson)
            => zjson.Type == JsonType.String ? zjson.Value.ToString() : null;
        public static implicit operator List<ZJson>(ZJson zjson)
            => zjson.Type == JsonType.List ? zjson.Value as List<ZJson> : null;
        public static implicit operator Dictionary<string, ZJson>(ZJson zjson)
            => zjson.Type == JsonType.Dictionary ? zjson.Value as Dictionary<string, ZJson> : null;

        public static implicit operator ZJson(float arg) => new ZJson(arg);
        public static implicit operator ZJson(string arg) => new ZJson(arg);
        public static implicit operator ZJson(List<ZJson> arg) => new ZJson(arg);
        public static implicit operator ZJson(Dictionary<string, ZJson> arg) => new ZJson(arg);
    }
}