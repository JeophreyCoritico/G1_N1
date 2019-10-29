CREATE TABLE [dbo].[Student]
(
	[Barcode] INT NOT NULL, 
	[GroupNumber] INT NOT NuLL,
    [GivenName] NCHAR(100) NOT NULL,
	[Surname] NCHAR(100) NOT NULL,
	[StudentID] INT NOT NULL,
	PRIMARY KEY (Barcode, GroupNumber),
    FOREIGN KEY (GroupNumber) REFERENCES [dbo].[_Group]
)
