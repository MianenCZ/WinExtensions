using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WinExtension.Common.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this T obj, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }
    }
}
