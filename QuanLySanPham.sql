Create database QuanLySanPham;
Go
Use QuanLySanPham;
Go

--  bảng Users
CREATE TABLE Users (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL -- Admin / User
);

 --nbảng Customers

CREATE TABLE Customers (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Phone NVARCHAR(20),
    Address NVARCHAR(500)
);

--  bảng Products

CREATE TABLE Products (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL CHECK (Price >= 0),
    Stock INT NOT NULL CHECK (Stock >= 0)
);

--  bảng Orders
CREATE TABLE Orders (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL CHECK (TotalAmount >= 0),
    FOREIGN KEY (CustomerID) REFERENCES Customers(ID)
);

--  bảng OrderDetails
CREATE TABLE OrderDetails (
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    PriceAtOrder DECIMAL(18,2) NOT NULL CHECK (PriceAtOrder >= 0),
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Orders(ID),
    FOREIGN KEY (ProductID) REFERENCES Products(ID)
);
