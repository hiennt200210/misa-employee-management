<template>
    <input
        type="checkbox"
        class="m-checkbox"
        :id="id"
        :checked="modelValue"
        @change="$emit('update:modelValue', $event.target.checked)" />
</template>

<script>
export default {
    name: "MSCheckbox",
    props: ["id", "modelValue"],
    emits: ["update:modelValue"],
};
</script>

<style scoped>
.m-checkbox {
    height: 20px;
    width: 20px;
    appearance: none;
    background-color: var(--secondary-color);
    border: 2px solid #707070;
    border-radius: 4px;
    cursor: pointer;
}

.m-checkbox:hover {
    background-color: var(--secondary-color--hover);
}

.m-checkbox:checked {
    position: relative;
    height: 20px;
    width: 20px;
    appearance: none;
    background-color: #fff;
    border: 2px solid #2ca01c;
}

.m-checkbox:checked:after {
    content: "";
    position: absolute;
    top: -2px;
    left: -2px;
    height: 20px;
    width: 20px;
    background-color: #2ca01c;
    background-image: url(../../../assets/img/sprites.svg);
    background-position: -846px -310px;
    border-radius: 4px;
}
</style>
