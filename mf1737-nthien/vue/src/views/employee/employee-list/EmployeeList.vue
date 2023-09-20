<template>
    <div class="employee">
        <!-- Phần tiêu đề trang -->
        <div class="page-title">
            <!-- Tiêu đề trang -->
            <MHeading
                :type="$MISAEnum.Component.Heading.Type.Heading1"
                :title="$MISAResource[$language].Page.Title.Employee"
            />

            <div class="page-title-right">
                <!-- Nút Thêm mới -->
                <MButton
                    @click="onClickAddEmployeeButton"
                    :type="$MISAEnum.Component.Button.Type.PrimaryIcon"
                    :label="
                        $MISAResource[$language].Component.Button.Label
                            .AddNewEmployee
                    "
                    icon="add"
                />

                <!-- Nút Cài đặt -->
                <MButton
                    :type="$MISAEnum.Component.Button.Type.Icon"
                    icon="settings"
                    :tooltip="$MISAResource[$languageCode].Setting"
                />
            </div>
        </div>

        <!-- Phần nội dung trang -->
        <div class="page-content">
            <!-- Bảng danh sách nhân viên -->
            <MTable ref="table" />
        </div>

        <!-- Employee Detail Form -->
        <EmployeeForm
            v-if="displayEmployeeForm"
            :form-data="employeeEdit"
            @showToastMessage="onShowToastMessage"
            @closeEmployeeForm="onCloseEmployeeForm"
        />

        <!-- Toast Message -->
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
import MHeading from "@components/base/heading/MHeading.vue";
import MTable from "@components/base/table/MTable.vue";
import MToastMessage from "@components/base/toast-message/MToastMessage.vue";
import EmployeeForm from "@views/employee/employee-form/EmployeeForm.vue";

export default {
    name: "EmployeeList",
    components: {
        EmployeeForm,
        MButton,
        MHeading,
        MTable,
        MToastMessage,
    },

    methods: {
        /**
         * Đóng form Thông tin chi tiết.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseEmployeeForm() {
            this.displayEmployeeForm = false;
        },

        /**
         * Mở form Thông tin chi tiết khi nhấn nút Thêm mới.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickAddEmployeeButton() {
            this.displayEmployeeForm = true;
            // this.$refs.employeeDetail.show();
        },

        /**
         * Hiển thị toast message trong 5 giây, sau đó ẩn.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowToastMessage() {
            this.showToastMessage = true;
            setTimeout(() => {
                this.showToastMessage = false;
            }, 5000);
            this.$refs.table.loadData();
        },

        /**
         * Đóng toast message.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseToastMessage() {
            this.showToastMessage = false;
        },
    },

    data() {
        return {
            employees: [],
            employeeEdit: {},
            displayEmployeeForm: false,
            showToastMessage: false,
        };
    },

    created() {
        this.$emitter.on("showToastMessage", this.onShowToastMessage);
    },

    beforeUnmount() {
        this.$emitter.off("showToastMessage", this.onShowToastMessage);
    },
};
</script>

<style scoped>
@import url(./employee-list.css);
</style>
