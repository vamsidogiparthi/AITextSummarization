// See https://aka.ms/new-console-template for more information



using AIAgentTextSummarization.Options;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddConfiguration(configuration.GetSection("Logging"))
);

kernelBuilder.Services.AddOptions();
kernelBuilder.Services.Configure<OpenAIConfiguration>(
    configuration.GetSection(OpenAIConfiguration.SectionName)
);

var openAIConfiguration =
    configuration.GetSection(OpenAIConfiguration.SectionName).Get<OpenAIConfiguration>()
    ?? throw new Exception("OpenAI configuration is missing");

kernelBuilder.Services.AddOpenAIChatCompletion(
    openAIConfiguration.ModelId,
    openAIConfiguration.ApiKey
);
