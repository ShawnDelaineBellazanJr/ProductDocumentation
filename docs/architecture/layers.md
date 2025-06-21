# Architecture Layers

The system is organized into distinct layers, following clean architecture principles to maximize maintainability, testability, and adaptability.

## 1. Interface Layer (API/UI)
- **Purpose:** Exposes RESTful APIs and UI endpoints for user and system interaction.
- **Technologies:** ASP.NET Core Web API, OpenAPI/Swagger, authentication/authorization middleware.
- **Key Features:**
  - Minimal business logic; delegates to orchestration layer
  - Supports dynamic endpoint registration (via NSwag)
  - API documentation auto-generated

## 2. Orchestration Layer (AI Brain)
- **Purpose:** Hosts the main AI agents (Orchestrator, Planner, Checker, Reflector, etc.) and manages workflow execution.
- **Technologies:** Semantic Kernel 1.57, Prompty templates, C# 13
- **Key Features:**
  - Multi-agent orchestration and context management
  - Declarative, template-driven workflows
  - Chain-of-thought reasoning and dynamic function invocation

## 3. Domain Layer (Business Logic & Prompts)
- **Purpose:** Encapsulates business logic, domain models, and declarative agent behavior.
- **Technologies:** C# skills/plugins, Prompty YAML/Markdown templates
- **Key Features:**
  - Skills (plugins) for all business actions
  - Prompt templates for agent roles and workflows
  - Versioned, updateable at runtime

## 4. Integration Layer (Dynamic Services)
- **Purpose:** Handles external integrations, dynamic code/API generation, and infrastructure services.
- **Technologies:** Roslyn, NSwag, Playwright, ML.NET, external APIs
- **Key Features:**
  - Runtime plugin/skill loading
  - Dynamic API/controller generation
  - External system and tool integration

## 5. Persistence Layer (State & Memory)
- **Purpose:** Provides persistent storage for all system state, configuration, and knowledge.
- **Technologies:** Entity Framework Core, PostgreSQL, Redis, blob storage
- **Key Features:**
  - Agent prompts, skills, and knowledge base
  - Audit logs, configuration, and system memory
  - Supports distributed, scalable deployments

---

**See also:** [System Overview](overview.md), [Agents](agents.md), [Extensibility](extensibility.md)
