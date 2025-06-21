# Meta-Agents

Meta-agents are specialized agents responsible for monitoring, evaluating, and improving the system itself. They enable self-reflection, governance, and continuous optimization.

## Types of Meta-Agents

### ReflectorAgent
- Analyzes outcomes, logs, and feedback to identify improvement opportunities
- Can update prompt templates, suggest new skills, or adjust workflows
- Maintains a history of changes and rationale for auditability

### CheckerAgent
- Validates outputs, checks for errors, and ensures compliance with policies
- Flags issues for review and can trigger corrective actions

### PlannerAgent (Meta Mode)
- Incorporates feedback and improvement suggestions into new plans
- Can reprioritize goals or adjust strategies based on system performance

### LearningAgent (Optional)
- Periodically retrains ML models or updates embeddings based on new data
- Ensures the system’s knowledge base evolves with usage

## How Meta-Agents Work
- Operate on system logs, feedback, and performance metrics
- Can act autonomously or require human approval for high-impact changes
- Use declarative templates and policies to guide their actions
- Collaborate with other agents to implement improvements

## Example: Self-Improvement Cycle
1. CheckerAgent detects repeated errors in a workflow
2. ReflectorAgent analyzes the root cause and suggests a prompt update
3. PlannerAgent incorporates the new prompt into the next plan
4. The system executes the improved workflow and monitors results

## Governance & Safety
- All meta-agent actions are logged and auditable
- Approval workflows can be enforced for sensitive changes
- Meta-agents operate within defined guardrails and policies

## Best Practices
- Regularly review meta-agent logs and improvement actions
- Use version control for prompt templates and configuration changes
- Balance autonomy with oversight for critical system functions

---

**See also:** [Feedback Loop](feedback-loop.md), [Self-Evolution](self-evolution.md)
