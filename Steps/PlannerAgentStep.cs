using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Prompty;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace Steps;

public sealed class PlannerAgentStep : KernelProcessStep
{
    private readonly string _promptyTemplatePath = "PromptyTemplates/planner-agent.prompty";
    
    [KernelFunction]
    public async Task<string> CreateExecutionPlanAsync(Kernel kernel, string goal, string availableSkills = "", string systemState = "", string constraints = "")
    {
        Console.WriteLine($"🧠 PlannerAgentStep: Creating execution plan");
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
                ["available_skills"] = availableSkills,
                ["system_state"] = systemState,
                ["constraints"] = constraints
            };
            
            // Invoke the AI agent
            var result = await kernel.InvokeAsync(function, arguments);
            var response = result.GetValue<string>() ?? "{}";
            
            Console.WriteLine($"✅ PlannerAgentStep: Execution plan created successfully");
            
            // Parse and validate JSON response
            var plan = ParseExecutionPlan(response);
            
            return JsonSerializer.Serialize(plan, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ PlannerAgentStep: Error - {ex.Message}");
            return JsonSerializer.Serialize(new
            {
                execution_plan = new[] { new { task = "Execute goal directly", dependencies = new string[0], success_criteria = "Goal completed" } },
                risk_assessment = new[] { "No risks identified" },
                resource_requirements = new[] { "Basic execution capabilities" },
                estimated_duration = "Unknown",
                confidence = 0.5,
                error = ex.Message
            }, new JsonSerializerOptions { WriteIndented = true });
        }
    }
    
    private ExecutionPlan ParseExecutionPlan(string response)
    {
        try
        {
            // Try to extract JSON from the response (in case it's wrapped in markdown)
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}') + 1;
            
            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var json = response.Substring(jsonStart, jsonEnd - jsonStart);
                return JsonSerializer.Deserialize<ExecutionPlan>(json) ?? CreateDefaultPlan();
            }
            
            return CreateDefaultPlan();
        }
        catch
        {
            return CreateDefaultPlan();
        }
    }
    
    private ExecutionPlan CreateDefaultPlan()
    {
        return new ExecutionPlan
        {
            ExecutionPlanTasks = new[] { new ExecutionTask { Task = "Execute goal directly", Dependencies = new string[0], SuccessCriteria = "Goal completed" } },
            RiskAssessment = new[] { "No risks identified" },
            ResourceRequirements = new[] { "Basic execution capabilities" },
            EstimatedDuration = "Unknown",
            Confidence = 0.5
        };
    }
    
    private class ExecutionPlan
    {
        public ExecutionTask[] ExecutionPlanTasks { get; set; } = Array.Empty<ExecutionTask>();
        public string[] RiskAssessment { get; set; } = Array.Empty<string>();
        public string[] ResourceRequirements { get; set; } = Array.Empty<string>();
        public string EstimatedDuration { get; set; } = string.Empty;
        public double Confidence { get; set; }
    }
    
    private class ExecutionTask
    {
        public string Task { get; set; } = string.Empty;
        public string[] Dependencies { get; set; } = Array.Empty<string>();
        public string SuccessCriteria { get; set; } = string.Empty;
    }
} 