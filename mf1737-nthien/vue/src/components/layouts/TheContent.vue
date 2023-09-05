<template>
    <div class="content">
        <router-view></router-view>
    </div>
</template>

<script>
export default {
    name: "TheContent",
};
</script>

<style scoped>
.content {
    height: 100%;
    width: calc(100% - var(--sidebar-width));
    padding: 24px;
    padding-top: 16px;
    background-color: #eeeff1;
}
</style>
