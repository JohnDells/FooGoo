IF OBJECT_ID('dbo.FooTypes', 'U') IS NOT NULL 
  DROP TABLE [dbo].[FooTypes]; 

CREATE TABLE [dbo].[FooTypes] (
	[FooTypeId] uniqueidentifier,
	[Name] varchar(50),
	[Active] bit
)
GO

IF OBJECT_ID('dbo.Foos', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Foos]; 

CREATE TABLE [dbo].[Foos] (
	[FooId] uniqueidentifier,
	[FooTypeId] uniqueidentifier,
	[Name] varchar(50),
	[Active] bit
)
GO

IF OBJECT_ID('dbo.Bars', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Bars]; 

CREATE TABLE [dbo].[Bars] (
	[BarId] uniqueidentifier,
	[FooId] uniqueidentifier,
	[Name] varchar(50),
	[Active] bit
)
GO

