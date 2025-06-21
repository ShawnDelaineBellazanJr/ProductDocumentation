# Prompt Templates

Prompt templates are the foundation of agent behavior in the system. They define how agents reason, plan, and interact—enabling declarative, template-driven orchestration that is easy to update, version, and extend.

## What Are Prompt Templates?
- **Prompty templates** are YAML or Markdown files that describe agent instructions, workflows, and policies in natural language, often with variables and logic (using Liquid/Handlebars syntax).
- Templates can be stored locally, in the database, or remotely (e.g., GitHub, cloud storage), enabling distributed collaboration and version control.
- Agents load and interpret these templates at runtime, allowing their behavior to be changed without code changes or redeployment.

## Template-Driven Plugins
- Prompt templates are a form of plugin: they can be swapped, versioned, and managed like DLL plugins.
- The system supports both code-based plugins (DLLs) and template-driven plugins (Prompty files), giving maximum flexibility.
- Templates can be shared, reused, and even published to public repositories (e.g., GitHub) for community-driven improvement or as a playground for experimentation.

## Liquid/Handlebars Syntax
- Templates use Liquid or Handlebars syntax for variables, loops, and conditionals.
- Example:
  ```yaml
  # PlannerAgent.prompty
  Goal: {{goal}}
  Constraints: {{constraints}}
  Tools Available:
  {{#each skills}}
    - {{this.Description}}
  {{/each}}
  Plan:
  1. {{First step}}
  2. {{Second step}}
  ...
  ```
- This allows dynamic injection of context, skills, and other runtime data into the agent’s reasoning process.

## Managing and Evolving Templates
- Templates are versioned and stored in a registry (DB or GitHub), with metadata for auditing and rollback.
- Meta-agents (e.g., Reflector) or admins can update templates at runtime to improve agent performance or adapt to new requirements.
- Templates can be tested in a playground (e.g., GitHub repo or web UI) before being promoted to production.

## Best Practices
- **Keep templates modular:** Use includes/partials for reusable logic.
- **Document variables and sections:** Make it easy for others to understand and extend.
- **Version and audit:** Track changes and maintain a history for compliance and debugging.
- **Test before deployment:** Use a playground or staging environment to validate new templates.
- **Leverage community:** Share templates on GitHub or other platforms for feedback and improvement.

## Example: OrchestratorAgent Template
```yaml
---
name: OrchestratorAgent
version: 1.0
description: Main orchestrator for self-evolving system
model:
  api: chat
  configuration:
    type: azure_openai
    azure_deployment: gpt-4
temperature: 0.3
---

You are the orchestrator. For each user goal:
- Break it down into steps
- Assign steps to agents or plugins
- Monitor progress and adapt as needed
- Incorporate feedback and improve the plan

Available Skills:
{{#each skills}}
- {{name}}: {{description}}
{{/each}}

Current Context:
Goal: {{context.goal}}
Status: {{context.status}}
Iteration: {{context.iterationCount}}
```

---

**See also:** [Plugin System](plugin-system.md), [Agent Orchestration](agent-orchestration.md)
