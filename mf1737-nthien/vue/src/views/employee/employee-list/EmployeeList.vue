<template>
    <div class="employee">
        <!-- Phần tiêu đề trang -->
        <div class="page-title">
            <!-- Tiêu đề trang -->
            <MHeading :type="$enums.HeadingType.Heading1" :title="$resx[$langCode].Page.Title.Employee" />

            <div class="page-title-right">
                <!-- Nút Thêm mới -->
                <MButton :type="$enums.Button.PrimaryWithIcon" :label="$resx[$langCode].Label.Add" icon-sprites="add"
                    @clickButton="onClickAddEmployeeButton" />
            </div>
        </div>

        <!-- Phần nội dung trang -->
        <div class="page-content">
            <!-- Bảng danh sách nhân viên -->
            <MTable ref="table" />
            <!-- <VTable /> -->
        </div>

        <!-- Dialog thông báo -->
        <MDialog v-if="dialog.display" :title="dialog.title" :content="dialog.content">
            <template #secondary>
                <MButton v-if="dialog.buttons.secondary" :type="$enums.Button.Secondary"
                    :label="dialog.buttons.secondary.label" @clickButton="onClickDialogSecondaryButton" />
            </template>
            <template #primary>
                <MButton :type="$enums.Button.Primary" :label="dialog.buttons.primary.label" />
            </template>
        </MDialog>

        <!-- Employee Detail Form -->
        <EmployeeForm v-if="displayEmployeeForm" :form-data="employeeEdit" @showToastMessage="onShowToastMessage"
            @closeEmployeeForm="onCloseEmployeeForm" />

        <!-- Toast Message -->
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
import MButton from "@components/bases/buttons/MButton.vue";
import MHeading from "@components/bases/headings/MHeading.vue";
import MTable from "@components/bases/tables/MTable.vue";
import VTable from "@components/bases/tables/VTable.vue";
import MToastMessage from "@components/bases/toast-message/MToastMessage.vue";
import MDialog from "@components/bases/dialogs/MDialog.vue";
import EmployeeForm from "@views/employee/employee-form/EmployeeForm.vue";
import employee from "@services/employee";

export default {
    name: "EmployeeList",

    components: {
        EmployeeForm,
        MButton,
        MHeading,
        MTable,
        VTable,
        MToastMessage,
        MDialog,
    },

    data() {
        return {
            employees: [],
            employeeEdit: {},
            displayEmployeeForm: false,
            showToastMessage: false,
            headers: [
                {
                    type: "checkbox",
                },
                {
                    type: "label",
                    label: "Mã nhân viên",
                },
                {
                    type: "label",
                    label: "Tên nhân viên",
                },
                {
                    type: "label",
                    label: "Giới tính",
                },
                {
                    type: "date",
                    label: "Ngày sinh",
                },
                {
                    type: "label",
                    label: "Số CCCD",
                    tooltip: "Số căn cước công dân",
                },
                {
                    type: "label",
                    label: "Chức vụ",
                },
                {
                    type: "label",
                    label: "Phòng ban",
                },
                {
                    type: "label",
                    label: "Số tài khoản",
                },
                {
                    type: "label",
                    label: "Tên ngân hàng",
                },
                {
                    type: "label",
                    label: "Chi nhánh",
                },
                {
                    type: "label",
                    label: "Mức lương",
                },
            ],
            dialog: {
                title: "Xóa nhân viên",
                content: "Bạn có chắc chắn muốn xóa nhân viên này không?",
                buttons: {
                    secondary: {
                        type: this.$enums.Button.Secondary,
                        label: "Hủy bỏ",
                    },
                    primary: {
                        type: this.$enums.Button.Primary,
                        label: "Xóa",
                    },
                },
                display: false,
            },
        };
    },

    methods: {
        /**
         * Lấy dữ liệu từ API.
         * CreatedBy: hiennt200210 (30/8/2023)
         */
        async loadData() {
            // Hiển thị spinner
            this.showSpinner = true;

            // Lấy dữ liệu
            try {
                const response = await employee.getByFilter(2, 1);
                this.rows = response.data;
                console.log(response.data);
            } catch (error) {
                console.log(error.response.data);
                this.onShowDialog(error.response.data);
            }

            // Ẩn spinner
            this.showSpinner = false;
        },

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

        onShowDialog(data) {
            this.dialog.title = data.UserMessage;
            this.dialog.content = data.Errors;
            this.dialog.display = true;
        },

        onClickDialogSecondaryButton() {
            this.dialog.display = false;
        },
    },

    async created() {
        // Lấy dữ liệu
        await this.loadData();

        this.$emitter.on("showToastMessage", this.onShowToastMessage);

        // const response = await employee.deleteMultiple(["65ecf5c2-5298-156e-d51d-086412a8ea97"]);
        // console.log(response);
    },

    beforeUnmount() {
        this.$emitter.off("showToastMessage", this.onShowToastMessage);
    },
};
</script>

<style scoped>
@import url(./employee-list.css);
</style>
