<template>
    <div class="employee">
        <!-- Phần tiêu đề trang -->
        <div class="page-title">
            <!-- Tiêu đề trang -->
            <MHeading
                :type="$MISAEnum.Component.Heading.Type.Heading1"
                :title="$MISAResource[$language].Page.Title.Employee" />

            <div class="page-title-right">
                <!-- Nút Thêm mới -->
                <MSButton
                    @click="onShowEmployeeDetail($MISAEnum.FormMode.Add)"
                    :type="$MISAEnum.Component.Button.Type.PrimaryIcon"
                    :label="
                        $MISAResource[$language].Component.Button.Label
                            .AddNewEmployee
                    "
                    :icon="{ style: 'solid', name: 'plus' }" />

                <!-- Nút Cài đặt -->
                <MButton
                    :type="$MISAEnum.ButtonType.Icon"
                    icon="setting"
                    :tooltip="$MISAResource[$languageCode].Setting" />
            </div>
        </div>

        <!-- Phần nội dung trang -->
        <div class="page-content">
            <!-- Bảng danh sách nhân viên -->
            <MTable />
        </div>

        <!-- Employee Detail Form -->
        <EmployeeDetail
            @showEmployeeDetail="onShowEmployeeDetail"
            @closeEmployeeDetail="onCloseEmployeeDetail"
            @showToastMessage="onShowToastMessage"
            ref="focusEmployeeDetail"
            v-if="showEmployeeDetail"
            :employeeEdit="employeeEdit" />

        <!-- Toast Message -->
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
import EmployeeDetail from "./EmployeeDetail.vue";
import MButton from "../../components/base/button/MButton.vue";
import MSButton from "../../components/base/button/MSButton.vue";
import MHeading from "../../components/base/heading/MHeading.vue";
import MTable from "../../components/base/table/MTable.vue";
import MToastMessage from "../../components/base/toast-message/MToastMessage.vue";

export default {
    name: "EmployeeList",
    components: {
        EmployeeDetail,
        MButton,
        MSButton,
        MHeading,
        MTable,
        MToastMessage,
    },

    methods: {
        /**
         * Mở form Thông tin chi tiết khi nhấn nút Thêm mới.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onShowEmployeeDetail(formMode) {
            if (formMode === this.$MISAEnum.FormMode.Add)
                this.showEmployeeDetail = true;
            else {
                this.showEmployeeDetail = true;
                // Gán đối tượng nhân viên cần sửa cho employeeEdit
            }
        },

        /**
         * Đóng form Thông tin chi tiết khi nhấn nút Hủy.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseEmployeeDetail() {
            this.showEmployeeDetail = false;
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
        },

        /**
         * Đóng toast message.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onCloseToastMessage() {
            this.showToastMessage = false;
            // this.showEmployeeDetail = false;
        },
    },

    data() {
        return {
            employees: [],
            employeeEdit: {},
            showEmployeeDetail: false,
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
@import url(../../styles/components/dropdownlist.css);
@import url(../../styles/views/employee/employee-list.css);
</style>
