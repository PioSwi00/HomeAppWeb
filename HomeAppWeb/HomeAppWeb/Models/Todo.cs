using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("ToDos")]
    public class ToDo
    {
        [Key]
        public Guid ToDoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public ToDoStatus Status { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public User Creator { get; set; }

        public ICollection<ToDoUser> AssignedUsers { get; set; } = new List<ToDoUser>();
    }


    public enum ToDoStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public class ToDoUser
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ToDoId { get; set; }
        [ForeignKey("ToDoId")]
        public ToDo ToDo { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
