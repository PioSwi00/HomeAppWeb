﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
