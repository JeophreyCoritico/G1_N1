CREATE TABLE [dbo].[Class]
(
	--[SubjectCode] NVARCHAR(30) PRIMARY KEY, 
	[TeacherID] int,
	[GroupNumber] int,
	[SubjectCode] NVARCHAR(30) NOT NULL,
    [RoomNo] NVARCHAR(100) NOT NULL,
	[Day] NVARCHAR(30) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL,
	[StartTime] time Not NULL,
	[EndTime] time NOT NULL,
	[Capacity] int NOT NULL,
	PRIMARY KEY (TeacherID, GroupNumber, SubjectCode, RoomNo),
	FOREIGN KEY (TeacherID) REFERENCES dbo.Teacher,
	FOREIGN KEY (GroupNumber) REFERENCES dbo._Group,
	FOREIGN KEY (SubjectCode) REFERENCES dbo.[subject],
	FOREIGN KEY (RoomNo) REFERENCES dbo.[room]
)
