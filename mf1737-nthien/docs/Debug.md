# Kỹ năng Debug

## 1. Phím tắt

### 1.1. Phím tắt hỗ trợ lập trình

| Phím tắt         | Công dụng                                        |
| ---------------- | ------------------------------------------------ |
| Ctrl + .         | Quick actions                                    |
| Ctrl + K, S      | Đặt mã trong _region_, _class_, _interface_, v.v |
| Ctrl + M, O      | Collapse các định nghĩa                          |
| Ctrl + M, P      | Toggle các định nghĩa                            |
| Ctrl + F         | Tìm kiếm                                         |
| Ctrl + H         | Tìm kiếm và thay thế                             |
| Ctrl + Shift + F | Tìm kiếm và thay thế nâng cao                    |
| Ctrl + R, R      | Đổi tên hàng loạt                                |
| Ctrl + K, D      | Định dạng mã                                     |
| Ctrl + K, C      | Comment                                          |
| Ctrl + K, U      | Bỏ comment                                       |
| Ctrl + R, R      | Đổi tên hàng loạt                                |
| Shift + Alt + .  | Chọn thêm                                        |
| Shift + Alt + ,  | Chọn bớt                                         |
| Shift + Alt + ;  | Chọn tất cả                                      |
| Ctrl + Shift + L | Xoá một dòng                                     |

### 1.2. Phím tắt điều hướng

| Phím tắt         | Công dụng                                        |
| ---------------- | ------------------------------------------------ |
| F12              | Đi tới định nghĩa                                |
| Alt + F12        | Xem nhanh định nghĩa                             |
| Ctrl + F12       | Đi tới khai báo                                  |
| Ctrl + -         | Navigate backward (Quay lại tệp vừa mở trước đó) |
| Ctrl + Shift + - | Navigate forward                                 |

### 1.3. Phím tắt trước debug

| Phím tắt         | Công dụng            |
| ---------------- | -------------------- |
| F9               | Đặt/bỏ breakpoint    |
| F5               | Chạy có debug        |
| Ctrl + F5        | Chạy không debug     |
| Shift + F5       | Dừng                 |
| Ctrl + Shift + B | Build solution       |
| Ctrl + Alt + P   | Attach to Process    |
| Ctrl + R, A      | Chạy tất cả các test |
| Ctrl + R, T      | Chạy test đang focus |
| Ctrl + \, E      | Mở danh sách lỗi     |

### 1.4. Phím tắt trong quá trình debug

| Phím tắt                 | Công dụng                                              |
| ------------------------ | ------------------------------------------------------ |
| F5                       | Continue                                               |
| F10                      | Step over                                              |
| Ctrl + F10               | Run to cursor                                          |
| Ctrl + Shift + F10       | Đặt câu lệnh tiếp theo (hoặc kéo thả mũi tên màu vàng) |
| F11                      | Step into (nhảy vào trong hàm)                         |
| Shift + F11              | Step out (nhảy ra ngoài hàm)                           |
| Shift + F9               | Xem nhanh giá trị một biến                             |
| Ctrl + Alt + V + A       | Mở của số Auto                                         |
| Ctrl + Alt + B           | Mở cửa số Breakpoint                                   |
| Ctrl + Alt + C           | Mở cửa sổ Callstack                                    |
| Ctrl + Alt + I           | Mở cửa sổ Immediate                                    |
| Ctrl + Alt + V + L       | Mở cửa số Local                                        |
| Ctrl + Alt + W + 1/2/3/4 | Mở cửa sổ Watch                                        |

#### Công dụng của một số cửa sổ:

-   **Auto**: Cho phép theo dõi giá trị của tất cả các biến trong phạm vi hiện tại.
-   **Breakpoint**: Quản lý các breakpoint.
-   **Callstack**: Cho biết thứ tự gọi và luồng hoạt động của chương trình, cách thức chương trình đến được điểm hiện tại.
-   **Immediate**: Cho phép thực thi trực tiếp các câu lệnh trong quá trình gỡ lỗi (Ví dụ như gọi phương thức, khai báo biến, điều chỉnh giá trị của biến, v.v.)
-   **Local**: Cho phép chọn và theo dõi giá trị của các biến cục bộ và tham số của phương thức hiện tại trong quá trình gỡ lỗi.
-   **Watch**: Cho phép chọn các biến để theo dõi giá trị trong quá trình debug.

## 2. Thực hành

**Debug bài làm của Giang Trung Nghĩa (MF1733)**

_EmployeeController.cs_

Nhấn F5 để chạy project trong chế độ debug.

### 2.1. GetAllEmployeesAsync

```csharp
[HttpGet]
public async Task<dynamic> GetAllEmployeeAsync()
{
    // declare connect string
    var connection = _dbConnectionService.CreateConnection();

    // create sql
    var sql = "CALL Proc_Employee_GetAll()";

    try
    {

        // get data
        var result = await connection.QueryAsync<EmployeeEntity>(sql);

        return result.ToList();

    }
    catch (Exception ex)
    {
        var errorResponse = new ErrorResponse("MISAE-006", Guid.NewGuid());
        Console.WriteLine(ex);
        return StatusCode(500, errorResponse);
    }
}
```

-   Focus vào đầu phương thức `GetAllEmployeeAsync`, nhấn F9 để đặt breakpoint.
-   Thực hiện gọi API, luồng thực thi dừng tại đầu phương thức. Nhấn F10 để thực thi lần lượt từng câu lệnh và theo dõi luồng thực thi của chương trình.

**Luồng thực thi**

-   Gọi phương thức `CreateConnection` (Nhấn F11 để nhảy vào phương thức `CreateConnection`.) trả về một đối tượng kết nối cho biến `connection`.
-   Khai báo câu lệnh SQL lưu trữ trong biến `sql`.
-   Gọi phương thức `QueryAsync`, thực hiện truy vấn tới cơ sở dữ liệu.
-   Nếu thành công, chuyển `result` sang `List` và trả về kết quả.

Nếu không thành công:

-   Khởi tạo một `Guid` mới.
-   Khởi tạo một đối tượng `ErrorResponse` (Nhấn F11 để nhảy vào trong hàm khởi tạo.)
-   Bên trong hàm khởi tạo `ErrorResponse` Lần lượt gán giá trị cho các thuộc tính.
-   Hover vào `errorResponse` hoặc nhấn chọn rồi nhấn Shift + F9 để thấy giá trị đã được thay đổi.
-   In ra giá trị của exception.
-   Gọi phương thức StatusCode rồi trả về mã 500 và `errorResponse`.

_(Sử dụng phím tắt tương tự để theo dõi luồng thực thi của các phương thức dưới đây.)_

### 2.2. AddEmployeeAsync

```csharp
[HttpPost]
public async Task<dynamic> CreateEmployeeAsync([FromBody] EmployeeEntity employee)
{

    var connection = _dbConnectionService.CreateConnection();

    #region handle request body
    try
    {

        if (employee.EmployeeCode == null)
        {
            throw new NotFoundException("Không được phép để trống mã nhân viên");
        }
        else if (employee.FullName == null)
        {
            throw new NotFoundException("Không được phép để trống tên nhân viên");
        }

    }
    catch (NotFoundException ex)
    {
        var errorResponse = new ErrorResponse("MISAE-005", Guid.NewGuid(), ex.Message, ex.Message, "");

        return StatusCode(404, errorResponse);
    }
    catch (Exception ex)
    {

    }
    employee.EmployeeId = Guid.NewGuid();
    employee.PositionId = Guid.NewGuid();

    int lastSpaceIndex = employee.FullName.LastIndexOf(' '); // split fullname by last space (' ') index

    if (lastSpaceIndex != -1)
    {
        employee.FirstName = employee.FullName.Substring(lastSpaceIndex + 1); // Lấy phần từ cuối cùng trong tên
        employee.LastName = employee.FullName.Substring(0, lastSpaceIndex);    // Lấy phần còn lại

    }
    else
    {
        employee.FirstName = employee.FullName;
    }

    if (employee.Gender == null)
    {
        employee.Gender = GenderEnum.Other;
    }

    employee.GenderName = Enum.GetName(typeof(Enums.GenderEnum), employee.Gender);

    #endregion

    var sql = $"CALL Proc_Employee_Create(" +
        $"@EmployeeId," + // employee id
        $"@EmployeeCode," +
        $"@FullName," +
        $"@FirstName," +
        $"@LastName," +
        $"@Gender," +
        $"@GenderName," +
        $"@DateOfBirth," +
        $"@DepartmentId," +
        $"@CreatedBy," +
        $"NOW()," +
        $"@ModifiedBy," +
        $"NOW()," +
        $"@Email," +
        $"@PhoneNumber," +
        $"@IdentityNumber," +
        $"@IdentityDate," +
        $"@IdentityPlace," +
        $"@Address," +
        $"@Salary," +
        $"@PersonalTaxCode," +
        $"@WorkStatus," +
        $"@PositionId," +
        $"@PositionCode," +
        $"@PositionName," +
        $"@joinDate," +
        $"@MaritalStatus," +
        $"@MaritalStatusName," +
        $"@EducationalBackground," +
        $"@EducationalBackgroundName," +
        $"@QualificationName," +
        $"@BankAccount," +
        $"@BankName," +
        $"@BankBranch)";

    Type entityType = employee.GetType();
    PropertyInfo[] properties = entityType.GetProperties();
    var param = new DynamicParameters();

    foreach (PropertyInfo property in properties)
    {
        string propertyName = property.Name;
        object propertyValue = property.GetValue(employee);
        param.Add(propertyName, propertyValue);
        Console.WriteLine($"{propertyName}: {propertyValue}");
    }

    try
    {

        // get data
        var result = await connection.ExecuteAsync(sql, param);

        return result;
    }
    catch (Exception ex)
    {
        var errorResponse = new ErrorResponse("MISAE-004", Guid.NewGuid());
        Console.WriteLine(ex);

        return StatusCode(500, errorResponse);
    }
}
```

**Luồng thực thi**

-   Gọi phương thức `CreateConnection` (tương tự phương thức `GetAllEmployeeAsync`).
-   Nếu `EmployeeCode` hoặc `FullName` là `null` thì khởi tạo và ném ra `NotFoundException` với thông báo tương ứng.

Bắt ngoại lệ:

-   Nếu là `NotFoundException`, khởi tạo một đối tượng `ErrorResponse`, trả về mã lỗi 404 và đối tượng lỗi vừa tạo.
-   Nếu là `Exception`, không làm gì cả.

_(tiếp tục)_

-   Tạo giá trị `EmployeeId` và `PositionId` mới.
-   Tìm vị trí của ký tự `" "` trong `FullName`.
-   Nếu `FullName` có nhiều hơn một từ, gán giá trị cho `FirstName` và `LastName`. Ngược lại, gán giá trị `FullName` cho `FirstName`.
-   Nếu `Gender` là `null`, đặt giá trị giới tính là `GenderEnum.Other`.
-   Gán giá trị cho `GenderName`.
-   Khai báo câu lệnh SQL.
-   Sử dụng reflection để duyệt qua tất cả các thuộc tính của đối tượng, tạo dynamic param.
-   Thực hiện truy vấn tới cơ sở dữ liệu.
-   Nếu thành công, trả về kết quả.

Nếu không thành công:

-   Tạo một đối tượng `ErrorResponse`.
-   In ra giá trị của exception.
-   Gọi phương thức StatusCode rồi trả về mã 500 và `errorResponse`.
