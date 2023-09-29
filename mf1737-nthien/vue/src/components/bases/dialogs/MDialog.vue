<template>
    <div class="dialog-container">
        <div class="m-dialog">
            <div class="dialog-header">
                <!-- Tiêu đề của Dialog -->
                <MHeading type="h2" :title="title" />

                <!-- Nút Close -->
                <MButton :type="$enums.Button.TitleBar" icon="close" @clickButton="onClickCloseButton" />
            </div>

            <!-- Nội dung của Dialog -->
            <div class="dialog-content">
                <!-- Biểu tượng thông báo -->
                <span :class="`icon-${icon} material-symbols-rounded`">
                    {{ icon }}
                </span>

                <!-- Nội dung thông báo -->
                <div class="content-text">
                    {{ content }}
                </div>
            </div>

            <div class="dialog-action">
                <div class="dialog-action__secondary-button">
                    <slot name="secondary"></slot>
                </div>
                <div class="dialog-action__primary-button">
                    <slot name="primary"></slot>
                </div>
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
            type: String,
            default: "",
        },
    },

    computed: {
        /**
         * Lấy icon của Dialog.
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        icon() {
            switch (this.type) {
                case this.$enums.DialogType.Success:
                    return "check_circle";
                case this.$enums.DialogType.Error:
                    return "cancel";
                case this.$enums.DialogType.Warning:
                    return "error";
                case this.$enums.DialogType.Info:
                    return "info";
                default:
                    return "error";
            }
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

<style scoped>
@import url(dialog.css);
</style>
