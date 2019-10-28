CREATE TABLE [dbo].[Student]
(
	[Barcode] INT NOT NULL PRIMARY KEY, 
    [GivenName] NCHAR(100) NOT NULL,
	[Surname] NCHAR(100) NOT NULL,
	[StudentID] INT NOT NULL,
    FOREIGN KEY (GroupNumber) REFERENCES [dbo].[Group]
)
