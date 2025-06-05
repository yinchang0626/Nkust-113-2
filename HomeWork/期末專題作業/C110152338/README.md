# Online Course Platform

This project is a simple online course platform built with ASP.NET Core MVC for the backend and Angular for the frontend.

## Project Structure

*   `nkust/`: Contains project-related files.
    *   `database.sql`: SQL script for creating the database schema.
*   `Backend/`: ASP.NET Core MVC project.
*   `Frontend/`: Angular project.

## Setup Instructions

1.  **Create the database:**
    *   Use the `database.sql` script to create the database.
2.  **Backend Setup:**
    *   Navigate to the `Backend/` directory.
    *   Restore NuGet packages.
    *   Update the database connection string in `appsettings.json`.
    *   Run the application.
3.  **Frontend Setup:**
    *   Navigate to the `Frontend/` directory.
    *   Install dependencies using `npm install`.
    *   Configure the API endpoint in `src/environments/environment.ts`.
    *   Run the application using `ng serve`.

## Completed Features

*   Course listing and details.
*   (Simulated) Course enrollment.
*   Admin panel for course management (CRUD).

## Future Enhancements

*   User authentication and authorization.
*   Frontend form validation.
*   Backend API input validation.
*   Course search/filtering.
*   Image upload for course covers.