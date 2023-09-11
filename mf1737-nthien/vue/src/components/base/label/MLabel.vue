<template>
    <label
        class="m-label"
        :for="id"
        @mouseover="onShowTooltip"
        @mouseout="onHideTooltip">
        {{ label }}

        <!-- Dấu sao của các trường bắt buộc -->
        <span v-if="required" class="required-star"> *</span>

        <!-- Tooltip -->
        <MTooltip v-if="tooltip" v-show="showTooltip" :content="tooltip" />
    </label>
</template>

<script>
import MTooltip from "../tooltip/MTooltip.vue";

export default {
    name: "MLabel",

    components: {
        MTooltip,
    },

    props: {
        id: String,
        label: String,
        required: Boolean,
        tooltip: String,
    },

    data() {
        return {
            showTooltip: false,
        };
    },

    methods: {
        /**
         * Hiển thị tooltip.
         * CreatedBy: hiennt200210 (2023/09/07)
         */
        onShowTooltip() {
            this.showTooltip = true;
        },

        /**
         * Ẩn tooltip.
         * CreatedBy: hiennt200210 (2023/09/07)
         */
        onHideTooltip() {
            this.showTooltip = false;
        },
    },
};
</script>

<style scoped>
.m-label {
    position: relative;
    font-family: var(--default-font-family);
    font-size: var(--default-font-size);
    font-weight: 500;
}

.required-star {
    color: #ff0000;
}
</style>
