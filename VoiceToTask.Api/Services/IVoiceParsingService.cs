using VoiceToTask.Api.Models;

namespace VoiceToTask.Api.Services
{
    public interface IVoiceParsingService
    {
        VoiceResponse Parse(string text);

    }
}
