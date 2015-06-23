CREATE TABLE [dbo].[tblJob] (
    [JobId]             UNIQUEIDENTIFIER NOT NULL,
    [EndDate]           DATETIME         NOT NULL,
    [CountFieldsChange] INT              NOT NULL,
    [CountCrmLogs]      INT              NOT NULL,
    [CountDuplicates]   INT              NULL,
    CONSTRAINT [PK_tblJob] PRIMARY KEY CLUSTERED ([JobId] ASC)
);



