using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoiceToTask.Api.Models
{
    [Table("WorkTask")]
    public class WorkTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime ScheduledTime { get; set; }

        public string Materials { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public string Priority { get; set; } = "Medium";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
