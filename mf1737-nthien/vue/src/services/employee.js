import axios from "axios";
import { employeeUrl } from "@constants/constants";

/**
 * Lấy danh sách nhân viên theo kết quả tìm kiếm, sắp xếp, phân trang.
 * @param { Number } limit Số nhân viên trên một trang
 * @param { Number } offset Vị trí bắt đầu lấy nhân viên
 * @param { Array } orders Sắp xếp theo trường nào và kiểu sắp xếp (VD: -FullName)
 * @param { String } search Từ khóa tìm kiếm
 * @returns Danh sách nhân viên theo kết quả tìm kiếm, sắp xếp, phân trang
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function getEmployeesByFilter(limit, offset, orders, search) {
    // Tạo request URL
    let url = `${employeeUrl}/pagination?limit=${limit}&offset=${offset}`;

    if (orders) {
        orders.forEach((order) => {
            url += `&orders=${order}`;
        });
    }

    if (search) {
        url += `&FullName=${search}&EmployeeCode=${search}&PhoneNumber=${search}`;
    }

    // Gọi API
    const response = await axios.get(url);

    return response;
}

/**
 * Lấy danh sách tất cả nhân viên.
 * @returns Danh sách tất cả nhân viên
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function getAllEmployees() {
    const response = await axios.get(employeeUrl);
    return response;
}

/**
 * Lấy dữ liệu của một nhân viên theo ID.
 * @param { String } id ID của nhân viên cần lấy
 * @returns Thông tin nhân viên có ID tương ứng
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function getEmployeeById(id) {
    const url = `${employeeUrl}/${id}`;
    const response = await axios.get(url);
    return response;
}

/**
 * Lấy mã nhân viên mới (tự động tăng).
 * @returns Mã nhân viên mới
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function generateEmployeeCode() {
    const url = `${employeeUrl}/newEmployeeCode`;
    const response = await axios.get(url);
    return response;
}

/**
 * Thêm mới một nhân viên.
 * @param { Object } employee Thông tin nhân viên cần thêm mới
 * @returns ID của nhân viên vừa thêm mới
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function createEmployee(employee) {
    const response = await axios.post(employeeUrl, employee);
    return response;
}

/**
 * Cập nhật thông tin của một nhân viên.
 * @param { String } id ID của nhân viên cần cập nhật
 * @param { Object } employee Thông tin nhân viên cần cập nhật
 * @returns Thông tin nhân viên sau khi cập nhật
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function updateEmployee(id, employee) {
    const url = `${employeeUrl}/${id}`;
    const response = await axios.put(url, employee);
    return response;
}

/**
 * Xoá một nhân viên.
 * @param { String } id ID của nhân viên cần xoá
 * @returns Kết quả xoá nhân viên (thông báo từ server)
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function deleteEmployee(id) {
    const url = `${employeeUrl}/${id}`;
    const response = await axios.delete(url);
    return response;
}

/**
 * Xoá nhiều nhân viên.
 * @param { Array } ids Danh sách ID của những nhân viên cần xoá
 * @returns Kết quả xoá nhân viên (thông báo từ server)
 *
 * CreatedBy: hiennt200210 (26/09/2023)
 */
async function deleteEmployees(ids) {
    // Tạo request URL
    let url = `${employeeUrl}?`;

    ids.forEach((id) => {
        url += `ids=${id}&`;
    });

    // Gọi API
    const response = await axios.delete(url);

    return response;
}

/**
 * Xuất khẩu file Excel danh sách nhân viên theo kết quả tìm kiếm, sắp xếp.
 * @param { Array } orders Sắp xếp theo trường nào và kiểu sắp xếp (VD: -FullName)
 * @param { String } search Từ khóa tìm kiếm
 * @returns File Excel danh sách nhân viên theo kết quả tìm kiếm, sắp xếp
 *
 * CreatedBy: hiennt200210 (04/10/2023)
 */
async function exportEmployeesToExcel(orders, search) {
    // Tạo request URL
    let url = `${employeeUrl}/export?`;

    if (orders) {
        orders.forEach((order) => {
            url += `&orders=${order}`;
        });
    }

    if (search) {
        url += `&search=${search}`;
    }

    // Gọi API
    const response = await axios({
        url: url,
        method: "GET",
        responseType: "blob",
    });

    // Tạo một thẻ "a" HTML để liên kết với file download và click vào thẻ đó
    const href = URL.createObjectURL(response.data);
    const link = document.createElement("a");
    link.href = href;
    link.setAttribute("download", "DanhSachNhanVien.xlsx");
    document.body.appendChild(link);
    link.click();

    // Xoá thẻ "a" HTML khi không cần dùng nữa
    document.body.removeChild(link);
    URL.revokeObjectURL(href);
}

export {
    getEmployeesByFilter,
    getAllEmployees,
    getEmployeeById,
    generateEmployeeCode,
    createEmployee,
    updateEmployee,
    deleteEmployee,
    deleteEmployees,
    exportEmployeesToExcel,
};
