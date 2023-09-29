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
    color: var(--default-font-color);
    font-family: var(--default-font-family);
    font-size: var(--h1-size);
    font-weight: 700;
}

h2 {
    color: var(--default-font-color);
    font-family: var(--default-font-family);
    font-size: var(--h2-size);
    font-weight: 700;
}
</style>
