<template>
    <div class="container">
        <TheHeader />
        <TheMain />
    </div>
</template>

<script>
import TheHeader from "@components/layouts/headers/TheHeader.vue";
import TheMain from "@components/layouts/mains/TheMain.vue";

export default {
    name: "App",
    components: {
        TheHeader,
        TheMain,
    },
};
</script>

<style>
@import url(@assets/styles/style.css);

.container {
    position: relative;
    height: 100vh;
    width: 100%;
}
</style>
