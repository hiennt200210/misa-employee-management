/**
 * Kiểm tra một chuỗi có trống hay không.
 * @param { String } string Chuỗi cần kiểm tra
 * @returns true nếu chuỗi không trống, false nếu chuỗi trống
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
function validateEmpty(string) {
    if (string === null || string === undefined || string.trim() === "") {
        return false;
    }
    return true;
}

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
function validateMaxLength(string, maxLength) {
    if (string.length > maxLength) {
        return false;
    }
    return true;
}

/**
 * Kiểm tra một chuỗi đúng định dạng email hay không.
 * @param { String } string Chuỗi cần kiểm tra
 * @returns { Boolean }
 * True: Nếu chuỗi đúng định dạng email.
 * False: Nếu chuỗi không đúng định dạng email.
 *
 * CreatedBy: hiennt200210 (17/08/2023)
 */
function validateEmail(string) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isValid = regex.test(string);
    return isValid;
}

export { validateEmpty, validateMaxLength, validateEmail };
