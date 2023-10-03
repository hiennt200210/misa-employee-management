export default {
    /**
     * Định dạng giới tính.
     * @param {Number} gender Giới tính (0: Nam, 1: Nữ, 2: Khác)
     * @returns Chuỗi giới tính
     * CreatedBy: hiennt200210 (17/08/2023)
     */
    formatGender(gender) {
        switch (gender) {
            case 0:
                return "Nam";
            case 1:
                return "Nữ";
            case 2:
                return "Khác";
            default:
                return "";
        }
    },

    /**
     * Định dạng ngày tháng năm.
     * @param {String} date - Ngày tháng năm
     * @returns {String} - Ngày tháng năm định dạng dd/MM/yyyy
     *
     * CreatedBy: hiennt200210 (17/08/2023)
     */
    formatDate(date) {
        try {
            date = new Date(date);
            let dateValue = date.getDate();
            let monthValue = date.getMonth() + 1;
            let yearValue = date.getFullYear();
            return `${dateValue < 10 ? `0${dateValue}` : dateValue}/${
                monthValue < 10 ? `0${monthValue}` : monthValue
            }/${yearValue}`;
        } catch (error) {
            console.error(error);
        }
    },

    /**
     * Định dạng dữ liệu số.
     * @param {Number} number - Dữ liệu số
     * @returns {String} - Dữ liệu số định dạng
     * CreatedBy: hiennt200210 (17/08/2023)
     */
    formatNumber(number) {
        // if (number === 0) {
        //     return "";
        // }
        // Sử dụng toLocaleString để thêm dấu phẩy vào giá trị số
        // và giữ lại hai chữ số thập phân
        return number.toLocaleString("en-US", {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        });
    },

    /**
     * Kiểm tra một chuỗi có trống hay không.
     * @param {*} value Chuỗi cần kiểm tra
     * @returns true nếu chuỗi không trống, false nếu chuỗi trống
     */
    validateEmpty(value) {
        if (value === null || value === undefined || value === "") {
            return false;
        }
        return true;
    },

    /**
     * Kiểm tra một chuỗi có vượt quá độ dài cho phép hay không.
     * @param {*} value Chuỗi cần kiểm tra
     * @param {*} maxLength Độ dài tối đa cho phép
     * @returns true nếu chuỗi không vượt quá độ dài cho phép, false nếu chuỗi vượt quá độ dài cho phép
     */
    validateMaxLength(value, maxLength) {
        if (value.length > maxLength) {
            return false;
        }
        return true;
    },

    /**
     * Kiểm tra một chuỗi có phải là email hay không.
     * @param {String} email Chuỗi cần kiểm tra
     * @returns {Boolean} Kết quả kiểm tra
     * CreatedBy: hiennt200210 (17/08/2023)
     */
    validateEmail(email) {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(email);
    },
};
