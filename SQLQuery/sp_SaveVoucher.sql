CREATE PROCEDURE sp_SaveVoucher
    @VoucherDate DATE,
    @ReferenceNo NVARCHAR(50),
    @VoucherType NVARCHAR(20),
    @VoucherEntries VoucherEntryType READONLY -- Table-valued parameter
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vouchers (VoucherDate, ReferenceNo, VoucherType)
    VALUES (@VoucherDate, @ReferenceNo, @VoucherType);

    DECLARE @VoucherID INT = SCOPE_IDENTITY();

    INSERT INTO VoucherEntries (VoucherID, AccountID, DebitAmount, CreditAmount, Remarks)
    SELECT @VoucherID, AccountID, DebitAmount, CreditAmount, Remarks
    FROM @VoucherEntries;
END;