using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("People")]
    public class People
    {
        [Key]
        public int Id { get; set; }
        public required string MobileNumber { get; set; }
        public required string Password { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
