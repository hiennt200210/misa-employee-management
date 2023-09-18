<template>
    <div v-show="display" :class="classes">
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

    data() {
        return {
            // Trạng thái hiển thị của tooltip.
            display: false,
        };
    },

    computed: {
        /**
         * Tạo ra các lớp CSS cho tooltip.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        classes() {
            return `m-tooltip m-tooltip--align-${this.align}`;
        },
    },

    methods: {
        /**
         * Hiển thị tooltip.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        show() {
            this.display = true;
        },

        /**
         * Ẩn tooltip.
         * CreatedBy: hiennt200210 (07/09/2023)
         */
        hide() {
            this.display = false;
        },
    },
};
</script>

<style scoped>
@import url(./tooltip.css);
</style>
