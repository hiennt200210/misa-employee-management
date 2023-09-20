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
                                v-model="employee.EmployeeCode"
                                label="Mã"
                                required
                                ref="firstInput"
                            />
                            <MTextfield
                                v-model="employee.FullName"
                                label="Tên"
                                required
                            />
                        </div>

                        <div class="row-2">
                            <MLabel label="Đơn vị" required />
                            <VueMultiselect
                                v-model="selected"
                                :options="options"
                                :searchable="false"
                                :close-on-select="false"
                                :show-labels="false"
                                placeholder="Pick a value"
                            ></VueMultiselect>
                            <!-- <div class="m-dropdownlist">
                                <select
                                    @change="onChangeDepartment"
                                    ref="changeDepartment"
                                >
                                    <option value="1">Phòng kế toán</option>
                                    <option value="2">Phòng nhân sự</option>
                                    <option value="2">Phòng sản xuất</option>
                                    <option value="2">Phòng đào tạo</option>
                                </select>
                            </div> -->
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.PositionName"
                                label="Chức danh"
                            />
                        </div>
                    </div>

                    <!-- Column Right -->
                    <div class="column-right">
                        <div class="row-1">
                            <MDatePicker
                                v-model="employee.DateOfBirth"
                                label="Ngày sinh"
                                id="birthdate"
                            />
                            <MRadioBox
                                v-model="employee.Gender"
                                label="Giới tính"
                            >
                                <MRadio label="Nam" id="male" name="gender" />
                                <MRadio label="Nữ" id="female" name="gender" />
                                <MRadio label="Khác" id="other" name="gender" />
                            </MRadioBox>
                        </div>

                        <div class="row-2">
                            <MTextfield
                                v-model="employee.IdentityNumber"
                                label="Số CCCD"
                                tooltip="Số căn cước công dân"
                            />
                            <MDatePicker
                                v-model="employee.IdentityDate"
                                label="Ngày cấp"
                                id="identity-date"
                            />
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.IdentityPlace"
                                label="Nơi cấp"
                            />
                        </div>
                    </div>
                </div>

                <div class="content-2">
                    <div class="row-1">
                        <MTextfield
                            v-model="employee.Address"
                            label="Địa chỉ"
                        />
                    </div>

                    <div class="row-2">
                        <MTextfield
                            v-model="employee.PhoneNumber"
                            label="ĐT di động"
                            tooltip="Điện thoại di động"
                        />
                        <MTextfield
                            v-model="employee.Landline"
                            label="ĐT cố định"
                            tooltip="Điện thoại cố định"
                        />
                        <MTextfield
                            v-model="employee.Email"
                            label="Email"
                            placeHolder="example@email.com"
                        />
                    </div>

                    <div class="row-3">
                        <MTextfield
                            label="Tài khoản ngân hàng"
                            v-model="employee.BankAccount"
                        />
                        <MTextfield
                            label="Tên ngân hàng"
                            v-model="employee.BankName"
                        />
                        <MTextfield
                            label="Chi nhánh"
                            v-model="employee.BankBranch"
                        />
                    </div>
                </div>
            </template>

            <!-- Form Actions -->
            <template #action>
                <div class="form-action">
                    <!-- Nút Hủy -->
                    <MButton
                        :type="$MISAEnum.Component.Button.Type.Secondary"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .Cancel
                        "
                        @click="onClickCancelButton"
                    />

                    <!-- Nút Cất -->
                    <MButton
                        :type="$MISAEnum.Component.Button.Type.Secondary"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .Store
                        "
                        @click="onSave"
                    />

                    <!-- Nút Cất và thêm -->
                    <MButton
                        :type="$MISAEnum.Component.Button.Type.Primary"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .StoreAndAdd
                        "
                        @click="onSaveAndAdd"
                    />
                </div>
            </template>
        </MForm>

        <!-- Dialog -->
        <MDialog
            v-if="showDialog"
            :type="$MISAEnum.DialogType.Error"
            :closeDialog="onCloseDialog"
            :title="dialogTitle"
            :description="dialogDescription"
        >
            <MButton
                @click="onCloseDialog"
                :type="$MISAEnum.ButtonType.Primary"
                :label="$MISAResource[$languageCode].Close"
            />
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
            ]"
        />
    </div>
</template>

<script>
import MButton from "@components/base/button/MButton.vue";
import MLabel from "@components/base/label/MLabel.vue";
import MTextfield from "@components/base/textfield/MTextfield.vue";
import MDatePicker from "@components/base/date-picker/MDatePicker.vue";
import MDialog from "@components/base/dialog/MDialog.vue";
import MForm from "@components/base/form/MForm.vue";
import MToastMessage from "@components/base/toast-message/MToastMessage.vue";
import MRadioBox from "@components/base/radio/MRadioBox.vue";
import MRadio from "@components/base/radio/MRadio.vue";
import VueMultiselect from "vue-multiselect";

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
        VueMultiselect,
    },

    props: ["form-data"],

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
                    this.dialogDescription =
                        this.$MISAResource[
                            this.$languageCode
                        ].InternalServerErrorMessage;
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
                    `${this.$api.BaseUrl}${this.$api.CreateEmployee}`,
                    this.employee
                )
                .then((response) => {
                    this.$emit("closeEmployeeForm");
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
                    `${this.$api.BaseUrl}${this.$api.CreateEmployee}`,
                    this.employee
                )
                .then(() => {
                    this.employee.EmployeeCode = "";
                    this.employee.FullName = "";
                    this.employee.DateOfBirth = "";
                    this.employee.Gender = 0;
                    this.employee.IdentityNumber = "";
                    this.employee.IdentityDate = "";
                    this.employee.IdentityPlace = "";
                    this.employee.Address = "";
                    this.employee.PhoneNumber = "";
                    this.employee.Landline = "";
                    this.employee.Email = "";
                    this.employee.BankAccount = "";
                    this.employee.BankName = "";
                    this.employee.BankBranch = "";
                    this.employee.PositionName = "";
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
            selected: null,
            options: ["list", "of", "options"],
        };
    },

    created() {
        this.employee.DepartmentId = "4e272fc4-7875-78d6-7d32-6a1673ffca7c";
        // Tải mã nhân viên mới
        this.$axios
            .get(`${this.$api.BaseUrl}${this.$api.NewEmployeeCode}`)
            .then((response) => {
                this.newEmployeeCode = response.data;
                this.employee.EmployeeCode = this.newEmployeeCode;
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

<style src="vue-multiselect/dist/vue-multiselect.css"></style>

<style scoped>
@import url(./employee-form.css);
</style>
