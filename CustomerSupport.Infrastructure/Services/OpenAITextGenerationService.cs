using CustomerSupport.Application.DTOs.AI;
using CustomerSupport.Application.Exceptions;
using CustomerSupport.Application.Interfaces.Services;
using CustomerSupport.Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Responses;
using System.ClientModel;

#pragma warning disable OPENAI001

namespace CustomerSupport.Infrastructure.Services
{
    public sealed class OpenAITextGenerationService : IOpenAITextGenerationService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly OpenAISettings _settings;
        private readonly ILogger<OpenAITextGenerationService> _logger;

        public OpenAITextGenerationService(
            OpenAIClient openAIClient,
            IOptions<OpenAISettings> settings,
            ILogger<OpenAITextGenerationService> logger)
        {
            _openAIClient = openAIClient;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<AITextResultDTO> GenerateAsync(
            string developerInstructions,
            string userInput,
            CancellationToken cancellationToken = default)
        {
            // Step 1: Reject invalid prompts before a billable call.
            if (string.IsNullOrWhiteSpace(developerInstructions) ||
                string.IsNullOrWhiteSpace(userInput))
            {
                throw new ApplicationValidationException(
                    ["AI instructions and input are required."]);
            }

            // Step 2: Build one Responses API request.
            CreateResponseOptions options = new()
            {
                Model = _settings.Model,
                MaxOutputTokenCount = _settings.MaxOutputTokens,
                StoredOutputEnabled = false
            };

            options.InputItems.Add(
                ResponseItem.CreateDeveloperMessageItem(developerInstructions));

            options.InputItems.Add(
                ResponseItem.CreateUserMessageItem(userInput));

            // Step 3: Apply the existing application timeout.
            using CancellationTokenSource timeout =
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            timeout.CancelAfter(TimeSpan.FromSeconds(_settings.TimeoutSeconds));

            try
            {
                // Step 4: Send the request to OpenAI.
                ResponsesClient responsesClient = _openAIClient.GetResponsesClient();

                ResponseResult result = await responsesClient
                    .CreateResponseAsync(options, timeout.Token);

                // Step 5: Validate the response.
                if (result.Status != ResponseStatus.Completed)
                {
                    throw new AIException("OpenAI did not complete the requested feature.");
                }

                string output = result.GetOutputText();
                if (string.IsNullOrWhiteSpace(output))
                {
                    throw new AIException("OpenAI returned empty output.");
                }

                // Log metadata, never the ticket text or API key.
                _logger.LogInformation(
                    "Ticket AI generation completed. " +
                    "ResponseId: {ResponseId}, Tokens: {Tokens}",
                    result.Id,
                    result.Usage?.TotalTokenCount);

                // Step 6: Return our own provider-neutral DTO.
                return new AITextResultDTO
                {
                    Text = output.Trim(),
                    Model = result.Model ?? _settings.Model,
                    ResponseId = result.Id ?? string.Empty,
                    InputTokens = result.Usage?.InputTokenCount ?? 0,
                    OutputTokens = result.Usage?.OutputTokenCount ?? 0,
                    TotalTokens = result.Usage?.TotalTokenCount ?? 0
                };
            }
            catch (ClientResultException ex)
            {
                _logger.LogWarning("OpenAI returned HTTP status {Status}.", ex.Status);
                throw new AIException("OpenAI could not complete the requested feature.");
            }
            catch (OperationCanceledException)
                when (!cancellationToken.IsCancellationRequested)
            {
                throw new AIException("The AI request took too long to complete.");
            }
            catch (HttpRequestException)
            {
                throw new AIException("Unable to communicate with the OpenAI service.");
            }
        }
    }
}
