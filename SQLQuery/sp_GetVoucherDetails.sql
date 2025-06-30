CREATE PROCEDURE sp_GetVoucherDetails
    @VoucherID INT
AS
BEGIN
    SELECT * FROM Vouchers WHERE VoucherID = @VoucherID;

    SELECT ve.*, coa.AccountName
    FROM VoucherEntries ve
    INNER JOIN ChartOfAccounts coa ON ve.AccountID = coa.AccountID
    WHERE ve.VoucherID = @VoucherID;
END;