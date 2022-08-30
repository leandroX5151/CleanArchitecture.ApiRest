BEGIN TRANSACTION;
GO

ALTER TABLE [Videos] DROP CONSTRAINT [FK_Videos_Streamers_StreamerId];
GO

CREATE TABLE [Actores] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Apellido] nvarchar(max) NULL,
    CONSTRAINT [PK_Actores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Directores] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Apellido] nvarchar(max) NULL,
    [VideoId] int NOT NULL,
    CONSTRAINT [PK_Directores] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Directores_Videos_VideoId] FOREIGN KEY ([VideoId]) REFERENCES [Videos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [VideoActor] (
    [VideoId] int NOT NULL,
    [ActorId] int NOT NULL,
    CONSTRAINT [PK_VideoActor] PRIMARY KEY ([ActorId], [VideoId]),
    CONSTRAINT [FK_VideoActor_Actores_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actores] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_VideoActor_Videos_VideoId] FOREIGN KEY ([VideoId]) REFERENCES [Videos] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Directores_VideoId] ON [Directores] ([VideoId]);
GO

CREATE INDEX [IX_VideoActor_VideoId] ON [VideoActor] ([VideoId]);
GO

ALTER TABLE [Videos] ADD CONSTRAINT [FK_Videos_Streamers_StreamerId] FOREIGN KEY ([StreamerId]) REFERENCES [Streamers] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211123200408_agregar-tablas-entidades-relaciones', N'6.0.0');
GO

COMMIT;
GO

