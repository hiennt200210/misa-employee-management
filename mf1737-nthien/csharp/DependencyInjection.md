# Dependency Injection

## Khái niệm Dependency Injection

Trong ngữ cảnh lập trình, _dependency (phụ thuộc)_ là một thành phần mà một đối tượng khác cần để hoạt động đúng. Đối tượng này có thể là một object, interface, service, hoặc bất cứ thứ gì mà đối tượng cần để thực hiện một nhiệm vụ cụ thể.

_Dependency Injection (DI)_ là một mẫu thiết kế phần mềm (design pattern) cho phép chúng ta cung cấp các dependency của một đối tượng từ bên ngoài, thay vì tạo ra chúng bên trong đối tượng đó. Điều này giúp tách biệt việc khởi tạo và quản lý dependency ra khỏi đối tượng chính, tạo ra sự linh hoạt và tái sử dụng cao hơn trong việc thiết kế và phát triển ứng dụng.

## Cách thức hoạt động của Dependency Injection

Trong Dependency Injection, chúng ta có ba phương pháp chính để cung cấp dependency vào một đối tượng: Constructor Injection, Property Injection và Method Injection.

### Constructor Injection (Tiêm phụ thuộc qua hàm khởi tạo)

Trong Constructor Injection, dependency được chuyển giao vào đối tượng thông qua các tham số của hàm khởi tạo. Đối tượng có thể sử dụng trực tiếp các dependency này để thực hiện các nhiệm vụ cần thiết. Dưới đây là một ví dụ về Constructor Injection:

```csharp
public class MyService
{
    private readonly IDependency _dependency;

    public MyService(IDependency dependency)
    {
        _dependency = dependency;
    }

    // Các phương thức của MyService sử dụng _dependency để thực hiện nhiệm vụ
}
```

### Property Injection (Tiêm phụ thuộc qua thuộc tính)

Trong Property Injection, dependency được chuyển giao vào đối tượng thông qua các thuộc tính công khai (public properties). Đối tượng có thể truy cập vào dependency thông qua các thuộc tính này. Dưới đây là một ví dụ về Property Injection:

```csharp
public class MyService
{
    public IDependency Dependency { get; set; }

    // Các phương thức của MyService sử dụng Dependency để thực hiện nhiệm vụ
}
```

### Method Injection (Tiêm phụ thuộc qua phương thức)

Trong Method Injection, dependency được chuyển giao vào đối tượng thông qua các phương thức. Đối tượng có thể sử dụng dependency thông qua các tham số của phương thức. Dưới đây là một ví dụ về Method Injection:

```csharp
public class MyService
{
    public void SetDependency(IDependency dependency)
    {
        // Đặt dependency vào một thuộc tính hoặc biến trong MyService
    }

    // Các phương thức của MyService sử dụng dependency để thực hiện nhiệm vụ
}
```

## Lợi ích của Dependency Injection

Sử dụng Dependency Injection trong việc quản lý các phụ thuộc trong ứng dụng mang lại nhiều lợi ích:

-   **Tái sử dụng và linh hoạt**: Dependency Injection giúp tách biệt việc khởi tạo và quản lý dependency ra khỏi đối tượng chính, làm cho việc thay đổi và tái sử dụng dependency dễ dàng hơn. Chúng ta có thể dễ dàng thay đổi implementation của một dependency mà không cần thay đổi mã nguồn của đối tượng sử dụng nó.

-   **Dễ kiểm thử**: Dependency Injection làm cho việc kiểm thử đơn vị (unit testing) dễ dàng hơn. Chúng ta có thể dễ dàng tạo ra các mock objects hoặc stubs để kiểm thử đối tượng mục tiêu mà không cần phụ thuộc vào các dependency thực tế.

-   **Giảm sự phụ thuộc**: Dependency Injection giúp giảm sự phụ thuộc giữa các thành phần của ứng dụng. Thay vì một đối tượng tự tạo và quản lý các dependency của nó, chúng ta chuyển giao các dependency từ bên ngoài, làm cho các thành phần trở nên độc lập và dễ dàng tái sử dụng.

-   **Dễ dàng mở rộng**: Dependency Injection cho phép chúng ta dễ dàng mở rộng ứng dụng bằng cách thêm mới các dependency và cấu hình chúng tại một vị trí tập trung. Chúng ta có thể thay đổi hoặc mở rộng các dependency mà không cần thay đổi mã nguồn của các thành phần khác.
