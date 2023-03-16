CREATE TABLE [dbo].[LoginPageStats] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [LoginUserStatsId]      INT           NOT NULL,
    [ButtonType]            NVARCHAR (12) NOT NULL,
    [ClickedAfterInseconds] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([LoginUserStatsId]) REFERENCES [dbo].[LoginUserStats] ([Id]) 	on delete cascade
);

