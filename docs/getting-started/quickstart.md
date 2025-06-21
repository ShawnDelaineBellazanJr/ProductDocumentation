# Quickstart

This quickstart guide will help you get the Meta-Programmable Self-Evolving System running in minutes.

## 1. Clone the Repository
```sh
git clone <your-repo-url>
cd <project-root>
```

## 2. Install Prerequisites
- .NET 9 SDK
- Node.js
- Docker (optional, for containers)
- PostgreSQL (or use Docker)
- DocFX (for documentation)

## 3. Configure Environment
- Copy `.env.example` to `.env` and fill in required secrets (DB, OpenAI, SMTP, etc.)
- Or set environment variables directly

## 4. Run Database Migrations
```sh
dotnet ef database update --project src/MetaSystem.Infrastructure
```

## 5. Start the Application
```sh
dotnet run --project src/MetaSystem.Api
```
- Access Swagger UI at `https://localhost:5001/swagger`

## 6. Generate Documentation Site
```sh
docfx docs/docfx.json
```
- Open `docs/_site/index.html` in your browser

## 7. Try the System
- Use the API to submit a product info event
- Watch as the system generates, refines, and publishes documentation
- Submit feedback to trigger the feedback loop

---

**Next:** Explore [Architecture](../architecture/overview.md) or [How-To Guides](../how-to/add-skill.md)
