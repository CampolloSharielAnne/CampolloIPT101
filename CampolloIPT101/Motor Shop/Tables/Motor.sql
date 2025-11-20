CREATE TABLE [dbo].[Motor]
(
	[MotorID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [MotorName] NVARCHAR(50) NULL, 
    [Brand] NVARCHAR(50) NULL, 
    [Price] NVARCHAR(50) NULL, 
    [Total] NVARCHAR(50) NULL
)
