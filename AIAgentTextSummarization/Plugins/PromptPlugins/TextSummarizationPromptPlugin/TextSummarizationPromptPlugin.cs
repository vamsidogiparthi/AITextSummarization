using System.ComponentModel;
using AIAgentTextSummarization.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

namespace AIAgentTextSummarization.Plugins.PromptPlugins.TextSummarizationPromptPlugin;

public class TextSummarizationPromptPlugin(ILogger<TextSummarizationPromptPlugin> logger)
{
    [KernelFunction("summarize_text")]
    [Description("Summarize the given text.")]
    public async Task<FunctionResult?> GetSummarizedText(
        [Description("input text to be summarize")] string input,
        Kernel kernel
    )
    {
        var handlebarsPromptYaml = EmbeddedResource.Read("TextSummarizationYamlTemplate.yaml");
        var templateFactory = new HandlebarsPromptTemplateFactory();
        var function = kernel.CreateFunctionFromPromptYaml(handlebarsPromptYaml, templateFactory);

        var arguments = new KernelArguments() { { "input", input } };

        var response = await kernel.InvokeAsync(function, arguments);
        logger.LogInformation("Response: {Response}", response);
        return response;
    }
}
