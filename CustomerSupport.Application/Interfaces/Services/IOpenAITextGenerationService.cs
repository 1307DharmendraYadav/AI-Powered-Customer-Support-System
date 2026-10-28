using CustomerSupport.Application.DTOs.AI;

namespace CustomerSupport.Application.Interfaces.Services
{
    public interface IOpenAITextGenerationService
    {
        // Generates plain text from application-controlled
        // instructions and prepared input context.
        Task<AITextResultDTO> GenerateAsync(
            string developerInstructions,
            string userInput,
            CancellationToken cancellationToken = default);
    }
}
