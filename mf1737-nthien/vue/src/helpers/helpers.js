import enums from "@helpers/enums";
import resources from "@helpers/resources";

/**
 * Định dạng dữ liệu giới tính.
 * @param { Number } gender Giới tính dạng số (0: Nam, 1: Nữ, 2: Khác)
 * @returns Giới tính dạng chuỗi
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
const formatGender = function (gender) {
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
};

/**
 * Định dạng dữ liệu ngày tháng.
 * @param { String } date - Ngày tháng chưa được định dạng
 * @returns { String } - Ngày tháng được định dạng dd/MM/yyyy
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
const formatDate = function (date) {
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
};

/**
 * Định dạng dữ liệu số.
 * @param { Number } number - Dữ liệu dạng số (VD: 10000000)
 * @returns { String } - Dữ liệu số dạng chuỗi (VD: 10,000,000.00)
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
const formatNumber = function (number) {
    return number.toLocaleString("en-US", {
        minimumFractionDigits: 0,
        maximumFractionDigits: 2,
    });
};

/**
 * Kiểm tra một chuỗi có trống hay không.
 * @param { String } string Chuỗi cần kiểm tra
 * @returns true nếu chuỗi không trống, false nếu chuỗi trống
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
const validateEmpty = function (string) {
    if (string === null || string === undefined || string === "") {
        return false;
    }
    return true;
};

/**
 * Kiểm tra một chuỗi có vượt quá độ dài cho phép hay không.
 * @param { String } string Chuỗi cần kiểm tra
 * @param { Number } maxLength Độ dài tối đa cho phép
 * @returns { Boolean }
 * True: Nếu chuỗi không vượt quá độ dài cho phép.
 * False: Nếu chuỗi vượt quá độ dài cho phép.
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
const validateMaxLength = function (string, maxLength) {
    if (string.length > maxLength) {
        return false;
    }
    return true;
};

/**
 * Kiểm tra một chuỗi đúng định dạng email hay không.
 * @param { String } string Chuỗi cần kiểm tra
 * @returns { Boolean }
 * True: Nếu chuỗi đúng định dạng email.
 * False: Nếu chuỗi không đúng định dạng email.
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
const validateEmail = function (string) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isValid = regex.test(string);
    if (!isValid) {
        return resources.VN.EmailInvalid;
    }
    return "";
};

export {
    formatGender,
    formatDate,
    formatNumber,
    validateEmpty,
    validateMaxLength,
    validateEmail,
};
