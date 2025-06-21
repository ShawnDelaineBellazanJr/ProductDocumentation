using Microsoft.SemanticKernel;
using System.Text;

namespace Steps;

public sealed class ApiGeneratorStep : KernelProcessStep
{
    [KernelFunction]
    public string GenerateApi(string apiSpec, string endpointName)
    {
        Console.WriteLine($"🌐 ApiGeneratorStep: Generating API endpoint: {endpointName}");
        
        // This is a stub implementation - in production this would use NSwag to generate controllers
        var controllerCode = GenerateControllerCode(apiSpec, endpointName);
        
        Console.WriteLine($"✅ ApiGeneratorStep: Generated API controller successfully");
        
        return $"API endpoint '{endpointName}' generated successfully. Controller code length: {controllerCode.Length} characters";
    }
    
    private string GenerateControllerCode(string apiSpec, string endpointName)
    {
        // Stub implementation - would use NSwag for actual API generation
        var sb = new StringBuilder();
        sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
        sb.AppendLine("using System.Threading.Tasks;");
        sb.AppendLine();
        sb.AppendLine("namespace GeneratedApis");
        sb.AppendLine("{");
        sb.AppendLine("    [ApiController]");
        sb.AppendLine("    [Route(\"api/[controller]\")]");
        sb.AppendLine($"    public class {endpointName}Controller : ControllerBase");
        sb.AppendLine("    {");
        sb.AppendLine("        [HttpGet]");
        sb.AppendLine("        public async Task<IActionResult> Get()");
        sb.AppendLine("        {");
        sb.AppendLine($"            // Generated API endpoint: {endpointName}");
        sb.AppendLine("            return Ok(new { message = \"Generated API endpoint working\" });");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        
        return sb.ToString();
    }
} 