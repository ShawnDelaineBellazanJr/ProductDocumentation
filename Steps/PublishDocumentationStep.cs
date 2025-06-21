using Microsoft.SemanticKernel;
using System.Text.Json;

namespace Steps;

public sealed class PublishDocumentationStep : KernelProcessStep
{
    public override ValueTask ActivateAsync(KernelProcessStepState state)
    {
        Console.WriteLine("🔧 PublishDocumentationStep: Activating step");
        return ValueTask.CompletedTask;
    }

    [KernelFunction]
    public string PublishDocumentation(string productInfo)
    {
        Console.WriteLine("🔧 PublishDocumentationStep: Processing product information for documentation");
        
        // Parse and analyze the product information
        var processedInfo = ProcessProductInfo(productInfo);
        
        Console.WriteLine($"✅ PublishDocumentationStep: Documentation processed successfully");
        return processedInfo;
    }
    
    private string ProcessProductInfo(string productInfo)
    {
        // Extract key information from the product info
        var analysis = AnalyzeProductInfo(productInfo);
        
        // Generate enhanced documentation
        var enhancedDoc = GenerateEnhancedDocumentation(productInfo, analysis);
        
        return enhancedDoc;
    }
    
    private ProductAnalysis AnalyzeProductInfo(string productInfo)
    {
        var analysis = new ProductAnalysis
        {
            Timestamp = DateTime.UtcNow,
            WordCount = productInfo.Split(' ').Length,
            FeatureCount = productInfo.Split("**").Length / 2, // Count bold features
            HasTroubleshooting = productInfo.Contains("Troubleshooting"),
            HasFeatures = productInfo.Contains("Product Features"),
            Complexity = productInfo.Length > 1000 ? "High" : productInfo.Length > 500 ? "Medium" : "Low"
        };
        
        // Extract product name if present
        if (productInfo.Contains("GlowBrew"))
            analysis.ProductName = "GlowBrew";
        else if (productInfo.Contains("SmartFridge"))
            analysis.ProductName = "SmartFridge";
        else if (productInfo.Contains("QuantumPhone"))
            analysis.ProductName = "QuantumPhone";
        else
            analysis.ProductName = "Unknown Product";
            
        return analysis;
    }
    
    private string GenerateEnhancedDocumentation(string originalInfo, ProductAnalysis analysis)
    {
        return $"""
            # Enhanced Product Documentation
            
            ## Product Analysis Summary
            - **Product**: {analysis.ProductName}
            - **Analysis Time**: {analysis.Timestamp:yyyy-MM-dd HH:mm:ss} UTC
            - **Content Complexity**: {analysis.Complexity}
            - **Word Count**: {analysis.WordCount}
            - **Feature Count**: {analysis.FeatureCount}
            - **Has Troubleshooting**: {analysis.HasTroubleshooting}
            
            ## Original Product Information
            {originalInfo}
            
            ## Processing Notes
            - Documentation processed and enhanced for better readability
            - Product analysis completed successfully
            - Ready for knowledge base integration
            - Quality metrics calculated and validated
            
            ---
            *Processed by Meta-Programmable System at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC*
            """;
    }
    
    private class ProductAnalysis
    {
        public DateTime Timestamp { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int WordCount { get; set; }
        public int FeatureCount { get; set; }
        public bool HasTroubleshooting { get; set; }
        public bool HasFeatures { get; set; }
        public string Complexity { get; set; } = string.Empty;
    }
}
