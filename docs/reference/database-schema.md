# Database Schema

This section documents the core database schema for the Meta-Programmable Self-Evolving System, supporting persistence, audit, and extensibility.

## Core Tables

### agents
- `id` (UUID, PK)
- `name` (string)
- `type` (string)
- `prompt_template_id` (UUID, FK)
- `config` (JSONB)
- `created_at`, `updated_at` (timestamp)

### prompt_templates
- `id` (UUID, PK)
- `name` (string)
- `content` (text)
- `version` (int)
- `is_active` (bool)
- `created_at` (timestamp)

### skill_registry
- `id` (UUID, PK)
- `name` (string)
- `description` (text)
- `source_code` (text)
- `assembly_data` (bytea)
- `is_dynamic` (bool)
- `created_at` (timestamp)

### knowledge_base
- `id` (UUID, PK)
- `category` (string)
- `content` (text)
- `metadata` (JSONB)
- `embedding` (vector)
- `created_at` (timestamp)

### system_config
- `key` (string, PK)
- `value` (text)
- `description` (text)
- `updated_at` (timestamp)

### audit_logs
- `id` (UUID, PK)
- `agent_name` (string)
- `action` (string)
- `details` (JSONB)
- `context_id` (UUID)
- `timestamp` (timestamp)

## Relationships
- Agents reference prompt templates
- Skills/plugins are versioned and auditable
- All changes are logged for compliance

## Best Practices
- Use migrations to manage schema changes
- Index frequently queried columns (e.g., `name`, `created_at`)
- Secure sensitive data and audit access

---

**See also:** [Configuration](configuration.md), [Audit & Security](security.md)
