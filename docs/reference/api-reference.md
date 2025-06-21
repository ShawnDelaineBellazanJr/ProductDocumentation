# API Reference

This section provides an overview of the system's REST API endpoints, dynamic APIs, and integration points. For full details, see the auto-generated Swagger UI at `/swagger`.

## Core Endpoints
- **/api/products**: Manage product entities
- **/api/skills**: List and manage available skills/plugins
- **/api/agents**: Query agent status and configuration
- **/api/docs**: Access generated documentation
- **/api/feedback**: Submit feedback for continuous improvement

## Dynamic Endpoints
- New endpoints can be generated at runtime via OpenAPI/NSwag and will appear in Swagger UI automatically.
- Each dynamic endpoint is versioned and documented.

## Authentication & Security
- All endpoints require authentication (JWT, OAuth, or API key)
- Role-based authorization is enforced for sensitive operations

## Usage Example
```http
POST /api/products
Content-Type: application/json
Authorization: Bearer <token>

{
  "name": "GlowBrew AI Coffee Machine",
  "features": ["AI Taste Assistant", "Programmable LEDs"]
}
```

## SDKs & Client Generation
- Use Swagger UI to generate client SDKs for C#, TypeScript, Python, etc.

---

**See also:** [Dynamic API](../design/dynamic-api.md), [Security](security.md)
