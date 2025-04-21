// See https://aka.ms/new-console-template for more information



using AIAgentTextSummarization.Brain;
using AIAgentTextSummarization.Options;
using AIAgentTextSummarization.Plugins.FunctionPlugins.TimePlugin;
using AIAgentTextSummarization.Plugins.PromptPlugins.TextSummarizationPromptPlugin;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddConsole().AddConfiguration(configuration.GetSection("Logging"))
);

kernelBuilder.Services.AddOptions();
kernelBuilder.Services.Configure<OpenAIConfiguration>(
    configuration.GetSection(OpenAIConfiguration.SectionName)
);

kernelBuilder.Services.AddTransient<IBrain, Brain>();

var openAIConfiguration =
    configuration.GetSection(OpenAIConfiguration.SectionName).Get<OpenAIConfiguration>()
    ?? throw new Exception("OpenAI configuration is missing");

kernelBuilder.Services.AddOpenAIChatCompletion(
    openAIConfiguration.ModelId,
    openAIConfiguration.ApiKey
);

var kernel = kernelBuilder.Build();
kernel.Plugins.AddFromType<TimePlugin>();
kernel.ImportPluginFromType<TextSummarizationPromptPlugin>(); // Add CalendarPlugin to the kernel. Use ImportPluginFromType instead of AddPluginFromType if you have services to be injected.

// kernelBuilder.Services.AddSingleton<IFunctionInvocationFilter, LoggingFunctionFilter>();
// kernelBuilder.Services.AddSingleton<IAutoFunctionInvocationFilter, LoggingFunctionFilter>();

var logger = kernel.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Starting the AI Calendar Agent");
await kernel.GetRequiredService<IBrain>().Run(kernel);
