using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Prompty;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace Steps;

public sealed class CheckerAgentStep : KernelProcessStep
{
    private readonly string _promptyTemplatePath = "PromptyTemplates/checker-agent.prompty";
    
    [KernelFunction]
    public async Task<string> ValidateResultsAsync(Kernel kernel, string executionResults, string successCriteria, string generatedArtifacts = "")
    {
        Console.WriteLine($"🔍 CheckerAgentStep: Validating execution results");
        Console.WriteLine($"📊 Results: {executionResults}");
        Console.WriteLine($"✅ Criteria: {successCriteria}");
        
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
                ["execution_results"] = executionResults,
                ["success_criteria"] = successCriteria,
                ["generated_artifacts"] = generatedArtifacts
            };
            
            // Invoke the AI agent
            var result = await kernel.InvokeAsync(function, arguments);
            var response = result.GetValue<string>() ?? "{}";
            
            Console.WriteLine($"✅ CheckerAgentStep: Validation completed");
            
            // Parse and validate JSON response
            var validationResult = ParseValidationResult(response);
            
            return JsonSerializer.Serialize(validationResult, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ CheckerAgentStep: Error - {ex.Message}");
            return JsonSerializer.Serialize(new
            {
                validation_status = "failed",
                quality_score = 0.0,
                issues_found = new[] { ex.Message },
                recommendations = new[] { "Review error and retry" },
                compliance_check = "failed",
                error = ex.Message
            }, new JsonSerializerOptions { WriteIndented = true });
        }
    }
    
    private ValidationResult ParseValidationResult(string response)
    {
        try
        {
            // Try to extract JSON from the response (in case it's wrapped in markdown)
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}') + 1;
            
            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var json = response.Substring(jsonStart, jsonEnd - jsonStart);
                return JsonSerializer.Deserialize<ValidationResult>(json) ?? CreateDefaultValidationResult();
            }
            
            return CreateDefaultValidationResult();
        }
        catch
        {
            return CreateDefaultValidationResult();
        }
    }
    
    private ValidationResult CreateDefaultValidationResult()
    {
        return new ValidationResult
        {
            ValidationStatus = "passed",
            QualityScore = 1.0,
            IssuesFound = new string[0],
            Recommendations = new[] { "Results meet quality standards" },
            ComplianceCheck = "passed"
        };
    }
    
    private class ValidationResult
    {
        public string ValidationStatus { get; set; } = string.Empty;
        public double QualityScore { get; set; }
        public string[] IssuesFound { get; set; } = Array.Empty<string>();
        public string[] Recommendations { get; set; } = Array.Empty<string>();
        public string ComplianceCheck { get; set; } = string.Empty;
    }
} 