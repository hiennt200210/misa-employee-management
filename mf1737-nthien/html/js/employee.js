window.onload = function () {
    new EmployeePage();
};

class EmployeePage {
    constructor() {
        // Tải dữ liệu nhân viên từ server
        this.loadData();

        // Thêm sự kiện cho các component
        document
            .getElementById("button-add")
            .addEventListener("click", this.buttonAddClick);
        document
            .getElementById("button-close")
            .addEventListener("click", this.buttonCloseClick);
        document
            .getElementById("button-cancel")
            .addEventListener("click", this.buttonCancel);
        document
            .getElementById("button-reload")
            .addEventListener("click", this.reloadData);
    }

    /*
     * Tải dữ liệu từ server.
     * Author: hiennt200210 (17/08/2023)
     */
    loadData() {
        try {
            // Hiển thị progess
            let progress = document.getElementById("loading");
            progress.style.display = "flex";

            fetch("https://cukcuk.manhnv.net/api/v1/customers")
                .then((res) => res.json())
                .then((employees) => {
                    // Chèn dữ liệu vừa tải về vào bảng
                    this.insertDataToTable(employees);

                    // Ẩn progress sau khi tải xong
                    progress.style.display = "none";
                });
        } catch (error) {
            console.log(error);
        }
    }

    /*
     * Chèn dữ liệu tải về vào bảng
     * Author: hiennt200210 (17/08/2023)
     */
    insertDataToTable(employees) {
        for (const employee of employees) {
            // Tạo hàng trong bảng Nhân viên
            let tableRow = document.createElement("tr");
            tableRow.innerHTML = `<tr>
                        <td
                            class="text-align-center sticky-column"
                        >
                            <input type="checkbox" />
                        </td>
                        <td class="text-align-left">
                            ${employee.CustomerCode}
                        </td>
                        <td class="text-align-left">
                            ${employee.FullName}
                        </td>
                        <td class="text-align-left">
                            ${employee.Gender ? "Nữ" : "Nam"}
                        </td>
                        <td class="text-align-center">
                            ${CommonJS.formatDate(employee.DateOfBirth)}
                        </td>
                        <td class="text-align-left">
                            ${123456789012 /* Dữ liệu mẫu không có số CMND */}
                        </td>
                        <td class="text-align-center">
                            ${CommonJS.formatDate(employee.CreatedDate)}
                        </td>
                        <td class="text-align-left">
                            ${employee.CreatedBy}
                        </td>
                        <td class="text-align-left">
                            Nhân viên tiếp tân
                        </td>
                        <td class="text-align-left">DV1728</td>
                        <td class="text-align-left">
                            Đơn vị 8
                        </td>
                        <td class="text-align-right">
                            10.000.000
                        </td>
                        <td class="text-align-right">0.10</td>
                        <td class="text-align-right">
                            8.000.000
                        </td>
                        <td class="text-align-center fix-cell">
                            <a href="">Sửa</a>
                            <div class="m-dropdownlist">
                                <select name="">
                                    <option value="1">
                                        Nhân bản
                                    </option>
                                    <option value="2">
                                        Xóa
                                    </option>
                                    <option value="3">
                                        Ngừng sử dụng
                                    </option>
                                </select>
                            </div>
                        </td>
                    </tr>`;
            document.querySelector("#tableEmployee tbody").append(tableRow);
        }
    }

    /*
     * Hiển thị form Thông tin chi tiết khi nhấp vào nút "Thêm mới".
     * Focus vào ô nhập liệu "Mã".
     * Author: hiennt200210 (17/08/2023)
     */
    buttonAddClick() {
        popup.style.display = "flex";
        document.getElementById("input-id").focus();
    }

    /*
     * Ẩn form Thông tin chi tiết khi nhấp vào nút Exit.
     * Author: hiennt200210 (17/08/2023)
     */
    buttonCloseClick() {
        popup.style.display = "none";
    }

    /*
     * Ẩn form Thông tin chi tiết khi nhấp vào nút Cancel.
     * Author: hiennt200210 (17/08/2023)
     */
    buttonCancel() {
        popup.style.display = "none";
    }

    /*
     * Reload data
     * Author: hiennt200210 (17/08/2023)
     */
    reloadData() {
        document.querySelector("#tableEmployee tbody").innerHTML = "";
        try {
            // Hiển thị progess
            let progress = document.getElementById("loading");
            progress.style.display = "flex";

            fetch("https://cukcuk.manhnv.net/api/v1/customers")
                .then((res) => res.json())
                .then((employees) => {
                    // Chèn dữ liệu vừa tải về vào bảng
                    this.insertDataToTable(employees);

                    // Ẩn progress sau khi tải xong
                    progress.style.display = "none";
                });
        } catch (error) {
            console.log(error);
        }
    }
}

// inputId.addEventListener("blur", function () {
//     if (inputId.value == null || inputId.value == "") {
//         inputId.classList.add("m-input-error");
//     } else {
//         inputId.classList.remove("m-input-error");
//     }
// });

// inputName.addEventListener("blur", function () {
//     if (inputName.value == null || inputName.value == "") {
//         inputName.classList.add("m-input-error");
//     } else {
//         inputName.classList.remove("m-input-error");
//     }
// });

// selectUnit.addEventListener("blur", function () {
//     if (selectUnit.value == 1) {
//         selectUnit.classList.add("m-input-error");
//     }
// });

/*
    Các thao tác cần thực hiện:
    1.  Màn hình Quản lý nhân viên:
        - Nhấn nút "Reload": Nạp lại toàn bộ dữ liệu.
        - Nhấn "Sửa": Hiển thị form Thông tin chi tiết và cho phép sửa thông tin(Lưu ý: chỉ làm tính năng "Xóa".)
        - Double Click vào một hàng của bảng để sửa thông tin nhân viên.
        - Khi nhấn "Xóa": hiển thị xác nhận xóa
    2.  Màn hình form Thông tin chi tiết:
        - Focus vào ô Mã nhân viên khi form xuất hiện.
        - Các ô nhập liệu bắt buộc nếu nhập thiếu và control được chuyển sang phần tử khác thì sẽ hiển thị viền đỏ và cảnh báo bên dưới.
        - Nếu nhấn "Cất và thêm" khi các trường dữ liệu chưa được nhập đúng thì sẽ hiển thị dialog cảnh báo.
        - Khi thêm mới nhân viên, form Thông tin chi tiết khi hiển thị sẽ được điền sẵn ô Mã nhân viên với giá trị: NV(mã số nhân viên lớn nhất + 1)
        - Khi sửa thông tin nhân viên, dữ liệu về nhân viên sẽ hiển thị sẵn trên các trường thông tin
        - Khi chỉnh sửa hoặc thêm mới nhân viên, nếu mã nhân viên đã tồn tại trong hệ thống thì phải thông báo cho người dùng bằng dialog.
        - Nhấn "Cất": tải dữ liệu lên server và đóng form.
        - Nhấn "Cất và Thêm mới": tải dữ liệu lên server, dọn sạch các trường dữ liệu để người dùng có thể nhập thông tin mới ngay lập tức.
*/
