# College Management System - .NET 8 Backend

This project is a modernized backend service for a College Management System, built with .NET 8 and following clean architecture principles. It provides a REST API that can be used by any client application, including the legacy VB.NET UI.

## Project Structure

The solution is organized into the following projects:

- **CMS.Api**: The API layer that handles HTTP requests and responses.
- **CMS.Application**: Contains application services, DTOs, and business logic.
- **CMS.Domain**: Contains domain entities and interfaces.
- **CMS.Infrastructure**: Contains data access implementations and external service integrations.
- **CMS.UnitTests**: Unit tests for the application.
- **CMS.IntegrationTests**: Integration tests for the API.

## Architecture

The solution follows Clean Architecture principles:

1. **Domain Layer**: Contains business entities, interfaces, and domain logic.
2. **Application Layer**: Contains application services, DTOs, and business logic.
3. **Infrastructure Layer**: Contains data access, external services integration.
4. **API Layer**: Contains controllers, middleware configurations, and API endpoints.

## Features

The system includes the following features:

- **Authentication and Authorization**: JWT-based authentication for both admin and faculty users.
- **User Management**: Create, read, update, and delete user accounts.
- **Student Management**: Manage student records, including personal details and course enrollment.
- **Faculty Management**: Manage faculty records, including subject assignments.
- **Course Management**: Create and manage courses offered by the institution.
- **Subject Management**: Define subjects within courses, including theory and practical components.
- **Attendance Management**: Track student attendance by date, course, and subject.
- **Marks Management**: Record and calculate student marks for theory and practical components.

## Technical Improvements

- **Clean Architecture**: Separation of concerns with clear boundaries between layers.
- **CQRS Pattern**: Separation of read and write operations for better scalability.
- **Repository Pattern**: Abstracts data access logic from business logic.
- **Dependency Injection**: Loose coupling between components.
- **Validation**: Input validation using FluentValidation.
- **Error Handling**: Global error handling middleware.
- **Caching**: Memory cache for frequently accessed data.
- **Response Compression**: Gzip compression for better performance.
- **JWT Authentication**: Secure token-based authentication.
- **Swagger Documentation**: API documentation with OpenAPI.
- **Unit and Integration Tests**: Test coverage for key components.

## Detailed Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed on your machine
- MySQL Server (version 8.0 or higher) installed and running
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/) for development
- [Postman](https://www.postman.com/) or similar tool for testing API endpoints (optional)

### Database Setup

1. **Install MySQL**: If not already installed, download and install MySQL Server from [MySQL Downloads](https://dev.mysql.com/downloads/mysql/).

2. **Create Database**:

   - Open MySQL Workbench or MySQL command line
   - Create a new database named `cmsdb`:
     ```sql
     CREATE DATABASE cmsdb;
     ```
   - Create a user with appropriate permissions or use the root user (not recommended for production)

3. **Configure Connection String**:

   - Open the `appsettings.json` file in the `CMS.Api` project
   - Update the connection string to match your MySQL server configuration:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=cmsdb;User=your_username;Password=your_password;"
     }
     ```
   - Replace `your_username` and `your_password` with your MySQL credentials

4. **Database Migration and Seeding**:
   - The application is configured to automatically create and seed the database on startup
   - If you want to manually create the database using Entity Framework migrations:
     ```bash
     cd src/CMS.Api
     dotnet ef migrations add InitialCreate --project ../CMS.Infrastructure/CMS.Infrastructure.csproj
     dotnet ef database update --project ../CMS.Infrastructure/CMS.Infrastructure.csproj
     ```

### Running the Backend API

#### Prerequisites Check:

1. Ensure .NET 8 SDK is installed:

   ```bash
   dotnet --version
   ```

   Should show version 8.x.x

2. Verify MySQL is running:
   ```bash
   brew services list | grep mysql
   # or check if MySQL service is running on your system
   ```

#### Step-by-Step Setup:

1. **Install MySQL (if not already installed)**:

   ```bash
   # On macOS using Homebrew
   brew install mysql
   brew services start mysql

   # Create database and set password
   mysql -u root -e "CREATE DATABASE IF NOT EXISTS cmsdb; ALTER USER 'root'@'localhost' IDENTIFIED BY 'yxdb'; FLUSH PRIVILEGES;"
   ```

2. **Clone and Setup Project**:

   ```bash
   git clone <your-repo-url>
   cd HAI-CMS-Modernize
   ```

3. **Restore Dependencies**:

   ```bash
   dotnet restore
   ```

4. **Build the Project**:

   ```bash
   dotnet build
   ```

5. **Create Database Migration** (if not already created):

   ```bash
   cd src/CMS.Api
   dotnet ef migrations add InitialCreate --project ../CMS.Infrastructure --startup-project .
   ```

6. **Update Database**:
   ```bash
   dotnet ef database update --project ../CMS.Infrastructure --startup-project .
   ```

#### Running the API:

**Option 1: Using Visual Studio**

1. Open the solution file `CMS.sln` in Visual Studio
2. Set `CMS.Api` as the startup project
3. Press F5 or click the "Start" button
4. The API will launch and open Swagger UI in your browser

**Option 2: Using Command Line (Development Mode)**

1. Navigate to the API project directory:
   ```bash
   cd src/CMS.Api
   ```
2. Run the API in Development mode (enables Swagger):
   ```bash
   ASPNETCORE_ENVIRONMENT=Development dotnet run --urls "http://localhost:5000"
   ```

**Option 3: Using Command Line (Production Mode)**

1. Navigate to the API project directory:
   ```bash
   cd src/CMS.Api
   ```
2. Run the API:
   ```bash
   dotnet run
   ```

#### Verifying the Backend is Running:

1. **Check API Status**:
   The console should show:

   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: http://localhost:5000
   info: Microsoft.Hosting.Lifetime[0]
         Application started. Press Ctrl+C to shut down.
   info: Microsoft.Hosting.Lifetime[0]
         Hosting environment: Development (or Production)
   ```

2. **Access Swagger Documentation** (Development mode only):

   - Open your browser and go to: `http://localhost:5000/swagger`
   - You should see the Swagger UI with all API endpoints

3. **Test API Endpoints**:

   ```bash
   # Test if API is responding
   curl -X GET "http://localhost:5000/api/users" -H "accept: application/json"

   # Test authentication endpoint
   curl -X POST "http://localhost:5000/api/auth/login" \
        -H "accept: application/json" \
        -H "Content-Type: application/json" \
        -d '{"username":"admin","password":"admin123"}'
   ```

4. **Available URLs**:
   - **API Base**: `http://localhost:5000`
   - **Swagger UI**: `http://localhost:5000/swagger` (Development mode only)
   - **Health Check**: `http://localhost:5000/api/users` (requires authentication)

#### Common Issues and Solutions:

**Port Already in Use Error**:

```bash
# Kill process using port 5000
lsof -ti:5000 | xargs kill -9

# Or run on different port
dotnet run --urls "http://localhost:5001"
```

**Database Connection Issues**:

- Verify MySQL is running: `brew services list | grep mysql`
- Check connection string in `src/CMS.Api/appsettings.json`
- Ensure database `cmsdb` exists and user has proper permissions

**Swagger Not Available**:

- Ensure you're running in Development mode: `ASPNETCORE_ENVIRONMENT=Development`
- Swagger is only available in Development environment for security reasons

**Entity Framework Migration Issues**:

```bash
# If migration fails, try updating EF tools
dotnet tool update --global dotnet-ef

# Recreate migration if needed
dotnet ef migrations remove --project ../CMS.Infrastructure --startup-project .
dotnet ef migrations add InitialCreate --project ../CMS.Infrastructure --startup-project .
```

### Running Tests

#### Using Visual Studio:

1. Open the Test Explorer (Test > Test Explorer)
2. Click "Run All Tests" or run specific tests

#### Using Command Line:

1. Run unit tests:

   ```bash
   cd tests/CMS.UnitTests
   dotnet test
   ```

2. Run integration tests:
   ```bash
   cd tests/CMS.IntegrationTests
   dotnet test
   ```

### Initial Login Credentials

The database is seeded with an admin user:

- **Username**: admin
- **Password**: admin123

Use these credentials to obtain a JWT token through the `/api/auth/login` endpoint.

## Connecting with the Legacy VB.NET UI

The modernized backend is a completely separate .NET 8 application that provides a REST API. The legacy VB.NET UI is not part of this modernized solution. To use the modernized backend with the legacy VB.NET UI, you would need to modify the legacy UI project (which is not included in this repository) to communicate with our REST API instead of directly accessing the database.

Here's how you would approach this:

### 1. Run Both Applications Side by Side

1. Run this modernized backend API (.NET 8)
2. Run the legacy VB.NET UI application separately

### 2. Modify the Legacy VB.NET UI

You would need to modify the legacy VB.NET UI to use HTTP requests to communicate with our API instead of using direct database access. This would involve:

1. Adding HTTP client capabilities to the legacy VB.NET application
2. Modifying the data access code to call our API endpoints
3. Handling authentication by obtaining and using JWT tokens

### Example of Required Changes in the Legacy UI

The legacy VB.NET application would need to be modified to include code similar to this:

```vb
' Example of how you would add HTTP client capabilities to the legacy VB.NET UI
' This would be added to the legacy UI project, not to our modernized backend

Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json

' This class would be added to the legacy VB.NET UI project
Public Class ApiClient
    Private Shared ReadOnly BaseUrl As String = "https://localhost:5001/api/"
    Private Shared ReadOnly Client As New HttpClient()
    Private Shared _token As String = ""

    ' Method to authenticate with our new API
    Public Shared Async Function Login(username As String, password As String) As Task(Of Boolean)
        Try
            ' Code to call our API's login endpoint
            ' ...
        Catch ex As Exception
            ' Error handling
        End Try
    End Function

    ' Methods to call our API endpoints
    ' ...
End Class
```

### 3. API Endpoints for Legacy UI Integration

Our modernized backend provides all the necessary API endpoints that the legacy UI would need to call. These include:

- Authentication endpoints for login
- CRUD operations for students, faculty, courses, etc.
- Specialized operations like updating student course, faculty subject, etc.

The full list of available endpoints is provided in the API Documentation section below.

## API Documentation

Swagger documentation is available at `/swagger` when running the application in development mode. It provides detailed information about all API endpoints, including request/response models and authentication requirements.

## Authentication

The API uses JWT Bearer token authentication. To obtain a token:

- For admin/user login: Use the `/api/auth/login` endpoint with valid credentials.
- For faculty login: Use the `/api/auth/faculty/login` endpoint with faculty email and password.

## API Endpoints

### Authentication

- `POST /api/auth/login`: Authenticate user and get JWT token
- `POST /api/auth/faculty/login`: Authenticate faculty and get JWT token

### Users

- `GET /api/users`: Get all users
- `GET /api/users/{id}`: Get user by ID
- `POST /api/users`: Create a new user
- `PUT /api/users/{id}`: Update an existing user
- `DELETE /api/users/{id}`: Delete a user

### Students

- `GET /api/students`: Get all students
- `GET /api/students/{id}`: Get student by ID
- `GET /api/students/course/{courseName}`: Get students by course
- `POST /api/students`: Create a new student
- `PUT /api/students/{id}`: Update an existing student
- `PUT /api/students/{id}/course/{courseName}`: Update student's course
- `DELETE /api/students/{id}`: Delete a student

### Faculties

- `GET /api/faculties`: Get all faculties
- `GET /api/faculties/{id}`: Get faculty by ID
- `GET /api/faculties/subject/{subjectName}`: Get faculties by subject
- `POST /api/faculties`: Create a new faculty
- `PUT /api/faculties/{id}`: Update an existing faculty
- `PUT /api/faculties/{id}/subject/{subjectName}`: Update faculty's subject
- `DELETE /api/faculties/{id}`: Delete a faculty

### Courses

- `GET /api/courses`: Get all courses
- `GET /api/courses/{id}`: Get course by ID
- `GET /api/courses/name/{name}`: Get course by name
- `POST /api/courses`: Create a new course
- `PUT /api/courses/{id}`: Update an existing course
- `DELETE /api/courses/{id}`: Delete a course

### Subjects

- `GET /api/subjects`: Get all subjects
- `GET /api/subjects/{id}`: Get subject by ID
- `GET /api/subjects/course/{courseName}`: Get subjects by course
- `GET /api/subjects/name/{name}`: Get subject by name
- `POST /api/subjects`: Create a new subject
- `PUT /api/subjects/{id}`: Update an existing subject
- `DELETE /api/subjects/{id}`: Delete a subject

### Attendances

- `GET /api/attendances`: Get all attendances
- `GET /api/attendances/{id}`: Get attendance by ID
- `GET /api/attendances/student/{studentId}`: Get attendances by student ID
- `GET /api/attendances/date/{date}`: Get attendances by date
- `GET /api/attendances/course/{courseName}/subject/{subjectName}`: Get attendances by course and subject
- `POST /api/attendances`: Create a new attendance record
- `DELETE /api/attendances/{id}`: Delete an attendance record

### Marks

- `GET /api/marks`: Get all marks
- `GET /api/marks/{id}`: Get mark by ID
- `GET /api/marks/student/{studentId}`: Get mark by student ID
- `GET /api/marks/course/{courseName}`: Get marks by course
- `POST /api/marks`: Create a new mark record
- `PUT /api/marks/{id}`: Update an existing mark record
- `DELETE /api/marks/{id}`: Delete a mark record

## Troubleshooting

### API Connection Issues

- Ensure the backend API is running
- Check that the API URL is correct
- Verify network connectivity between the client and server
- Check for any firewall or security restrictions

### Database Connection Issues

- Verify that MySQL Server is running
- Check the connection string in `appsettings.json`
- Ensure the database user has appropriate permissions
- Look for any error messages in the console or logs

### Authentication Issues

- Make sure you're using the correct credentials
- Check that the JWT token is being properly stored and sent with requests
- Verify that the token hasn't expired
- Look for any CORS-related issues if using a web client

## License

This project is licensed under the MIT License.
