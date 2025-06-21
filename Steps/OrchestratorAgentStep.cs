using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Prompty;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace Steps;

public sealed class OrchestratorAgentStep : KernelProcessStep
{
    private readonly string _promptyTemplatePath = "PromptyTemplates/orchestrator-agent.prompty";
    
    [KernelFunction]
    public async Task<string> OrchestrateGoalAsync(Kernel kernel, string goal, string context = "", string priority = "medium", string availableSkills = "")
    {
        Console.WriteLine($"🧠 OrchestratorAgentStep: Analyzing goal for orchestration");
        Console.WriteLine($"📋 Goal: {goal}");
        
        try
        {
            // Load the Prompty template
            var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
#pragma warning disable SKEXP0040 // Prompty is experimental
            var function = kernel.CreateFunctionFromPromptyFile(_promptyTemplatePath, fileProvider);
#pragma warning restore SKEXP0040
            
            // Prepare arguments
            var arguments = new KernelArguments
            {
                ["goal"] = goal,
                ["context"] = context,
                ["priority"] = priority,
                ["available_skills"] = availableSkills
            };
            
            // Invoke the AI agent
            var result = await kernel.InvokeAsync(function, arguments);
            var response = result.GetValue<string>() ?? "{}";
            
            Console.WriteLine($"✅ OrchestratorAgentStep: Decision made successfully");
            
            // Parse and validate JSON response
            var decision = ParseOrchestratorDecision(response);
            
            return JsonSerializer.Serialize(decision, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ OrchestratorAgentStep: Error - {ex.Message}");
            return JsonSerializer.Serialize(new
            {
                decision = "direct_execution",
                reasoning = "Fallback due to orchestration error",
                next_steps = new[] { "Proceed with direct execution" },
                confidence = 0.5,
                error = ex.Message
            }, new JsonSerializerOptions { WriteIndented = true });
        }
    }
    
    private OrchestratorDecision ParseOrchestratorDecision(string response)
    {
        try
        {
            // Try to extract JSON from the response (in case it's wrapped in markdown)
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}') + 1;
            
            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var json = response.Substring(jsonStart, jsonEnd - jsonStart);
                return JsonSerializer.Deserialize<OrchestratorDecision>(json) ?? CreateDefaultDecision();
            }
            
            return CreateDefaultDecision();
        }
        catch
        {
            return CreateDefaultDecision();
        }
    }
    
    private OrchestratorDecision CreateDefaultDecision()
    {
        return new OrchestratorDecision
        {
            Decision = "direct_execution",
            Reasoning = "Default decision due to parsing error",
            NextSteps = new[] { "Proceed with direct execution" },
            Confidence = 0.5
        };
    }
    
    private class OrchestratorDecision
    {
        public string Decision { get; set; } = string.Empty;
        public string Reasoning { get; set; } = string.Empty;
        public string[] NextSteps { get; set; } = Array.Empty<string>();
        public double Confidence { get; set; }
    }
} 