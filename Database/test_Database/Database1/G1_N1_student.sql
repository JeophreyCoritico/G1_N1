CREATE TABLE [dbo].[G1_N1_Student]
(
	[Barcode] INT NOT NULL, 
	[GroupNumber] INT NOT NuLL,
    [GivenName] NCHAR(100) NOT NULL,
	[Surname] NCHAR(100) NOT NULL,
	[StudentID] INT NOT NULL,
	PRIMARY KEY (Barcode, GroupNumber),
    FOREIGN KEY (GroupNumber) REFERENCES [dbo].[G1_N1_Group]
)
