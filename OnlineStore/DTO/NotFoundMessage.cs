using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class NotFoundMessage
    {
        [Newtonsoft.Json.JsonRequired]
        public string Message { get; set; }
    }
}
