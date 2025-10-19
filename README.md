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

<img width="197" height="511" alt="image" src="https://github.com/user-attachments/assets/2fc8b458-4457-46ca-aadd-4f3a21b925fe" />


**Fluxo:**  
Cliente (Swagger / HTTP) → API (Controllers) → EF Core (AppDbContext) → Banco SQLite (XpBetSafe.db)

---

## 👥 Integrantes

- Victor Eid Carbutti Nicolas – RM 98668  
- João Pedro Do Vale Cruz Novo – RM 98650  
- Tiago Rafael Paulino Ferreira – RM 551169

---
