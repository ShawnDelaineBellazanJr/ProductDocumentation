using System;
using System.IO;
using System.Globalization;

namespace ProductDocumentation.Utils
{
    public static class ChainOfThoughtLogger
    {
        private static readonly string LogDir = Path.Combine("logs", "chain-of-thought");

        public static void LogStep(string agent, string phase, string content)
        {
            Directory.CreateDirectory(LogDir);
            string logFile = Path.Combine(LogDir, $"{DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.log");
            string entry = $"[{DateTime.UtcNow:O}] [{agent}] [{phase}] {content}";
            File.AppendAllText(logFile, entry + Environment.NewLine);
        }
    }
} 