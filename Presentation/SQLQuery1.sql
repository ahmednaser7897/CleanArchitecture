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
/*
INSERT INTO Schedules
    (Title, SUN, MON, TUE, WED, THU, FRI, SAT)
VALUES
('Sunday - Tuesday',      1, 0, 1, 0, 0, 0, 0),
('Monday - Wednesday',    0, 1, 0, 1, 0, 0, 0),
('Saturday - Thursday',   0, 1, 1, 1, 1, 0, 1);
*/

/* =========================================
   4. INSTRUCTORS
   ========================================= */

-- Don't set OfficeId yet because Offices don't exist yet.
INSERT INTO Instructors (Name, OfficeId)
VALUES
('Ahmed Nasser', NULL),
('Mohamed Ali', NULL),
('Omar Hassan', NULL);


/* =========================================
   5. OFFICES
   ========================================= */

INSERT INTO Offices (Name, Location, InstructorId)
VALUES
('Office 101', 'Building A - First Floor', 1),
('Office 202', 'Building B - Second Floor', 2),
('Office 303', 'Building C - Third Floor', 3);


/* =========================================
   6. UPDATE INSTRUCTORS
   ========================================= */

UPDATE Instructors
SET OfficeId = 1
WHERE Id = 1;

UPDATE Instructors
SET OfficeId = 2
WHERE Id = 2;

UPDATE Instructors
SET OfficeId = 3
WHERE Id = 3;


/* =========================================
   7. SECTIONS
   ========================================= */

INSERT INTO Sections
    (Name, CourseId, InstructorId, ScheduleId, StartTime, EndTime)
VALUES
('C# Morning', 1, 1, 1, '09:00', '11:00'),
('ASP.NET Core', 2, 2, 2, '10:00', '12:00'),
('EF Core', 3, NULL, 3, '12:00', '14:00'),
('SQL Server', 4, 3, 1, '14:00', '16:00');


/* =========================================
   8. ENROLLMENTS
   ========================================= */

INSERT INTO Enrollments (SectionId, StudentId)
VALUES
(1, 1),
(3, 1),
(4, 1),

(2, 2),
(3, 2),

(4, 3),
(1, 3),

(2, 4),

(4, 5),

(3, 5);