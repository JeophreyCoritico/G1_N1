
CREATE TABLE dbo.Attendance
(
    SignIn DATETIME,
    SignOut DATETIME,
	[TeacherID] int,
	[GroupNumber] int,
	[SubjectCode] NVARCHAR(30) NOT NULL,
    [RoomNo] NVARCHAR(100) NOT NULL,
	[Barcode] INT NOT NULL,
	EarlyLeave BIT,
	Late BIT,
	PRIMARY KEY (SignIn, SignOut, TeacherID, GroupNumber, SubjectCode, RoomNo, Barcode),
	FOREIGN KEY ([TeacherID], [GroupNumber], [SubjectCode], [RoomNo]) REFERENCES [dbo].[Class],
	FOREIGN KEY ([Barcode], [GroupNumber]) REFERENCES [dbo].[Student]
);