using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("ActionLogs")]
    public class ActionLog
    {
        [Key]
        public Guid ActionLogId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
