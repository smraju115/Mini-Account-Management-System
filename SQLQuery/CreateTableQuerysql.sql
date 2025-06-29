CREATE TABLE ChartOfAccounts (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    AccountName NVARCHAR(100) NOT NULL,
    ParentAccountID INT NULL,
    AccountType NVARCHAR(50) NOT NULL, -- Asset, Liability, Equity, Income, Expense
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);