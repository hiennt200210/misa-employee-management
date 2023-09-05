const CommonJS = {
    /*
     * Format một xâu ngày tháng sang định dạng DD/MM/YYYY
     * Author:  hiennt200210 (17/08/2023)
     */
    formatDate(dateString) {
        try {
            let date = new Date(dateString);
            let dateValue = date.getDate();
            let monthValue = date.getMonth() + 1;
            let yearValue = date.getFullYear();
            return `${dateValue}/${
                monthValue < 10 ? `0${monthValue}` : monthValue
            }/${yearValue}`;
        } catch (error) {
            console.log(error);
        }
    },
};
