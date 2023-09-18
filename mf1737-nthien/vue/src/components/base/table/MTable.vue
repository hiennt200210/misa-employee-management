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
                        label="Bỏ chọn"
                    />
                </div>

                <div v-if="numberSelectedRows" class="selected-rows-action">
                    <!-- Nút Nhân bản -->
                    <MButton
                        :type="$MISAEnum.Component.Button.Type.SecondaryIcon"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .Clone
                        "
                        icon="content_copy"
                    />

                    <!-- Nút Xóa -->
                    <MButton
                        id="delete-button"
                        :type="$MISAEnum.Component.Button.Type.SecondaryIcon"
                        :label="
                            $MISAResource[$language].Component.Button.Label
                                .Delete
                        "
                        icon="delete"
                        @click="onClickDeleteButton"
                    />

                    <!-- Nút Khác -->
                    <MButton
                        :type="$MISAEnum.Component.Button.Type.Icon"
                        icon="more_horiz"
                        :tooltip="$MISAResource[$language].Tooltip.Button.Other"
                    />
                </div>
            </div>

            <div class="toolbar-right">
                <!-- Trường Tìm kiếm -->
                <MTextfield
                    id="search-textfield"
                    placeHolder="Tìm kiếm trong danh sách"
                    :icon="{ style: 'regular', name: 'magnifying-glass' }"
                />

                <!-- Nút Xuất -->
                <MButton
                    :type="$MISAEnum.Component.Button.Type.Icon"
                    icon-sprites="excel-file"
                    :tooltip="$MISAResource[$language].Tooltip.Button.Export"
                />
                <!-- Nút Tải lại -->
                <MButton
                    :type="$MISAEnum.Component.Button.Type.Icon"
                    icon="refresh"
                    :tooltip="$MISAResource[$language].Tooltip.Button.Reload"
                    @click="loadData"
                />
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
                                @click="selectAll"
                            />
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
                                tooltip="Số căn cước công dân"
                            />
                        </th>
                        <th class="text-align-center">Ngày cấp</th>
                        <th class="text-align-left">Nơi cấp</th>
                        <th class="text-align-left">Phòng ban</th>
                        <th class="text-align-left">Chức vụ</th>
                        <th class="text-align-left">Tài khoản ngân hàng</th>
                        <th class="text-align-left">Tên ngân hàng</th>
                        <th class="text-align-left">Chi nhánh ngân hàng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                        v-for="employee in employees"
                        :key="employee.EmployeeId"
                        @mouseover="employee.showRowAction = true"
                        @mouseout="employee.showRowAction = false"
                        :class="{ 'selected-row': employee.isSelected }"
                    >
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

                        <!-- Tài khoản ngân hàng -->
                        <td class="text-align-left">
                            {{ employee.BankAccount }}
                        </td>

                        <!-- Tên ngân hàng -->
                        <td class="text-align-left">
                            {{ employee.BankName }}
                        </td>

                        <!-- Chi nhánh ngân hàng -->
                        <td class="text-align-left">
                            {{ employee.BankBranch }}
                        </td>

                        <div v-show="employee.showRowAction" class="row-action">
                            <!-- Nút Sửa -->
                            <MButton
                                class="edit-button"
                                :type="$MISAEnum.Component.Button.Type.Icon"
                                icon="edit"
                                :tooltip="
                                    $MISAResource[$language].Tooltip.Button.Edit
                                "
                            />

                            <!-- Nút Khác -->
                            <MButton
                                :type="$MISAEnum.Component.Button.Type.Icon"
                                icon="more_horiz"
                                :tooltip="
                                    $MISAResource[$language].Tooltip.Button
                                        .Other
                                "
                            />
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
import MButton from "../button/MButton.vue";
import MLabel from "../label/MLabel.vue";
import MTextfield from "../textfield/MTextfield.vue";
import MSCheckbox from "../checkbox/MSCheckbox.vue";
import MSpinner from "../loading/MSpinner.vue";

export default {
    name: "MTable",
    components: {
        MButton,
        MLabel,
        MTextfield,
        MSCheckbox,
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
        loadData() {
            this.showSpinner = true;
            this.$axios
                .get(`${this.$api.BaseUrl}${this.$api.GetAllEmployees}`)
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

        /**
         * Xử lý khi nhấn nút Xóa.
         * CreatedBy: hiennt200210 (01/09/2023)
         */
        onClickDeleteButton() {
            console.log("Delete");
        },
    },

    created() {
        this.loadData();
    },
};
</script>

<style scoped>
@import url("./table.css");
</style>
