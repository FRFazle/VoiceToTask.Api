using System;
using VoiceToTask.Api.Models;

namespace VoiceToTask.Api.Services
{
    public class VoiceParsingService : IVoiceParsingService
    {
        public VoiceResponse Parse(string text)
        {
            text = text.ToLower();

            var response = new VoiceResponse
            {
                Title = text,
                Description = text,
                Location = ExtractLocation(text),
                ScheduledTime = ExtractTime(text),
                Priority = DetectPriority(text)
            };

            return response;
        }

        private string ExtractLocation(string text)
        {
            if (text.Contains("dhaka")) return "Dhaka";
            if (text.Contains("chittagong")) return "Chittagong";
            return "Not detected";
        }

        private DateTime? ExtractTime(string text)
        {
            if (text.Contains("tomorrow"))
                return DateTime.UtcNow.AddDays(1);

            if (text.Contains("today"))
                return DateTime.UtcNow;

            return null;
        }

        private string DetectPriority(string text)
        {
            if (text.Contains("urgent") || text.Contains("asap") || text.Contains("emergency"))
                return "High";

            if (text.Contains("important"))
                return "Medium";

            return "Low";
        }
    }
}