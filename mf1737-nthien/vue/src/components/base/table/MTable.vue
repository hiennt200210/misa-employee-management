<template>
    <div class="component-wrapper">
        <!-- Toolbar -->
        <div class="table-toolbar">
            <div class="toolbar-left">
                <div v-if="numberSelectedRows > 0" class="selected-rows">
                    <div>
                        {{ $MISAResource[$languageCode].Selected }}
                        <span>{{ numberSelectedRows }}</span>
                    </div>
                    <MButton
                        @click="unSelectAll"
                        :type="$MISAEnum.ButtonType.Link"
                        label="Bỏ chọn" />
                </div>

                <div v-if="numberSelectedRows" class="selected-rows-action">
                    <!-- Nút Nhân bản -->
                    <MSButton
                        :type="$MISAEnum.Component.Button.Type.SecondaryIcon"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .Clone
                        "
                        :icon="{ style: 'regular', name: 'clone' }" />

                    <!-- Nút Xóa -->
                    <MSButton
                        id="delete-button"
                        :type="$MISAEnum.Component.Button.Type.SecondaryIcon"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .Delete
                        "
                        :icon="{ style: 'regular', name: 'trash-can' }" />

                    <!-- Nút Khác -->
                    <MSButton
                        :type="$MISAEnum.Component.Button.Type.Icon"
                        :icon="{ style: 'regular', name: 'ellipsis' }"
                        :tooltip="
                            $MISAResource[$language].Tooltip.Button.Other
                        " />
                </div>
            </div>

            <div class="toolbar-right">
                <!-- Trường Tìm kiếm -->
                <MTextfield
                    id="search-textfield"
                    placeHolder="Tìm kiếm trong danh sách"
                    :icon="{ style: 'regular', name: 'magnifying-glass' }" />

                <!-- Nút Xuất -->
                <MButton
                    :type="$MISAEnum.Component.Button.Type.Icon"
                    icon="excel-file"
                    :tooltip="$MISAResource[$language].Tooltip.Button.Export" />

                <!-- Nút Tải lại -->
                <MSButton
                    :type="$MISAEnum.Component.Button.Type.Icon"
                    :icon="{ style: 'regular', name: 'arrow-rotate-forward' }"
                    :tooltip="$MISAResource[$language].Tooltip.Button.Reload"
                    @click="loadData" />
            </div>
        </div>

        <!-- Table -->
        <div class="table-wrapper">
            <table>
                <thead>
                    <tr>
                        <th class="text-align-center sticky-column-left">
                            <MSCheckbox
                                v-model="allSelected"
                                @click="selectAll" />
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
                            <MLabel
                                label="Số CCCD"
                                tooltip="Số căn cước công dân" />
                        </th>
                        <th class="text-align-center">Ngày cấp</th>
                        <th class="text-align-left">Nơi cấp</th>
                        <th class="text-align-left">Đơn vị</th>
                        <th class="text-align-left">Chức vụ</th>
                        <th class="text-align-left">Trạng thái</th>
                        <th class="text-align-left">Mã số thuế cá nhân</th>
                        <th class="text-align-right">Lương</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                        v-for="employee in employees"
                        :key="employee.id"
                        @mouseover="employee.showRowAction = true"
                        @mouseout="employee.showRowAction = false"
                        :class="{ 'selected-row': employee.isSelected }">
                        <!-- Checkbox -->
                        <td class="text-align-center sticky-column-left">
                            <MSCheckbox v-model="employee.isSelected" />
                        </td>

                        <!-- Mã nhân viên -->
                        <td class="text-align-left">
                            {{ employee.EmployeeCode }}
                        </td>

                        <!-- Tên nhân viên -->
                        <td class="text-align-left">{{ employee.FullName }}</td>

                        <!-- Giới tính -->
                        <td class="text-align-left">
                            {{
                                employee.Gender === $MISAEnum.Gender.Female
                                    ? $MISAResource[$languageCode].GenderFemale
                                    : $MISAResource[$languageCode].GenderMale
                            }}
                        </td>

                        <!-- Ngày sinh -->
                        <td class="text-align-center">
                            {{ $helpers.formatDate(employee.DateOfBirth) }}
                        </td>

                        <!-- Số điện thoại -->
                        <td class="text-align-left">
                            {{ employee.PhoneNumber }}
                        </td>

                        <!-- Email -->
                        <td class="text-align-left">{{ employee.Email }}</td>

                        <!-- Địa chỉ -->
                        <td class="text-align-left">{{ employee.Address }}</td>

                        <!-- Số căn cước công dân -->
                        <td class="text-align-left">
                            {{ employee.IdentityNumber }}
                        </td>

                        <!-- Ngày cấp -->
                        <td class="text-align-center">
                            {{ $helpers.formatDate(employee.IdentityDate) }}
                        </td>

                        <!-- Nơi cấp -->
                        <td class="text-align-left">
                            {{ employee.IdentityPlace }}
                        </td>

                        <!-- Đơn vị -->
                        <td class="text-align-left">
                            {{ employee.DepartmentName }}
                        </td>

                        <!-- Chức vụ -->
                        <td class="text-align-left">
                            {{ employee.PositionName }}
                        </td>

                        <!-- Trạng thái -->
                        <td class="text-align-left">
                            {{ employee.WorkStatus }}
                        </td>

                        <!-- Mã số thuế cá nhân -->
                        <td class="text-align-left">
                            {{ employee.PersonalTaxCode }}
                        </td>

                        <!-- Lương -->
                        <td class="text-align-right">{{ employee.Salary }}</td>

                        <div v-show="employee.showRowAction" class="row-action">
                            <!-- Nút Sửa -->
                            <MSButton
                                class="edit-button"
                                :type="$MISAEnum.Component.Button.Type.Icon"
                                :icon="{ style: 'solid', name: 'pen' }"
                                :tooltip="
                                    $MISAResource[$language].Tooltip.Button.Edit
                                " />

                            <!-- Nút Khác -->
                            <MSButton
                                :type="$MISAEnum.Component.Button.Type.Icon"
                                :icon="{ style: 'regular', name: 'ellipsis' }"
                                :tooltip="
                                    $MISAResource[$language].Tooltip.Button
                                        .Other
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
                    <MButton type="title-bar" icon="prev" />
                    <MButton type="title-bar" icon="next" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "../button/MButton.vue";
import MSButton from "../button/MSButton.vue";
import MLabel from "../label/MLabel.vue";
import MTextfield from "../textfield/MTextfield.vue";
import MSCheckbox from "../checkbox/MSCheckbox.vue";
import MSpinner from "../loading/MSpinner.vue";

export default {
    name: "MTable",
    components: {
        MButton,
        MSButton,
        MLabel,
        MTextfield,
        MSCheckbox,
        MSpinner,
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
        loadData() {
            this.showSpinner = true;
            this.$axios
                .get("https://cukcuk.manhnv.net/api/v1/Employees")
                .then((response) => {
                    this.employees = response.data;
                    this.showSpinner = false;
                })
                .catch((error) => {
                    console.error(error);
                    this.showSpinner = false;
                });
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
         *
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        selectAll() {
            if (this.allSelected) {
                this.unSelectAll();
            } else {
                for (let employee of this.employees) employee.isSelected = true;
            }
        },
    },

    data() {
        return {
            employees: [],
            showSpinner: false,
        };
    },

    created() {
        this.loadData();
    },
};
</script>

<style scoped>
.component-wrapper {
    height: 100%;
    width: 100%;
    position: relative;
}

/* Table Toolbar */

.table-toolbar {
    height: 60px;
    width: 100%;
    padding: 12px 16px;
    display: flex;
    justify-content: space-between;
    background-color: #fff;
    border-bottom: 1px solid var(--border-color);
    border-top-left-radius: var(--border-radius);
    border-top-right-radius: var(--border-radius);
}

.toolbar-left,
.toolbar-right {
    display: flex;
    align-items: center;
}

.selected-rows {
    display: flex;
    align-items: center;
}

.selected-rows > *:not(:first-child) {
    margin-left: 16px;
}

.selected-rows .m-button--link {
    color: #e61d1d;
}

.selected-rows span {
    font-family: "Google Sans Bold", sans-serif;
}

.selected-rows-action {
    margin-left: 24px;
    display: flex;
    align-items: center;
}

.selected-rows-action > *:not(:first-child) {
    margin-left: 12px;
}

.selected-rows-action #delete-button i {
    color: red;
}

.toolbar-right > * {
    margin-left: 12px;
}

/* Table */

.table-wrapper {
    height: calc(100% - 48px - 60px);
    width: 100%;
    overflow: auto;
    background-color: #fff;
    position: relative;
}

.table-wrapper::-webkit-scrollbar {
    height: 8px;
    width: 8px;
    border-radius: 4px;
    background-color: var(--secondary-color);
}

.table-wrapper::-webkit-scrollbar-thumb {
    border-radius: 4px;
    background-color: var(--input-placeholder-color);
}

.table-wrapper::-webkit-scrollbar-track {
    border-radius: 4px;
    background-color: var(--secondary-color);
}

table {
    border-collapse: collapse;
    color: var(--text-color);
}

th,
td {
    max-width: 300px;
    padding: 0 16px;
    overflow: hidden;
    border: 1px solid var(--border-color);
    text-align: left;
    text-overflow: ellipsis;
    white-space: nowrap;
}

tr {
    height: var(--table-row-height);
    display: table-row;
}

tr > *:first-child {
    border-left: none;
}

thead tr {
    background-color: #f5f5f5;
    font-family: "Google Sans Medium", sans-serif;
    position: sticky;
    top: 0;
    z-index: 1;
}

thead tr > *:first-child {
    z-index: 2;
}

th {
    overflow: visible;
    border-top: none;
}

th:hover {
    background-color: #eee;
}

tbody tr {
    background-color: #fff;
}

tbody tr:hover {
    background-color: #f2f2f2;
}

.text-align-left {
    text-align: left;
}

.text-align-right {
    text-align: right;
}

.text-align-center {
    text-align: center;
}

.selected-row,
.selected-row:hover {
    background-color: #edf8eb;
}

.row-action {
    height: var(--table-row-height);
    margin-right: 16px;
    display: flex;
    align-items: center;
    position: absolute;
    right: 0;
}

.row-action .m-button {
    height: var(--control-height);
    width: var(--control-height);
    background-color: #fff;
    border: none;
    border-radius: 50%;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.16);
}

.row-action .m-button:first-child {
    font-size: 18px;
}

.row-action .m-button:not(:first-child) {
    margin-left: 8px;
}

.sticky-column-left {
    background-color: inherit;
    position: sticky;
    left: 0;
}

/* Table Paging */

.table-paging {
    height: 48px;
    width: 100%;
    border-bottom-left-radius: 3px;
    border-bottom-right-radius: 3px;
    padding: 0px 12px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    background-color: #f5f5f5;
    position: absolute;
    bottom: 0;
}

.paging-left .number {
    font-family: "Google Sans Bold", sans-serif;
}

.paging-right {
    display: flex;
    align-items: center;
}

.paging-right > * {
    margin-left: 16px;
}

.paging-right .record-per-page {
    display: flex;
    align-items: center;
}

.paging-right .record-per-page select {
    color: var(--color-label);
    padding: 0 8px;
    margin-left: 8px;
}

.paging-button {
    display: flex;
    align-items: center;
}

.paging-button > *:not(:last-child) {
    margin-right: 8px;
}
</style>
