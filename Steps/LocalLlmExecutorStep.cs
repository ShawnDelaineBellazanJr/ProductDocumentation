using Microsoft.SemanticKernel;
using System.Text.Json;

namespace Steps;

public sealed class LocalLlmExecutorStep : KernelProcessStep
{
    [KernelFunction]
    public string ExecuteLocalLlm(string prompt, string model = "llama3", double temperature = 0.7)
    {
        Console.WriteLine($"🤖 LocalLlmExecutorStep: Executing with model {model}");
        Console.WriteLine($"📝 Prompt: {prompt}");
        
        // This is a stub implementation - in production this would call actual local LLM endpoints
        var response = SimulateLocalLlmResponse(prompt, model, temperature);
        
        Console.WriteLine($"✅ LocalLlmExecutorStep: Generated response successfully");
        
        return response;
    }
    
    private string SimulateLocalLlmResponse(string prompt, string model, double temperature)
    {
        // Simulate local LLM response - in production this would make HTTP calls to Ollama, etc.
        var response = new
        {
            model = model,
            temperature = temperature,
            response = $"Simulated response from {model} for: {prompt.Substring(0, Math.Min(50, prompt.Length))}...",
            timestamp = DateTime.UtcNow
        };
        
        return JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
    }
} 