IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Cadastros] (
    [Id] uniqueidentifier NOT NULL,
    [Email] varchar(100) NOT NULL,
    [Nome] varchar(250) NOT NULL,
    [Telefone] varchar(50) NOT NULL,
    [Logradouro] varchar(250) NOT NULL,
    [Numero] varchar(8) NOT NULL,
    [Bairro] nvarchar(max) NULL,
    [Cep] varchar(8) NOT NULL,
    [Complemento] varchar(250) NOT NULL,
    [Cidade] varchar(50) NOT NULL,
    [Estado] varchar(50) NOT NULL,
    CONSTRAINT [PK_Cadastros] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Produtos] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(250) NOT NULL,
    [Nome] varchar(250) NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Pedidos] (
    [Id] uniqueidentifier NOT NULL,
    [CadastroId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedidos_Cadastros_CadastroId] FOREIGN KEY ([CadastroId]) REFERENCES [Cadastros] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ItemPedidos] (
    [Id] uniqueidentifier NOT NULL,
    [PedidoId] uniqueidentifier NOT NULL,
    [ProdutoId] uniqueidentifier NOT NULL,
    [Quantidade] int NOT NULL,
    [PrecoUnitario] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ItemPedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemPedidos_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ItemPedidos_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_ItemPedidos_PedidoId] ON [ItemPedidos] ([PedidoId]);

GO

CREATE INDEX [IX_ItemPedidos_ProdutoId] ON [ItemPedidos] ([ProdutoId]);

GO

CREATE INDEX [IX_Pedidos_CadastroId] ON [Pedidos] ([CadastroId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200721003001_Tabelas', N'2.2.6-servicing-10079');

GO

ALTER TABLE [Produtos] ADD [Descricao] varchar(500) NOT NULL DEFAULT N'';

GO

ALTER TABLE [Produtos] ADD [Imagem] varchar(max) NOT NULL DEFAULT N'';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200724142223_FixInitialCreate', N'2.2.6-servicing-10079');

GO

