# 🎰 XP BetSafe - Sprint 4

API desenvolvida em **C# (.NET 8)** com **ASP.NET Core Web API**, **Entity Framework Core** e **SQLite**.  
Implementa **CRUD completo**, consultas **LINQ** e documentação via **Swagger**.

---

## 🚀 Execução

\`\`\`bash
dotnet restore
dotnet ef database update
dotnet run
\`\`\`

Acesse o Swagger:  
👉 http://localhost:5086/swagger

---

## 🧩 Diagrama da Arquitetura

![architecture](docs/architecture.png)

**Fluxo:**  
Cliente (Swagger / HTTP) → API (Controllers) → EF Core (AppDbContext) → Banco SQLite (XpBetSafe.db)

---

## 👥 Integrantes

- Victor Eid Carbutti Nicolas – RM 98668  
- João Pedro Do Vale Cruz Novo – RM 98650  
- Tiago Rafael Paulino Ferreira – RM 551169

---
