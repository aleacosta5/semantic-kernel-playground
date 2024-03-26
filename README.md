# semantic-kernel-playground

This is a C# application that demonstrates the usage of the Semantic Kernel based on the examples provided in [Microsoft's Semantic Kernel Overview](https://learn.microsoft.com/en-us/semantic-kernel/overview/).

## Getting Started

To run the application, you need to have .NET installed on your machine.

## Usage

The main entry point for the application is the `Main` method in the `Program` class.

```csharp
public async static Task Main(string[] args)
{
    var builder = Kernel.CreateBuilder();

    // Alternative using OpenAI
    builder.AddOpenAIChatCompletion(
            "gpt-3.5-turbo",              // OpenAI Model name
            "...your OpenAI Key...");     // OpenAI API Key

    var kernel = builder.Build();

    // Raw Prompt Example
    await RunRawPrompt(kernel);

    // Coding Plugin Example
    await RunCodingPluginExample(kernel);

    // Data Anomaly Detection Plugin Example
    await RunDataAnomalyDetectionPluginExample(kernel);
}
```
The `Main` method creates a `Kernel` builder, adds an OpenAI chat completion model to it, and then builds the kernel. It then runs a series of examples using the built kernel.

## Examples

The application includes three examples:

1. `RunRawPrompt`: This method runs a raw prompt example using the built kernel.
2. `RunCodingPluginExample`: This method runs a coding plugin example using the built kernel.
3. `RunDataAnomalyDetectionPluginExample`: This method runs a data anomaly detection plugin example using the built kernel.

## Dependencies

- `System.Text.Json`
- `Microsoft.SemanticKernel`
- `Microsoft.SemanticKernel.Connectors.OpenAI`
- `SkCli.Models`

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.