# Task Management API

A RESTful API for managing tasks and users, built with .NET 9.0, Entity Framework Core, and SQLite.

## Features

- **User Management**: Create and retrieve users with pagination
- **Task Management**: Full CRUD operations with filtering by status, priority, and assigned user
- **API Key Authentication**: Secure endpoints with header-based authentication
- **Input Validation**: Comprehensive validation with meaningful error messages
- **Swagger Documentation**: Interactive API documentation

## Tech Stack

- **.NET 9.0** - Web API Framework
- **Entity Framework Core 9.0** - ORM
- **SQLite** - Embedded Database
- **Swagger/OpenAPI** - API Documentation

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Installation

1. Clone the repository:
```bash
git clone <your-repo-url>
cd TaskManagementAPI
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

The API will be available at `http://localhost:5056`

### Swagger UI

Access the interactive API documentation at:
```
http://localhost:5056/swagger
```

## Authentication

All API endpoints require an API Key in the request header:

```
X-API-KEY: your-secret-api-key-12345
```

> **Note**: In production, change the API key in `appsettings.json`

## API Endpoints

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/users` | Create a new user |
| GET | `/api/users` | Get all users (with pagination) |
| GET | `/api/users/{id}` | Get user by ID |

### Tasks

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/tasks` | Create a new task |
| GET | `/api/tasks` | Get all tasks (with filters & pagination) |
| GET | `/api/tasks/{id}` | Get task by ID |
| PUT | `/api/tasks/{id}` | Update a task |
| PATCH | `/api/tasks/{id}/status` | Update task status only |
| DELETE | `/api/tasks/{id}` | Delete a task |

## Query Parameters

### GET /api/users
- `page` (default: 1) - Page number
- `pageSize` (default: 10, max: 100) - Items per page

### GET /api/tasks
- `page` (default: 1) - Page number
- `pageSize` (default: 10, max: 100) - Items per page
- `status` - Filter by status (`TODO`, `IN_PROGRESS`, `DONE`)
- `priority` - Filter by priority (`LOW`, `MEDIUM`, `HIGH`)
- `assignedToId` - Filter by assigned user ID

## Request Examples

### Create User

```bash
POST /api/users
Content-Type: application/json
X-API-KEY: your-secret-api-key-12345

{
  "name": "John Doe",
  "email": "john.doe@example.com"
}
```

### Create Task

```bash
POST /api/tasks
Content-Type: application/json
X-API-KEY: your-secret-api-key-12345

{
  "title": "Complete project",
  "description": "Finish the task management API",
  "status": "TODO",
  "priority": "HIGH",
  "dueDate": "2026-12-31T23:59:59Z",
  "assignedToId": 1
}
```

### Update Task Status

```bash
PATCH /api/tasks/1/status
Content-Type: application/json
X-API-KEY: your-secret-api-key-12345

{
  "status": "IN_PROGRESS"
}
```

## Validation Rules

### User
- **name**: Required, minimum 2 characters
- **email**: Required, valid email format, unique

### Task
- **title**: Required, 3-100 characters
- **description**: Optional, max 500 characters
- **status**: Required (`TODO`, `IN_PROGRESS`, `DONE`)
- **priority**: Required (`LOW`, `MEDIUM`, `HIGH`)
- **dueDate**: Optional
- **assignedToId**: Optional, must reference existing user

## Error Responses

### 400 Bad Request
```json
{
  "errors": ["Name is required.", "Email must be valid."]
}
```

### 404 Not Found
```json
{
  "error": "User with ID 999 not found."
}
```

### 409 Conflict
```json
{
  "error": "User with email john@example.com already exists."
}
```

### 401 Unauthorized
```json
{
  "error": "Invalid or missing API key."
}
```

## Project Structure

```
TaskManagementAPI/
├── Controllers/          # API Controllers
├── Data/                # Database Context
├── Exceptions/          # Custom Exceptions
├── Middleware/          # Authentication Middleware
├── Models/
│   ├── DTOs/           # Data Transfer Objects
│   ├── Entities/       # Database Entities
│   └── Enums/          # Status & Priority Enums
├── Services/           # Business Logic Layer
└── Program.cs          # Application Entry Point
```

## Database

The application uses SQLite with automatic database creation on startup. The database file (`taskmanagement.db`) is created in the project root directory.

### Schema

**Users Table:**
- Id (Primary Key)
- Name (Required)
- Email (Required, Unique)

**Tasks Table:**
- Id (Primary Key)
- Title (Required)
- Description (Optional)
- Status (Enum)
- Priority (Enum)
- DueDate (Optional)
- CreatedAt (Timestamp)
- UpdatedAt (Timestamp)
- AssignedToId (Foreign Key → Users)

## Testing

Use Swagger UI at `http://localhost:5056/swagger` to test all endpoints interactively.

## License

This project is for educational purposes.

## Author

Sarthak Pandey
