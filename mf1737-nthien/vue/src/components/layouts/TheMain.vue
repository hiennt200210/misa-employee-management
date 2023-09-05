<template>
    <div class="main">
        <slot></slot>
    </div>
</template>

<script>
export default {
    name: "TheMain",
};
</script>

<style scoped>
.main {
    width: 100%;
    height: calc(100vh - var(--header-height));
    display: flex;
}
</style>
