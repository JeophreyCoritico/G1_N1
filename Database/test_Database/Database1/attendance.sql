
CREATE TABLE dbo.Attendance
(
    SignIn DATETIME PRIMARY KEY,
    SignOut DATETIME PRIMARY KEY,
	EarlyLeave BIT,
	Late BIT
);