<template>
    <component :is="tag" :class="styleClass" @mouseover="onMouseover" @mouseout="onMouseout">
        <!-- Checkbox -->
        <MCheckbox v-if="isCheckbox" />

        <!-- Thông tin được hiển thị trong cell -->
        {{ content }}

        <!-- Tooltip của cell -->
        <MTooltip v-if="tooltip" v-show="displayTooltip" :content="tooltip" />
    </component>
</template>

<script>
import MCheckbox from '@components/bases/checkbox/MCheckbox.vue';
import MTooltip from '@components/bases/tooltips/MTooltip.vue';

export default {
    name: "MTableCell",

    components: {
        MCheckbox,
        MTooltip,
    },

    props: {
        // Xác định cell là thead hay tbody
        isHeader: {
            type: Boolean,
            default: false,
        },

        // Kiểu dữ liệu của cell
        type: {
            type: String,
            default: "text",
            validator: (value) => {
                return ["checkbox", "text", "date", "number"].includes(value);
            },
        },

        // Nội dung của cell
        content: {
            type: String,
        },

        // Tooltip của cell
        tooltip: {
            type: String,
        },
    },

    data() {
        return {
            displayTooltip: false,
        };
    },

    computed: {
        /**
         * Xác định cell là thead hay tbody.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        tag() {
            return this.isHeader ? "th" : "td";
        },

        /**
         * Kiểm tra xem head có phải là checkbox hay không.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        isCheckbox() {
            return this.type === "checkbox";
        },

        /**
         * Tạo ra các lớp CSS cho cell.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        styleClass() {
            const cellClass = `m-table-cell m-table-cell--${this.type}`;

            if (this.isHeader) {
                return `${cellClass} m-table-th`;
            }

            return `${cellClass} m-table-td`;
        },
    },

    methods: {
        /**
         * Xử lý sự kiện khi rê chuột vào cell.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        onMouseover() {
            this.displayTooltip = true;
        },

        /**
         * Xử lý sự kiện khi rê chuột ra khỏi cell.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        onMouseout() {
            this.displayTooltip = false;
        },
    },
};
</script>

<style scoped>
@import url(table-cell.css);
</style>