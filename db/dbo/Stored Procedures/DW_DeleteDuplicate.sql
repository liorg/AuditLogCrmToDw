-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

-- exec DW_DeleteDuplicate
CREATE PROCEDURE [dbo].[DW_DeleteDuplicate]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
WITH cte AS (
  SELECT FieldSchemaName, CrmAuditId , EntityType , 
     row_number() OVER(PARTITION BY FieldSchemaName, CrmAuditId , EntityType ORDER BY ModifiedOn asc) AS [rn]
  FROM tblAuditLog
)

DELETE cte WHERE [rn] > 1
select @@ROWCOUNT

END

--WITH cte AS (
--  SELECT FieldSchemaName, CrmAuditId , EntityType , ModifiedOn,
--     row_number() OVER(PARTITION BY FieldSchemaName, CrmAuditId , EntityType ORDER BY ModifiedOn asc) AS [rn]
--  FROM tblAuditLog
--  )
--  select * from cte 
