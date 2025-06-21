# Plugin System

The plugin system is the backbone of extensibility in the Meta-Programmable Self-Evolving System. It supports both code-based (DLL) plugins and template-driven (Prompty) plugins, enabling rapid, safe, and collaborative extension of system capabilities.

## Types of Plugins

### 1. Code-Based Plugins (DLLs)
- Written in C# and compiled as .NET assemblies (DLLs)
- Loaded at runtime using Roslyn or standard .NET mechanisms
- Can be developed, tested, and versioned independently
- Support for dynamic loading, unloading, and sandboxing
- Examples: DatabaseSkill, NotificationSkill, custom integrations

### 2. Template-Driven Plugins (Prompty)
- Defined as YAML/Markdown files using Liquid/Handlebars syntax
- Describe agent behavior, workflows, and policies declaratively
- Can be stored in the database, local filesystem, or remote repositories (e.g., GitHub)
- Swappable and versioned like code plugins
- Enable non-developers to extend system logic without writing code
- Examples: PlannerAgent.prompty, OrchestratorAgent.prompty

## Plugin Discovery and Registry
- All plugins (DLL and template) are registered in a central registry (database or config)
- Metadata includes name, type, version, source, and audit history
- Agents and orchestrators can query the registry to discover available plugins and their capabilities

## Loading and Execution
- Plugins are loaded at startup and/or on demand
- Dynamic plugins can be added at runtime via Roslyn (for code) or by uploading new templates
- Security validation and approval workflows are enforced for new or updated plugins
- Agents invoke plugins via the Semantic Kernel, which abstracts away the implementation details

## Storage and Distribution
- Plugins can be stored locally, in cloud storage, or in remote repositories (e.g., GitHub, Azure Blob, S3)
- Template plugins can be shared and collaboratively edited via GitHub, enabling a community-driven ecosystem
- The system can pull updates from remote sources, supporting continuous improvement

## Best Practices
- **Version all plugins** and maintain a changelog for traceability
- **Test plugins** in a staging environment before production use
- **Document plugin interfaces** and usage for discoverability
- **Leverage both code and template plugins** for maximum flexibility
- **Use GitHub or similar platforms** as a playground for experimentation and sharing

## Example: Registering a Plugin
```csharp
// Register a DLL plugin
kernel.ImportSkill(new NotificationSkill(), "notify");

// Register a template plugin
var plannerPrompt = File.ReadAllText("PlannerAgent.prompty");
kernel.ImportSemanticFunction("planner", plannerPrompt);
```

---

**See also:** [Prompt Templates](prompt-templates.md), [Extensibility](../architecture/extensibility.md)
