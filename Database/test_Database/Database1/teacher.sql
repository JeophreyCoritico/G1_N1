CREATE TABLE dbo.Teacher
(
    TeacherID int PRIMARY KEY,
    GivenName NVARCHAR(100) NOT NULL,
	Surname NVARCHAR(100) NOT NULL,
	TeacherPassword NvarChar(100) NOT NULL
);