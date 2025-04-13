# 🏆 Ballon d'Or Award Ceremony Management System

The **Ballon d'Or Award Ceremony Management System** is a robust, web-based content management system (CMS) designed to facilitate the administration of the prestigious Ballon d'Or awards. This system streamlines player data management, tracks award history, and efficiently handles the voting process, all within a secure and scalable backend architecture.

---

## 🚀 Features

- 🧑‍💼 **Admin Dashboard**: Manage players, awards, and voting results.
- 📋 **Player Management**: Add, edit, and delete player profiles with relevant statistics.
- 🏅 **Award Tracking**: Maintain historical records of Ballon d'Or winners.
- 🗳️ **Voting System**: Manage voting phases and ensure vote integrity.
- 📊 **Reporting Tools**: Generate reports for nominations and winners.

---

## ⚙️ Tech Stack

| Tech             | Description                                  |
|------------------|----------------------------------------------|
| **ASP.NET Core MVC** | Web framework for building the backend structure. |
| **Entity Framework Core** | ORM used for data access with LINQ and migrations. |
| **SQL Server**        | Database for storing player data, award records, and voting details. |
| **C#**                | Primary programming language for backend logic. |

---

## 🏗️ Project Structure

BallonDorCMS/ │ ├── Controllers/ # MVC Controllers ├── Models/ # Entity classes and DB context ├── Views/ # Razor Views for frontend (optional) ├── Migrations/ # EF Core migration files ├── wwwroot/ # Static files (if needed) ├── appsettings.json # Configuration file for DB and settings └── Program.cs / Startup.cs # App startup and middleware

## 🛠️ Setup Instructions

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (version 6 or above)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)