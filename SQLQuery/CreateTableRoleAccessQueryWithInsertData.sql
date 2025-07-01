CREATE TABLE RoleAccess (
    Id INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(100),
    PageName NVARCHAR(100)
);

INSERT INTO RoleAccess (RoleName, PageName) VALUES 
('Admin', 'Home.Index'),
('Admin', 'ChartOfAccounts.Index'),
('Accountant', 'Voucher.Index'),
('Viewer', 'Home.Index');
Go
select * From RoleAccess