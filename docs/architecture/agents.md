# Agents

Agents are the intelligent actors in the system, each with a specialized role and prompt template. They collaborate to plan, execute, check, and improve workflows.

## Core Agent Types

### OrchestratorAgent
- **Role:** Main coordinator; receives goals, delegates tasks, manages context, and closes the loop.
- **Prompt:** "You are the orchestrator. For each goal, break it down, assign steps, and ensure completion."

### PlannerAgent
- **Role:** Decomposes high-level goals into actionable plans (step lists, workflows).
- **Prompt:** "Given a goal, output a plan as a sequence of steps."

### MakerAgent
- **Role:** Executes individual steps in a plan, invoking skills/plugins as needed.
- **Prompt:** "For each step, call the appropriate skill and record the result."

### CheckerAgent
- **Role:** Validates outputs, checks for errors, and ensures quality.
- **Prompt:** "Review results and flag any issues or incomplete steps."

### ReflectorAgent
- **Role:** Analyzes process outcomes, suggests improvements, and can update prompts or plans.
- **Prompt:** "Reflect on the process, log lessons learned, and propose changes for next time."

### Additional/Custom Agents
- **Examples:** ComplianceAgent, LearningAgent, ParallelExecutorAgent, etc. (add as needed for your domain)

## Agent Collaboration
- Agents communicate via shared context (SK AgentThread), passing results, plans, and feedback.
- Each agent’s behavior is defined by a Prompty template, which can be updated at runtime for self-evolution.
- Agents can be chained (chain-of-thought) or run in parallel (federated reasoning).

## Example: Planner–Maker–Checker–Reflector Loop
1. **PlannerAgent** creates a plan.
2. **MakerAgent** executes each step.
3. **CheckerAgent** validates results.
4. **ReflectorAgent** analyzes and updates prompts or plans.
5. **OrchestratorAgent** manages the overall flow and context.

---

**See also:** [Agent Orchestration](../design/agent-orchestration.md), [Prompt Templates](../design/prompt-templates.md)
