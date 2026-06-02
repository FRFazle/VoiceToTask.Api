namespace VoiceToTask.Api.DTOs
{
    public class WorkTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime ScheduledTime { get; set; }
        public string Materials { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Medium";

    }
}
