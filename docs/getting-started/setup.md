# Setup

This guide walks you through setting up the Meta-Programmable Self-Evolving System for local development, testing, and production deployment.

## 1. Clone the Repository

```sh
git clone <your-repo-url>
cd <project-root>
```

## 2. Install Dependencies

- .NET 9 SDK: [Download](https://dotnet.microsoft.com/download)
- Node.js: [Download](https://nodejs.org/)
- Docker: [Download](https://www.docker.com/)
- PostgreSQL: [Download](https://www.postgresql.org/download/)
- Redis (optional): [Download](https://redis.io/download)
- DocFX: `dotnet tool install -g docfx`

## 3. Configure Environment Variables

Create a `.env` or use your platform’s secret manager for:

- `ConnectionStrings__Default`
- `OpenAI__ApiKey` or `AzureOpenAI__ApiKey`
- `Aspire__ConfigPath`
- `SMTP__Host`, `SMTP__User`, `SMTP__Password`

## 4. Database Setup

- Run EF Core migrations:

  ```sh
  dotnet ef database update --project src/MetaSystem.Infrastructure
  ```

- (Optional) Seed initial data (prompts, skills, configs).

## 5. Build and Run the System

```sh
dotnet build
# For local run:
dotnet run --project src/MetaSystem.Api
```

- Access the API at `https://localhost:5001/swagger`
- Access the DocFX docs at `docs/_site/index.html` (after running `docfx docs/docfx.json`)

## 6. Cloud/Production Deployment

- Use provided Helm charts, Bicep, or Terraform scripts in `/deploy` (if available)
- Configure CI/CD pipeline for automated builds, tests, and deployments
- Set up monitoring and alerting (see [Monitoring](../how-to/monitoring.md))

---

**Next:** [Quickstart](quickstart.md)
