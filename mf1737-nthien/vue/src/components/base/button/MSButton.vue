<template>
    <button
        :class="styleClass"
        @mouseover="showTooltip = true"
        @mouseout="showTooltip = false">
        
        <i v-if="icon" :class="iconClass"></i>

        {{ label }}

        <MTooltip
            v-if="tooltip"
            v-show="showTooltip"
            :content="tooltip" />
    </button>
</template>

<script>
import MTooltip from "../tooltip/MTooltip.vue";

export default {
    name: "MSButton",
    components: {
        MTooltip,
    },

    props: {
        type: String,
        label: String,
        icon: Object,
        tooltip: String,
        disable: Boolean,
    },

    computed: {
        /**
         * Xác định các style class phù hợp cho mỗi loại nút.
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        styleClass() {
            const classes = `m-button m-button--${this.type}`;
            if (this.disable) return `${classes} m-button--disable`;
            return classes;
        },

        /**
         * Xác định các class phù hợp cho icon trên nút.
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        iconClass() {
            return `fa-${this.icon.style} fa-${this.icon.name}`;
        },
    },

    data() {
        return {
            showTooltip: false,
        };
    },
};
</script>

<style scoped>
.m-button {
    position: relative;
    height: var(--control-height);
    border-radius: var(--border-radius);
    font-family: "Google Sans Medium", sans-serif;
    font-size: 14px;
    cursor: pointer;
}

/* Primary buttons */

.m-button--primary {
    min-width: 80px;
    padding: 0 16px;
    background-color: var(--primary-color);
    border: none;
    color: #fff;
}

.m-button--primary:hover {
    background-color: var(--primary-color--hover);
}

.m-button--primary:active {
    background-color: var(--primary-color--active);
}

/* Primary button with icon */

.m-button--primary-icon {
    display: flex;
    align-items: center;
    min-width: 80px;
    padding-left: 12px;
    padding-right: 16px;
    background-color: var(--primary-color);
    border: none;
    color: #fff;
}

.m-button--primary-icon:hover {
    background-color: var(--primary-color--hover);
}

.m-button--primary-icon:active {
    background-color: var(--primary-color--active);
}

.m-button--primary-icon i {
    height: 16px;
    width: 16px;
    margin-right: 4px;
    font-size: 16px;
}

/* Secondary buttons */

.m-button--secondary {
    min-width: 80px;
    padding: 0 16px;
    background-color: var(--secondary-color);
    border: 1px solid var(--border-color);
    color: var(--text-color);
}

.m-button--secondary:hover {
    background-color: var(--secondary-color--hover);
}

.m-button--secondary:active {
    background-color: var(--secondary-color--active);
}

/* Secondary with icon buttons */

.m-button--secondary-icon {
    display: flex;
    align-items: center;
    min-width: 80px;
    padding-left: 12px;
    padding-right: 16px;
    background-color: var(--secondary-color);
    border: 1px solid var(--border-color);
    color: var(--text-color);
}

.m-button--secondary-icon:hover {
    background-color: var(--secondary-color--hover);
}

.m-button--secondary-icon:active {
    background-color: var(--secondary-color--active);
}

.m-button--secondary-icon i {
    height: 16px;
    width: 16px;
    margin-right: 4px;
    color: var(--icon-color);
    font-size: 16px;
}

/* Icon buttons */

.m-button--icon {
    display: flex;
    align-items: center;
    justify-content: center;
    min-width: var(--control-height);
    background-color: var(--secondary-color);
    border: 1px solid var(--border-color);
    color: var(--icon-color);
    font-size: 24px;
}

.m-button--icon:hover {
    background-color: var(--secondary-color--hover);
}

.m-button--icon:active {
    background-color: var(--secondary-color--active);
}

.m-button--icon .icon {
    width: 24px;
    height: 24px;
}

/* Title-bar buttons */
.m-button--title-bar {
    height: 24px;
    width: 24px;
    background-color: transparent;
    border: none;
    border-radius: 50%;
}

.m-button--title-bar:hover {
    background-color: var(--secondary-color--hover);
}

.m-button--title-bar:active {
    background-color: var(--secondary-color--active);
}

.m-button--title-bar .icon {
    height: 24px;
    width: 24px;
}

/* Link buttons */

.m-button--link {
    background-color: transparent;
    border: none;
    color: var(--primary-color);
}

.m-button--link:hover {
    color: var(--primary-color--hover);
}

.m-button--link:active {
    color: var(--primary-color--active);
}

/* Disabled buttons */

.m-button--disable {
    opacity: var(--disable-opacity);
}
</style>
