CREATE TABLE [dbo].[Users] (
    [ID]       UNIQUEIDENTIFIER CONSTRAINT [DF_Users_ID] DEFAULT (newsequentialid()) NOT NULL,
    [Username] VARCHAR (50)     NOT NULL,
    [Password] VARBINARY (20)   NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users]
    ON [dbo].[Users]([Username] ASC);

