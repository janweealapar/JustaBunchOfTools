IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Operators] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Operators] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Statuses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Statuses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UnitTests] (
    [Id] int NOT NULL IDENTITY,
    [TestName] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Server] nvarchar(max) NULL,
    [DatabaseId] int NOT NULL,
    [DatabaseName] nvarchar(max) NULL,
    [ObjectId] int NOT NULL,
    [ObjectName] nvarchar(max) NULL,
    [ObjectType] nvarchar(max) NULL,
    [StatusId] int NULL,
    [RecordUser] nvarchar(max) NULL,
    [DateRecorded] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NOT NULL,
    CONSTRAINT [PK_UnitTests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UnitTests_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id])
);
GO

CREATE TABLE [UnitTestParameter] (
    [Id] int NOT NULL IDENTITY,
    [ParameterId] int NOT NULL,
    [ParameterName] nvarchar(max) NULL,
    [ParameterType] nvarchar(max) NULL,
    [MaxLength] int NOT NULL,
    [Precision] int NOT NULL,
    [Scale] int NOT NULL,
    [IsOutput] bit NOT NULL,
    [Value] nvarchar(max) NULL,
    [UnitTestId] int NULL,
    [RecordUser] nvarchar(max) NULL,
    [DateRecorded] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NOT NULL,
    CONSTRAINT [PK_UnitTestParameter] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UnitTestParameter_UnitTests_UnitTestId] FOREIGN KEY ([UnitTestId]) REFERENCES [UnitTests] ([Id])
);
GO

CREATE TABLE [UnitTestAssertation] (
    [Id] int NOT NULL IDENTITY,
    [UnitTestId] int NULL,
    [UnitTestParameterId] int NULL,
    [ExpectedValue] nvarchar(max) NULL,
    [OperatorId] int NULL,
    [ActualValue] nvarchar(max) NULL,
    [StatusId] int NULL,
    [RecordUser] nvarchar(max) NULL,
    [DateRecorded] datetime2 NOT NULL,
    [ModifiedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NOT NULL,
    CONSTRAINT [PK_UnitTestAssertation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UnitTestAssertation_Operators_OperatorId] FOREIGN KEY ([OperatorId]) REFERENCES [Operators] ([Id]),
    CONSTRAINT [FK_UnitTestAssertation_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([Id]),
    CONSTRAINT [FK_UnitTestAssertation_UnitTestParameter_UnitTestParameterId] FOREIGN KEY ([UnitTestParameterId]) REFERENCES [UnitTestParameter] ([Id]),
    CONSTRAINT [FK_UnitTestAssertation_UnitTests_UnitTestId] FOREIGN KEY ([UnitTestId]) REFERENCES [UnitTests] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Operators]'))
    SET IDENTITY_INSERT [Operators] ON;
INSERT INTO [Operators] ([Id], [Name])
VALUES (1, N'Equal'),
(2, N'Not Equal'),
(3, N'Greater Than'),
(4, N'Greater Than Or Equal To'),
(5, N'LessThan'),
(6, N'Less Than Or Equal To');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Operators]'))
    SET IDENTITY_INSERT [Operators] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Statuses]'))
    SET IDENTITY_INSERT [Statuses] ON;
INSERT INTO [Statuses] ([Id], [Name])
VALUES (1, N'Failed'),
(2, N'Success');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Statuses]'))
    SET IDENTITY_INSERT [Statuses] OFF;
GO

CREATE INDEX [IX_UnitTestAssertation_OperatorId] ON [UnitTestAssertation] ([OperatorId]);
GO

CREATE INDEX [IX_UnitTestAssertation_StatusId] ON [UnitTestAssertation] ([StatusId]);
GO

CREATE INDEX [IX_UnitTestAssertation_UnitTestId] ON [UnitTestAssertation] ([UnitTestId]);
GO

CREATE INDEX [IX_UnitTestAssertation_UnitTestParameterId] ON [UnitTestAssertation] ([UnitTestParameterId]);
GO

CREATE INDEX [IX_UnitTestParameter_UnitTestId] ON [UnitTestParameter] ([UnitTestId]);
GO

CREATE INDEX [IX_UnitTests_StatusId] ON [UnitTests] ([StatusId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230201182504_InitialMigration', N'7.0.2');
GO

COMMIT;
GO

