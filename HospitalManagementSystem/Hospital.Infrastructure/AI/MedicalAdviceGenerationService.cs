using Hospital.Application.Abstractions;
using OpenAI_API;
using OpenAI_API.Chat;
using System.IO;

namespace Hospital.Infrastructure.AI
{
    public class MedicalAdviceGenerationService : IMedicalAdviceGenerationService
    {
        private readonly string _chatSetupMessage = "Please answer with a single sentence without any additions.";
        private readonly string _apiKey;
        private readonly string _gptModel;

        public MedicalAdviceGenerationService()
        {
            _apiKey = DotNetEnv.Env.GetString("OPENAI_API_KEY");
            _gptModel = DotNetEnv.Env.GetString("OPENAI_MODEL");
        }

        public async Task<string> GenerateMedicalAdviceForPatient(IEnumerable<string> patientRecentIllnesses)
        {
            APIAuthentication apiAuthentication = new APIAuthentication(_apiKey);
            OpenAIAPI api = new OpenAIAPI(apiAuthentication);

            string prompt = $"Generate a single practical and friendly medical advice to make the patient feel better " + 
                $"without mentioning directly his/her illnesses for the patient who recently had the following illnesses: {string.Join(", ", patientRecentIllnesses)}";

            var chatRequest = new ChatRequest
            {
                Model = _gptModel,
                Messages =
                [
                    new ChatMessage { Role = ChatMessageRole.System, TextContent = _chatSetupMessage },
                    new ChatMessage { Role = ChatMessageRole.User, TextContent = prompt}
                ]
            };

            var result = await api.Chat.CreateChatCompletionAsync(chatRequest);

            return result.Choices[0].Message.TextContent.Trim();
        }
    }
}
