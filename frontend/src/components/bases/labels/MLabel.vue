<template>
    <label
        class="m-label"
        :for="id"
        @click="onClick"
        @mouseover="onShowTooltip"
        @mouseout="onHideTooltip"
    >
        {{ label }}

        <!-- Dấu sao của các trường bắt buộc -->
        <span v-if="required" class="required-star"> *</span>

        <!-- Tooltip -->
        <MTooltip v-if="tooltip" v-show="showTooltip" :content="tooltip" />
    </label>
</template>

<script>
import MTooltip from "../tooltips/MTooltip.vue";

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
         * Xử lý sự kiện click vào nhãn.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        onClick() {
            this.$emit("clickLabel");
        },
        
        /**
         * Hiển thị tooltip.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        onShowTooltip() {
            this.showTooltip = true;
        },

        /**
         * Ẩn tooltip.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        onHideTooltip() {
            this.showTooltip = false;
        },
    },
};
</script>

<style scoped>
@import url(label.css);
</style>
