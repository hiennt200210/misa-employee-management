<template>
    <div class="header">
        <div class="header-left">
            <!-- Menu button -->
            <MButton
                :type="$MISAEnum.Component.Button.Type.Icon"
                icon="menu" />

            <!-- Logo -->
            <a class="logo"></a>
        </div>

        <div class="header-right">
            <div class="m-dropdownlist">
                <select @change="onChangeLanguage" ref="changeLanguage">
                    <option value="VN">Tiếng Việt</option>
                    <option value="EN">English</option>
                </select>
            </div>

            <!-- User's Avatar -->
            <div
                class="m-avatar m-button m-button-icon m-button-icon-small"
                title="Nguyễn Thế Hiển"></div>
        </div>
    </div>
</template>

<script>
import app from "../../main";
import MButton from "../base/button/MButton.vue";

export default {
    name: "TheHeader",
    components: {
        MButton,
    },

    methods: {
        /**
         * Thay đổi ngôn ngữ giao diện.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        onChangeLanguage() {
            const value = this.$refs["changeLanguage"].value;
            app.config.globalProperties.$languageCode = value;
        },
    },
};
</script>

<style scoped>
@import url("../../styles/components/dropdownlist.css");

.header {
    background-color: #fff;
    display: flex;
    height: var(--header-height);
    width: 100%;
}

.header-left {
    align-items: center;
    display: flex;
    height: 100%;
    padding-left: 16px;
    width: var(--sidebar-width);
}

.header-left .m-button {
    border: none;
    border-radius: 50%;
}

.header-left .logo {
    background-image: url(../../assets/img/logo.svg);
    background-position: center;
    background-repeat: no-repeat;
    background-size: 103px;
    height: 36px;
    margin-left: 10px;
    width: 100px;
}

.header-right {
    align-items: center;
    display: flex;
    height: 100%;
    justify-content: flex-end;
    padding: 0 24px;
    width: calc(100% - var(--sidebar-width));
}

.header-right .m-avatar {
    height: 36px;
    width: 36px;
    margin-left: 16px;
    background-color: lightcoral;
    border: none;
    border-radius: 50%;
}
</style>
