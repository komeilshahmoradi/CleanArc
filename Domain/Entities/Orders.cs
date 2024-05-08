using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }

        public People People { get; set; }
        public Products Product { get; set; }
    }
}
