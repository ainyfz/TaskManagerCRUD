using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerApps.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
