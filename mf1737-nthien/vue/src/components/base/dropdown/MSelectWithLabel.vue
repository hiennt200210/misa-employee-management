<template>
    <div class="m-select-with-label">
        <MLabel v-if="label" :label="label" :required="required" />
        <MSelect :options="options" />
    </div>
</template>

<script>
export default {
    name: "MSelectWithLabel",
    props: ["label", "required", "options"],
};
</script>

<style scoped>
.m-select-with-label {
    display: flex;
    flex-direction: column;
}

.m-select-with-label .m-select {
    margin-top: 8px;
}

.m-select-with-label label {
    font-family: "Google Sans Medium", sans-serif;
}
</style>
