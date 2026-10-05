/* =========================================
   1. COURSES
   ========================================= */

INSERT INTO Courses (Name, Price)
VALUES
('C# Fundamentals', 1500),
('ASP.NET Core', 2000),
('Entity Framework Core', 1800),
('SQL Server', 1700);


/* =========================================
   2. STUDENTS
   ========================================= */

INSERT INTO Students (Name)
VALUES
('Ahmed'),
('Mohamed'),
('Omar'),
('Ali'),
('Youssef');


/* =========================================
   3. SCHEDULES
   ========================================= */

INSERT INTO Schedules (Title, SUN, MON, TUE, WED, THU, FRI, SAT)
VALUES
('Sunday - Tuesday', 1, 0, 1, 0, 0, 0, 0),
('Monday - Wednesday', 0, 1, 0, 1, 0, 0, 0),
('Saturday - Thursday', 0, 1, 1, 1, 1, 0, 1);


/* =========================================
   4. INSTRUCTORS
   ========================================= */

INSERT INTO Instructors (Name, OfficeId)
VALUES
('Ahmed Nasser', 0),
('Mohamed Ali', 1),
('Omar Hassan', 2);


/* =========================================
   5. OFFICES
   ========================================= */

INSERT INTO Offices (Name, Location, InstructorId)
VALUES
('Office 101', 'Building A - First Floor', 1),
('Office 202', 'Building B - Second Floor', 2),
('Office 303', 'Building C - Third Floor', 3);


/* =========================================
   6. SECTIONS
   ========================================= */

INSERT INTO Sections (Name, CourseId, InstructorId, ScheduleId, StartTime, EndTime)
VALUES
('C# Morning', 0, 4, 1, '09:00', '11:00'),
('ASP.NET Core', 1, 5, 2, '10:00', '12:00'),
('EF Core', 2,6, 3, '12:00', '14:00'),
('SQL Server', 3, 6, 4, '14:00', '16:00');


/* =========================================
   7. ENROLLMENTS
   ========================================= */

INSERT INTO Enrollments (SectionId, StudentId)
VALUES
(9, 0),
(11, 0),
(12, 0),
(10, 1),
(11, 1),
(12, 2),
(9, 2),
(10, 3),
(12, 4),
(11, 4);