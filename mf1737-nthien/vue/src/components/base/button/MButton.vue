<template>
    <button
        :class="buttonClass"
        @click="onClick"
        @mouseover="onMouseover"
        @mouseout="onMouseout"
    >
        <!-- Icon của nút -->
        <span v-if="icon" :class="iconClass">
            {{ icon }}
        </span>

        <!-- Icon từ sprites -->
        <div v-if="iconSprites" :class="iconSpritesClass"></div>

        <!-- Nhãn của nút -->
        {{ label }}

        <!-- Tooltip của nút -->
        <MTooltip v-if="tooltip" ref="tooltip" :content="tooltip" />
    </button>
</template>

<script>
import MTooltip from "../tooltip/MTooltip.vue";

export default {
    name: "MButton",
    components: {
        MTooltip,
    },

    props: {
        // Loại nút.
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
         */
        onClick() {
            this.$emit("click");
        },

        /**
         * Xử lý sự kiện mouseover.
         */
        onMouseover() {
            if (this.tooltip) {
                this.$refs.tooltip.show();
            }
        },

        /**
         * Xử lý sự kiện mouseout.
         */
        onMouseout() {
            if (this.tooltip) {
                this.$refs.tooltip.hide();
            }
        },
    },
};
</script>

<style scoped>
@import url("./button.css");
</style>
