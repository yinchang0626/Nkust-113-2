-- Create Courses table
CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Instructor NVARCHAR(255),
    Price DECIMAL(10, 2),
    StartDate DATETIME2
);

-- Create Users table (Optional)
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255),
    Role NVARCHAR(50) DEFAULT 'User'
);

-- Create Enrollments table (Optional)
CREATE TABLE Enrollments (
    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    CourseId INT FOREIGN KEY REFERENCES Courses(CourseId),
    EnrollmentDate DATETIME2 DEFAULT GETDATE()
);