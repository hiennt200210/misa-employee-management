import axios from "axios";
import api from "@configs/api.js";

export default {
    /**
     * Lấy tất cả nhân viên
     * @returns Danh sách nhân viên
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getAll() {
        try {
            const response = await axios.get(api.baseUrl + "api/v1/employees");
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },

    async getByFilter(limit, offset, order, search) {
        let url =
            api.baseUrl +
            "api/v1/employees/pagination?limit=" +
            limit +
            "&offset=" +
            offset +
            "&orders=" +
            order;
        if (search) {
            url +=
                "&FullName=" +
                search +
                "&EmployeeCode=" +
                search +
                "&PhoneNumber=" +
                search;
        }
        const response = await axios.get(url);
        return response;
    },

    /**
     * Lấy nhân viên theo ID
     * @param {String} id ID của nhân viên cần lấy
     * @returns Thông tin nhân viên có ID tương ứng
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getById(id) {
        const response = await axios.get(
            api.baseUrl + "api/v1/employees/" + id
        );
        return response;
    },

    /**
     * Lấy mã nhân viên mới
     * @returns Mã nhân viên mới
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getNewEmployeeCode() {
        const response = await axios.get(
            api.baseUrl + "api/v1/employees/newEmployeeCode"
        );
        return response;
    },

    /**
     * Thêm mới một nhân viên
     * @param {Object} employee Thông tin nhân viên cần thêm mới
     * @returns
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async insert(employee) {
        const response = await axios.post(
            api.baseUrl + "api/v1/employees",
            employee
        );
        return response;
    },

    /**
     * Cập nhật thông tin nhân viên
     * @param {String} id ID của nhân viên cần cập nhật
     * @param {Object} employee Thông tin nhân viên cần cập nhật
     * @returns
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async update(id, employee) {
        const response = await axios.put(
            api.baseUrl + "api/v1/employees/" + id,
            employee
        );
        return response;
    },

    /**
     * Xoá một nhân viên
     * @param {String} id ID của nhân viên cần xoá
     * @returns
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async delete(id) {
        try {
            const response = await axios.delete(
                api.baseUrl + "api/v1/employees/" + id
            );
            return response;
        } catch (error) {
            // this.$helpers.handleError(error);
            // console.log(error.response.data);
        }
    },

    /**
     * Xoá nhiều nhân viên
     * @param {Array} ids Danh sách ID của những nhân viên cần xoá
     * @returns
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async deleteMultiple(ids) {
        let url = api.baseUrl + "api/v1/employees?";
        ids.forEach((id) => {
            url += `ids=${id}&`;
        });
        const response = await axios.delete(url);
        return response;
    },
};
