# ExamApp

A  **ASP.NET Core Web API** built using **Clean Architecture** principles.  
It manages exams, questions, submissions, and results — designed for scalability, testability, and clean separation of concerns.

---

## 📁 Project Structure

ExamApp.sln

- ExamApp.Domain           # Core domain entities, value objects, and interfaces
- ExamApp.Application      # Business logic, use cases, DTOs, validation, and service contracts
- ExamApp.Infrastructure   # Data access, EF Core configurations, and persistence implementations
- ExamApp.API              # ASP.NET Core Web API (entry point with controllers, DI, and endpoints)
- SharedKernel             # Common abstractions and utilities shared across layers



## ✅ Features

- 👤 **Exam Management** — Secure user registration, login, and exam submission workflows.  
- 🛠 **Admin Panel** — Full control to create, update, assign, and delete exams.  
- 🔐 **Authentication & Authorization** — JWT-based authentication with role-based access (Admin / User).  
- 🗂 **Clean Architecture** — Layered separation of concerns ensuring maintainability and scalability.  
- ⚙️ **CQRS + Mediator Pattern** — Command and Query segregation by MediatR for clean request handling.  
- 🧾 **FluentValidation** — Strongly-typed validation integrated at the Application layer.  
- 🧠 **Logging with ILogger** — Centralized and structured logging for better debugging and traceability.  
- 📦 **Repository & Unit of Work Patterns** — Consistent data access and transaction management.  
- 🔄 **AutoMapper Integration** — Simplified object-to-object mapping between entities and DTOs.  
- 📄 **RESTful API Design** — Easily consumable endpoints for frontend or mobile integration.  

---

## 🛠 Technologies Used

- **.NET 9** 
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **Clean Architecture**
- **Automapper**
---

## ERD

<img width="1187" height="650" alt="image" src="https://github.com/user-attachments/assets/01f2d6b5-d30b-4c75-90d2-1d3adef2284c" />

