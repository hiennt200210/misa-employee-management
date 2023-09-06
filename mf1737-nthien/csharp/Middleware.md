# Middleware

## Khái niệm Middleware

Khi một request được gửi từ client đến server, nó sẽ trải qua quá trình xử lý để trả về response. Quá trình này thường được chia thành nhiều công đoạn xử lý khác nhau, trong đó mỗi công đoạn được phụ trách bởi một thành phần của ứng dụng được gọi là _middleware_.

Các middleware thường được triển khai dưới dạng một _pipeline (đường ống)_, tạo thành một chuỗi middleware nối tiếp. Mỗi middleware có thể thực hiện các xử lý và sau đó chuyển tiếp request đến middleware tiếp theo trong chuỗi. Quá trình này cứ tiếp diễn cho đến khi request được xử lý hoàn toàn và trả về response cho client.

## Cách hoạt động của Middleware

Khi một yêu cầu được gửi từ client tới server, nó đi qua chuỗi các thành phần middleware trong pipeline. Mỗi thành phần middleware nhận yêu cầu, thực hiện một số tác vụ xử lý như xác thực, ghi log, kiểm tra quyền truy cập, xử lý lỗi và nén dữ liệu, sau đó chuyển tiếp yêu cầu tới thành phần middleware tiếp theo. Quá trình này tiếp diễn cho đến khi yêu cầu đi qua tất cả các thành phần middleware trong pipeline và đến đích cuối cùng.

Thứ tự các thành phần middleware trong pipeline quyết định thứ tự xử lý yêu cầu và ảnh hưởng đến quá trình xử lý của yêu cầu. Bằng cách thay đổi thứ tự các thành phần middleware hoặc thêm/xóa các thành phần, chúng ta có thể điều chỉnh quá trình xử lý yêu cầu theo yêu cầu cụ thể của ứng dụng.

## Ví dụ về Middleware

Ví dụ một số middleware thực hiện các phần xử lý khác nhau trong ứng dụng:

1. Middleware xác thực: Middleware xác thực kiểm tra xem người dùng đã đăng nhập hay chưa. Nếu người dùng chưa đăng nhập, yêu cầu có thể được chuyển hướng tới trang đăng nhập. Nếu người dùng đã đăng nhập, yêu cầu được chuyển tiếp tới middleware tiếp theo.

2. Middleware ghi log: Middleware ghi log ghi lại các sự kiện, hoạt động và thông tin quan trọng trong quá trình xử lý yêu cầu. Điều này giúp quản lý và giám sát ứng dụng, phân tích lỗi và hiểu rõ hơn về hoạt động của ứng dụng.

3. Middleware kiểm tra quyền truy cập: Middleware kiểm tra quyền truy cập đảm bảo rrằng người dùng chỉ có quyền truy cập vào các phần của ứng dụng mà họ được phép. Nếu người dùng không có quyền truy cập, yêu cầu có thể bị từ chối hoặc chuyển hướng tới trang lỗi.

4. Middleware xử lý lỗi: Middleware xử lý lỗi giúp xử lý các ngoại lệ và lỗi xảy ra trong quá trình xử lý yêu cầu. Nó có thể ghi log lỗi, hiển thị thông báo lỗi cho người dùng hoặc chuyển hướng tới trang lỗi tùy thuộc vào tình huống.

5. Middleware nén dữ liệu: Middleware nén dữ liệu giảm kích thước của dữ liệu trước khi gửi từ server tới client. Điều này giúp tăng tốc độ truyền dữ liệu và giảm lưu lượng mạng.

6. Middleware bảo mật: Middleware bảo mật bao gồm các thành phần như xác thực, mã hóa và chứng thực để đảm bảo an toàn và bảo mật cho ứng dụng.

7. Middleware định tuyến: Middleware định tuyến định tuyến yêu cầu tới các tác vụ xử lý cụ thể dựa trên các tiêu chí như URL, phương thức HTTP hoặc các thông tin khác về yêu cầu.
