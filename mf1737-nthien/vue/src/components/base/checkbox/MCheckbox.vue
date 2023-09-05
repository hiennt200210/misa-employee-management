<template>
    <div class="m-checkbox">
        <input type="checkbox" :id="id" />
        <MLabel :label="label" :forInput="id" />
    </div>
</template>

<script>
export default {
    name: "MCheckbox",
    props: ["label", "id"],
};
</script>

<style scoped>
.m-checkbox {
    display: flex;
    align-items: center;
}

.m-checkbox label {
    margin-left: 12px;
}

input[type="checkbox"] {
    height: 20px;
    width: 20px;
    appearance: none;
    background-color: var(--secondary-color);
    border: 2px solid #707070;
    border-radius: 4px;
}

input[type="checkbox"]:hover {
    background-color: var(--secondary-color--hover);
    cursor: pointer;
}

input[type="checkbox"]:checked {
    height: 20px;
    width: 20px;
    appearance: none;
    background-color: #fff;
    border: 2px solid #2ca01c;
    position: relative;
}

input[type="checkbox"]:checked:after {
    content: "";
    width: 20px;
    height: 20px;
    background-color: #2ca01c;
    background-image: url(../../../assets/img/sprites.svg);
    background-position: -846px -310px;
    border-radius: 4px;
    position: absolute;
    top: -2px;
    left: -2px;
}
</style>
