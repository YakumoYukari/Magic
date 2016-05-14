CREATE TABLE [dbo].[CardSets]
(
	[Name] NVARCHAR(MAX) NOT NULL PRIMARY KEY, 
    [Code] NVARCHAR(MAX) NULL, 
    [GathererCode] NVARCHAR(MAX) NULL, 
    [OldCode] NVARCHAR(MAX) NULL, 
    [ReleaseDate] NCHAR(10) NULL, 
    [Border] NCHAR(10) NULL, 
    [Type] NVARCHAR(MAX) NULL, 
    [Block] NVARCHAR(MAX) NULL, 
    [OnlineOnly] BIT NULL, 
    [Booster] NVARCHAR(MAX) NULL
)
