# Architecture Diagrams

This section provides visual representations of the system's architecture, workflows, and agent interactions. Use these diagrams to understand system structure, data flow, and extensibility points.

## 1. High-Level System Architecture

```mermaid
graph TD;
  User-->|REST/API|InterfaceLayer[Interface Layer]
  InterfaceLayer-->|Delegates|OrchestrationLayer[Orchestration Layer]
  OrchestrationLayer-->|Invokes|DomainLayer[Domain Layer]
  OrchestrationLayer-->|Loads|PluginSystem[Dynamic Plugin System]
  DomainLayer-->|Persists|PersistenceLayer[Persistence Layer]
  PluginSystem-->|Registers|SemanticKernel[Semantic Kernel]
  OrchestrationLayer-->|Monitors|Telemetry[Telemetry/Monitoring]
```

## 2. Agent Orchestration (PMCR Loop)

```mermaid
graph LR;
  PlannerAgent-->|Plan|MakerAgent
  MakerAgent-->|Result|CheckerAgent
  CheckerAgent-->|Feedback|ReflectorAgent
  ReflectorAgent-->|Improvement|PlannerAgent
  OrchestratorAgent-->|Manages|PlannerAgent
  OrchestratorAgent-->|Manages|MakerAgent
  OrchestratorAgent-->|Manages|CheckerAgent
  OrchestratorAgent-->|Manages|ReflectorAgent
```

## 3. Dynamic Plugin/API Loading

```mermaid
graph TD;
  Agent-->|Generates|PluginCode[Plugin Code]
  PluginCode-->|Compiles|Roslyn[Roslyn Compiler]
  Roslyn-->|Loads|SkillRegistry[Skill Registry]
  SkillRegistry-->|Registers|SemanticKernel
  SemanticKernel-->|Exposes|API[Dynamic API]
  API-->|Documents|Swagger[Swagger UI]
```

## 4. Data Flow and State

- All agent context, plans, and results are persisted in the database for audit, recovery, and learning.
- Dynamic plugins and templates are versioned and stored for traceability.
- Monitoring and telemetry data flows to dashboards for real-time observability.

## 5. Extensibility Points

- New skills/plugins can be added at runtime via the plugin system.
- New API endpoints can be generated and exposed dynamically.
- Prompty templates can be updated for agent behavior evolution.

---

**See also:** [Layers](layers.md), [Agents](agents.md), [Extensibility](extensibility.md)
