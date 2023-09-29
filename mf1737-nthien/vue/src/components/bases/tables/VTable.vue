<template>
    <div class="m-table-container">
        <!-- Table -->
        <table class="m-table">
            <thead>
                <tr>
                    <MTableCell v-for="item in headers" is-header :type="item.type" :content="item.title"
                        :tooltip="item.tooltip" />
                </tr>
            </thead>
            <tbody>
                <MTableRow v-for="item in data" :headers="headers" :data="item" />
            </tbody>
        </table>
    </div>
</template>

<script>
import MTableCell from '@/components/bases/tables/MTableCell.vue';
import MTableRow from '@/components/bases/tables/MTableRow.vue';

export default {
    name: 'VTable',

    components: {
        MTableCell,
        MTableRow,
    },

    data() {
        return {
            headers: [
                {
                    name: "checkbox",
                    type: "checkbox",
                },
                {
                    title: "Mã nhân viên",
                    name: "employeeCode",
                    type: "text",
                },
                {
                    title: "Tên nhân viên",
                    name: "fullName",
                    type: "text",
                },
                {
                    title: "Giới tính",
                    name: "genderName",
                    type: "text",
                },
                {
                    title: "Ngày sinh",
                    name: "dateOfBirth",
                    type: "date",
                },
                {
                    title: "Số CCCD",
                    tooltip: "Số căn cước công dân",
                    type: "text",
                    name: "identityNumber",
                },
                {
                    title: "Chức vụ",
                    name: "positionName",
                    type: "text",
                },
                {
                    title: "Phòng ban",
                    name: "departmentName",
                    type: "text",
                },
                {
                    title: "Số tài khoản",
                    name: "bankAccount",
                    type: "text",
                },
                {
                    title: "Tên ngân hàng",
                    name: "bankName",
                    type: "text",
                },
                {
                    title: "Chi nhánh",
                    name: "bankBranch",
                    type: "text",
                },
                {
                    title: "Mức lương",
                    name: "salary",
                    type: "number",
                },
            ],

            data: [
                {
                    "employeeId": "78b1d514-338c-3cdc-8641-c1ec321fe728",
                    "employeeCode": "NV-403099",
                    "fullName": "Dewey Morrell",
                    "gender": 2,
                    "genderName": "Khác",
                    "dateOfBirth": "1953-01-08T00:00:00",
                    "phoneNumber": "(437) 389-4694",
                    "landlineNumber": "(741) 863-7414",
                    "email": "AllardG@outlook.com",
                    "address": "2942 Red Lake Highway, Trenton, NJ, 66784",
                    "identityNumber": "409989287523",
                    "identityDate": "1970-01-08T00:00:00",
                    "identityPlace": "42 Quailwood Circle, Atlanta, Georgia, 35891",
                    "positionName": "Giám đốc",
                    "departmentName": "Phòng kinh doanh",
                    "bankAccount": "49133160",
                    "bankName": "Agribank",
                    "bankBranch": "Tp. Hồ Chí Minh",
                    "departmentId": "768f8e64-7d10-20c9-967d-e8c757976496",
                    "createdDate": "1970-01-01T00:00:08",
                    "createdBy": "Quentin Abraham",
                    "modifiedDate": "1983-05-02T12:41:08",
                    "modifiedBy": "Michel Rose",
                    "salary": 10000000,
                },
            ],
        };
    },
};
</script>

<style scoped>
.m-table-container {
    position: relative;
    height: 100%;
    width: 100%;
    overflow: auto;
    background-color: #fff;
}

.m-table-container::-webkit-scrollbar {
    height: 8px;
    width: 8px;
    border-radius: 4px;
    background-color: var(--secondary-color);
}

.m-table-container::-webkit-scrollbar-thumb {
    border-radius: 4px;
    background-color: var(--input-placeholder-color);
}

.m-table-container::-webkit-scrollbar-track {
    border-radius: 4px;
    background-color: var(--secondary-color);
}

.m-table {
    border-collapse: collapse;
    color: var(--default-font-color);
}

thead tr {
    height: var(--table-row-height);
    background-color: #f5f5f5;
    font-family: var(--default-font-family);
    font-weight: 600;
    position: sticky;
    top: 0;
    z-index: 1;
}

thead tr>*:first-child {
    z-index: 2;
}
</style>