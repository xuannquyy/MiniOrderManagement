#Mini Order Management System
Dự án xây dựng hệ thống quản lý đơn hàng mini (Mini Order Management) áp dụng kiến trúc RESTful API, phân quyền JWT 
* Tính năng chính
1. Back-end (.NET Core API)
- bAuthentication & Authorization: Đăng ký, Đăng nhập, Phân quyền (Admin/User) sử dụng JWT.
- Quản lý Sản phẩm (CRUD): Chỉ Admin được phép Thêm, Sửa, Xóa.
-  Quản lý Đơn hàng:
     + User: Tạo đơn hàng, Xem lịch sử đơn hàng của cá nhân.
     + Admin: Có quyền xem toàn bộ đơn hàng hệ thống.
     + Kiến trúc: Sử dụng mô hình Service Layer, Repository Pattern (thông qua EF Core), DTOs, AutoMapper.
     + Database: SQL Server (Code First Migration).
2. Front-end (HTML/JS)
* Giao diện thân thiện, hiện đại (Inter font).
- Trang Login: Đăng nhập và điều hướng theo quyền (Admin -> Trang quản lý, User -> Trang mua hàng).
- Trang Admin: Quản lý danh sách sản phẩm.
- Trang Order: Mua hàng, Giỏ hàng, Thanh toán.
- Trang Lịch sử: Xem lại đơn hàng đã đặt.
* Công nghệ sử dụng
- BE: ASP.NET Core 6.0, Entity Framework Core, Identity, AutoMapper.
- FE: HTML5, CSS3, Vanilla JavaScript (Fetch API).
- DB: SQL Server.
- Tools: Visual Studio code, Swagger UI.
3. Hướng dẫn cài đặt & Chạy
* PHẦN 1: BACK-END (API & DATABASE)
- Cấu hình Database
    + Mở file appsettings.json trong project Backend.
    + Tìm đoạn ConnectionStrings.
    + Sửa lại Server=... cho phù hợp với tên SQL Server của máy bạn.
Ví dụ: Server=.\\SQLEXPRESS;Database=MiniOrderDb;Trusted_Connection=True;MultipleActiveResultSets=true
    + Khởi chạy Server API
    + Mở Project bằng Visual Studio.
        --> Nhấn F5 hoặc dotnet run để khởi chạy.
Lưu ý: Hệ thống tự động chạy Migration và tạo Admin/User mặc định.
Khi trình duyệt bật lên trang Swagger, copy đường dẫn API (Ví dụ: https://localhost:7288) để dùng cho phần Frontend.
3. Test API với Swagger (Lấy Token)
* Hệ thống tích hợp sẵn Swagger UI để test API trực quan. Dưới đây là cách lấy Token để test các API có khóa bảo mật 🔒.
- Lấy Token (Đăng nhập):
+ Tìm API POST /api/Auth/login -> Nhấn Try it out.
+ Nhập JSON tài khoản Admin (admin@mini.local / Admin@123).
+ Nhấn Execute.
- Copy chuỗi token trong phần Response Body (chỉ copy chuỗi token, bỏ dấu ngoặc kép).
- Xác thực (Authorize):
= Kéo lên đầu trang Swagger, nhấn nút Authorize (hình ổ khóa 🔓).
+ Nhập vào ô Value: Bearer <dán_token_vừa_copy>. (Lưu ý dấu cách).
+ Nhấn Authorize -> Close.
--> Bây giờ bạn có thể gọi các API bị khóa.
 PHẦN 2: FRONT-END (WEB APP)
--> Lưu ý: Đảm bảo Back-end đang chạy trước khi thực hiện phần này.
* Cách chạy 1: Môi trường Dev (Visual Studio + Live Server)
- Dùng khi đang phát triển hoặc sửa lỗi.
+ Vào thư mục wwwroot (hoặc thư mục chứa file HTML).
+ Mở file HTML bất kỳ (ví dụ login.html), sửa dòng đầu tiên trong thẻ <script>:const API_URL = "https://localhost:7288/api"; //port Swagger chay
+ Chạy file HTML bằng trình duyệt (hoặc Live Server).
* Cách chạy 2: Môi trường Production (IIS Local) - (Khuyên dùng)
- Đây là phần triển khai thực tế theo yêu cầu đề bài (Triển khai IIS).
- Cấu hình Hosts (Tên miền ảo):
+ Mở file C:\Windows\System32\drivers\etc\hosts (quyền Admin).
+ Thêm dòng: 127.0.0.1  MiniOrderAPI.com
- Triển khai IIS:
+ Publish code Backend ra thư mục (Ví dụ C:\Deploy).
+ Tạo Website mới trên IIS trỏ về thư mục đó.
+ Binding: Host name: MiniOrderAPI.com, Port: 80
+ App Pool: Chuyển Identity sang LocalSystem (để kết nối SQL).
- Chạy:
+ Mở trình duyệt truy cập: http://MiniOrderAPI.com/login.html
--> (Lúc này Frontend tự động gọi API qua domain ảo, không cần sửa port thủ công).
4. TÀI KHOẢN DÙNG THỬ (SEED DATA)
* Hệ thống đã tạo sẵn 2 tài khoản để thầy cô chấm điểm ngay lập tức:
| Vai trò | Email (Tài khoản) | Mật khẩu |
| 👑 ADMIN| admin@mini.local | Admin@123 |
| 👤 USER | user@mini.local | User@123 |
