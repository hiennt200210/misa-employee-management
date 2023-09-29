<template>
    <div :class="styleClass">
        {{ content }}
    </div>
</template>

<script>
export default {
    name: "MTooltip",

    props: {
        // Hướng hiển thị tooltip.
        align: {
            type: String,
            default: "bottom",
            validator: (value) => {
                return ["top", "bottom", "left", "right"].includes(value);
            },
        },

        // Nội dung của tooltip.
        content: {
            type: String,
        },
    },

    computed: {
        /**
         * Tạo ra các lớp CSS cho tooltip.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        styleClass() {
            return `m-tooltip m-tooltip--align-${this.align}`;
        },
    },
};
</script>

<style scoped>
@import url(./tooltip.css);
</style>
