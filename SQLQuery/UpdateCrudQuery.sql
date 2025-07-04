
ALTER PROCEDURE [dbo].[sp_ManageChartOfAccounts]
	@Action NVARCHAR(10),
	@AccountID INT = NULL,
	@AccountName NVARCHAR(100)= NULL,
	@ParentAccountID INT = NULL,
	@AccountType NVARCHAR(50) = NULL
AS
BEGIN

	SET NOCOUNT ON;

	IF @Action = 'GET'
    BEGIN
        SELECT * FROM ChartOfAccounts
    END
    ELSE IF @Action = 'GETBYID'
    BEGIN
        SELECT * FROM ChartOfAccounts WHERE AccountID = @AccountID
    END

	ELSE IF @Action = 'INSERT'
	BEGIN
		INSERT INTO ChartOfAccounts (AccountName, ParentAccountID, AccountType)
		VALUES (@AccountName, @ParentAccountID, @AccountType)
	END

	ELSE IF @Action = 'UPDATE'
		BEGIN
			UPDATE ChartOfAccounts
			SET AccountName = @AccountName,
				ParentAccountID = @ParentAccountID,
				AccountType = @AccountType
			WHERE	AccountID = @AccountID
		END

	ELSE IF @Action = 'DELETE'
		BEGIN 
			DELETE FROM ChartOfAccounts WHERE AccountID = @AccountID
		END

	
END;

