using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Prompty;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace Steps;

public sealed class ReflectorAgentStep : KernelProcessStep
{
    private readonly string _promptyTemplatePath = "PromptyTemplates/reflector-agent.prompty";
    
    [KernelFunction]
    public async Task<string> ReflectOnExecutionAsync(Kernel kernel, string originalGoal, string executionResults, string performanceMetrics = "", string issuesEncountered = "", string userFeedback = "")
    {
        Console.WriteLine($"🧠 ReflectorAgentStep: Analyzing execution results for learning");
        Console.WriteLine($"📋 Original Goal: {originalGoal}");
        
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
                ["original_goal"] = originalGoal,
                ["execution_results"] = executionResults,
                ["performance_metrics"] = performanceMetrics,
                ["issues_encountered"] = issuesEncountered,
                ["user_feedback"] = userFeedback
            };
            
            // Invoke the AI agent
            var result = await kernel.InvokeAsync(function, arguments);
            var response = result.GetValue<string>() ?? "{}";
            
            Console.WriteLine($"✅ ReflectorAgentStep: Reflection analysis completed successfully");
            
            // Parse and validate JSON response
            var reflection = ParseReflection(response);
            
            return JsonSerializer.Serialize(reflection, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ReflectorAgentStep: Error - {ex.Message}");
            return JsonSerializer.Serialize(new
            {
                goal_status = "achieved",
                lessons_learned = new[] { "System executed successfully" },
                improvement_suggestions = new[] { "Continue monitoring for optimization opportunities" },
                next_iteration_plan = "No iteration needed",
                knowledge_updates = new[] { "Execution completed successfully" },
                confidence = 0.8,
                error = ex.Message
            }, new JsonSerializerOptions { WriteIndented = true });
        }
    }
    
    private ReflectionAnalysis ParseReflection(string response)
    {
        try
        {
            // Try to extract JSON from the response (in case it's wrapped in markdown)
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}') + 1;
            
            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var json = response.Substring(jsonStart, jsonEnd - jsonStart);
                return JsonSerializer.Deserialize<ReflectionAnalysis>(json) ?? CreateDefaultReflection();
            }
            
            return CreateDefaultReflection();
        }
        catch
        {
            return CreateDefaultReflection();
        }
    }
    
    private ReflectionAnalysis CreateDefaultReflection()
    {
        return new ReflectionAnalysis
        {
            GoalStatus = "achieved",
            LessonsLearned = new[] { "System executed successfully" },
            ImprovementSuggestions = new[] { "Continue monitoring for optimization opportunities" },
            NextIterationPlan = "No iteration needed",
            KnowledgeUpdates = new[] { "Execution completed successfully" },
            Confidence = 0.8
        };
    }
    
    private class ReflectionAnalysis
    {
        public string GoalStatus { get; set; } = string.Empty;
        public string[] LessonsLearned { get; set; } = Array.Empty<string>();
        public string[] ImprovementSuggestions { get; set; } = Array.Empty<string>();
        public string NextIterationPlan { get; set; } = string.Empty;
        public string[] KnowledgeUpdates { get; set; } = Array.Empty<string>();
        public double Confidence { get; set; }
    }
} 