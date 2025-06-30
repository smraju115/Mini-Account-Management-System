CREATE TYPE VoucherEntryType AS TABLE
(
    AccountID INT,
    DebitAmount DECIMAL(18,2),
    CreditAmount DECIMAL(18,2),
    Remarks NVARCHAR(200)
);