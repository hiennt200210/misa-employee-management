<template>
    <div class="dialog-container">
        <div class="m-dialog">
            <div class="dialog-header">
                <!-- Tiêu đề của Dialog -->
                <MHeading type="h2" :title="title" />

                <!-- Nút Close -->
                <MButton
                    :type="$enums.Button.TitleBar"
                    icon="close"
                    @clickButton="onClickCloseButton"
                />
            </div>

            <!-- Nội dung của Dialog -->
            <div class="dialog-content">
                <!-- Biểu tượng thông báo -->
                <div :class="iconClass"></div>

                <!-- Nội dung thông báo -->
                <div class="notification">
                    <ul v-if="content.length > 1">
                        <li v-for="item in content">{{ `${item}.` }}</li>
                    </ul>
                    <p v-else>{{ `${content[0]}.` }}</p>
                </div>
            </div>

            <div class="dialog-action">
                <slot></slot>
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "../buttons/MButton.vue";
import MHeading from "../headings/MHeading.vue";

export default {
    name: "MDialog",
    components: {
        MButton,
        MHeading,
    },

    props: {
        type: {
            type: String,
            default: "error",
            validator: (value) => {
                return ["success", "error", "warning", "info"].includes(value);
            },
        },

        title: {
            type: String,
            default: "Thông báo",
        },

        content: {
            type: Array,
            default: [],
        },
    },

    computed: {
        /**
         * Lấy icon của Dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        icon() {
            switch (this.type) {
                case this.$enums.Dialog.Success:
                    return "check_circle";
                case this.$enums.Dialog.Error:
                    return "cancel";
                case this.$enums.Dialog.Warning:
                    return "error";
                case this.$enums.Dialog.Info:
                    return "info";
                default:
                    return "error";
            }
        },

        /**
         * Lấy class của icon.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        iconClass() {
            return `icon icon-${this.type}`;
        },
    },

    methods: {
        /**
         * Xử lý sự kiện click nút Close.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickCloseButton() {
            this.$emit("closeDialog");
        },
    },
};
</script>

<style>
.dialog-action .m-button {
    margin-left: 8px;
}
</style>

<style scoped>
@import url(dialog.css);
</style>
