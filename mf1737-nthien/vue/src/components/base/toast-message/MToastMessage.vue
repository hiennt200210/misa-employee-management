<template>
    <div class="m-toast-wrapper">
        <div v-for="toast in toasts" :key="toast.id" class="m-toast">
            <div class="m-toast-left">
                <div :class="`icon icon-${toast.type}`"></div>
                <label :class="`m-toast__label m-toast__label--${toast.type}`">
                    {{ toastLabel(toast) }}
                </label>
                <p class="m-toast__message">{{ toast.message }}</p>
            </div>
            <div class="m-toast-right">
                <a v-if="toast.link" class="m-toast__link">{{ toast.link }}</a>
                <MButton @click="toast.close()" type="title-bar" icon="close" />
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "../button/MButton.vue";

export default {
    name: "MToastMessage",
    components: {
        MButton,
    },
    // props: ["type", "message", "link", "action", "close"],
    props: ["toasts"],
    computed: {
        /**
         * Xác định nhãn phù hợp với loại toast message.
         * Author: hiennt200210 (23/08/2023)
         */
        // toastLabel(toast) {
        //     return this.allLabel[toast.type];
        // },
        // showToastMessage() {
        //     let show = [];
        //     this.toasts.forEach(() => {
        //         show.append(true);
        //     });
        //     return show;
        // },
    },
    methods: {
        /**
         * Hiển thị toast mesage.
         * Author: hiennt200210 (23/08/2023)
         */
        onShowToastMessage() {
            this.showToastMessage = true;
        },

        /**
         * Đóng toast mesage.
         * Author: hiennt200210 (23/08/2023)
         */
        closeToastMessage() {
            this.toast.close();
        },

        /**
         * Xác định nhãn phù hợp với loại toast message.
         * Author: hiennt200210 (23/08/2023)
         */
        toastLabel(toast) {
            return this.allLabel[toast.type];
        },
    },
    data() {
        return {
            allLabel: {
                successful: this.$MISAResource[this.$languageCode].Successful,
                error: this.$MISAResource[this.$languageCode].Error,
                warning: this.$MISAResource[this.$languageCode].Warning,
                information: this.$MISAResource[this.$languageCode].Information,
            },
        };
    },
};
</script>

<style scoped>
@import url("./toast-message.css");
</style>
