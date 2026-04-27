using System;
using System.Collections.Generic;
using System.Text;

namespace TodoCliApp.Models
{
    public class Todo 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public TodoStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Title} - Priority: {Priority}, Status: {Status}, Due: {DueDate.ToShortDateString()}";
        }
    }
}
