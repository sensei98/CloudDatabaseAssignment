using OnlineStore.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class Product: IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? ImageURL { get; set; }
    }
}
