using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string ImageURL { get; set; }
    }
}
