<template>
    <component :is="type">{{ title }}</component>
</template>

<script>
export default {
    name: "MHeading",
    props: ["type", "title"],
};
</script>

<style scoped>
h1 {
    color: var(--text-color);
    font-family: "Google Sans Bold", sans-serif;
    font-size: var(--h1-size);
}

h2 {
    color: var(--text-color);
    font-family: "Google Sans Bold", sans-serif;
    font-size: var(--h2-size);
}
</style>
