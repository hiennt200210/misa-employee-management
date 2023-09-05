<template>
    <div class="dialog-container">
        <div class="m-dialog">
            <div class="dialog-header">
                <!-- Tiêu đề Dialog -->
                <MHeading type="h2" :title="title" />
                <!-- Nút Close -->
                <MButton
                    @click="closeDialog()"
                    :type="$MISAEnum.ButtonType.TitleBar"
                    icon="close" />
            </div>

            <!-- Mô tả chi tiết của Dialog -->
            <div class="dialog-description">
                <div :class="`icon icon-${type}`"></div>
                {{ description }}
            </div>

            <div class="dialog-action">
                <slot></slot>
            </div>
        </div>
    </div>
</template>

<script>
import MButton from "../button/MButton.vue";
import MHeading from "../heading/MHeading.vue";

export default {
    name: "MDialog",
    components: {
        MButton,
        MHeading,
    },
    props: ["type", "title", "description", "closeDialog"],
};
</script>

<style scoped>
.dialog-container {
    background-color: rgba(0, 0, 0, 0.5);
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 4;
}

.m-dialog {
    background-color: var(--secondary-color);
    border-radius: var(--border-radius);
    height: fit-content;
    width: 400px;
    padding: 24px;
    position: relative;
    bottom: 3%;
}

.m-dialog > *:not(:first-child) {
    margin-top: 24px;
}

.dialog-header {
    height: 24px;
    display: flex;
    align-items: center;
    justify-content: space-between;
}

.dialog-description {
    display: flex;
    align-items: center;
}

.dialog-description .icon {
    height: 24px;
    width: 24px;
    margin-right: 8px;
}

.dialog-action {
    display: flex;
    justify-content: flex-end;
}

.dialog-action > *:not(:first-child) {
    margin-left: 8px;
}
</style>
