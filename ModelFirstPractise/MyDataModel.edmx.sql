
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/06/2018 09:38:25
-- Generated from EDMX file: C:\Users\NeptuneDev1\Documents\Visual Studio 2013\Projects\Git\TobeMvcPractise\ModelFirstPractise\MyDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LibraryDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [OtherName] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNo] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Faculty] nvarchar(max)  NOT NULL,
    [Department] nvarchar(max)  NOT NULL,
    [Level] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NULL
);
GO

-- Creating table 'Drawers'
CREATE TABLE [dbo].[Drawers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DrawerId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [ISBN] nvarchar(max)  NOT NULL,
    [DatePublished] datetime  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NULL
);
GO

-- Creating table 'BorrowedBooks'
CREATE TABLE [dbo].[BorrowedBooks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [BookId] int  NOT NULL,
    [DateBorrowed] datetime  NOT NULL,
    [DateToReturn] datetime  NULL,
    [IsReturned] bit  NOT NULL,
    [DateReturned] nvarchar(max)  NOT NULL,
    [AdminId] int  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NULL
);
GO

-- Creating table 'Administrators'
CREATE TABLE [dbo].[Administrators] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNo] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [OtherName] nvarchar(max)  NOT NULL,
    [Designation] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Drawers'
ALTER TABLE [dbo].[Drawers]
ADD CONSTRAINT [PK_Drawers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BorrowedBooks'
ALTER TABLE [dbo].[BorrowedBooks]
ADD CONSTRAINT [PK_BorrowedBooks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Administrators'
ALTER TABLE [dbo].[Administrators]
ADD CONSTRAINT [PK_Administrators]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DrawerId] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_DrawersBooks]
    FOREIGN KEY ([DrawerId])
    REFERENCES [dbo].[Drawers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DrawersBooks'
CREATE INDEX [IX_FK_DrawersBooks]
ON [dbo].[Books]
    ([DrawerId]);
GO

-- Creating foreign key on [AdminId] in table 'BorrowedBooks'
ALTER TABLE [dbo].[BorrowedBooks]
ADD CONSTRAINT [FK_AdministratorsBorrowedBooks]
    FOREIGN KEY ([AdminId])
    REFERENCES [dbo].[Administrators]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdministratorsBorrowedBooks'
CREATE INDEX [IX_FK_AdministratorsBorrowedBooks]
ON [dbo].[BorrowedBooks]
    ([AdminId]);
GO

-- Creating foreign key on [UserId] in table 'BorrowedBooks'
ALTER TABLE [dbo].[BorrowedBooks]
ADD CONSTRAINT [FK_UsersBorrowedBooks]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersBorrowedBooks'
CREATE INDEX [IX_FK_UsersBorrowedBooks]
ON [dbo].[BorrowedBooks]
    ([UserId]);
GO

-- Creating foreign key on [BookId] in table 'BorrowedBooks'
ALTER TABLE [dbo].[BorrowedBooks]
ADD CONSTRAINT [FK_BooksBorrowedBooks]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BooksBorrowedBooks'
CREATE INDEX [IX_FK_BooksBorrowedBooks]
ON [dbo].[BorrowedBooks]
    ([BookId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------