# Automated Product Documentation Process

This document describes the automated, agent-driven workflow for generating, refining, and publishing product documentation in the Meta-Programmable Self-Evolving System.

## Overview
The documentation process is fully automated and iterative, leveraging both code-driven and LLM agent-driven steps. It is designed to:
- Gather product information
- Generate high-quality documentation using an LLM agent
- Accept feedback and suggestions for improvement
- Iterate and refine documentation until it meets requirements
- Publish the final documentation

## Workflow Steps

1. **GetProductInfoStep**
   - Gathers all relevant product information (internal docs, specs, troubleshooting guides).
   - Triggers the next step upon completion.

2. **GenerateDocumentationAgent**
   - An LLM-powered agent that creates customer-facing documentation based on the gathered info.
   - If feedback is provided, the agent incorporates suggestions and rewrites the documentation.
   - This step can be triggered multiple times for iterative improvement.

3. **PublishDocumentationStep**
   - Publishes the final, approved documentation to the appropriate location (site, portal, etc.).

## Iterative Feedback Loop
- The process listens for a `documentation_feedback_received` event.
- When feedback is received (e.g., suggestions, corrections), the GenerateDocumentationAgent is triggered again.
- This loop continues until the documentation is approved or no further feedback is provided.

## YAML Process Example
```yaml
orchestration:
  - listen_for:
      event: input_message_received
      from: _workflow_
    then:
      - node: GetProductInfoStep
  - listen_for:
      event: GetProductInfo.OnResult
      from: GetProductInfoStep
    then:
      - node: GenerateDocumentationAgent
  - listen_for:
      event: GenerateDocumentation.OnResult
      from: GenerateDocumentationAgent
    then:
      - node: PublishDocumentationStep
  - listen_for:
      event: documentation_feedback_received
      from: _workflow_
    then:
      - node: GenerateDocumentationAgent
```

## Best Practices
- Provide clear, actionable feedback to maximize the quality of each iteration.
- Use the audit trail to track changes and improvements over time.
- Integrate this process with your CI/CD pipeline for continuous documentation delivery.

---

**See also:** [How to Add an Agent](add-agent.md), [Prompt Templates](../design/prompt-templates.md)
