<template>
    <div class="m-tooltip">
        {{ content }}
    </div>
</template>

<script>
export default {
    name: "MTooltip",
    props: ["align", "content"],
};
</script>

<style scoped>
.m-tooltip {
    position: absolute;
    top: calc(100% + 10px);
    left: 50%;
    transform: translateX(-50%);
    z-index: 999;
    min-height: 40px;
    max-width: 250px;
    padding: 12px 16px;
    white-space: nowrap;
    background-color: rgba(0, 0, 0, 0.8);
    border-radius: var(--border-radius);
    color: #fff;
    font-family: "Google Sans", san-serif;
    font-size: 14px;
}

.m-tooltip::after {
    content: "";
    position: absolute;
    top: -5px;
    left: calc(50% - 4px);
    border-left: 5px solid transparent;
    border-right: 5px solid transparent;
    border-bottom: 6px solid rgba(0, 0, 0, 0.8);
    font-size: 0;
}

</style>
