using AIAgentTextSummarization.Services;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIAgentTextSummarization.Brain;

public interface IBrain
{
    Task Run(Kernel kernel);
}

public class Brain(IServiceProvider serviceProvider, ILogger<Brain> logger) : IBrain
{
    public async Task Run(Kernel kernel)
    {
        logger.LogInformation("Starting AI Agent Text Summarization...");
        var conversation = EmbeddedResource.Read("Conversation.txt");
        // var chatCompletionService =
        //     serviceProvider.GetService<IChatCompletionService>()
        //     ?? throw new Exception("ChatCompletionService is not registered.");
        // OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        // {
        //     FunctionChoiceBehavior = FunctionChoiceBehavior.Required(),
        // };

        // var history = new ChatHistory();
        // history.AddUserMessage(conversation);

        // var result = await chatCompletionService.GetChatMessageContentAsync(
        //     conversation,
        //     executionSettings: openAIPromptExecutionSettings,
        //     kernel: kernel
        // );

        // logger.LogInformation("Result: {Result}", result.Content);

        var result = await kernel.InvokeAsync(
            "TextSummarizationPromptPlugin",
            "summarize_text",
            new KernelArguments() { { "input", conversation } }
        );

        logger.LogInformation("Result: {Result}", result.ToString());
    }
}
