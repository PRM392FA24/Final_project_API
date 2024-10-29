using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObj.DTO
{
    public class ResponseType<T>
    {
        public string message { get; set; }
        public bool _isIgnoreNullData;
        public object _data;

        public object Data { get; set; }
        public static JsonSerializerSettings _jsonSerializer = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
        };
        public ResponseType(bool isIgnoreNullData = true)
        {
            this._isIgnoreNullData = isIgnoreNullData;
        }
    }
}
