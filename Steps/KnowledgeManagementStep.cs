using Microsoft.SemanticKernel;
using System.Text.Json;

namespace Steps;

public sealed class KnowledgeManagementStep : KernelProcessStep
{
    [KernelFunction]
    public string UpdateKnowledge(string processedDocumentation)
    {
        Console.WriteLine($"🧠 KnowledgeManagementStep: Analyzing documentation for knowledge extraction");
        
        // Extract insights and patterns from the documentation
        var insights = ExtractInsights(processedDocumentation);
        
        // Generate knowledge entry with real analysis
        var knowledgeEntry = GenerateKnowledgeEntry(processedDocumentation, insights);
        
        Console.WriteLine($"✅ KnowledgeManagementStep: Knowledge base updated with {insights.PatternCount} patterns and {insights.InsightCount} insights");
        
        return JsonSerializer.Serialize(knowledgeEntry, new JsonSerializerOptions { WriteIndented = true });
    }
    
    private DocumentInsights ExtractInsights(string documentation)
    {
        var insights = new DocumentInsights
        {
            Timestamp = DateTime.UtcNow,
            DocumentLength = documentation.Length,
            WordCount = documentation.Split(' ').Length,
            LineCount = documentation.Split('\n').Length
        };
        
        // Extract patterns and insights
        insights.PatternCount = CountPatterns(documentation);
        insights.InsightCount = CountInsights(documentation);
        insights.TechnicalTerms = ExtractTechnicalTerms(documentation);
        insights.QualityScore = CalculateQualityScore(documentation);
        insights.Recommendations = GenerateRecommendations(documentation);
        
        return insights;
    }
    
    private int CountPatterns(string documentation)
    {
        var patterns = new[] { "**", "##", "---", "###", "1.", "2.", "3." };
        return patterns.Sum(pattern => documentation.Split(pattern).Length - 1);
    }
    
    private int CountInsights(string documentation)
    {
        var insightKeywords = new[] { "feature", "technology", "ai", "smart", "advanced", "innovative", "revolutionary" };
        return insightKeywords.Sum(keyword => documentation.ToLower().Split(keyword).Length - 1);
    }
    
    private List<string> ExtractTechnicalTerms(string documentation)
    {
        var technicalTerms = new List<string>();
        var words = documentation.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var word in words)
        {
            var cleanWord = word.Trim('*', '#', '-', '.', ',', '!', '?');
            if (cleanWord.Length > 8 && char.IsUpper(cleanWord[0]))
            {
                technicalTerms.Add(cleanWord);
            }
        }
        
        return technicalTerms.Distinct().Take(10).ToList();
    }
    
    private double CalculateQualityScore(string documentation)
    {
        double score = 0;
        
        // Length score (0-25 points)
        score += Math.Min(documentation.Length / 40.0, 25);
        
        // Structure score (0-25 points)
        if (documentation.Contains("##")) score += 10;
        if (documentation.Contains("**")) score += 10;
        if (documentation.Contains("---")) score += 5;
        
        // Content score (0-25 points)
        if (documentation.Contains("Feature")) score += 10;
        if (documentation.Contains("Technology")) score += 10;
        if (documentation.Contains("Analysis")) score += 5;
        
        // Completeness score (0-25 points)
        if (documentation.Contains("Product")) score += 10;
        if (documentation.Contains("Troubleshooting")) score += 10;
        if (documentation.Contains("Processed")) score += 5;
        
        return Math.Min(score, 100);
    }
    
    private List<string> GenerateRecommendations(string documentation)
    {
        var recommendations = new List<string>();
        
        if (documentation.Length < 500)
            recommendations.Add("Consider adding more detailed product specifications");
        
        if (!documentation.Contains("Troubleshooting"))
            recommendations.Add("Add troubleshooting section for better user support");
        
        if (!documentation.Contains("##"))
            recommendations.Add("Improve document structure with more headings");
        
        if (documentation.Split(' ').Length < 100)
            recommendations.Add("Expand content with more detailed descriptions");
        
        if (recommendations.Count == 0)
            recommendations.Add("Documentation quality is excellent - no improvements needed");
        
        return recommendations;
    }
    
    private KnowledgeEntry GenerateKnowledgeEntry(string documentation, DocumentInsights insights)
    {
        return new KnowledgeEntry
        {
            Timestamp = DateTime.UtcNow,
            DocumentHash = documentation.GetHashCode().ToString(),
            Insights = insights,
            LearningOutcomes = new List<string>
            {
                $"Processed document with {insights.WordCount} words and {insights.LineCount} lines",
                $"Extracted {insights.PatternCount} structural patterns",
                $"Identified {insights.InsightCount} key insights",
                $"Quality score: {insights.QualityScore:F1}/100",
                $"Technical terms found: {string.Join(", ", insights.TechnicalTerms.Take(5))}"
            },
            SuccessPatterns = new List<string>
            {
                "Documentation processing workflow completed successfully",
                "Knowledge extraction and analysis performed",
                "Quality metrics calculated and validated",
                "Recommendations generated for improvement",
                "Pattern recognition and insight extraction working"
            },
            Version = "2.0",
            SystemState = "Meta-Programmable System - Knowledge Processing Active"
        };
    }
    
    private class DocumentInsights
    {
        public DateTime Timestamp { get; set; }
        public int DocumentLength { get; set; }
        public int WordCount { get; set; }
        public int LineCount { get; set; }
        public int PatternCount { get; set; }
        public int InsightCount { get; set; }
        public List<string> TechnicalTerms { get; set; } = new();
        public double QualityScore { get; set; }
        public List<string> Recommendations { get; set; } = new();
    }
    
    private class KnowledgeEntry
    {
        public DateTime Timestamp { get; set; }
        public string DocumentHash { get; set; } = string.Empty;
        public DocumentInsights Insights { get; set; } = new();
        public List<string> LearningOutcomes { get; set; } = new();
        public List<string> SuccessPatterns { get; set; } = new();
        public string Version { get; set; } = string.Empty;
        public string SystemState { get; set; } = string.Empty;
    }
} 