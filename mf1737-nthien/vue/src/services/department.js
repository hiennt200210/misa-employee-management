import axios from "axios";
import api from "@configs/api.js";

export default {
    /**
     * Lấy tất cả phòng ban
     * @returns Danh sách phòng ban
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getAll() {
        try {
            const response = await axios.get(api.baseUrl + "api/v1/departments");
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },

    /**
     * Lấy phòng ban theo ID
     * @param {String} id ID của phòng ban cần lấy
     * @returns Thông tin phòng ban có ID tương ứng
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getById(id) {
        try {
            const response = await axios.get(api.baseUrl + "api/v1/departments/" + id);
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },

    /**
     * Thêm mới một phòng ban
     * @param {Object} department Thông tin phòng ban cần thêm mới
     * @returns 
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async insert(department) {
        try {
            const response = await axios.post(api.baseUrl + "api/v1/departments", department);
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },

    /**
     * Cập nhật thông tin phòng ban
     * @param {String} id ID của phòng ban cần cập nhật
     * @param {Object} department Thông tin phòng ban cần cập nhật
     * @returns 
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async update(id, department) {
        try {
            const response = await axios.put(api.baseUrl + "api/v1/departments/" + id, department);
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },

    /**
     * Xoá một phòng ban
     * @param {String} id ID của phòng ban cần xoá
     * @returns 
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async delete(id) {
        try {
            const response = await axios.delete(api.baseUrl + "api/v1/departments/" + id);
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },

    /**
     * Xoá nhiều phòng ban
     * @param {Array} ids Danh sách ID của những phòng ban cần xoá
     * @returns 
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async deleteMultiple(ids) {
        try {
            const response = await axios.delete(api.baseUrl + "api/v1/departments/multiple", ids);
            return response;
        } catch (error) {
            this.handleError(error);
        }
    },
};