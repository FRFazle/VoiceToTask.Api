using System;

namespace VoiceToTask.Api.Models
{
    public class VoiceResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime? ScheduledTime { get; set; }
        public string Priority { get; set; } = "Medium";
    }
}
