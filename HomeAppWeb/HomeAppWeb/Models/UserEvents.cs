using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("UserEvents")]
    public class UserEvent
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } 
        public User User { get; set; }
        [ForeignKey(nameof(Event))]
        public string EventId { get; set; }

        public Event Event { get; set; }
    }
}
