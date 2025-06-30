CREATE TABLE Vouchers (
	VoucherID INT IDENTITY (1, 1) PRIMARY KEY,
	VoucherDate DATETIME NOT NULL,
	ReferenceNo NVARCHAR(50), 
	VoucherType NVARCHAR(20), --- Journal, Payment, Receipt
	CreatedDate DATETIME DEFAULT GETDATE()

);

GO
CREATE TABLE VoucherEntries (
	EntryID INT IDENTITY(1,1) PRIMARY KEY,
	VoucherID INT FOREIGN KEY REFERENCES Vouchers(VoucherID),
    AccountID INT FOREIGN KEY REFERENCES ChartOfAccounts(AccountID),
	DebitAmount DECIMAL(18,2) DEFAULT 0,
    CreditAmount DECIMAL(18,2) DEFAULT 0,
    Remarks NVARCHAR(200)
) 