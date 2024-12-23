using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("Bills")]
    public class Bill
    {
        [Key]
        public Guid BillId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
