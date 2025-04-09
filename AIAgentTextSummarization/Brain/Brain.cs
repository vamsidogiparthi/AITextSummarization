using Microsoft.SemanticKernel.ChatCompletion;

namespace AIAgentTextSummarization.Brain
{
    public class Brain(IServiceProvider serviceProvider, ILogger<Brain> logger)
    {
        public async Task Run(Kernel kernel)
        {
            var chatCompletionService = serviceProvider.GetService<IChatCompletionService>();
        }
    }
}
