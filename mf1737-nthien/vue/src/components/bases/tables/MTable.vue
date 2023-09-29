<template>
    <div class="component-wrapper">
        <!-- Toolbar -->
        <div class="table-toolbar">
            <div class="toolbar-left">
                <div v-if="numberSelectedRows > 0" class="selected-rows">
                    <div>
                        {{ $resx[$langCode].Selected }}
                        <span>{{ numberSelectedRows }}</span>
                    </div>
                    <MButton @clickButton="unSelectAll" :type="$enums.Button.Link" label="Bỏ chọn" />
                </div>

                <div v-if="numberSelectedRows" class="selected-rows-action">
                    <!-- Nút Xóa -->
                    <MButton id="delete-button" :type="$enums.Button.SecondaryWithIcon" :label="$resx[$langCode].Component.Button.Label
                        .Delete
                        " icon="delete" @clickButton="onClickDeleteButton" />

                    <!-- Nút Khác -->
                    <MButton :type="$enums.Button.Icon" icon="more_horiz"
                        :tooltip="$resx[$langCode].Tooltip.Button.Other" />
                </div>
            </div>

            <div class="toolbar-right">
                <!-- Trường Tìm kiếm -->
                <MTextfield id="search-textfield" placeHolder="Tìm kiếm trong danh sách" icon="search" />

                <!-- Nút Xuất -->
                <MButton :type="$enums.Button.Icon" icon-sprites="excel-file"
                    :tooltip="$resx[$langCode].Tooltip.Button.Export" />

                <!-- Nút Tải lại -->
                <MButton :type="$enums.Button.Icon" icon-sprites="refresh" :tooltip="$resx[$langCode].Tooltip.Button.Reload"
                    @clickButton="loadData" />
            </div>
        </div>

        <!-- Table -->
        <div class="table-wrapper">
            <table>
                <thead>
                    <tr>
                        <th class="text-align-center sticky-column-left">
                            <MCheckbox v-model="allSelected" @click="selectAll" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Mã nhân viên" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Tên nhân viên" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Giới tính" />
                        </th>
                        <th class="text-align-center">
                            <MLabel label="Ngày sinh" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Số điện thoại" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Email" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Địa chỉ" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Số CCCD" tooltip="Số căn cước công dân" />
                        </th>
                        <th class="text-align-center">
                            <MLabel label="Ngày cấp" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Nơi cấp" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Phòng ban" />
                        </th>
                        <th class="text-align-left">
                            <MLabel label="Chức vụ" />
                        </th>
                        <th class=" text-align-left">
                            <MLabel label="Tài khoản ngân hàng" />
                        </th>
                        <th class=" text-align-left">
                            <MLabel label="Tên ngân hàng" />
                        </th>
                        <th class=" text-align-left">
                            <MLabel label="Chi nhánh ngân hàng" />
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for=" employee in employees" :key="employee.employeeId" @mouseover="employee.showRowAction = true"
                        @mouseout="employee.showRowAction = false" :class="{ 'selected-row': employee.isSelected }">
                        <!-- Checkbox -->
                        <td class="text-align-center sticky-column-left">
                            <MCheckbox v-model="employee.isSelected" />
                        </td>

                        <!-- Mã nhân viên -->
                        <td class="text-align-left">
                            {{ employee.employeeCode }}
                        </td>

                        <!-- Tên nhân viên -->
                        <td class="text-align-left">{{ employee.fullName }}</td>

                        <!-- Giới tính -->
                        <td class="text-align-left">
                            {{
                                employee.gender === $enums.Gender.Female
                                ? $resx[$langCode].GenderFemale
                                : $resx[$langCode].GenderMale
                            }}
                        </td>

                        <!-- Ngày sinh -->
                        <td class="text-align-center">
                            {{ $helpers.formatDate(employee.dateOfBirth) }}
                        </td>

                        <!-- Số điện thoại -->
                        <td class="text-align-left">
                            {{ employee.phoneNumber }}
                        </td>

                        <!-- Email -->
                        <td class="text-align-left">{{ employee.email }}</td>

                        <!-- Địa chỉ -->
                        <td class="text-align-left">{{ employee.address }}</td>

                        <!-- Số căn cước công dân -->
                        <td class="text-align-left">
                            {{ employee.identityNumber }}
                        </td>

                        <!-- Ngày cấp -->
                        <td class="text-align-center">
                            {{ $helpers.formatDate(employee.identityDate) }}
                        </td>

                        <!-- Nơi cấp -->
                        <td class="text-align-left">
                            {{ employee.identityPlace }}
                        </td>

                        <!-- Đơn vị -->
                        <td class="text-align-left">
                            {{ employee.departmentName }}
                        </td>

                        <!-- Chức vụ -->
                        <td class="text-align-left">
                            {{ employee.positionName }}
                        </td>

                        <!-- Tài khoản ngân hàng -->
                        <td class="text-align-left">
                            {{ employee.bankAccount }}
                        </td>

                        <!-- Tên ngân hàng -->
                        <td class="text-align-left">
                            {{ employee.bankName }}
                        </td>

                        <!-- Chi nhánh ngân hàng -->
                        <td class="text-align-left">
                            {{ employee.bankBranch }}
                        </td>

                        <div v-show="employee.showRowAction" class="row-action">
                            <!-- Nút Sửa -->
                            <MButton :type="$enums.Button.Icon" icon-sprites="edit" :tooltip="$resx[$langCode].Tooltip.Edit
                                " />

                            <!-- Nút Nhân bản -->
                            <MButton :type="$enums.Button.Icon" icon-sprites="duplicate" :tooltip="$resx[$langCode].Tooltip.Duplicate
                                " />
                        </div>
                    </tr>
                </tbody>
            </table>
            <MSpinner v-if="showSpinner" />
        </div>

        <!-- Paging -->
        <div class="table-paging">
            <div class="paging-left">
                Tổng: <span class="number">{{ employees.length }}</span> bản ghi
            </div>
            <div class="paging-right">
                <div class="record-per-page">
                    Số bản ghi/trang:
                    <div class="m-dropdownlist">
                        <select name="">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                    </div>
                </div>
                <div>1 - 4 bản ghi</div>
                <div class="paging-button">
                    <MButton type="title-bar" icon="navigate_before" />
                    <MButton type="title-bar" icon="navigate_next" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "../buttons/MButton.vue";
import MLabel from "../labels/MLabel.vue";
import MTextfield from "../text-fields/MTextfield.vue";
import MCheckbox from "../checkbox/MCheckbox.vue";
import MSpinner from "../loadings/MSpinner.vue";
import employee from "@services/employee.js";

export default {
    name: "MTable",
    components: {
        MButton,
        MLabel,
        MTextfield,
        MCheckbox,
        MSpinner,
    },

    data() {
        return {
            employees: [],
            showSpinner: false,
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
            const response = await employee.getAll();

            // Gán dữ liệu
            this.employees = response.data;

            // Ẩn spinner
            this.showSpinner = false;
        },

        /**
         * Bỏ chọn tất cả.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        unSelectAll() {
            for (let employee of this.employees) employee.isSelected = false;
        },

        /**
         * Xử lý khi nhấn chọn checkbox tại header của bảng:
         * - Bỏ chọn tất cả bản ghi nếu tất cả bản ghi đang được chọn.
         * - Chọn tất cả bản ghi nếu có một trong các bản ghi chưa được chọn.
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
        async onClickDeleteButton() {
            // console.log("Xoá");
            // Lấy danh sách các bản ghi được chọn
            const selectedEmployees = this.employees.filter(
                (anemployee) => anemployee.isSelected
            );

            // Lấy danh sách các employeeId
            const employeeIds = this.selectedEmployees?.map((selectedEmployee) => selectedEmployee.employeeId);

            try {
                await employee.deleteMultiple(employeeIds);
                console.log("Xoá thành công");
            } catch (error) {
                console.log("Xoá thất bại");
            }

            // Hiển thị dialog xác nhận
            // this.$refs.deleteDialog.show(selectedEmployees);
        },
    },

    async created() {
        this.loadData();
    },
};
</script>

<style scoped>
@import url("./table.css");
</style>
