#Mini Order Management System
Dự án xây dựng hệ thống quản lý đơn hàng mini (Mini Order Management) áp dụng kiến trúc RESTful API, phân quyền JWT 
Tính năng chính
1. Back-end (.NET Core API)
Authentication & Authorization: Đăng ký, Đăng nhập, Phân quyền (Admin/User) sử dụng JWT.
Quản lý Sản phẩm (CRUD): Chỉ Admin được phép Thêm, Sửa, Xóa.
Quản lý Đơn hàng:
User: Tạo đơn hàng, Xem lịch sử đơn hàng của cá nhân.
Admin: Có quyền xem toàn bộ đơn hàng hệ thống.
Kiến trúc: Sử dụng mô hình Service Layer, Repository Pattern (thông qua EF Core), DTOs, AutoMapper.
Database: SQL Server (Code First Migration).
2. Front-end (HTML/JS)
Giao diện thân thiện, hiện đại (Inter font).
Trang Login: Đăng nhập và điều hướng theo quyền (Admin -> Trang quản lý, User -> Trang mua hàng).
Trang Admin: Quản lý danh sách sản phẩm.
Trang Order: Mua hàng, Giỏ hàng, Thanh toán.
Trang Lịch sử: Xem lại đơn hàng đã đặt.
- Công nghệ sử dụng
+BE: ASP.NET Core 6.0, Entity Framework Core, Identity, AutoMapper.
+FE: HTML5, CSS3, Vanilla JavaScript (Fetch API).
+DB: SQL Server.
+Tools: Visual Studio code, Swagger UI.
3. Hướng dẫn cài đặt & Chạy
Phần 1: Back-end (API)
Cấu hình Database:
Mở file appsettings.json.
Chỉnh sửa ConnectionStrings:DefaultConnection phù hợp với SQL Server của máy bạn (VD: Server=.\\SQLEXPRESS;...).
Chạy ứng dụng:
Mở Project bằng Visual Studio.
Nhấn F5 hoặc dotnet run để khởi chạy.
Phần 2: Front-end (Web)
Vào thư mục chứa các file HTML (login.html, admin.html, ...).
Mở file login.html bằng trình duyệt (Double click hoặc dùng Live Server).
Tiến hành đăng nhập và sử dụng.
4. Kiểm tra Port:
Khi API chạy, hãy để ý đường dẫn trên trình duyệt (Ví dụ: https://localhost:5058).
Quan trọng: Nếu Port khác 5058, hãy vào file admin.html, order.html, login.html... sửa lại hằng số const API_URL ở đầu file JS cho khớp.

