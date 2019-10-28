CREATE TABLE [dbo].[Class]
(
	[CourseCode] NVARCHAR(30) PRIMARY KEY, 
    [Day] NVARCHAR(30) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL,
	[StartTime] time Not NULL,
	[EndTime] time NOT NULL,
	[Capacity] int NOT NULL
)
