# 📌 Lead Management System

Sistema full-stack com **.NET (ASP.NET Core)** no backend e **Angular** no frontend, utilizando **SQLite** como base de dados.

---

# 🚀 Tecnologias utilizadas

- ⚙️ Backend: .NET 8 / ASP.NET Core
- 🖥️ Frontend: Angular
- 🗄️ Base de dados: SQLite (Code First / EF Core)
- 🔗 Comunicação: REST API

---

# 📦 Pré-requisitos

Antes de executar o projeto, garante que tens instalado:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js + npm](https://nodejs.org/)
- Angular CLI (opcional, pode ser usado via `npx`)

---

# ▶️ Execução rápida (recomendado)

Na raiz do projeto existe um ficheiro:


run-app.bat


O script irá automaticamente:
- Instalar dependências do frontend
- Iniciar o Angular
- Restaurar dependências do backend
- Iniciar a API .NET
- Criar a base de dados SQLite (se não existir)

---

# 🧩 Execução manual

## 🔙 Backend (.NET)


cd backend
dotnet restore
dotnet run


API disponível em:

https://localhost:5000


---

## 🌐 Frontend (Angular)


cd frontend
npm install
npx ng serve --open


Aplicação disponível em:

http://localhost:4200


---

# 🗄️ Base de dados

- Utiliza SQLite
- O ficheiro `app.db` é criado automaticamente ao iniciar o backend
- Estrutura gerida por Entity Framework Core Migrations

---

# ⚠️ Notas importantes

- O backend deve estar a correr antes do frontend consumir a API
- Certifica-te que as portas 4200 e 5000 estão livres
- Em caso de erro:
  - dotnet restore
  - npm install

---
