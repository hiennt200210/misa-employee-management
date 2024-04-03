<template>
    <div class="employee-detail" @keydown="onKeydown">
        <MForm :title="formTitle">
            <!-- Form Content -->
            <template #content>
                <div class="content-1">
                    <!-- Column Left -->
                    <div class="column-left">
                        <div class="row-1">
                            <MTextfield
                                v-model="employee.employeeCode"
                                ref="first-input"
                                required
                                :label="$resx.Code"
                                :max-length="20"
                            />
                            <MTextfield
                                v-model="employee.fullName"
                                required
                                :label="$resx.Name"
                                :max-length="100"
                            />
                        </div>

                        <div class="row-2">
                            <MCombobox
                                required
                                url="https://localhost:44376/api/v1/Departments"
                                propValue="departmentId"
                                propText="departmentName"
                                :label="$resx.Department"
                                :formValue="employee.departmentId"
                                @getValue="getDepartmentOptions"
                            >
                            </MCombobox>
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.positionName"
                                :label="$resx.Position"
                                :max-length="255"
                            />
                        </div>
                    </div>

                    <!-- Column Right -->
                    <div class="column-right">
                        <div class="row-1">
                            <MDatePicker
                                v-model="employee.dateOfBirth"
                                :label="$resx.DateOfBirth"
                                id="date-of-birth"
                            />
                            <MRadioBox :label="$resx.Gender">
                                <MRadio
                                    v-model="employee.gender"
                                    :label="$resx.Male"
                                    id="male"
                                    name="gender"
                                    :value="$enums.Gender.Male"
                                />
                                <MRadio
                                    v-model="employee.gender"
                                    :label="$resx.Female"
                                    id="female"
                                    name="gender"
                                    :value="$enums.Gender.Female"
                                />
                                <MRadio
                                    v-model="employee.gender"
                                    :label="$resx.Other"
                                    id="other"
                                    name="gender"
                                    :value="$enums.Gender.Other"
                                />
                            </MRadioBox>
                        </div>

                        <div class="row-2">
                            <MTextfield
                                v-model="employee.identityNumber"
                                :label="$resx.IdentityNumberAbbr"
                                :tooltip="$resx.IdentityNumber"
                                :max-length="25"
                            />
                            <MDatePicker
                                v-model="employee.identityDate"
                                :label="$resx.IdentityDate"
                                id="identity-date"
                            />
                        </div>

                        <div class="row-3">
                            <MTextfield
                                v-model="employee.identityPlace"
                                :label="$resx.IdentityPlace"
                                :max-length="255"
                            />
                        </div>
                    </div>
                </div>

                <div class="content-2">
                    <div class="row-1">
                        <MTextfield
                            v-model="employee.address"
                            :label="$resx.Address"
                            :max-length="255"
                        />
                    </div>

                    <div class="row-2">
                        <MTextfield
                            v-model="employee.phoneNumber"
                            :label="$resx.PhoneNumberAbbr"
                            :tooltip="$resx.PhoneNumber"
                            :max-length="50"
                        />
                        <MTextfield
                            v-model="employee.landlineNumber"
                            :label="$resx.LandlineNumberAbbr"
                            :tooltip="$resx.LandlineNumber"
                            :max-length="50"
                        />
                        <MTextfield
                            v-model="employee.email"
                            type="email"
                            :label="$resx.Email"
                            :placeHolder="$resx.ExampleEmail"
                            :max-length="100"
                            :validate="validateEmail"
                        />
                    </div>

                    <div class="row-3">
                        <MTextfield
                            :label="$resx.BankAccount"
                            v-model="employee.bankAccount"
                            :max-length="25"
                        />
                        <MTextfield
                            :label="$resx.BankName"
                            v-model="employee.bankName"
                            :max-length="255"
                        />
                        <MTextfield
                            :label="$resx.BankBranch"
                            v-model="employee.bankBranch"
                            :max-length="255"
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
                        :label="$resx.Cancel"
                        @clickButton="onClickCancelButton"
                    />

                    <!-- Nút Cất -->
                    <MButton
                        :type="$enums.Button.Secondary"
                        :label="$resx.Store"
                        @clickButton="onSave"
                    />

                    <!-- Nút Cất và thêm -->
                    <MButton
                        :type="$enums.Button.Primary"
                        :label="$resx.StoreAndAdd"
                        @clickButton="onSaveAndAdd"
                        @keydown="onKeydownStoreAndAdd"
                    />
                </div>
            </template>
        </MForm>

        <!-- Dialog thông báo -->
        <MDialog
            v-if="dialog.display"
            :type="dialog.type"
            :title="dialog.title"
            :content="dialog.content"
            :primary-button-label="dialog.buttons.primary"
            @click-close-button="onCloseDialog"
            @click-primary-button="onClickDialogPrimaryButton"
        >
            <!-- Nút phụ -->
            <MButton
                v-if="dialog.buttons.secondary"
                :type="this.$enums.Button.Secondary"
                :label="dialog.buttons.secondary"
                @clickButton="onClickDialogSecondaryButton"
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
import {
    generateEmployeeCode,
    createEmployee,
    updateEmployee,
} from "@services/employee";
import { validateEmail } from "@utils/validate";
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

    computed: {
        /**
         * Tiêu đề của form Thông tin nhân viên.
         *
         * CreatedBy: hiennt200210 (09/10/2023)
         */
        formTitle() {
            if (this.mode === this.$enums.Form.Add) {
                return this.$resx.AddEmployee;
            } else if (this.mode === this.$enums.Form.Edit) {
                return this.$resx.EditEmployee;
            } else if (this.mode === this.$enums.Form.Duplicate) {
                return this.$resx.AddEmployee;
            }
        },
    },

    methods: {
        /**
         * Lấy mã nhân viên mới.
         *
         * CreatedBy: hiennt200210 (26/09/2023)
         */
        async getNewEmployeeCode() {
            try {
                const response = await generateEmployeeCode();
                return response.data;
            } catch {
                // Thông báo không lấy được mã nhân viên mới bằng toast message
                this.onShowToastMessage({
                    type: this.$enums.Toast.Error,
                    message: this.$resx.CannotGetNewEmployeeCode,
                });

                return "";
            }
        },

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
         * Lưu dữ liệu khi nhấn Cất.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        async onSave() {
            if (this.mode == this.$enums.Form.Edit) {
                try {
                    const response = await updateEmployee(
                        this.employee.employeeId,
                        this.employee
                    );
                    this.$emit("updateSuccess");
                } catch (error) {
                    this.onShowDialog({
                        type: this.$enums.Dialog.Error,
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resx.Close,
                        },
                        primaryAction: this.onCloseDialog,
                    });
                }
            } else {
                try {
                    const response = await createEmployee(this.employee);
                    this.$emit("insertSuccess");
                } catch (error) {
                    this.onShowDialog({
                        type: "error",
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resx.Close,
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
                    const response = await updateEmployee(
                        this.employee.employeeId,
                        this.employee
                    );

                    // Hiển thị toast message
                    this.onShowToastMessage({
                        type: this.$enums.Toast.Success,
                        message: this.$resx.EmployeeUpdate,
                    });

                    // Lấy mã nhân viên mới
                    this.employee.employeeCode =
                        await this.getNewEmployeeCode();
                    this.$refs["first-input"].focus();
                    // this.employee.fullName = "";
                    // this.employee.dateOfBirth = "";
                    // this.employee.gender = 0;
                    // this.employee.identityNumber = "";
                    // this.employee.identityDate = "";
                    // this.employee.identityPlace = "";
                    // this.employee.address = "";
                    // this.employee.phoneNumber = "";
                    // this.employee.landlineNumber = "";
                    // this.employee.email = "";
                    // this.employee.bankAccount = "";
                    // this.employee.bankName = "";
                    // this.employee.bankBranch = "";
                    // this.employee.positionName = "";
                    // this.employee.departmentId = "";
                } catch (error) {
                    this.onShowDialog({
                        type: this.$enums.Dialog.Error,
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resx.Close,
                        },
                        primaryAction: this.onCloseDialog,
                    });
                }
            } else {
                try {
                    const response = await createEmployee(this.employee);
                    // Hiển thị toast message
                    this.onShowToastMessage({
                        type: this.$enums.Toast.Success,
                        message: this.$resx.EmployeeAdd,
                    });

                    this.employee = {};
                    this.employee.departmentId = "";

                    // Lấy mã nhân viên mới
                    this.employee.employeeCode =
                        await this.getNewEmployeeCode();
                    // this.$refs["first-input"].focus();
                    // this.employee.fullName = "";
                    // this.employee.dateOfBirth = "";
                    // this.employee.gender = 0;
                    // this.employee.identityNumber = "";
                    // this.employee.identityDate = "";
                    // this.employee.identityPlace = "";
                    // this.employee.address = "";
                    // this.employee.phoneNumber = "";
                    // this.employee.landlineNumber = "";
                    // this.employee.email = "";
                    // this.employee.bankAccount = "";
                    // this.employee.bankName = "";
                    // this.employee.bankBranch = "";
                    // this.employee.positionName = "";
                    // this.employee.departmentId = "";
                } catch (error) {
                    this.onShowDialog({
                        type: this.$enums.Dialog.Error,
                        title: error.response.data.UserMessage,
                        content: error.response.data.Errors,
                        buttons: {
                            primary: this.$resx.Close,
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

        /**
         * Xử lý sự kiện nhấn phím ESC.
         * CreatedBy: hiennt200210 (04/10/2023)
         */
        onKeydown(event) {
            if (event.key === "Escape" || event.keyCode === 27) {
                this.$emit("keydownEsc");
            }
        },

        /**
         * Quay lại input đầu tiên khi nhấn phím Tab (focus tại nút Cất và thêm).
         * CreatedBy: hiennt200210 (04/10/2023)
         */
        onKeydownStoreAndAdd(event) {
            if (event.key === "Tab" && !event.shiftKey) {
                event.preventDefault();
                this.$refs["first-input"].focus();
            }
        },
    },

    async created() {
        if (this.mode === this.$enums.Form.Edit) {
            this.employee = this.data;
            this.employee.dateOfBirth = new Date(this.employee.dateOfBirth);
        } else {
            if (this.mode === this.$enums.Form.Duplicate) {
                this.employee = this.data;
            }
            this.employee.employeeCode = await this.getNewEmployeeCode();
        }
    },

    mounted() {
        this.$refs["first-input"].setWidth("152px");
        this.$refs["first-input"].focus();
    },
};
</script>

<style scoped>
@import url(./employee-form.css);
</style>
