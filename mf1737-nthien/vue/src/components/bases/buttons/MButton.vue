<template>
    <button
        ref="button"
        :class="buttonClass"
        @click="onClick"
        @mouseover="onMouseover"
        @mouseout="onMouseout"
    >
        <!-- Icon của nút -->
        <div v-if="icon" :class="iconClass"></div>

        <!-- Nhãn của nút -->
        {{ label }}

        <!-- Tooltip của nút -->
        <MTooltip
            v-if="tooltip"
            v-show="displayTooltip"
            ref="tooltip"
            :content="tooltip"
        />
    </button>
</template>

<script>
import MTooltip from "@components/bases/tooltips/MTooltip.vue";

export default {
    name: "MButton",

    components: {
        MTooltip,
    },

    props: {
        // Loại nút
        type: {
            type: String,
            default: "primary",
            validator: (value) => {
                return [
                    "primary", // Nút chính
                    "primary-icon", // Nút chính có icon
                    "secondary", // Nút phụ
                    "secondary-icon", // Nút phụ có icon
                    "icon", // Nút chỉ có icon
                    "icon-rounded", // Nút chỉ có icon tròn
                    "title-bar", // Nút trên thanh tiêu đề (Minimize, Maximize, Close)
                    "link", // Nút dạng link
                ].includes(value);
            },
        },

        // Nhãn của nút
        label: String,

        // Icon của nút
        icon: String,

        // Trạng thái disabled của nút
        disabled: Boolean,

        // Tooltip của nút
        tooltip: String,
    },

    data() {
        return {
            displayTooltip: false,
        };
    },

    computed: {
        /**
         * Tạo các lớp CSS cho button.
         *
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        buttonClass() {
            const classes = `m-button m-button--${this.type}`;

            if (this.disabled) {
                return `${classes} m-button--disable`;
            }
            return classes;
        },

        /**
         * Tạo các lớp CSS cho icon.
         *
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        iconClass() {
            return `icon icon-${this.icon}`;
        },
    },

    methods: {
        /**
         * Focus vào nút.
         *
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        focus() {
            this.$refs.button.focus();
        },

        /**
         * Xử lý sự kiện khi nhấn nút.
         *
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        onClick() {
            if (this.disabled) {
                return;
            }
            this.$emit("clickButton");
        },

        /**
         * Xử lý sự kiện khi di chuột vào nút.
         *
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        onMouseover() {
            this.displayTooltip = true;
        },

        /**
         * Xử lý sự kiện khi di chuột ra khỏi nút.
         *
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        onMouseout() {
            this.displayTooltip = false;
        },
    },
};
</script>

<style scoped>
@import url("./button.css");
</style>
