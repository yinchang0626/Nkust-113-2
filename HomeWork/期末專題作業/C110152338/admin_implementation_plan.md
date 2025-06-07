# Admin Login and Course Management Implementation Plan

This document outlines the plan for implementing admin login and course management features in the online course platform.

**1. Database Changes:**

*   Add an `isAdmin` column to the `Users` table to indicate admin status.
*   Create a default admin user with username "kerong" and password "abc123" and set `isAdmin` to true.

**2. Backend (C# .NET):**

*   **Authentication:**
    *   Implement an authentication controller (`AuthController.cs`) with methods for:
        *   `Login`: Authenticates the user and returns a JWT token.
    *   Use JWT (JSON Web Tokens) for authentication.
*   **Authorization:**
    *   Create an `AdminAuthorizeAttribute` to protect admin-only endpoints.
*   **Course Management API:**
    *   Modify the `CoursesController.cs` to include CRUD operations:
        *   `CreateCourse`: Creates a new course (protected by `AdminAuthorizeAttribute`).
        *   `UpdateCourse`: Updates an existing course (protected by `AdminAuthorizeAttribute`).
        *   `DeleteCourse`: Deletes a course (protected by `AdminAuthorizeAttribute`).
    *   Ensure that the existing `GetCourses` method is accessible to all users.

**3. Frontend (Angular):**

*   **Admin Login Page:**
    *   Create an admin login page (`AdminLoginComponent`) with a form for username and password.
    *   On successful login, store the JWT token in local storage.
*   **Admin Interface:**
    *   Create an admin interface (`AdminComponent`) with the following features:
        *   Display a list of courses with options to edit or delete.
        *   Add a form to create new courses.
    *   Use the JWT token to authenticate API requests to the backend.
*   **Authentication Service:**
    *   Create an `AuthService` to handle authentication-related tasks:
        *   `login`: Sends a login request to the backend and stores the JWT token.
        *   `logout`: Removes the JWT token from local storage.
        *   `isAdmin`: Checks if the user is an admin based on the JWT token.

## Backend Architecture Diagram

```mermaid
graph LR
    UserController --> AuthService
    CoursesController --> AdminAuthorizeAttribute
    CoursesController --> CourseService
    AuthService --> JWT
    AdminAuthorizeAttribute --> AuthService
    CourseService --> ApplicationDbContext
    ApplicationDbContext --> Database