# Clean Architecture

Clean architecture is the foundation of this system, ensuring modularity, testability, and long-term maintainability. It leverages generic patterns, dependency injection, and strict separation of concerns to support both traditional and AI-driven workflows.

## Principles
- **Separation of Concerns:** Each layer (Domain, Application, Infrastructure, Interface) has a clear responsibility and communicates via well-defined interfaces.
- **Dependency Inversion:** High-level modules (business logic, agents) do not depend on low-level modules (data access, external APIs); both depend on abstractions.
- **Generic Patterns:** Use of generic base classes, repositories, and services to maximize code reuse and flexibility.
- **Testability:** Business logic and agents can be tested independently of infrastructure or UI.
- **Plug-in Extensibility:** Both code and template-driven plugins can be injected at runtime, supporting dynamic evolution.

## Layered Structure

1. **Domain Layer**
   - Contains core business models, logic, and interfaces (e.g., `IRepository<T>`, `IAgent<TContext>`)
   - No dependencies on other layers

2. **Application Layer**
   - Orchestrates workflows, agent coordination, and business use cases
   - Uses generic services and unit of work patterns
   - Depends only on domain abstractions

3. **Infrastructure Layer**
   - Implements data access (e.g., `GenericRepository<T>`), external integrations, and dynamic plugin loading
   - Injected into application layer via dependency injection
   - Can be swapped or extended without changing business logic

4. **Interface Layer (API/UI)**
   - Exposes REST APIs and UI endpoints
   - Controllers/services are injected with generic repositories and services
   - Minimal logic; delegates to application layer

## Example: Generic Repository and Service
```csharp
// Domain
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task RemoveAsync(T entity);
    // ...
}

// Infrastructure
public class GenericRepository<T> : IRepository<T> where T : class
{
    // ...implementation using EF Core DbContext
}

// Application
public class ProductService<T> where T : class
{
    private readonly IRepository<T> _repo;
    public ProductService(IRepository<T> repo) { _repo = repo; }
    // ...business logic
}

// Interface (API)
[ApiController]
public class ProductController<T> : ControllerBase where T : class
{
    private readonly ProductService<T> _service;
    public ProductController(ProductService<T> service) { _service = service; }
    // ...endpoints
}
```

## AI and Plugin Integration
- Agents and plugins are injected as services, following the same DI and generic patterns
- Template-driven plugins (Prompty) are loaded and managed via abstractions, keeping the core clean
- Dynamic skills and APIs are registered without breaking architectural boundaries

## Best Practices
- Keep domain logic pure and free of infrastructure concerns
- Use generic patterns for maximum flexibility and code reuse
- Inject dependencies via interfaces and abstractions
- Test each layer independently
- Document interfaces and contracts for plugin and agent integration

---

**See also:** [Plugin System](plugin-system.md), [Agent Orchestration](agent-orchestration.md)
