<template>
    <div class="employee">
        <!-- Phần tiêu đề trang -->
        <div class="page-title">
            <!-- Tiêu đề -->
            <MHeading
                :type="$enums.Heading.Heading1"
                :title="this.$resxLang.Heading.Employee"
            />

            <div class="page-title-right">
                <!-- Nút Thêm mới -->
                <MButton
                    :type="$enums.Button.PrimaryWithIcon"
                    :label="$resxLang.Label.Add"
                    icon="add"
                    @clickButton="onClickAddEmployeeButton"
                />
            </div>
        </div>

        <!-- Phần nội dung trang -->
        <div class="page-content">
            <!-- Bảng danh sách nhân viên -->
            <MTable>
                <template #toolbar-left>
                    <div v-if="numberSelectedRows > 0" class="selected-rows">
                        <div>
                            {{ $resxLang.Selected }}
                            <span>{{ numberSelectedRows }}</span>
                        </div>
                        <MButton
                            @clickButton="unSelectAll"
                            :type="$enums.Button.Link"
                            :label="$resxLang.Label.Unselect"
                        />
                    </div>

                    <div v-if="numberSelectedRows" class="selected-rows-action">
                        <!-- Nút Xóa -->
                        <MButton
                            id="delete-button"
                            :type="$enums.Button.Secondary"
                            :label="$resxLang.Label.Delete"
                            @clickButton="onClickDeleteButton"
                        />
                    </div>
                </template>

                <template #toolbar-right>
                    <!-- Trường Tìm kiếm -->
                    <MTextfield
                        v-model="searchText"
                        id="search-textfield"
                        :placeHolder="$resxLang.PlaceHolder.Search"
                        icon="search"
                        @input="onChangeSearchText"
                    />

                    <!-- Nút Xuất -->
                    <MButton
                        :type="$enums.Button.Icon"
                        icon="excel-file"
                        :tooltip="$resxLang.Tooltip.Button.Export"
                    />

                    <!-- Nút Tải lại -->
                    <MButton
                        :type="$enums.Button.Icon"
                        icon="refresh"
                        :tooltip="$resxLang.Tooltip.Button.Reload"
                        @clickButton="onClickReloadButton"
                    />
                </template>

                <template #thead>
                    <tr>
                        <th class="tr__checkbox">
                            <MCheckbox
                                v-model="allSelected"
                                @click="selectAll"
                            />
                        </th>
                        <th
                            v-for="item in columns"
                            :key="item.name"
                            :class="item.type"
                        >
                            <MLabel
                                :label="item.title"
                                :tooltip="item.tooltip"
                            />
                        </th>
                    </tr>
                </template>

                <template #tbody>
                    <tr
                        v-for="item in employees"
                        :key="item.employeeId"
                        :class="{ 'selected-row': item.isSelected }"
                        @mouseover="item.showRowAction = true"
                        @mouseout="item.showRowAction = false"
                        @dblclick="onClickEditButton(item)"
                    >
                        <td class="tr__checkbox">
                            <MCheckbox v-model="item.isSelected" />
                        </td>
                        <td
                            v-for="cell in columns"
                            :key="cell.name"
                            :class="cell.type"
                        >
                            {{ cell.format(item[cell.name]) }}
                        </td>

                        <div v-show="item.showRowAction" class="row-action">
                            <!-- Nút Sửa -->
                            <MButton
                                :type="$enums.Button.Icon"
                                icon="edit"
                                :tooltip="$resxLang.Tooltip.Edit"
                                @clickButton="onClickEditButton(item)"
                            />

                            <!-- Nút Nhân bản -->
                            <MButton
                                :type="$enums.Button.Icon"
                                icon="duplicate"
                                :tooltip="$resxLang.Tooltip.Duplicate"
                                @clickButton="onClickDuplicateButton(item)"
                            />
                        </div>
                    </tr>
                </template>

                <template #paging-left>
                    {{ $resxLang.Label.Total }}
                    <span class="number">{{ total }}</span>
                    {{ $resxLang.Label.RecordLowerCase }}
                </template>

                <template #paging-right>
                    <div class="record-per-page">
                        {{ $resxLang.Label.RecordPerPage }}
                        <div class="m-dropdownlist" @change="onChangeLimit">
                            <select name="limit" v-model="limit">
                                <option value="10">10</option>
                                <option value="20">20</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select>
                        </div>
                    </div>
                    <div>
                        {{ $resxLang.Label.Record }} {{ Number(offset) + 1 }}-{{
                            lastIndex
                        }}
                    </div>
                    <div class="paging-button">
                        <MButton
                            :type="$enums.Button.TitleBar"
                            icon="prev"
                            :disabled="prevDisabled"
                            @clickButton="onClickPrevButton"
                        />
                        <MButton
                            :type="$enums.Button.TitleBar"
                            icon="next"
                            :disabled="nextDisabled"
                            @clickButton="onClickNextButton"
                        />
                    </div>
                </template>
            </MTable>

            <MSpinner v-if="displaySpinner" />
        </div>

        <!-- Form thông tin nhân viên -->
        <EmployeeForm
            v-if="displayEmployeeForm"
            :mode="employeeFormMode"
            :data="employeeEdit"
            @showToastMessage="onShowToastMessage"
            @closeEmployeeForm="onCloseEmployeeForm"
            @insertSuccess="onInsertSuccess"
            @updateSuccess="onUpdateSuccess"
        />

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
                v-if="dialog.buttons.primary"
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
import employee from "@services/employee";
import EmployeeForm from "@views/employee/employee-form/EmployeeForm.vue";
import MButton from "@components/bases/buttons/MButton.vue";
import MCheckbox from "@components/bases/checkbox/MCheckbox.vue";
import MDialog from "@components/bases/dialogs/MDialog.vue";
import MHeading from "@components/bases/headings/MHeading.vue";
import MLabel from "@components/bases/labels/MLabel.vue";
import MSpinner from "@components/bases/loadings/MSpinner.vue";
import MTable from "@components/bases/tables/MTable.vue";
import MTextfield from "@components/bases/text-fields/MTextfield.vue";
import MToast from "@components/bases/toast-message/MToast.vue";
import MTooltip from "@components/bases/tooltips/MTooltip.vue";

export default {
    name: "EmployeeList",

    components: {
        EmployeeForm,
        MButton,
        MCheckbox,
        MDialog,
        MHeading,
        MLabel,
        MSpinner,
        MTable,
        MTextfield,
        MToast,
        MTooltip,
    },

    data() {
        return {
            columns: [
                {
                    title: this.$resxLang.Title.EmployeeCode,
                    name: "employeeCode",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.FullName,
                    name: "fullName",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.Gender,
                    name: "gender",
                    type: "text",
                    format: this.$helpers.formatGender,
                },
                {
                    title: this.$resxLang.Title.DateOfBirth,
                    name: "dateOfBirth",
                    type: "date",
                    format: this.$helpers.formatDate,
                },
                {
                    title: this.$resxLang.Title.IdentityNumber,
                    tooltip: this.$resxLang.Tooltip.IdentityNumber,
                    type: "text",
                    name: "identityNumber",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.IdentityDate,
                    type: "date",
                    name: "identityDate",
                    format: this.$helpers.formatDate,
                },
                {
                    title: this.$resxLang.Title.IdentityPlace,
                    name: "identityPlace",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.PositionName,
                    name: "positionName",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.DepartmentName,
                    name: "departmentName",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.BankAccount,
                    name: "bankAccount",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.BankName,
                    name: "bankName",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.BankBranch,
                    name: "bankBranch",
                    type: "text",
                    format: (value) => value,
                },
                {
                    title: this.$resxLang.Title.Salary,
                    name: "salary",
                    type: "number",
                    format: this.$helpers.formatNumber,
                },
            ],

            employees: [],

            employeeFormMode: this.$enums.Form.Add,

            employeeEdit: {},

            displaySpinner: false,

            displayEmployeeForm: false,

            toast: {
                display: false,
                type: "",
                message: "",
            },

            dialog: {
                display: false,
                type: "",
                title: "",
                content: [],
                buttons: {
                    secondary: "",
                    primary: "",
                },
            },

            total: 0,
            limit: 20,
            offset: 0,
            order: "-EmployeeCode",
            searchText: "",
            prevDisabled: true,
            nextDisabled: false,
            debounceTimer: null,
        };
    },

    computed: {
        /**
         * Lấy số lượng bản ghi được chọn.
         * CreatedBy: hiennt200210 (30/08/2023)
         */
        numberSelectedRows() {
            let result = 0;
            for (let employee of this.employees) {
                if (employee.isSelected) result++;
            }
            return result;
        },

        /**
         * Nếu tất cả bản ghi được chọn, trả về true.
         * Nếu không, trả về false.
         *
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        allSelected() {
            for (let employee of this.employees) {
                if (!employee.isSelected) return false;
            }
            return true;
        },

        /**
         * Lấy index của bản ghi cuối cùng trên trang hiện tại.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        lastIndex() {
            let result = Number(this.offset) + Number(this.limit);
            if (result > this.total) result = this.total;
            return result;
        },
    },

    methods: {
        /**
         * Lấy dữ liệu từ API.
         * CreatedBy: hiennt200210 (30/8/2023)
         */
        async loadData(limit, offset, order, search) {
            // Hiển thị spinner
            this.displaySpinner = true;

            // Lấy dữ liệu
            try {
                const response = await employee.getByFilter(
                    limit,
                    offset,
                    order,
                    search
                );
                this.employees = response.data.data;
                this.total = response.data.total;
            } catch (error) {
                console.log(error);
            }

            // Ẩn spinner
            this.displaySpinner = false;
        },

        /**
         * Đóng form Thông tin chi tiết.
         * CreatedBy: hiennt200210 (20/09/2023)
         */
        onCloseEmployeeForm() {
            this.displayEmployeeForm = false;
            this.loadData(this.limit, this.offset, this.order, this.searchText);
        },

        /**
         * Xử lý khi thêm mới thành công.
         * CreatedBy: hiennt200210 (20/09/2023)
         */
        onInsertSuccess() {
            this.displayEmployeeForm = false;

            // Hiển thị toast message
            this.onShowToastMessage({
                type: this.$enums.Toast.Success,
                message: this.$resxLang.EmployeeAdd,
            });

            this.loadData(this.limit, this.offset, this.order, this.searchText);
        },

        /**
         * Xử lý khi cập nhật thành công.
         * CreatedBy: hiennt200210 (20/09/2023)
         */
        onUpdateSuccess() {
            this.displayEmployeeForm = false;

            // Hiển thị toast message
            this.onShowToastMessage({
                type: this.$enums.Toast.Success,
                message: this.$resxLang.EmployeeUpdate,
            });

            this.loadData(this.limit, this.offset, this.order, this.searchText);
        },

        /**
         * Mở form Thông tin chi tiết khi nhấn nút Thêm mới.
         * CreatedBy: hiennt200210 (20/09/2023)
         */
        onClickAddEmployeeButton() {
            this.employeeFormMode = this.$enums.Form.Add;
            this.displayEmployeeForm = true;
        },

        /**
         * Xử lý khi nhấn nút Tải lại.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickReloadButton() {
            this.loadData(this.limit, this.offset, this.order, this.searchText);
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
         * Đóng dialog thông báo.
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
         * Bỏ chọn tất cả.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        unSelectAll() {
            for (let employee of this.employees) employee.isSelected = false;
        },

        /**
         * Xử lý khi nhấn chọn checkbox tại header của bảng
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        selectAll() {
            if (this.allSelected) {
                this.unSelectAll();
            } else {
                for (let employee of this.employees) employee.isSelected = true;
            }
        },

        /**
         * Xử lý khi nhấn nút Xóa.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onClickDeleteButton() {
            // Lấy danh sách các bản ghi được chọn
            const selectedEmployees = this.employees.filter(
                (item) => item.isSelected
            );

            this.onShowDialog({
                type: this.$enums.Dialog.Warning,
                title: this.$resxLang.DeleteEmployee,
                content: [
                    `${this.$resxLang.DeleteEmployee1} ${selectedEmployees.length} ${this.$resxLang.DeleteEmployee2}`,
                ],
                buttons: {
                    secondary: this.$resxLang.Label.Cancel,
                    primary: this.$resxLang.Label.Delete,
                },
                secondaryAction: this.onCloseDialog,
                primaryAction: this.onClickDeleteDialogPrimaryButton,
            });
        },

        async onClickDeleteDialogPrimaryButton() {
            // Đóng dialog
            this.onCloseDialog();

            this.displaySpinner = true;

            // Lấy danh sách các bản ghi được chọn
            const selectedEmployees = this.employees.filter(
                (item) => item.isSelected
            );

            // Lấy danh sách các employeeId
            const employeeIds = selectedEmployees.map(
                (selectedEmployee) => selectedEmployee.employeeId
            );

            try {
                const response = await employee.deleteMultiple(employeeIds);
            } catch (error) {
                console.log(error);
            }

            this.displaySpinner = false;

            // Load lại dữ liệu
            this.loadData(this.limit, this.offset, this.order);

            // Hiển thị toast message
            this.onShowToastMessage({
                type: this.$enums.Toast.Success,
                message: `${this.$resxLang.DeleteEmployee3} ${selectedEmployees.length} ${this.$resxLang.DeleteEmployee4}`,
            });
        },

        /**
         * Xử lý khi nhấn nút Sửa.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onClickEditButton(employeeEdit) {
            this.employeeFormMode = this.$enums.Form.Edit;
            this.employeeEdit = employeeEdit;
            this.displayEmployeeForm = true;
        },

        /**
         * Xử lý khi nhấn nút Nhân bản.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onClickDuplicateButton(employeeEdit) {
            this.employeeFormMode = this.$enums.Form.Duplicate;
            this.employeeEdit = employeeEdit;
            this.displayEmployeeForm = true;
        },

        /**
         * Xử lý khi thay đổi số lượng bản ghi/trang.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onChangeLimit() {
            this.loadData(this.limit, this.offset, this.order, this.searchText);
        },

        /**
         * Xử lý khi nhấn nút Prev.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onClickPrevButton() {
            if (this.offset - this.limit <= 0) {
                this.offset = 0;
                this.prevDisabled = true;
                this.nextDisabled = false;
            } else {
                this.offset -= this.limit;
                this.loadData(
                    this.limit,
                    this.offset,
                    this.order,
                    this.searchText
                );
                this.prevDisabled = false;
                this.nextDisabled = false;
            }
        },

        /**
         * Xử lý khi nhấn nút Next.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onClickNextButton() {
            if (this.offset + this.limit >= this.total - 1) {
                this.nextDisabled = true;
                this.prevDisabled = false;
            } else {
                this.offset += this.limit;
                this.loadData(
                    this.limit,
                    this.offset,
                    this.order,
                    this.searchText
                );
                this.prevDisabled = false;
            }
        },

        /**
         * Xử lý khi thay đổi nội dung tìm kiếm.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onChangeSearchText() {
            let load = () => {
                this.loadData(
                    this.limit,
                    this.offset,
                    this.order,
                    this.searchText
                );
            };

            clearTimeout(this.debounceTimer);
            this.debounceTimer = setTimeout(load, 500);
        },
    },

    async created() {
        // Lấy dữ liệu
        await this.loadData(this.limit, this.offset, this.order);
    },
};
</script>

<style scoped>
@import url(./employee-list.css);
</style>
