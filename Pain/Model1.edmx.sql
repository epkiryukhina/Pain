
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/14/2018 16:36:20
-- Generated from EDMX file: C:\Users\Xiaomi\Desktop\Учеба\2 курс\Pain\Pain\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [pain];
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

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateBirth] datetime  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Passport] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VisitSet'
CREATE TABLE [dbo].[VisitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstDate] datetime  NOT NULL,
    [SecondDate] datetime  NOT NULL,
    [RoomId] int  NOT NULL,
    [Client_Id] int  NOT NULL
);
GO

-- Creating table 'ServiceSet'
CREATE TABLE [dbo].[ServiceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [NumberOfPeople] int  NOT NULL,
    [NumberOfHours] int  NOT NULL,
    [VisitId] int  NOT NULL,
    [ClientId] int  NOT NULL,
    [TypeOfService_Id] int  NOT NULL
);
GO

-- Creating table 'TypeOfRoomSet'
CREATE TABLE [dbo].[TypeOfRoomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Capacity] int  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'RoomSet'
CREATE TABLE [dbo].[RoomSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [TypeOfRoom_Id] int  NOT NULL
);
GO

-- Creating table 'JobSet'
CREATE TABLE [dbo].[JobSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TypeOfServiceSet'
CREATE TABLE [dbo].[TypeOfServiceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [TypeOfPrice_Id] int  NOT NULL
);
GO

-- Creating table 'TypeOfPriceSet'
CREATE TABLE [dbo].[TypeOfPriceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PersonSet_Client'
CREATE TABLE [dbo].[PersonSet_Client] (
    [Debt] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Employee'
CREATE TABLE [dbo].[PersonSet_Employee] (
    [Id] int  NOT NULL,
    [Job_Id] int  NOT NULL
);
GO

-- Creating table 'ClientService'
CREATE TABLE [dbo].[ClientService] (
    [Client_Id] int  NOT NULL,
    [Service_Id] int  NOT NULL
);
GO

-- Creating table 'EmployeeTypeOfService'
CREATE TABLE [dbo].[EmployeeTypeOfService] (
    [Employee_Id] int  NOT NULL,
    [TypeOfService_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [PK_VisitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [PK_ServiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfRoomSet'
ALTER TABLE [dbo].[TypeOfRoomSet]
ADD CONSTRAINT [PK_TypeOfRoomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [PK_RoomSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobSet'
ALTER TABLE [dbo].[JobSet]
ADD CONSTRAINT [PK_JobSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfServiceSet'
ALTER TABLE [dbo].[TypeOfServiceSet]
ADD CONSTRAINT [PK_TypeOfServiceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfPriceSet'
ALTER TABLE [dbo].[TypeOfPriceSet]
ADD CONSTRAINT [PK_TypeOfPriceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Client'
ALTER TABLE [dbo].[PersonSet_Client]
ADD CONSTRAINT [PK_PersonSet_Client]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Employee'
ALTER TABLE [dbo].[PersonSet_Employee]
ADD CONSTRAINT [PK_PersonSet_Employee]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Client_Id], [Service_Id] in table 'ClientService'
ALTER TABLE [dbo].[ClientService]
ADD CONSTRAINT [PK_ClientService]
    PRIMARY KEY CLUSTERED ([Client_Id], [Service_Id] ASC);
GO

-- Creating primary key on [Employee_Id], [TypeOfService_Id] in table 'EmployeeTypeOfService'
ALTER TABLE [dbo].[EmployeeTypeOfService]
ADD CONSTRAINT [PK_EmployeeTypeOfService]
    PRIMARY KEY CLUSTERED ([Employee_Id], [TypeOfService_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Client_Id] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_ClientVisit]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[PersonSet_Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientVisit'
CREATE INDEX [IX_FK_ClientVisit]
ON [dbo].[VisitSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'ClientService'
ALTER TABLE [dbo].[ClientService]
ADD CONSTRAINT [FK_ClientService_Client]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[PersonSet_Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Service_Id] in table 'ClientService'
ALTER TABLE [dbo].[ClientService]
ADD CONSTRAINT [FK_ClientService_Service]
    FOREIGN KEY ([Service_Id])
    REFERENCES [dbo].[ServiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientService_Service'
CREATE INDEX [IX_FK_ClientService_Service]
ON [dbo].[ClientService]
    ([Service_Id]);
GO

-- Creating foreign key on [Job_Id] in table 'PersonSet_Employee'
ALTER TABLE [dbo].[PersonSet_Employee]
ADD CONSTRAINT [FK_EmployeeJob]
    FOREIGN KEY ([Job_Id])
    REFERENCES [dbo].[JobSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeJob'
CREATE INDEX [IX_FK_EmployeeJob]
ON [dbo].[PersonSet_Employee]
    ([Job_Id]);
GO

-- Creating foreign key on [Employee_Id] in table 'EmployeeTypeOfService'
ALTER TABLE [dbo].[EmployeeTypeOfService]
ADD CONSTRAINT [FK_EmployeeTypeOfService_Employee]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[PersonSet_Employee]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TypeOfService_Id] in table 'EmployeeTypeOfService'
ALTER TABLE [dbo].[EmployeeTypeOfService]
ADD CONSTRAINT [FK_EmployeeTypeOfService_TypeOfService]
    FOREIGN KEY ([TypeOfService_Id])
    REFERENCES [dbo].[TypeOfServiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeTypeOfService_TypeOfService'
CREATE INDEX [IX_FK_EmployeeTypeOfService_TypeOfService]
ON [dbo].[EmployeeTypeOfService]
    ([TypeOfService_Id]);
GO

-- Creating foreign key on [RoomId] in table 'VisitSet'
ALTER TABLE [dbo].[VisitSet]
ADD CONSTRAINT [FK_RoomVisit]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[RoomSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomVisit'
CREATE INDEX [IX_FK_RoomVisit]
ON [dbo].[VisitSet]
    ([RoomId]);
GO

-- Creating foreign key on [TypeOfService_Id] in table 'ServiceSet'
ALTER TABLE [dbo].[ServiceSet]
ADD CONSTRAINT [FK_ServiceTypeOfService]
    FOREIGN KEY ([TypeOfService_Id])
    REFERENCES [dbo].[TypeOfServiceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceTypeOfService'
CREATE INDEX [IX_FK_ServiceTypeOfService]
ON [dbo].[ServiceSet]
    ([TypeOfService_Id]);
GO

-- Creating foreign key on [TypeOfRoom_Id] in table 'RoomSet'
ALTER TABLE [dbo].[RoomSet]
ADD CONSTRAINT [FK_RoomTypeOfRoom]
    FOREIGN KEY ([TypeOfRoom_Id])
    REFERENCES [dbo].[TypeOfRoomSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomTypeOfRoom'
CREATE INDEX [IX_FK_RoomTypeOfRoom]
ON [dbo].[RoomSet]
    ([TypeOfRoom_Id]);
GO

-- Creating foreign key on [TypeOfPrice_Id] in table 'TypeOfServiceSet'
ALTER TABLE [dbo].[TypeOfServiceSet]
ADD CONSTRAINT [FK_TypeOfServiceTypeOfPrice]
    FOREIGN KEY ([TypeOfPrice_Id])
    REFERENCES [dbo].[TypeOfPriceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfServiceTypeOfPrice'
CREATE INDEX [IX_FK_TypeOfServiceTypeOfPrice]
ON [dbo].[TypeOfServiceSet]
    ([TypeOfPrice_Id]);
GO

-- Creating foreign key on [Id] in table 'PersonSet_Client'
ALTER TABLE [dbo].[PersonSet_Client]
ADD CONSTRAINT [FK_Client_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Employee'
ALTER TABLE [dbo].[PersonSet_Employee]
ADD CONSTRAINT [FK_Employee_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------