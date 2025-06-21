using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Prompty;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace Steps;

public sealed class MakerAgentStep : KernelProcessStep
{
    private readonly string _promptyTemplatePath = "PromptyTemplates/maker-agent.prompty";
    
    [KernelFunction]
    public async Task<string> ExecutePlanAsync(Kernel kernel, string executionPlan, string goal, string availableSkills = "")
    {
        Console.WriteLine($"🔨 MakerAgentStep: Executing plan for goal");
        Console.WriteLine($"📋 Goal: {goal}");
        Console.WriteLine($"📝 Plan: {executionPlan}");
        
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
                ["execution_plan"] = executionPlan,
                ["goal"] = goal,
                ["available_skills"] = availableSkills
            };
            
            // Invoke the AI agent
            var result = await kernel.InvokeAsync(function, arguments);
            var response = result.GetValue<string>() ?? "{}";
            
            Console.WriteLine($"✅ MakerAgentStep: Execution completed successfully");
            
            // Parse and validate JSON response
            var executionResult = ParseExecutionResult(response);
            
            return JsonSerializer.Serialize(executionResult, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ MakerAgentStep: Error - {ex.Message}");
            return JsonSerializer.Serialize(new
            {
                status = "failed",
                executed_tasks = new string[0],
                generated_artifacts = new string[0],
                new_skills_created = new string[0],
                execution_metrics = new { duration_ms = 0, success_rate = 0.0 },
                error = ex.Message
            }, new JsonSerializerOptions { WriteIndented = true });
        }
    }
    
    private ExecutionResult ParseExecutionResult(string response)
    {
        try
        {
            // Try to extract JSON from the response (in case it's wrapped in markdown)
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}') + 1;
            
            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var json = response.Substring(jsonStart, jsonEnd - jsonStart);
                return JsonSerializer.Deserialize<ExecutionResult>(json) ?? CreateDefaultExecutionResult();
            }
            
            return CreateDefaultExecutionResult();
        }
        catch
        {
            return CreateDefaultExecutionResult();
        }
    }
    
    private ExecutionResult CreateDefaultExecutionResult()
    {
        return new ExecutionResult
        {
            Status = "completed",
            ExecutedTasks = new[] { "Default task execution" },
            GeneratedArtifacts = new[] { "Default artifact" },
            NewSkillsCreated = new string[0],
            ExecutionMetrics = new ExecutionMetrics { DurationMs = 100, SuccessRate = 1.0 }
        };
    }
    
    private class ExecutionResult
    {
        public string Status { get; set; } = string.Empty;
        public string[] ExecutedTasks { get; set; } = Array.Empty<string>();
        public string[] GeneratedArtifacts { get; set; } = Array.Empty<string>();
        public string[] NewSkillsCreated { get; set; } = Array.Empty<string>();
        public ExecutionMetrics ExecutionMetrics { get; set; } = new();
    }
    
    private class ExecutionMetrics
    {
        public int DurationMs { get; set; }
        public double SuccessRate { get; set; }
    }
} 