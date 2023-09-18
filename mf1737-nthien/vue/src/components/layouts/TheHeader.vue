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

.m-dropdownlist select {
    height: 36px;
    border: 1px solid #e0e1e4;
    border-radius: 4px;
    min-width: 30px;
    background-color: #fff;
    outline: none;
    padding: 0 16px;
    color: var(--input-placeholder-color);
    font-family: "Google Sans Regular", sans-serif;
    font-size: 14px;
}

.m-dropdownlist select:hover {
    background-color: var(--secondary-color--hover);
    cursor: pointer;
}

.m-dropdownlist select:focus {
    background-color: var(--secondary-color);
    border: 1px solid var(--primary-color);
    outline: none;
}

.m-dropdownlist select:active {
    background-color: var(--bgcolor-button-secondary-pressed);
}

</style>
