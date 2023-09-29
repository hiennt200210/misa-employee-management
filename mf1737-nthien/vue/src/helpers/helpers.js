export default {
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

    formatNumber(number) {
        return "1,000,000";
    },

    /**
     * Xử lý lỗi, trả về mô tả lỗi.
     * CreatedBy: hiennt200210 (30/08/2023)
     */
    handleError(error) {
        const statusCode = error.response.status;
        let statusDescription = "";
        const userMessage = error.response.data.userMsg;

        switch (statusCode) {
            case 400:
                statusDescription = this.$resx[this.$langCode].BadRequest;
                break;
            case 401:
                statusDescription = this.$resx[this.$langCode].UnAuthorized;
                break;
            case 500:
                statusDescription =
                    this.$resx[this.$langCode].InternalServerError;
                break;
        }

        return { statusCode, statusDescription, userMessage };
    },
};
