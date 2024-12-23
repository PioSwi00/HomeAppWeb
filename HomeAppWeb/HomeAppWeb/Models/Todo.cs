using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public Guid TodoId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
