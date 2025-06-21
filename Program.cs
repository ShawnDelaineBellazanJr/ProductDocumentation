using Azure.Identity;
using Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Process;
using Steps;

namespace ProductDocumentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Build configuration
        IConfiguration configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        // Run the complete Meta-Programmable Self-Evolving System
        await RunCompleteMetaProgrammableSystem(configuration);
    }

    private static async Task RunCompleteMetaProgrammableSystem(IConfiguration configuration)
    {
        Console.WriteLine("\n🚀 === META-PROGRAMMABLE SELF-EVOLVING SYSTEM ===");
        Console.WriteLine("🎯 Implementing the complete PMCR (Plan-Make-Check-Reflect) loop");
        Console.WriteLine("🔄 Enabling the Strange Loop for continuous self-improvement\n");
        
        try
        {
            // Create the process builder for the complete system
            ProcessBuilder processBuilder = new("CompleteMetaProgrammableSystem");

            // Add all five core agents in the PMCR loop
            var orchestratorStep = processBuilder.AddStepFromType<OrchestratorAgentStep>();
            var plannerStep = processBuilder.AddStepFromType<PlannerAgentStep>();
            var makerStep = processBuilder.AddStepFromType<MakerAgentStep>();
            var checkerStep = processBuilder.AddStepFromType<CheckerAgentStep>();
            var reflectorStep = processBuilder.AddStepFromType<ReflectorAgentStep>();

            // Add supporting infrastructure steps
            var knowledgeManagementStep = processBuilder.AddStepFromType<KnowledgeManagementStep>();
            var auditLoggingStep = processBuilder.AddStepFromType<AuditLoggingStep>();

            // Implement the complete PMCR orchestration flow
            processBuilder
                .OnInputEvent("user_goal_received")
                .SendEventTo(new ProcessFunctionTargetBuilder(orchestratorStep));

            // Orchestrator decides next steps
            orchestratorStep
                .OnFunctionResult()
                .SendEventTo(new ProcessFunctionTargetBuilder(plannerStep));

            // Planner creates execution plan
            plannerStep
                .OnFunctionResult()
                .SendEventTo(new ProcessFunctionTargetBuilder(makerStep));

            // Maker executes the plan
            makerStep
                .OnFunctionResult()
                .SendEventTo(new ProcessFunctionTargetBuilder(checkerStep));

            // Checker validates results
            checkerStep
                .OnFunctionResult()
                .SendEventTo(new ProcessFunctionTargetBuilder(reflectorStep));

            // Reflector learns and improves
            reflectorStep
                .OnFunctionResult()
                .SendEventTo(new ProcessFunctionTargetBuilder(knowledgeManagementStep));

            // Knowledge management persists learnings
            knowledgeManagementStep
                .OnFunctionResult()
                .SendEventTo(new ProcessFunctionTargetBuilder(auditLoggingStep));

            // Audit logging completes the cycle
            auditLoggingStep
                .OnFunctionResult()
                .StopProcess();

            // Create kernel with enhanced configuration
            Kernel kernel = CreateEnhancedKernel(configuration);
            KernelProcess kernelProcess = processBuilder.Build();

            // Test with a complex goal that will exercise the full system
            var complexGoal = "Create a comprehensive product documentation system with API generation, automated testing, and continuous deployment capabilities";
            
            Console.WriteLine($"🎯 Testing Complete Meta-Programmable System");
            Console.WriteLine($"📋 Goal: {complexGoal}");
            Console.WriteLine($"🔄 Starting PMCR loop execution...\n");
            
            await using var runningProcess = await kernelProcess!.StartAsync(kernel, new() { 
                Id = "user_goal_received", 
                Data = complexGoal 
            });

            Console.WriteLine($"\n✅ Complete Meta-Programmable System execution finished");
            Console.WriteLine($"🔄 The system has completed one full PMCR cycle");
            Console.WriteLine($"🧠 Knowledge has been captured and system has evolved");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Complete Meta-Programmable System Error: {ex.Message}");
            Console.WriteLine($"🔍 Stack Trace: {ex.StackTrace}");
        }
    }

    private static Kernel CreateEnhancedKernel(IConfiguration configuration)
    {
        string deploymentName = configuration["AZUREOPENAI_DEPLOYMENT_NAME"] ?? throw new InvalidOperationException("User secret AZUREOPENAI_DEPLOYMENT_NAME is not configured.");
        string endpoint = configuration["AZUREOPENAI_ENDPOINT"] ?? throw new InvalidOperationException("User secret AZUREOPENAI_ENDPOINT is not configured.");

        // Create enhanced kernel with all necessary components
        Kernel kernel = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(deploymentName, endpoint, new AzureCliCredential())
            .Build();

        // Register console output filter for visibility
        kernel.FunctionInvocationFilters.Add(new ConsoleOutputFunctionInvocationFilter());

        return kernel;
    }

    // Legacy methods for reference (keeping for backward compatibility)
    private static async Task ImperativeProcessAsync(IConfiguration configuration)
    {
        Console.WriteLine("\n=== Legacy Imperative Process ===\n");

        const string StartEvent = "input_message_received";

        // Create process
        ProcessBuilder process = new("ProductDocumentation");

        // Build steps
        ProcessStepBuilder getProductInfoStep = process.AddStepFromType<GetProductInfoStep>();
        ProcessStepBuilder generateDocumentationStep = process.AddStepFromType<GenerateDocumentationStep>();
        ProcessStepBuilder publishDocumentationStep = process.AddStepFromType<PublishDocumentationStep>();

        // Define relationship between steps
        process
            .OnInputEvent(StartEvent)
            .SendEventTo(new ProcessFunctionTargetBuilder(getProductInfoStep));

        getProductInfoStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(generateDocumentationStep));

        generateDocumentationStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(publishDocumentationStep));

        publishDocumentationStep
            .OnFunctionResult()
            .StopProcess();

        // Create kernel and build process from defined steps.
        Kernel kernel = CreateEnhancedKernel(configuration);
        KernelProcess kernelProcess = process.Build();

        // Start process
        await using var runningProcess = await kernelProcess!.StartAsync(kernel, new() { Id = StartEvent });
    }

    private static async Task DeclarativeProcessAsync(IConfiguration configuration)
    {
        Console.WriteLine("\n=== Legacy Declarative Process ===\n");

        const string FileName = "meta-programmable-system-mutation1.process.yaml";
        const string StartEvent = "input_message_received";

        // Read Process YAML content
        string filePath = Path.Combine(AppContext.BaseDirectory, FileName);
        string content = File.ReadAllText(filePath);

        // Create kernel and load process from YAML
        Kernel kernel = CreateEnhancedKernel(configuration);
        KernelProcess? kernelProcess = await ProcessBuilder.LoadFromYamlAsync(content);

        // Start process
        await using var runningProcess = await kernelProcess!.StartAsync(kernel, new() { Id = StartEvent });
    }

    private static async Task RunImperativeMetaProgrammableSystem(IConfiguration configuration)
    {
        Console.WriteLine("\n=== Legacy Imperative Meta-Programmable System ===");
        
        // Create the process builder
        ProcessBuilder processBuilder = new("MetaProgrammableSystem");

        // Add the steps
        var getProductInfoStep = processBuilder.AddStepFromType<GetProductInfoStep>();
        var productDocumentationStep = processBuilder.AddStepFromType<PublishDocumentationStep>();
        var knowledgeManagementStep = processBuilder.AddStepFromType<KnowledgeManagementStep>();
        var finalPublishStep = processBuilder.AddStepFromType<FinalPublishStep>();

        // Orchestrate the events
        processBuilder
            .OnInputEvent("Start")
            .SendEventTo(new ProcessFunctionTargetBuilder(getProductInfoStep));

        getProductInfoStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(productDocumentationStep));

        productDocumentationStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(knowledgeManagementStep));

        knowledgeManagementStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(finalPublishStep));

        // Use the existing kernel configuration
        Kernel kernel = CreateEnhancedKernel(configuration);
        KernelProcess kernelProcess = processBuilder.Build();

        // Start process
        await using var runningProcess = await kernelProcess!.StartAsync(kernel, new() { Id = "Start", Data = "QuantumPhone" });
    }

    private static async Task RunAIAgentEnhancedSystem(IConfiguration configuration)
    {
        Console.WriteLine("\n=== Legacy AI Agent Enhanced Meta-Programmable System ===");
        
        try
        {
            // Create the process builder
            ProcessBuilder processBuilder = new("AIAgentEnhancedSystem");

            // Add just the orchestrator step for testing
            var orchestratorStep = processBuilder.AddStepFromType<OrchestratorAgentStep>();

            // Simple orchestration
            processBuilder
                .OnInputEvent("Start")
                .SendEventTo(new ProcessFunctionTargetBuilder(orchestratorStep));

            // Use the existing kernel configuration
            Kernel kernel = CreateEnhancedKernel(configuration);
            KernelProcess kernelProcess = processBuilder.Build();

            // Start process with a simple goal
            var simpleGoal = "Generate documentation for a new product";
            
            Console.WriteLine($"🎯 Testing AI Agent with goal: {simpleGoal}");
            await using var runningProcess = await kernelProcess!.StartAsync(kernel, new() { Id = "Start", Data = simpleGoal });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ AI Agent Enhanced System Error: {ex.Message}");
            Console.WriteLine($"🔍 Stack Trace: {ex.StackTrace}");
        }
    }
}
