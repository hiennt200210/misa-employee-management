import enums from "@constants/enums";
import resources from "@constants/resources";

/**
 * Định dạng dữ liệu giới tính.
 * @param { Number } gender Giới tính dạng số (0: Nam, 1: Nữ, 2: Khác)
 * @returns Giới tính dạng chuỗi
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
function formatGender(gender) {
    switch (gender) {
        case enums.Gender.Male:
            return resources.VN.Male;
        case enums.Gender.Female:
            return resources.VN.Female;
        case enums.Gender.Other:
            return resources.VN.Other;
        default:
            return "";
    }
}

/**
 * Định dạng dữ liệu ngày tháng.
 * @param { String } date - Ngày tháng chưa được định dạng
 * @returns { String } - Ngày tháng được định dạng dd/MM/yyyy
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
function formatDate(date) {
    try {
        date = new Date(date);
        const dayValue = date.getDate();
        const monthValue = date.getMonth() + 1;
        const yearValue = date.getFullYear();

        const dayString = `${dayValue < 10 ? `0${dayValue}` : dayValue}`;
        const monthString = `${
            monthValue < 10 ? `0${monthValue}` : monthValue
        }`;
        const yearString = `${yearValue}`;

        return `${dayString}/${monthString}/${yearString}`;
    } catch (error) {
        console.log(error);
        return "";
    }
}

/**
 * Định dạng dữ liệu số.
 * @param { Number } number - Dữ liệu dạng số (VD: 10000000)
 * @returns { String } - Dữ liệu số dạng chuỗi (VD: 10,000,000.00)
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
function formatNumber(number) {
    return number.toLocaleString("en-US", {
        minimumFractionDigits: 0,
        maximumFractionDigits: 2,
    });
}

export { formatGender, formatDate, formatNumber };
