<template>
    <div class="container">
        <TheHeader />
        <TheMain>
            <TheSidebar />
            <TheContent />
        </TheMain>
    </div>
</template>

<script>
import TheHeader from "./components/layouts/TheHeader.vue";
import TheMain from "./components/layouts/TheMain.vue";
import TheSidebar from "./components/layouts/TheSidebar.vue";
import TheContent from "./components/layouts/TheContent.vue";

export default {
    name: "App",
    components: {
        TheHeader,
        TheMain,
        TheSidebar,
        TheContent,
    },
};
</script>

<style>
@import url(./assets/fontawesome/css/all.min.css);
@import url(./styles/main.css);

.container {
    font-family: "Google Sans Regular", sans-serif;
    font-size: 14px;
    height: 100vh;
    width: 100%;
    position: relative;
}
</style>
