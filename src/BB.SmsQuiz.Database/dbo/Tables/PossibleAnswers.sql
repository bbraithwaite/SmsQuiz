CREATE TABLE [dbo].[PossibleAnswers] (
    [CompetitionID]   UNIQUEIDENTIFIER NOT NULL,
    [AnswerKey]       TINYINT          NOT NULL,
    [AnswerText]      VARCHAR (50)     NOT NULL,
    [IsCorrectAnswer] BIT              NOT NULL,
    [ID] INT NOT NULL IDENTITY, 
    CONSTRAINT [FK_PossibleAnswers_Competitions] FOREIGN KEY ([CompetitionID]) REFERENCES [dbo].[Competitions] ([ID]), 
    CONSTRAINT [PK_PossibleAnswers] PRIMARY KEY ([ID]),
);


GO

CREATE UNIQUE INDEX [UIX_PossibleAnswers_CompetitionID_AnswerKey] ON PossibleAnswers(CompetitionID, AnswerKey)

GO
