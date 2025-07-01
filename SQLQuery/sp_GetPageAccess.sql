CREATE PROCEDURE sp_GetPageAccess
    @RoleName NVARCHAR(100),
    @PageName NVARCHAR(100)
AS
BEGIN
    SELECT COUNT(*) 
    FROM RoleAccess 
    WHERE RoleName = @RoleName AND PageName = @PageName
END