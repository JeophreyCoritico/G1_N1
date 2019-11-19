CREATE TABLE [dbo].[G1_N1_Class]
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
	FOREIGN KEY (TeacherID) REFERENCES dbo.G1_N1_Teacher,
	FOREIGN KEY (GroupNumber) REFERENCES dbo.G1_N1_Group,
	FOREIGN KEY (SubjectCode) REFERENCES dbo.[G1_N1_subject],
	FOREIGN KEY (RoomNo) REFERENCES dbo.[G1_N1_room]
)
