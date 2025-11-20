# MiniOrderManagement Repo nhóm đề thi cuối kỳ Backend
Mini Order Management – Hướng dẫn chạy Frontend và Backend
1. Giới thiệu
Dự án gồm hai phần:
Backend: ASP.NET Core Web API
Frontend: ReactJS
Cơ sở dữ liệu: SQL Server
2. Chạy Backend (API)
2.1 Yêu cầu
.NET 6 trở lên
SQL Server 2019 hoặc mới hơn
SQL Server Management Studio (tùy chọn)
2.2 Tạo cơ sở dữ liệu SQL Server
Mở SSMS và chạy câu lệnh:
CREATE DATABASE MiniOrderManagement;
GO

Ví dụ bảng:

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200),
    Price DECIMAL(18,2),
    Quantity INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);

2.3 Cấu hình chuỗi kết nối

Mở file appsettings.json trong Backend và sửa:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MiniOrderManagement;Trusted_Connection=True;TrustServerCertificate=True;"
}

2.4 Chạy Backend
cd MiniOrderManagement.Backend
dotnet restore
dotnet run


Backend chạy tại:

http://localhost:5000

https://localhost:7000
3. Chạy Frontend (React)
3.1 Yêu cầu
Node.js 18+
npm hoặc yarn
3.2 Cài đặt thư viện
cd mini-order-fe
npm install
3.3 Cấu hình URL API cho Frontend
Mở file:
src/config.js
Sửa giá trị:
export const API_URL = "http://localhost:5000/api";
3.4 Chạy Frontend
npm start
Frontend chạy tại:
http://localhost:3000/
4. Ghi chú
Cần chạy Backend trước để FE truy cập API.
Nếu Backend dùng HTTPS, FE phải trỏ đến HTTPS tương ứng.
Đảm bảo Backend đã bật CORS.