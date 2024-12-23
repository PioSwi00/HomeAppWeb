using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("TodoItems")]
    public class TodoItem
    {
        [Key]
        public Guid TodoItemId { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid TodoId { get; set; }

        [ForeignKey(nameof(TodoId))]
        public Todo Todo { get; set; }
    }
}
