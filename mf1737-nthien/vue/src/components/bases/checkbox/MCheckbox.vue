<template>
    <input type="checkbox" class="m-checkbox" :id="id" :checked="modelValue"
        @change="$emit('update:modelValue', $event.target.checked)" />
</template>

<script>
export default {
    name: "MCheckbox",
    props: ["id", "modelValue"],
    emits: ["update:modelValue"],
};
</script>

<style scoped>
@import url(checkbox.css);
</style>
