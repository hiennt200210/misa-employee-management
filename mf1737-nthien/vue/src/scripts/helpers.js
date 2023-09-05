export default {
    /*
     * Format một xâu ngày tháng năm sang định dạng DD/MM/YYYY.
     * Author:  hiennt200210 (17/08/2023)
     */
    formatDate(date) {
        try {
            date = new Date(date);
            let dateValue = date.getDate();
            let monthValue = date.getMonth() + 1;
            let yearValue = date.getFullYear();
            return `${dateValue < 10 ? `0${dateValue}` : dateValue}/${monthValue < 10 ? `0${monthValue}` : monthValue
                }/${yearValue}`;
        } catch (error) {
            console.error(error);
        }
    },

    /**
     * Xử lý lỗi, trả về mô tả lỗi.
     * CreatedBy: hiennt200210 (30/08/2023)
     */
    errorHandle(error) {
        const statusCode = error.response.status;
        let statusDescription = "";
        const userMessage = error.response.data.userMsg;

        switch (statusCode) {
            case 400:
                statusDescription =
                    this.$MISAResource[this.$languageCode].BadRequest;
                break;
            case 401:
                statusDescription =
                    this.$MISAResource[this.$languageCode].UnAuthorized;
                break;
            case 500:
                statusDescription =
                    this.$MISAResource[
                        this.$languageCode
                    ].InternalServerError;
                break;
        }

        return { statusCode, statusDescription, userMessage };
    }
};
