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
.m-toast-wrapper {
    height: fit-content;
    width: fit-content;
    padding: 24px;
    display: flex;
    flex-direction: column-reverse;
    align-items: flex-end;
    background-color: transparent;
    position: absolute;
    bottom: 0;
    right: 0;
    z-index: 4;
}

.m-toast-wrapper > *:not(:first-child) {
    margin-bottom: 8px;
}

.m-toast {
    height: 56px;
    width: 450px;
    padding: 0 16px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    background-color: #fff;
    border-radius: var(--border-radius);
    box-shadow: 0 4px 40px rgba(0, 0, 0, 0.078);
}

/* Color of toast message's label */
.m-toast__label--successful {
    color: var(--primary-color);
}

.m-toast__label--error {
    color: var(--danger-color);
}

.m-toast__label--warning {
    color: var(--warning-color);
}

.m-toast__label--information {
    color: var(--information-color);
}

/* Toast message's left-side */
.m-toast-left {
    display: flex;
    align-items: center;
}

.m-toast__label {
    margin-left: 8px;
    font-family: "Google Sans Bold", san-serif;
}

.m-toast .icon {
    height: 24px;
    width: 24px;
}

.m-toast__message {
    margin-left: 6px;
    color: var(--text-color);
}

/* Toast message's right-side */
.m-toast-right {
    display: flex;
    align-items: center;
}

.m-toast__link {
    margin-right: 16px;
    font-family: "Google Sans Medium", san-serif;
    text-decoration: underline;
}
</style>
