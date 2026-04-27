using System;
using System.Collections.Generic;
using System.Text;

namespace TodoCliApp.Models
{

    // model for how the todo task should look like
    public class Todo 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public TodoStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


        // Returns a formatted string representation of the Todo task for easy display and debugging.
        public override string ToString()
        {
            return $"[{Id}] - {Title} - Priority: {Priority}, Status: {Status}, Due: {DueDate.ToShortDateString()}";
        }
    }
}
