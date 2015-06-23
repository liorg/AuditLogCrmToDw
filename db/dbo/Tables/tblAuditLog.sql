CREATE TABLE [dbo].[tblAuditLog] (
    [AuditLogId]      UNIQUEIDENTIFIER NOT NULL,
    [ChangeDateTime]  DATETIME         NOT NULL,
    [ModifiedByName]  NVARCHAR (MAX)   NOT NULL,
    [ModifiedByID]    UNIQUEIDENTIFIER NOT NULL,
    [ChangeType]      NVARCHAR (MAX)   NOT NULL,
    [EntityType]      NVARCHAR (MAX)   NOT NULL,
    [EntityTypeDesc]  NVARCHAR (MAX)   NOT NULL,
    [FieldSchemaName] NVARCHAR (MAX)   NOT NULL,
    [FieldDesc]       NVARCHAR (MAX)   NOT NULL,
    [OldValue]        NVARCHAR (MAX)   NULL,
    [NewValue]        NVARCHAR (MAX)   NULL,
    [CrmAuditId]      UNIQUEIDENTIFIER NOT NULL,
    [ModifiedOn]      DATETIME         CONSTRAINT [DF_tblAuditLog_ModifiedOn] DEFAULT (getdate()) NOT NULL,
    [JobId]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_tblAuditLog] PRIMARY KEY CLUSTERED ([AuditLogId] ASC)
);



