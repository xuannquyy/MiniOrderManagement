CREATE DATABASE MiniOrderDb;
GO
USE MiniOrderDb;
GO

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL  -- Admin / User
);

CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(20),
    Address NVARCHAR(255)
);

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL CHECK (Price >= 0),
    Description NVARCHAR(255),
    Stock INT NOT NULL CHECK (Stock >= 0)
);

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',  -- Pending, Completed, Cancelled
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,

    FOREIGN KEY (CustomerID) REFERENCES Customers(Id)
);

CREATE TABLE OrderDetails (
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    PriceAtOrder DECIMAL(18,2) NOT NULL CHECK (PriceAtOrder >= 0),

    PRIMARY KEY (OrderID, ProductID),

    FOREIGN KEY (OrderID) REFERENCES Orders(Id),
    FOREIGN KEY (ProductID) REFERENCES Products(Id)
);

CREATE VIEW View_OrderFullInfo AS
SELECT 
    o.Id AS OrderID,
    c.Name AS CustomerName,
    o.OrderDate,
    o.Status,
    o.TotalAmount,
    od.ProductID,
    p.Name AS ProductName,
    od.Quantity,
    od.PriceAtOrder
FROM Orders o
JOIN Customers c ON o.CustomerID = c.Id
JOIN OrderDetails od ON o.Id = od.OrderID
JOIN Products p ON od.ProductID = p.Id;

INSERT INTO Users (Name, Email, PasswordHash, Role)
VALUES 
('Admin', 'vienxuanquy82024@gmail.com', 'admin123', 'Admin'),
('User1', 'truongthianh23ct112@gmail.com', 'user123', 'User');

INSERT INTO Customers (Name, Email, Phone, Address)
VALUES
(N'Nguyễn Thị Hồng Nhung', 'honggnhungg1605@gmail.com', '0909123456', N'TP.HCM'),
(N'Nguyễn Thị Phương Thảo', 'phuongthao2005ab@gmail.com', '0912345678', N'Hà Nội');

INSERT INTO Products (Name, Price, Description, Stock)
VALUES
(N'Bánh mì thịt', 25000, N'Bánh mì kẹp thịt ngon, nóng hổi', 50),
(N'Phở bò', 40000, N'Phở bò Hà Nội, nước dùng thơm ngon', 30),
(N'Gỏi cuốn', 15000, N'Gỏi cuốn tươi, chấm nước mắm đặc biệt', 40),
(N'Cà phê sữa đá', 20000, N'Cà phê rang xay, đá lạnh', 60),
(N'Trà sữa trân châu', 35000, N'Trà sữa thơm, trân châu dai', 50);
