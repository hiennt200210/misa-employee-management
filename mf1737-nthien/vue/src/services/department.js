import axios from "axios";
import { departmentUrl } from "@configs/constants";

export default {
    /**
     * Lấy danh sách tất cả phòng ban.
     * @returns Danh sách tất cả phòng ban
     *
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getAll() {
        const response = await axios.get(departmentUrl);
        return response;
    },

    /**
     * Lấy dữ liệu của một phòng ban theo ID.
     * @param { String } id ID của phòng ban cần lấy
     * @returns Thông tin phòng ban có ID tương ứng
     *
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async getById(id) {
        const url = `${departmentUrl}/${id}`;
        const response = await axios.get(url);
        return response;
    },

    /**
     * Thêm mới một phòng ban.
     * @param { Object } department Thông tin phòng ban cần thêm mới
     * @returns ID của phòng ban vừa thêm mới
     *
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async insert(department) {
        const response = await axios.post(departmentUrl, department);
        return response;
    },

    /**
     * Cập nhật thông tin của một phòng ban.
     * @param { String } id ID của phòng ban cần cập nhật
     * @param { Object } department Thông tin phòng ban cần cập nhật
     * @returns Thông tin phòng ban sau khi cập nhật
     *
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async update(id, department) {
        const url = `${departmentUrl}/${id}`;
        const response = await axios.put(url, department);
        return response;
    },

    /**
     * Xoá một phòng ban.
     * @param { String } id ID của phòng ban cần xoá
     * @returns Kết quả xoá phòng ban (thông báo từ server)
     *
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async delete(id) {
        const url = `${departmentUrl}/${id}`;
        const response = await axios.delete(url);
        return response;
    },

    /**
     * Xoá nhiều phòng ban.
     * @param { Array } ids Danh sách ID của những phòng ban cần xoá
     * @returns Kết quả xoá phòng ban (thông báo từ server)
     * CreatedBy: hiennt200210 (26/09/2023)
     */
    async deleteMultiple(ids) {
        // Tạo request URL
        let url = `${departmentUrl}?`;

        ids.forEach((id) => {
            url += `ids=${id}&`;
        });

        // Gọi API
        const response = await axios.delete(url);

        return response;
    },
};
