<template>
    <button :class="buttonClass" @click="onClick" @mouseover="onMouseover" @mouseout="onMouseout">
        <!-- Icon của nút -->
        <span v-if="icon" :class="iconClass">
            {{ icon }}
        </span>

        <!-- Icon của nút được lấy từ file sprites.svg -->
        <div v-if="iconSprites" :class="iconSpritesClass"></div>

        <!-- Nhãn của nút -->
        {{ label }}

        <!-- Tooltip của nút -->
        <MTooltip v-if="tooltip" v-show="displayTooltip" ref="tooltip" :content="tooltip" />
    </button>
</template>

<script>
import MTooltip from "../tooltips/MTooltip.vue";

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
                    "primary",
                    "primary-icon",
                    "secondary",
                    "secondary-icon",
                    "icon",
                    "icon-rounded",
                    "title-bar",
                    "link",
                ].includes(value);
            },
        },
        // Nhãn của nút
        label: {
            type: String,
        },

        // Icon của nút
        icon: {
            type: String,
        },

        // Icon lấy từ sprites
        iconSprites: {
            type: String,
        },

        // Trạng thái disabled của nút
        disabled: {
            type: Boolean,
        },

        // Tooltip của nút
        tooltip: {
            type: String,
        },
    },

    data() {
        return {
            // Trạng thái hiển thị của tooltip.
            displayTooltip: false,
        };
    },

    computed: {
        /**
         * Tạo các lớp CSS cho button.
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
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        iconClass() {
            return "m-button__icon material-symbols-rounded";
        },

        /**
         * Tạo các lớp CSS cho icon lấy từ sprites.
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        iconSpritesClass() {
            return `icon icon-${this.iconSprites}`;
        },
    },

    methods: {
        /**
         * Xử lý sự kiện click.
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        onClick() {
            this.$emit("clickButton");
        },

        /**
         * Xử lý sự kiện mouseover.
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        onMouseover() {
            this.displayTooltip = true;
        },

        /**
         * Xử lý sự kiện mouseout.
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
