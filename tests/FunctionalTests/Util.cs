using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunctionalTests
{
    public class Util
    {
        public static HttpContent CreateHttpContent<T>(T dto)
        {
            var pc = JsonConvert.SerializeObject(dto);
            return new StringContent(pc, Encoding.UTF8, "application/json");
        }
    }
}
