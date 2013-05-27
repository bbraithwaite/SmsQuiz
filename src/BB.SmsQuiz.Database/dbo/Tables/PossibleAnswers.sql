CREATE TABLE [dbo].[PossibleAnswers] (
    [CompetitionID]   UNIQUEIDENTIFIER NOT NULL,
    [AnswerKey]       TINYINT          NOT NULL,
    [AnswerText]      VARCHAR (50)     NOT NULL,
    [IsCorrectAnswer] BIT              NOT NULL,
    CONSTRAINT [FK_PossibleAnswers_Competitions] FOREIGN KEY ([CompetitionID]) REFERENCES [dbo].[Competitions] ([ID])
);

