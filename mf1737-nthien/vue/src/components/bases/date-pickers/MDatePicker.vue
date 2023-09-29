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
import MLabel from "@components/bases/labels/MLabel.vue";

export default {
    name: "MDatePicker",
    components: {
        MLabel,
    },
    props: ["label", "id", "modelValue"],
    emits: ["update:modelValue"],
};
</script>

<style scoped>
@import url("./date-picker.css");
</style>
