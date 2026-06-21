-- 建立資料庫與 Members 資料表，並插入 3 筆測試資料
-- 連線到 localdb 時可先執行此腳本
IF DB_ID('RestaurantDB') IS NULL
BEGIN
    CREATE DATABASE RestaurantDB;
END
GO

USE RestaurantDB;
GO

IF OBJECT_ID('dbo.Members', 'U') IS NOT NULL
    DROP TABLE dbo.Members;
GO

CREATE TABLE dbo.Members
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) NOT NULL UNIQUE,
    Email NVARCHAR(100) NULL,
    IsVIP BIT NOT NULL DEFAULT 0,
    DiscountRate DECIMAL(4,2) NOT NULL DEFAULT (0.05),
    RegisterDate DATETIME NOT NULL DEFAULT (GETDATE()),
    TotalPurchases DECIMAL(10,2) NOT NULL DEFAULT (0)
);
GO

-- 插入三筆測試資料：1 VIP、2 一般會員
INSERT INTO dbo.Members (Name, Phone, Email, IsVIP, DiscountRate, TotalPurchases)
VALUES
(N'王小明', N'0911000111', N'xming@example.com', 1, 0.15, 1200.00),
(N'陳美麗', N'0922000222', N'meili@example.com', 0, 0.05, 350.00),
(N'張志強', N'0933000333', N'zhang@example.com', 0, 0.05, 0.00);
GO