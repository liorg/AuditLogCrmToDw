

-- exec DW_DeleteDuplicate
CREATE PROCEDURE [dbo].[DW_DeleteDuplicate]

AS
BEGIN
	SET NOCOUNT ON;
 
WITH cte AS (
  SELECT FieldSchemaName, CrmAuditId , EntityType , 
     row_number() OVER(PARTITION BY FieldSchemaName, CrmAuditId , EntityType ORDER BY ModifiedOn asc) AS [rn]
  FROM tblAuditLog
)

DELETE cte WHERE [rn] > 1
select @@ROWCOUNT

END

