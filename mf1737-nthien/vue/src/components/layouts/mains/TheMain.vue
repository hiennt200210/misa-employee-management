<template>
    <div class="main">
        <TheSidebar />
        <TheContent />
    </div>
</template>

<script>
import TheSidebar from "@components/layouts/sidebars/TheSidebar.vue";
import TheContent from "@components/layouts/contents/TheContent.vue";

export default {
    name: "TheMain",
    components: {
        TheSidebar,
        TheContent,
    },
};
</script>

<style scoped>
@import url(main.css);
</style>
