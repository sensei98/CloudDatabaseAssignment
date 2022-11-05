using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}
