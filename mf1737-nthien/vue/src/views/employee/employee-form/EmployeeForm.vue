<template>
    <div class="employee-detail">
        <MForm :title="$resxLang.Heading.EmployeeDetail">
            <!-- Form Content -->
            <template #content>
                <div class="content-1">
                    <!-- Column Left -->
                    <div class="column-left">
                        <div class="row-1">
                            <MTextfield
                                v-model="employee.employeeCode"
                                :label="$resxLang.Label.Code"
                                required
                                ref="first-input"
                            />
                            <MTextfield
                                v-model="employee.fullName"
                                :label="$resxLang.Label.Name"
                                required
                            />
                        </div>

                        <div class="row-2">
                            <MLabel
                                :label="$resxLang.Label.Department"
                                required
                            />
                            <MCombobox
                                :url="departmentUrl"
                                propValue="departmentId"
                                propText="departmentName"
                                :formValue="employee.departmentId"
                                @getValue="getDepartmentOptions"
                            >
                            </MCombobox>
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.positionName"
                                :label="$resxLang.Label.Position"
                            />
                        </div>
                    </div>

                    <!-- Column Right -->
                    <div class="column-right">
                        <div class="row-1">
                            <MDatePicker
                                v-model="employee.dateOfBirth"
                                :label="$resxLang.Label.DateOfBirth"
                                id="date-of-birth"
                            />
                            <MRadioBox :label="$resxLang.Label.Gender">
                                <MRadio
                                    v-model="employee.gender"
                                    :label="$resxLang.Gender.Male"
                                    id="male"
                                    name="gender"
                                    :value="$enums.Gender.Male"
                                />
                                <MRadio
                                    v-model="employee.gender"
                                    :label="$resxLang.Gender.Female"
                                    id="female"
                                    name="gender"
                                    :value="$enums.Gender.Female"
                                />
                                <MRadio
                                    v-model="employee.gender"
                                    :label="$resxLang.Gender.Other"
                                    id="other"
                                    name="gender"
                                    :value="$enums.Gender.Other"
                                />
                            </MRadioBox>
                        </div>

                        <div class="row-2">
                            <MTextfield
                                v-model="employee.identityNumber"
                                :label="$resxLang.Label.IdentityNumber"
                                :tooltip="$resxLang.Tooltip.IdentityNumber"
                            />
                            <MDatePicker
                                v-model="employee.identityDate"
                                :label="$resxLang.Label.IdentityDate"
                                id="identity-date"
                            />
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.identityPlace"
                                :label="$resxLang.Label.IdentityPlace"
                            />
                        </div>
                    </div>
                </div>

                <div class="content-2">
                    <div class="row-1">
                        <MTextfield
                            v-model="employee.address"
                            :label="$resxLang.Label.Address"
                        />
                    </div>

                    <div class="row-2">
                        <MTextfield
                            v-model="employee.phoneNumber"
                            :label="$resxLang.Label.PhoneNumber"
                            :tooltip="$resxLang.Tooltip.PhoneNumber"
                        />
                        <MTextfield
                            v-model="employee.landlineNumber"
                            :label="$resxLang.Label.LandlineNumber"
                            :tooltip="$resxLang.Tooltip.LandlineNumber"
                        />
                        <MTextfield
                            v-model="employee.email"
                            :label="$resxLang.Label.Email"
                            :placeHolder="$resxLang.PlaceHolder.Email"
                        />
                    </div>

                    <div class="row-3">
                        <MTextfield
                            :label="$resxLang.Label.BankAccount"
                            v-model="employee.bankAccount"
                        />
                        <MTextfield
                            :label="$resxLang.Label.BankName"
                            v-model="employee.bankName"
                        />
                        <MTextfield
                            :label="$resxLang.Label.BankBranch"
                            v-model="employee.bankBranch"
                        />
                    </div>
                </div>
            </template>

            <!-- Form Actions -->
            <template #action>
                <div class="form-action">
                    <!-- Nút Hủy -->
                    <MButton
                        :type="$enums.Button.Secondary"
                        :label="$resxLang.Label.Cancel"
                        @clickButton="onClickCancelButton"
                    />

                    <!-- Nút Cất -->
                    <MButton
                        :type="$enums.Button.Secondary"
                        :label="$resxLang.Label.Store"
                        @clickButton="onSave"
                    />

                    <!-- Nút Cất và thêm -->
                    <MButton
                        :type="$enums.Button.Primary"
                        :label="$resxLang.Label.StoreAndAdd"
                        @clickButton="onSaveAndAdd"
                    />
                </div>
            </template>
        </MForm>

        <!-- Dialog -->
        <!-- Dialog thông báo -->
        <MDialog
            v-if="dialog.display"
            :type="dialog.type"
            :title="dialog.title"
            :content="dialog.content"
            @closeDialog="onCloseDialog"
        >
            <!-- Nút phụ -->
            <MButton
                v-if="dialog.buttons.secondary"
                :type="this.$enums.Button.Secondary"
                :label="dialog.buttons.secondary"
                @clickButton="onClickDialogSecondaryButton"
            />

            <!-- Nút chính -->
            <MButton
                :type="$enums.Button.Primary"
                :label="dialog.buttons.primary"
                @clickButton="onClickDialogPrimaryButton"
            />
        </MDialog>

        <!-- Toast message -->
        <MToast
            v-if="toast.display"
            :type="toast.type"
            :message="toast.message"
            :link="toast.link"
            :action="toast.action"
            @closeToastMessage="onCloseToastMessage"
        />
    </div>
</template>

<script>
import MButton from "@components/bases/buttons/MButton.vue";
import MLabel from "@components/bases/labels/MLabel.vue";
import MTextfield from "@components/bases/text-fields/MTextfield.vue";
import MDatePicker from "@components/bases/date-pickers/MDatePicker.vue";
import MDialog from "@components/bases/dialogs/MDialog.vue";
import MForm from "@components/bases/forms/MForm.vue";
import MToast from "@components/bases/toast-message/MToast.vue";
import MRadioBox from "@components/bases/radios/MRadioBox.vue";
import MRadio from "@components/bases/radios/MRadio.vue";
import MCombobox from "@components/bases/combobox/MCombobox.vue";
import employee from "@services/employee.js";
import api from "@configs/api.js";

export default {
    name: "EmployeeDetail",
    components: {
        MButton,
        MLabel,
        MTextfield,
        MDatePicker,
        MDialog,
        MForm,
        MToast,
        MRadioBox,
        MRadio,
        MCombobox,
    },

    props: {
        mode: {
            type: Number,
            default: 0,
        },
        data: {
            type: Object,
            default: {},
        },
    },

    data() {
        return {
            employee: {},
            showDialog: false,
            showToastMessage: false,
            departmentUrl: `${api.baseUrl}api/v1/departments`,
            toast: {
                display: false,
                type: "",
                message: "",
            },
            dialog: {
                display: false,
                type: "",
                title: "",
                content: "",
                buttons: {
                    secondary: "",
                    primary: "",
                },
            },
        };
    },

    methods: {
        /**
         * Hiển thị dialog thông báo.
         * @param {*} dialog Thông tin thông báo
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowDialog(dialog) {
            this.dialog.type = dialog.type;
            this.dialog.title = dialog.title;
            this.dialog.content = dialog.content;
            this.dialog.buttons = dialog.buttons;
            this.onClickDialogSecondaryButton = dialog.secondaryAction;
            this.onClickDialogPrimaryButton = dialog.primaryAction;
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
         * Xử lý sự kiện click secondary button trên dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickDialogSecondaryButton() {
            this.dialog.display = false;
        },

        /**
         * Xử lý sự kiện click primary button trên dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickDialogPrimaryButton() {
            // Chưa gán hành động nào
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
        onShowToastMessage(toast) {
            this.toast.type = toast.type;
            this.toast.message = toast.message;
            this.toast.link = toast.link;
            this.toast.action = toast.action;
            this.toast.close = toast.close;
            this.toast.display = true;
            setTimeout(() => {
                this.toast.display = false;
            }, 5000);
        },

        /**
         * Đóng toast message.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseToastMessage() {
            this.toast.display = false;
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
         * Lưu dữ liệu khi nhấn Cất.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        async onSave() {
            if (this.mode == this.$enums.Form.Edit) {
                try {
                    const response = await employee.update(
                        this.employee.employeeId,
                        this.employee
                    );
                    console.log(response);
                    this.$emit("updateSuccess");
                } catch (error) {
                    console.log(error);
                    this.onShowDialog({
                        type: this.$enums.Dialog.Error,
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resxLang.Label.Close,
                        },
                        primaryAction: this.onCloseDialog,
                    });
                }
            } else {
                try {
                    const response = await employee.insert(this.employee);
                    console.log(response);
                    this.$emit("insertSuccess");
                } catch (error) {
                    console.log(error);
                    this.onShowDialog({
                        type: "error",
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resxLang.Label.Close,
                        },
                        primaryAction: this.onCloseDialog,
                    });
                }
            }
        },

        /**
         * Lưu dữ liệu và reset form khi nhấn Cất và thêm.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        async onSaveAndAdd() {
            if (this.mode == this.$enums.Form.Edit) {
                try {
                    const response = await employee.update(
                        this.employee.employeeId,
                        this.employee
                    );
                    console.log(response);
                    // Hiển thị toast message
                    this.onShowToastMessage({
                        type: this.$enums.Toast.Success,
                        message: this.$resxLang.EmployeeUpdate,
                    });
                    // Lấy mã nhân viên mới
                    try {
                        this.employee.employeeCode =
                            await this.getNewEmployeeCode();
                    } catch (error) {
                        this.employee.employeeCode = "";
                    }
                    this.$refs["first-input"].focus();
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
                    this.employee.departmentId = "";
                } catch (error) {
                    console.log(error);
                    this.onShowDialog({
                        type: this.$enums.Dialog.Error,
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resxLang.Label.Close,
                        },
                        primaryAction: this.onCloseDialog,
                    });
                }
            } else {
                try {
                    const response = await employee.insert(this.employee);
                    console.log(response);
                    // Hiển thị toast message
                    this.onShowToastMessage({
                        type: this.$enums.Toast.Success,
                        message: this.$resxLang.EmployeeAdd,
                    });
                    // Lấy mã nhân viên mới
                    try {
                        this.employee.employeeCode =
                            await this.getNewEmployeeCode();
                    } catch (error) {
                        this.employee.employeeCode = "";
                    }
                    this.$refs["first-input"].focus();
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
                    this.employee.departmentId = "";
                } catch (error) {
                    console.log(error);
                    this.onShowDialog({
                        type: this.$enums.Dialog.Error,
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resxLang.Label.Close,
                        },
                        primaryAction: this.onCloseDialog,
                    });
                }
            }
        },

        /**
         * Lấy giá trị của phòng ban.
         * CreatedBy: hiennt200210 (26/09/2023)
         */
        getDepartmentOptions(value, text) {
            this.employee.departmentId = value;
        },
    },

    async created() {
        if (this.mode === this.$enums.Form.Edit) {
            console.log(this.data);
            this.employee = this.data;
            this.employee.dateOfBirth = new Date(this.employee.dateOfBirth);
        } else if (this.mode === this.$enums.Form.Duplicate) {
            this.employee = this.data;
            // Lấy mã nhân viên mới
            try {
                this.employee.employeeCode = await this.getNewEmployeeCode();
            } catch (error) {
                this.employee.employeeCode = "";
            }
        } else {
            // Lấy mã nhân viên mới
            try {
                this.employee.employeeCode = await this.getNewEmployeeCode();
            } catch (error) {
                this.employee.employeeCode = "";
            }
        }
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
