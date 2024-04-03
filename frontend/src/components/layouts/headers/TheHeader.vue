<template>
    <div class="header">
        <div class="header-left">
            <!-- Menu button -->
            <MButton
                :type="$enums.Button.IconRounded"
                icon="menu"
                tooltip="Menu"
            />

            <!-- Logo -->
            <a class="logo"></a>
        </div>

        <div class="header-right">
            <!-- Nút Thông báo -->
            <MButton
                :type="$enums.Button.IconRounded"
                icon="notifications"
                :tooltip="$resx.Notifications"
            />

            <!-- Nút Thông tin người dùng -->
            <div class="user">Nguyễn Thế Hiển</div>
        </div>
    </div>
</template>

<script>
import MButton from "@components/bases/buttons/MButton.vue";

export default {
    name: "TheHeader",
    components: {
        MButton,
    },
};
</script>

<style scoped>
@import url(header.css);
</style>
