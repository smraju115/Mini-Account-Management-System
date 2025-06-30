CREATE PROCEDURE sp_GetVouchersByFilter
    @FromDate DATE = NULL,
    @ToDate DATE = NULL,
    @VoucherType NVARCHAR(20) = NULL
AS
BEGIN
    SELECT *
    FROM Vouchers
    WHERE
        (@FromDate IS NULL OR VoucherDate >= @FromDate)
        AND (@ToDate IS NULL OR VoucherDate <= @ToDate)
        AND (@VoucherType IS NULL OR VoucherType = @VoucherType)
    ORDER BY VoucherDate DESC
END