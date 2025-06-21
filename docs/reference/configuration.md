# Configuration Reference

This section documents all configuration options, environment variables, and recommended settings for the system.

## Environment Variables
- `ConnectionStrings__Default`: Database connection string
- `OpenAI__ApiKey` / `AzureOpenAI__ApiKey`: LLM API keys
- `Aspire__ConfigPath`: Path to Aspire configuration
- `SMTP__Host`, `SMTP__User`, `SMTP__Password`: Email service credentials
- `Redis__ConnectionString`: Redis cache (optional)
- `Telemetry__InstrumentationKey`: For monitoring/telemetry

## AppSettings.json
- All environment variables can be set in `appsettings.json` for local development
- Use secrets management (Azure Key Vault, AWS Secrets Manager) for production

## Dynamic Configuration
- System supports runtime config updates via the API or admin UI
- Config changes are versioned and auditable
- Meta-agents can update config for self-optimization

## Best Practices
- Never commit secrets to source control
- Use separate configs for dev, staging, and production
- Monitor config changes and roll back if needed

---

**See also:** [Database Schema](database-schema.md), [Security](security.md)
