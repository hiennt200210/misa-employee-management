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
        content: String,
    },

    computed: {
        /**
         * Xác định các style class phù hợp cho tooltip.
         * CreatedBy: hiennt200210 (2023/09/07)
         */
        styleClass() {
            return `m-tooltip m-tooltip--align-${this.align}`;
        },
    },
};
</script>

<style scoped>


.m-tooltip {
    --tooltip-background: rgba(0, 0, 0, 0.8);

    /* Position */
    position: absolute;
    z-index: 99;
    
    /* Size and display */
    max-width: 250px;
    min-height: 40px;
    padding: 12px 16px;
    white-space: nowrap;

    /* Border and background */
    border-radius: var(--default-border-radius);
    background-color: var(--tooltip-background);

    /* Content */
    font-family: var(--default-font-family);
    font-size: var(--default-font-size);
    color: #fff;
}

.m-tooltip::after {
    content: "";
    position: absolute;
}

.m-tooltip--align-top {
    bottom: calc(100% + 10px);
    left: 50%;
    transform: translateX(-50%);
}

.m-tooltip--align-top::after {
    bottom: -5px;
    left: calc(50% - 5px);
    border-left: 5px solid transparent;
    border-right: 5px solid transparent;
    border-top: 5px solid var(--tooltip-background);
}

.m-tooltip--align-bottom {
    top: calc(100% + 10px);
    left: 50%;
    transform: translateX(-50%);
}

.m-tooltip--align-bottom::after {
    top: -5px;
    left: calc(50% - 5px);
    border-left: 5px solid transparent;
    border-right: 5px solid transparent;
    border-bottom: 5px solid var(--tooltip-background);
}
</style>
