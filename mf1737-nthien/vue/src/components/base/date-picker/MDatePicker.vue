<template>
    <div class="m-date-picker">
        <MLabel :label="label" :forInput="id" />
        <input
            type="date"
            :id="id"
            :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)" />
    </div>
</template>

<script>
export default {
    name: "MDatePicker",
    props: ["label", "id", "modelValue"],
    emits: ["update:modelValue"],
};
</script>

<style scoped>
@import url("./date-picker.css");
</style>
