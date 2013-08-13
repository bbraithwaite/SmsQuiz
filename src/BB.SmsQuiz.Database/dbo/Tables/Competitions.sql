CREATE TABLE [dbo].[Competitions] (
    [ID]             UNIQUEIDENTIFIER CONSTRAINT [DF_Competitions_ID] DEFAULT (newsequentialid()) NOT NULL,
    [ClosingDate]    SMALLDATETIME    NOT NULL,
    [CompetitionKey] VARCHAR (10)     NOT NULL,
    [CreatedDate]    SMALLDATETIME    NOT NULL,
    [Question]       VARCHAR (250)    NOT NULL,
    [Status]         TINYINT          NOT NULL,
    [CreatedByID]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Competitions] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Competitions_Users] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[Users] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Competitions]
    ON [dbo].[Competitions]([CompetitionKey] ASC);

