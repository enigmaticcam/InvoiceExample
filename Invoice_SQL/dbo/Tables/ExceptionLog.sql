CREATE TABLE [dbo].[ExceptionLog]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LogMessage] NVARCHAR(MAX) NOT NULL, 
    [LogStackTrace] NVARCHAR(MAX) NOT NULL, 
    [AddedOn] DATETIME NOT NULL DEFAULT (getutcdate())
)
