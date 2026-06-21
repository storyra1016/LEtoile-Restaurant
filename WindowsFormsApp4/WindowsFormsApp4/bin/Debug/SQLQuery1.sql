USE RestaurantDB;

CREATE TABLE dbo.Members (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    Name           NVARCHAR(50)  NOT NULL,
    Phone          NVARCHAR(20)  NOT NULL UNIQUE,
    Email          NVARCHAR(100) NULL,
    IsVIP          BIT           NOT NULL DEFAULT 0,
    DiscountRate   DECIMAL(4,2)  NOT NULL DEFAULT 0.05,
    RegisterDate   DATETIME      NOT NULL DEFAULT GETDATE(),
    TotalPurchases DECIMAL(10,2) NOT NULL DEFAULT 0
);

INSERT INTO dbo.Members (Name, Phone, Email, IsVIP, DiscountRate, TotalPurchases)
VALUES 
    (N'王小明', N'0911000111', N'wang@test.com', 1, 0.15, 0),
    (N'陳美麗', N'0922000222', N'lee@test.com',  0, 0.05, 0),
    (N'張志強', N'0933000333', N'chen@test.com', 0, 0.05, 0);