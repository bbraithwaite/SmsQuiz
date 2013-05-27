CREATE TABLE [dbo].[Entrants] (
    [ID]            UNIQUEIDENTIFIER CONSTRAINT [DF_Entrants_ID] DEFAULT (newsequentialid()) NOT NULL,
    [CompetitionID] UNIQUEIDENTIFIER NOT NULL,
    [AnswerKey]     INT              NOT NULL,
    [Source]        INT              NOT NULL,
    [ContactType]   INT              NOT NULL,
    [ContactDetail] VARCHAR (50)     NOT NULL,
    [EntryDate]     SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_Entrants] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Entrants_Competitions] FOREIGN KEY ([CompetitionID]) REFERENCES [dbo].[Competitions] ([ID])
);

