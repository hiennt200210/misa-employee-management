<template>
    <div class="toast-wrapper">
        <div class="toast">
            <!-- Left -->
            <div class="toast-left">
                <!-- Icon -->
                <div :class="iconClass"></div>

                <!-- Label -->
                <label :class="labelClass">
                    {{ label }}
                </label>

                <!-- Message -->
                <p class="toast__message">{{ message }}</p>
            </div>

            <!-- Right -->
            <div class="toast-right">
                <!-- Link -->
                <a v-if="link" class="toast__link">{{ link }}</a>

                <!-- Close button -->
                <MButton
                    :type="$enums.Button.TitleBar"
                    icon="close"
                    @clickButton="onClickCloseButton"
                />
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "@components/bases/buttons/MButton.vue";

export default {
    name: "MToast",

    components: {
        MButton,
    },

    props: {
        type: {
            type: String,
            default: "success",
            validator: (value) => {
                return ["success", "error", "warning", "info"].includes(value);
            },
        },
        message: {
            type: String,
            default: "",
        },
        link: {
            type: String,
            default: "",
        },
    },

    computed: {
        /**
         * Lấy class của icon.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        iconClass() {
            return `icon icon-${this.type}-small`;
        },

        /**
         * Lấy class của label.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        labelClass() {
            return `toast__label toast__label--${this.type}`;
        },

        /**
         * Lấy nhãn của toast message.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        label() {
            switch (this.type) {
                case this.$enums.Toast.Success:
                    return this.$resxLang.Label.Success;
                case this.$enums.Toast.Error:
                    return this.$resxLang.Label.Error;
                case this.$enums.Toast.Warning:
                    return this.$resxLang.Label.Warning;
                case this.$enums.Toast.Info:
                    return this.$resxLang.Label.Info;
                default:
                    return this.$resxLang.Label.Error;
            }
        },
    },

    methods: {
        /**
         * Xử lý sự kiện click nút Close.
         * Author: hiennt200210 (23/08/2023)
         */
        onClickCloseButton() {
            this.$emit("closeToastMessage");
        },
    },
};
</script>

<style scoped>
@import url(toast.css);
</style>
