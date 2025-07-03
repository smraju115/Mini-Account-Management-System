-- 1. Page List Table
CREATE TABLE PageList (
    PageId INT PRIMARY KEY IDENTITY,
    PageName NVARCHAR(100) NOT NULL UNIQUE
);

-- 2. Role-Page Permission Table (Mapping)
CREATE TABLE RolePagePermission (
    Id INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50),
    PageId INT FOREIGN KEY REFERENCES PageList(PageId)
);