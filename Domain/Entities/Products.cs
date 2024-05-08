using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
