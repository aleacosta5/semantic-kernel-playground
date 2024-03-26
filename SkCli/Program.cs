using System.Text.Json;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SkCli.Models;

namespace SkCli;

public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// Example usage of the Semantic Kernel based on the examples provided in <see href="https://learn.microsoft.com/en-us/semantic-kernel/overview/"/>. 
    /// </summary>
    public async static Task Main(string[] args)
    {
        var builder = Kernel.CreateBuilder();

        // Alternative using OpenAI
        builder.AddOpenAIChatCompletion(
                "gpt-3.5-turbo",              // OpenAI Model name
                "...your OpenAI Key...");     // OpenAI API Key

        // Alternative using AzureOpenAI
        // builder.AddAzureOpenAIChatCompletion(
        //         "gpt-35-turbo",                      // Azure OpenAI Deployment Name
        //         "https://contoso.openai.azure.com/", // Azure OpenAI Endpoint
        //         "...your Azure OpenAI Key...");      // Azure OpenAI Key

        var kernel = builder.Build();

        if (kernel == null)
        {
            Console.WriteLine("Kernel failed to build.");
            return;
        }

        // Raw Prompt Example
        await RunRawPrompt(kernel);

        // Coding Plugin Example
        await RunCodingPluginExample(kernel);

        // Data Anomaly Detection Plugin Example
        await RunDataAnomalyDetectionPluginExample(kernel);
    }

    /// <summary>
    /// Runs a raw prompt example.
    /// </summary>
    /// <param name="kernel">The kernel to use for running the plugin.</param>
    private static async Task RunRawPrompt(Kernel kernel)
    {
        string prompt = """
            One line TLDR with the fewest words for the following text:
            {{$input}}
            """;

        OpenAIPromptExecutionSettings settings = new() 
        {
            MaxTokens = 100,
            Temperature = 0.7
        };

        var summarize = kernel.CreateFunctionFromPrompt(prompt, executionSettings: settings);

        string text1 = """
            One line TLDR with the fewest words for the following text:
            The universe is a vast and mysterious place. It is home to billions of galaxies, each containing billions of stars. 
            These stars are the building blocks of the universe, and they come in many different sizes and colors. 
            Some stars are so large that they can swallow up entire solar systems, while others are so small that they can fit inside the palm of your hand. 
            The universe is also home to black holes, which are regions of space where gravity is so strong that nothing, not even light, can escape. 
            Black holes are formed when massive stars die and collapse in on themselves. 
            The universe is constantly expanding, and scientists believe that it is over 13.8 billion years old. 
            Despite its vastness, the universe is still largely unexplored, and there is much that we do not know about it.
            """;

        string text2 = """
            1. An object at rest remains at rest, and an object in motion remains in motion at constant speed and in a straight line unless acted on by an unbalanced force.
            2. The acceleration of an object depends on the mass of the object and the amount of force applied.
            3. Whenever one object exerts a force on another object, the second object exerts an equal and opposite on the first.
            """;

        Console.WriteLine(await kernel.InvokeAsync(summarize, new() { ["input"] = text1 }));

        Console.WriteLine(await kernel.InvokeAsync(summarize, new() { ["input"] = text2 }));
    }

    /// <summary>
    /// Runs a coding plugin example.
    /// </summary>
    /// <param name="kernel">The kernel to use for running the plugin.</param>
    private static async Task RunCodingPluginExample(Kernel kernel)
    {
        KernelPlugin? codingPlugin = kernel.ImportPluginFromPromptDirectory(Path.Combine("Plugins", "CodingPlugin"));
        string codingProblem = "Write a function to reverse a Linked List.";
        KernelArguments arguments = new()
        {
            { "input", codingProblem }
        };

        FunctionResult? result = await kernel.InvokeAsync(codingPlugin["ProblemSolver"], arguments);

        if (result != null)
        {
            Console.WriteLine(result);
        }
    }

    /// <summary>
    /// Runs a data anomaly detection plugin example.
    /// </summary>
    /// <param name="kernel">The kernel to use for running the plugin.</param>
    private static async Task RunDataAnomalyDetectionPluginExample(Kernel kernel)
    {
        KernelPlugin? dataAnomalyPlugin = kernel.ImportPluginFromPromptDirectory(Path.Combine("Plugins", "EngineeringPlugin"));

        List<ElectricalData> dataSet =
        [
            new() { TimeStamp = "2024-01-01 00:00", MachineId = "1", Measurement = 110 },
            new() { TimeStamp = "2024-01-01 01:00", MachineId = "1", Measurement = 101 },
            new() { TimeStamp = "2024-01-01 00:00", MachineId = "2", Measurement = 218 },
            new() { TimeStamp = "2024-01-01 01:00", MachineId = "2", Measurement = 224 }
        ];

        string jsonData = JsonSerializer.Serialize(dataSet);

        KernelArguments arguments = new()
        {
            { "data", jsonData }
        };

        FunctionResult? result = await kernel.InvokeAsync(dataAnomalyPlugin["DataAnomalyDetection"], arguments);

        if (result != null)
        {
            Console.WriteLine(result);
        }
    }
}