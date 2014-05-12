
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/08/2014 10:50:06
-- Generated from EDMX file: C:\Users\magnus\Source\Repos\TextLine2\textis\TextisModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VERK014_H13];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProjectProjectLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectLine] DROP CONSTRAINT [FK_ProjectProjectLine];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_ProjectComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_ProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectUpvote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Upvote] DROP CONSTRAINT [FK_ProjectUpvote];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comment];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
GO
IF OBJECT_ID(N'[dbo].[ProjectLine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectLine];
GO
IF OBJECT_ID(N'[dbo].[Upvote]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Upvote];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comment'
CREATE TABLE [dbo].[Comment] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Project'
CREATE TABLE [dbo].[Project] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NULL,
    [CategoryId] int  NOT NULL
);
GO

-- Creating table 'ProjectLine'
CREATE TABLE [dbo].[ProjectLine] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [TimeFrom] datetime  NOT NULL,
    [TimeTo] datetime  NOT NULL,
    [TextLine1] nvarchar(max)  NULL,
    [TextLine2] nvarchar(max)  NULL,
    [Date] datetime  NOT NULL,
    [Language] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Upvote'
CREATE TABLE [dbo].[Upvote] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [PK_Comment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [PK_Project]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectLine'
ALTER TABLE [dbo].[ProjectLine]
ADD CONSTRAINT [PK_ProjectLine]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Upvote'
ALTER TABLE [dbo].[Upvote]
ADD CONSTRAINT [PK_Upvote]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectId] in table 'ProjectLine'
ALTER TABLE [dbo].[ProjectLine]
ADD CONSTRAINT [FK_ProjectProjectLine]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectLine'
CREATE INDEX [IX_FK_ProjectProjectLine]
ON [dbo].[ProjectLine]
    ([ProjectId]);
GO

-- Creating foreign key on [ProjectId] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_ProjectComment]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectComment'
CREATE INDEX [IX_FK_ProjectComment]
ON [dbo].[Comment]
    ([ProjectId]);
GO

-- Creating foreign key on [ProjectId] in table 'Upvote'
ALTER TABLE [dbo].[Upvote]
ADD CONSTRAINT [FK_ProjectUpvote]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Project]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectUpvote'
CREATE INDEX [IX_FK_ProjectUpvote]
ON [dbo].[Upvote]
    ([ProjectId]);
GO

-- Creating foreign key on [CategoryId] in table 'Project'
ALTER TABLE [dbo].[Project]
ADD CONSTRAINT [FK_CategoryProject]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Category]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProject'
CREATE INDEX [IX_FK_CategoryProject]
ON [dbo].[Project]
    ([CategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------