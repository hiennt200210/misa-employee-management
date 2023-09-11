<template>
    <div class="employee-detail">
        <MForm title="Thông tin nhân viên">
            <!-- Form Content -->
            <template #content>
                <div class="content-1">
                    <!-- Column Left -->
                    <div class="column-left">
                        <div class="row-1">
                            <MTextfield
                                v-model="employee.employeeCode"
                                label="Mã"
                                required
                                ref="firstInput" />
                            <MTextfield
                                v-model="employee.fullName"
                                label="Tên"
                                required />
                        </div>

                        <div class="row-2">
                            <MLabel label="Đơn vị" required />
                            <div class="m-dropdownlist">
                                <select
                                    @change="onChangeDepartment"
                                    ref="changeDepartment">
                                    <option value="1">Phòng kế toán</option>
                                    <option value="2">Phòng bảo vệ</option>
                                </select>
                            </div>
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.positionName"
                                label="Chức danh" />
                        </div>
                    </div>

                    <!-- Column Right -->
                    <div class="column-right">
                        <div class="row-1">
                            <MDatePicker label="Ngày sinh" id="birthdate" />
                            <MRadioBox label="Giới tính">
                                <MRadio label="Nam" id="male" name="gender" />
                                <MRadio label="Nữ" id="female" name="gender" />
                                <MRadio label="Khác" id="other" name="gender" />
                            </MRadioBox>
                        </div>

                        <div class="row-2">
                            <MTextfield
                                v-model="employee.identityNumber"
                                label="Số CCCD"
                                tooltip="Số căn cước công dân" />
                            <MDatePicker label="Ngày cấp" />
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.identityPlace"
                                label="Nơi cấp" />
                        </div>
                    </div>
                </div>

                <div class="content-2">
                    <div class="row-1">
                        <MTextfield
                            v-model="employee.address"
                            label="Địa chỉ" />
                    </div>

                    <div class="row-2">
                        <MTextfield
                            v-model="employee.phoneNumber"
                            label="ĐT di động"
                            tooltip="Điện thoại di động" />
                        <MTextfield
                            v-model="employee.phoneNumber"
                            label="ĐT cố định"
                            tooltip="Điện thoại cố định" />
                        <MTextfield
                            v-model="employee.email"
                            label="Email"
                            placeHolder="example@email.com" />
                    </div>

                    <div class="row-3">
                        <MTextfield label="Tài khoản ngân hàng" />
                        <MTextfield label="Tên ngân hàng" />
                        <MTextfield label="Chi nhánh" />
                    </div>
                </div>
            </template>

            <!-- Form Actions -->
            <template #action>
                <!-- Nút Hủy -->
                <MSButton
                    :type="$MISAEnum.Component.Button.Type.Secondary"
                    :label="
                        $MISAResource[$language].Component.Button.Label.Cancel
                    "
                    @click="onCloseEmployeeDetail" />

                <!-- Nút Cất -->
                <MSButton
                    :type="$MISAEnum.Component.Button.Type.Secondary"
                    :label="
                        $MISAResource[$language].Component.Button.Label.Store
                    "
                    @click="onSave" />

                <!-- Nút Cất và thêm -->
                <MSButton
                    :type="$MISAEnum.Component.Button.Type.Primary"
                    :label="
                        $MISAResource[$language].Component.Button.Label
                            .StoreAndAdd
                    "
                    @click="onSaveAndAdd" />
            </template>
        </MForm>

        <!-- Dialog -->
        <MDialog
            v-if="showDialog"
            :type="$MISAEnum.DialogType.Error"
            :closeDialog="onCloseDialog"
            :title="dialogTitle"
            :description="dialogDescription">
            <MButton
                @click="onCloseDialog"
                :type="$MISAEnum.ButtonType.Primary"
                :label="$MISAResource[$languageCode].Close" />
        </MDialog>

        <!-- Toast message -->
        <MToastMessage
            v-if="showToastMessage"
            :toasts="[
                {
                    type: $MISAEnum.ToastMessageType.Successful,
                    message: $MISAResource[$languageCode].EmployeeAdd,
                    close: onCloseToastMessage,
                },
            ]" />
    </div>
</template>

<script>
import MButton from "../../components/base/button/MButton.vue";
import MSButton from "../../components/base/button/MSButton.vue";
import MLabel from "../../components/base/label/MLabel.vue";
import MTextfield from "../../components/base/textfield/MTextfield.vue";
import MDatePicker from "../../components/base/date-picker/MDatePicker.vue";
import MDialog from "../../components/base/dialog/MDialog.vue";
import MForm from "../../components/base/form/MForm.vue";
import MToastMessage from "../../components/base/toast-message/MToastMessage.vue";
import MRadioBox from "../../components/base/radio/MRadioBox.vue";
import MRadio from "../../components/base/radio/MRadio.vue";

export default {
    name: "EmployeeDetail",
    components: {
        MButton,
        MSButton,
        MLabel,
        MTextfield,
        MDatePicker,
        MDialog,
        MForm,
        MToastMessage,
        MRadioBox,
        MRadio,
    },

    props: ["employeeEdit"],

    methods: {
        /**
         * Hiển thị dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowDialog() {
            this.showDialog = true;
        },

        /**
         * Đóng dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseDialog() {
            this.showDialog = false;
        },

        /**
         * Đóng form Thông tin nhân viên khi nhấn nút Hủy hoặc Close
         * CreatedBy: hiennt200210 (21/08/2023)
         */
        onCloseEmployeeDetail() {
            this.$emit("closeEmployeeDetail");
        },

        /**
         * Hiển thị toast message trong 5 giây, sau đó ẩn.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowToastMessage() {
            this.showDialog = false;
            this.showToastMessage = true;
            setTimeout(() => {
                this.showToastMessage = false;
            }, 5000);
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
         * Xử lý lỗi.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        errorHandle(error) {
            const statusCode = error.response.status;
            const userMessage = error.response.data.userMsg;
            this.dialogDescription = userMessage;
            this.showDialog = true;

            switch (statusCode) {
                case 400:
                    this.dialogTitle =
                        this.$MISAResource[this.$languageCode].BadRequest;
                    break;
                case 401:
                    this.dialogTitle =
                        this.$MISAResource[this.$languageCode].UnAuthorized;
                    break;
                case 500:
                    this.dialogTitle =
                        this.$MISAResource[
                            this.$languageCode
                        ].InternalServerError;
                    break;
            }
        },

        /**
         * Lưu dữ liệu khi nhấn Cất.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        onSave() {
            this.$axios
                .post(
                    "https://cukcuk.manhnv.net/api/v1/Employees",
                    this.employee
                )
                .then(() => {
                    this.$emit("closeEmployeeDetail");
                    // this.$emit("showToastMessage");
                    this.$emitter.emit("showToastMessage");
                })
                .catch((error) => {
                    this.errorHandle(error);
                });
        },

        /**
         * Lưu dữ liệu và reset form khi nhấn Cất và thêm.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        onSaveAndAdd() {
            this.$axios
                .post(
                    "https://cukcuk.manhnv.net/api/v1/Employees",
                    this.employee
                )
                .then(() => {
                    this.onShowToastMessage();
                    this.employee.employeeCode = "";
                    this.employee.fullName = "";
                    this.employee.dateOfBirth = "";
                    this.employee.gender = "";
                    this.employee.identityNumber = "";
                    this.employee.identityDate = "";
                    this.employee.identityPlace = "";
                    this.employee.address = "";
                    this.employee.phoneNumber = "";
                    this.employee.email = "";
                })
                .catch((error) => {
                    this.errorHandle(error);
                });
        },
    },

    data() {
        return {
            employee: {},
            newEmployeeCode: "",
            showDialog: false,
            showToastMessage: false,
            dialogTitle: "",
            dialogDescription: "",
        };
    },

    created() {
        // Tải mã nhân viên mới
        this.$axios
            .get("https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode")
            .then((response) => {
                this.newEmployeeCode = response.data;
                this.employee.employeeCode = this.newEmployeeCode;
            })
            .catch((error) => {
                console.error(error);
            });
    },

    mounted() {
        this.$refs["firstInput"].setWidth("150px");
        this.$refs["firstInput"].focus();
    },
};
</script>

<style scoped>
@import url(../../styles/components/dropdownlist.css);
@import url(../../styles/views/employee/employee-detail.css);
</style>
