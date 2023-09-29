<template>
    <div class="employee-detail">
        <MForm title="Thông tin nhân viên">
            <!-- Form Content -->
            <template #content>
                <div class="content-1">
                    <!-- Column Left -->
                    <div class="column-left">
                        <div class="row-1">
                            <MTextfield v-model="employee.employeeCode" label="Mã" required ref="first-input" />
                            <MTextfield v-model="employee.fullName" label="Tên" required />
                        </div>

                        <div class="row-2">
                            <MLabel label="Đơn vị" required />
                            <MCombobox v-model="employee.departmentId" value="departmentId"
                                url="https://localhost:44376/api/Departments" propValue="departmentId"
                                propText="departmentName">
                            </MCombobox>
                        </div>

                        <div class="row-3">
                            <MTextfield v-model="employee.positionName" label="Chức danh" />
                        </div>
                    </div>

                    <!-- Column Right -->
                    <div class="column-right">
                        <div class="row-1">
                            <MDatePicker v-model="employee.dateOfBirth" label="Ngày sinh" id="birthdate" />
                            <MRadioBox v-model="employee.gender" label="Giới tính">
                                <MRadio label="Nam" id="male" name="gender" />
                                <MRadio label="Nữ" id="female" name="gender" />
                                <MRadio label="Khác" id="other" name="gender" />
                            </MRadioBox>
                        </div>

                        <div class="row-2">
                            <MTextfield v-model="employee.identityNumber" label="Số CCCD" tooltip="Số căn cước công dân" />
                            <MDatePicker v-model="employee.identityDate" label="Ngày cấp" id="identity-date" />
                        </div>

                        <div class="row-3">
                            <MTextfield v-model="employee.identityPlace" label="Nơi cấp" />
                        </div>
                    </div>
                </div>

                <div class="content-2">
                    <div class="row-1">
                        <MTextfield v-model="employee.address" label="Địa chỉ" />
                    </div>

                    <div class="row-2">
                        <MTextfield v-model="employee.phoneNumber" label="ĐT di động" tooltip="Điện thoại di động" />
                        <MTextfield v-model="employee.landlineNumber" label="ĐT cố định" tooltip="Điện thoại cố định" />
                        <MTextfield v-model="employee.email" label="Email" placeHolder="example@email.com" />
                    </div>

                    <div class="row-3">
                        <MTextfield label="Tài khoản ngân hàng" v-model="employee.bankAccount" />
                        <MTextfield label="Tên ngân hàng" v-model="employee.bankName" />
                        <MTextfield label="Chi nhánh" v-model="employee.bankBranch" />
                    </div>
                </div>
            </template>

            <!-- Form Actions -->
            <template #action>
                <div class="form-action">
                    <!-- Nút Hủy -->
                    <MButton :type="$enums.Button.Secondary" :label="$resx[$langCode].Component.Button.Label
                        .Cancel
                        " @clickButton="onClickCancelButton" />

                    <!-- Nút Cất -->
                    <MButton :type="$enums.Button.Secondary" :label="$resx[$langCode].Component.Button.Label
                        .Store
                        " @clickButton="onSave" />

                    <!-- Nút Cất và thêm -->
                    <MButton :type="$enums.Button.Primary" :label="$resx[$langCode].Component.Button.Label
                        .StoreAndAdd
                        " @clickButton="onSaveAndAdd" />
                </div>
            </template>
        </MForm>

        <!-- Dialog -->
        <MDialog v-if="dialog.display" :type="dialog.type" :title="dialog.title" :content="dialog.content"
            @closeDialog="onCloseDialog">
            <MButton @clickButton="onCloseDialog" :type="$enums.Button.Primary" :label="$resx[$langCode].Close" />
        </MDialog>

        <!-- Toast message -->
        <MToastMessage v-if="showToastMessage" :toasts="[
            {
                type: $enums.ToastMessageType.Success,
                message: $resx[$langCode].EmployeeAdd,
                close: onCloseToastMessage,
            },
        ]" />
    </div>
</template>

<script>
import api from "@configs/api.js";
import MButton from "@components/bases/buttons/MButton.vue";
import MLabel from "@components/bases/labels/MLabel.vue";
import MTextfield from "@components/bases/text-fields/MTextfield.vue";
import MDatePicker from "@components/bases/date-pickers/MDatePicker.vue";
import MDialog from "@components/bases/dialogs/MDialog.vue";
import MForm from "@components/bases/forms/MForm.vue";
import MToastMessage from "@components/bases/toast-message/MToastMessage.vue";
import MRadioBox from "@components/bases/radios/MRadioBox.vue";
import MRadio from "@components/bases/radios/MRadio.vue";
import MCombobox from "@components/bases/combobox/MCombobox.vue";
import department from "@services/department.js";
import employee from "@services/employee.js";

export default {
    name: "EmployeeDetail",
    components: {
        MButton,
        MLabel,
        MTextfield,
        MDatePicker,
        MDialog,
        MForm,
        MToastMessage,
        MRadioBox,
        MRadio,
        MCombobox,
    },

    props: ["form-data"],

    data() {
        return {
            employee: {},
            showDialog: false,
            showToastMessage: false,
            dialogTitle: "",
            dialogDescription: "",
            selected: null,
            departmentOptions: [],
            dialog: {
                display: false,
                type: `${this.$enums.DialogType.Error}`,
                title: "",
                content: "",
            },
        };
    },

    methods: {
        /**
         * Hiển thị dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowDialog() {
            this.dialog.display = true;
        },

        /**
         * Đóng dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseDialog() {
            this.dialog.display = false;
        },

        /**
         * Đóng form Thông tin nhân viên khi nhấn nút Hủy hoặc Close
         * CreatedBy: hiennt200210 (21/08/2023)
         */
        onClickCancelButton() {
            this.$emit("closeEmployeeForm");
        },

        /**
         * Hiển thị toast message trong 5 giây, sau đó ẩn.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowToastMessage() {
            this.showDialog = false;
            this.showToastMessage = true;
        },

        /**
         * Đóng toast message.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseToastMessage() {
            this.showToastMessage = false;
            this.showEmployeeDetail = false;
        },

        /**
         * Kiểm tra các trường dữ liệu bắt buộc có bị trống hay không.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        validateData() {
            if (this.employee.code === "" || this.employee.fullName === "") {
                this.onShowDialog();
            }
        },

        /**
         * Lấy mã nhân viên mới.
         * CreatedBy: hiennt200210 (26/09/2023)
         */
        async getNewEmployeeCode() {
            const response = await employee.getNewEmployeeCode();
            return response.data;
        },

        /**
         * Lấy danh sách phòng ban.
         * CreatedBy: hiennt200210 (26/09/2023)
         */
        async getDepartmentOptions() {
            const response = await employee.getAll();
            return response.data;
        },

        /**
         * Xử lý lỗi.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        errorHandle(error) {
            const statusCode = error.response.status;
            let userMessage = error.response.MoreInfo;

            this.dialogDescription = userMessage;
            this.showDialog = true;

            switch (statusCode) {
                case 400:
                    this.dialogTitle =
                        this.$resx[this.$langCode].BadRequest;
                    break;
                case 401:
                    this.dialogTitle =
                        this.$resx[this.$langCode].UnAuthorized;
                    break;
                case 500:
                    this.dialogTitle =
                        this.$resx[
                            this.$langCode
                        ].InternalServerError;
                    this.dialogDescription =
                        this.$resx[
                            this.$langCode
                        ].InternalServerErrorMessage;
                    break;
            }
        },

        /**
         * Lưu dữ liệu khi nhấn Cất.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        onSave() {
            employee.insert(this.employee);
            this.$emit("closeEmployeeForm");
            this.$emitter.emit("showToastMessage");
        },

        /**
         * Lưu dữ liệu và reset form khi nhấn Cất và thêm.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        onSaveAndAdd() {
            employee.insert(this.employee);
            this.employee.employeeCode = "";
            this.employee.fullName = "";
            this.employee.dateOfBirth = "";
            this.employee.gender = 0;
            this.employee.identityNumber = "";
            this.employee.identityDate = "";
            this.employee.identityPlace = "";
            this.employee.address = "";
            this.employee.phoneNumber = "";
            this.employee.landline = "";
            this.employee.email = "";
            this.employee.bankAccount = "";
            this.employee.bankName = "";
            this.employee.bankBranch = "";
            this.employee.positionName = "";
        },
    },

    async created() {
        // Lấy mã nhân viên mới
        this.employee.employeeCode = await this.getNewEmployeeCode();
    },

    mounted() {
        this.$refs["first-input"].setWidth("150px");
        this.$refs["first-input"].focus();
    },
};
</script>

<style scoped>
@import url(./employee-form.css);
</style>
