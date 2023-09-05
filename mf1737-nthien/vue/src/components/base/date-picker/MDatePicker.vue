<template>
    <div class="m-date-picker">
        <MLabel :label="label" :forInput="id" />
        <input type="date" :id="id" />
    </div>
</template>

<script>
export default {
    name: "MDatePicker",
    props: ["label", "id"],
};
</script>

<style scoped>
.m-date-picker {
    height: fit-content;
    display: flex;
    flex-direction: column;
}

.m-date-picker label {
    font-family: "Google Sans Medium", sans-serif;
    font-size: 14px;
}

input[type="date"] {
    height: var(--control-height);
    margin-top: 8px;
    padding: 0 12px;
    background-color: var(--secondary-color);
    border: 1px solid var(--border-color);
    border-radius: var(--border-radius);
    font-family: "Google Sans Regular", sans-serif;
    font-size: 14px;
    color: var(--input-placeholder-color);
    text-transform: uppercase;
}

input[type="date"]:hover {
    background-color: var(--secondary-color--hover);
}

input[type="date"]:focus {
    background-color: var(--secondary-color);
    border: 1px solid var(--primary-color);
    outline: none;
}
</style>
