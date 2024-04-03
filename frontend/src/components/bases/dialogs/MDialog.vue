<template>
    <div class="dialog-container">
        <div class="dialog">
            <div class="dialog-header">
                <!-- Tiêu đề của Dialog -->
                <MHeading type="h2" :title="title" />

                <!-- Nút Close -->
                <MButton
                    :type="$enums.Button.TitleBar"
                    icon="close"
                    :tooltip="$resx.Close"
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
                        <li v-for="item in content">{{ `${item}` }}</li>
                    </ul>
                    <p v-else>{{ `${content[0]}` }}</p>
                </div>
            </div>

            <div class="dialog-action">
                <!-- Các nút thực hiện hành động phụ -->
                <slot></slot>

                <!-- Nút thực hiện hành động chính -->
                <MButton
                    ref="primary-button"
                    :type="$enums.Button.Primary"
                    :label="this.primaryButtonLabel"
                    @clickButton="onClickPrimaryButton"
                />
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "@components/bases/buttons/MButton.vue";
import MHeading from "@components/bases/headings/MHeading.vue";

export default {
    name: "MDialog",

    components: {
        MButton,
        MHeading,
    },

    props: {
        // Loại dialog
        type: {
            type: String,
            default: "error",
            validator: (value) => {
                return [
                    "success", // Thành công
                    "error", // Lỗi
                    "warning", // Cảnh báo
                    "info", // Thông tin
                ].includes(value);
            },
        },

        // Tiêu đề của dialog
        title: String,

        // Nội dung thông báo của dialog
        content: Array,

        // Nhãn của nút thực hiện hành động chính
        primaryButtonLabel: String,
    },

    computed: {
        /**
         * Lấy class của icon.
         *
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        iconClass() {
            return `icon icon-${this.type}`;
        },
    },

    methods: {
        /**
         * Xử lý sự kiện click nút Close.
         *
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickCloseButton() {
            this.$emit("clickCloseButton");
        },

        /**
         * Xử lý sự kiện click nút thực hiện hành động chính.
         *
         * CreatedBy: hiennt200210 (20/08/2023)
         */
        onClickPrimaryButton() {
            this.$emit("clickPrimaryButton");
        },
    },

    mounted() {
        // Focus vào nút thực hiện hành động chính khi dialog được hiển thị
        this.$refs["primary-button"].focus();
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
