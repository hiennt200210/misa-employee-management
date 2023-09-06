# Attribute trong C#

## Khái niệm Attribute

Trong ngôn ngữ lập trình C#, _attribute_ là một tính năng cho phép liên kết thông tin mô tả (metadata) với các thành phần của chương trình như lớp, phương thức, thuộc tính, trường, v.v.

Thông tin mô tả thường là thông tin về tác giả, phiên bản, chú thích, hướng dẫn sử dụng, thông tin gỡ lỗi.

Attribute được đặt trong cặp dấu ngoặc vuông và phân tách với thành phần mã bởi khoảng trắng (có thể bao gồm cả dấu ngắt dòng).

## Sử dụng Attribute

Để gắn metadata cho một thành phần trong chương trình, ta đặt attribute ngay trên thành phần đó.

Ví dụ:

```csharp
[Serializable]
public class SampleClass {..}
```

## Attribute có sẵn

Một số Attribute có sẵn trong C#:

| Attribute          | Mô tả                                                                                     |
| ------------------ | ----------------------------------------------------------------------------------------- |
| `[Obsolete]`       | Cho biết các thành phần đã lỗi thời, không nên sử dụng nữa.                               |
| `[Serializable]`   | Cho biết một thành phần có thể tuần tự hoá (serialize) và giải tuần tự hoá (deserialize). |
| `[DllImport]`      | Cho biết một phương thức được nhập từ thư viện liên kết động (DLL).                       |
| `[Conditional]`    | Cho biết điều kiện để gọi một thành phần.                                                 |
| `[AttributeUsage]` | Cho biết các quy tắc của một attribute tự định nghĩa.                                     |
| `[MethodImpl]`     | Cho biết cách triển khai của một phương thức.                                             |

## Tạo Attribute tuỳ chỉnh

**Bước 1**. Xác định các quy tắc sử dụng cho attribute bằng `AttributeUsage`.

Ví dụ:

```csharp
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class SampleClass {..}
```

Giải thích:

-   `AttributeTargets` cho biết các kiểu thành phần trong mã có thể được áp dụng attribute này.

-   `Inherited` cho biết attribute có thể được các lớp con kế thừa.

-   `AllowMultiple` cho biết attribute có thể được áp dụng nhiều phiên bản trên cùng một thành phần mã.

**Bước 2**. Khai báo class cho attribute

Lưu ý:

-   Kế thừa trực tiếp hoặc gián tiếp từ lớp Attribute.
-   Các lớp attribute phải được khai báo là public
-   (Không bắt buộc) Tên lớp attribute kết thúc bằng "Attribute". Tuy nhiên, khi áp dụng attribute, có thể bao gồm cả "Attribute" hoặc không.
-   Tất cả attribute tự định nghĩa phải kế thừa (trực tiếp hoặc gián tiếp) lớp System.Attribute

Ví dụ:

```csharp
public class MyAttribute : Attribute {..}

```

**Bước 3**. Khai báo constructor

Các tham số của constructor sẽ là các tham số bắt buộc khi áp dụng attribute.

```csharp
public MyAttribute(bool myvalue)
{
    this.myvalue = myvalue;
}
```

**Bước 4**. Khai báo các tham số tuỳ chọn

```csharp
// Tham số tuỳ chọn MyProperty
public bool MyProperty
{
    get {return this.myvalue;}
    set {this.myvalue = value;}
}
```

## Truy cập dữ liệu của Attribute

**Bước 1**. Khởi tạo một đối tượng thuộc lớp của attribute cần truy xuất các giá trị đó thông qua phương thức `Attribute.GetCustomAttribute`.

**Bước 2**. Truy xuất các giá trị của attribute thông qua đối tượng vừa khởi tạo.

Ví dụ:

```csharp
[Developer("Joan Smith", "42", Reviewed = true)]
class MainApp
{
    public static void Main()
    {
        // Call function to get and display the attribute.
        GetAttribute(typeof(MainApp));
    }

    public static void GetAttribute(Type t)
    {
        // Get instance of the attribute.
        DeveloperAttribute MyAttribute =
            (DeveloperAttribute) Attribute.GetCustomAttribute(t, typeof (DeveloperAttribute));

        if (MyAttribute == null)
        {
            Console.WriteLine("The attribute was not found.");
        }
        else
        {
            // Get the Name value.
            Console.WriteLine("The Name Attribute is: {0}." , MyAttribute.Name);
            // Get the Level value.
            Console.WriteLine("The Level Attribute is: {0}." , MyAttribute.Level);
            // Get the Reviewed value.
            Console.WriteLine("The Reviewed Attribute is: {0}." , MyAttribute.Reviewed);
        }
    }
}
```
