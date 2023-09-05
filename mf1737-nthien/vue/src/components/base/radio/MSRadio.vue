<template>
    <input type="radio" :id="id" :name="name" :value="value" class="m-radio" />
</template>

<script>
export default {
    name: "MRadio",
    props: ["id", "name", "value"],
};
</script>

<style scoped>
.m-radio {
    height: 20px;
    width: 20px;
    margin-right: 8px;
    appearance: none;
    background-color: #fff;
    border: 2px solid #707070;
    border-radius: 50%;
}

.m-radio:hover {
    background-color: var(--secondary-color--hover);
}

.m-radio:checked {
    position: relative;
    height: 20px;
    width: 20px;
    margin-right: 8px;
    appearance: none;
    background-color: #fff;
    border: 2px solid var(--primary-color);
    border-radius: 50%;
}

.m-radio:checked:after {
    content: "";
    position: absolute;
    top: 2px;
    left: 2px;
    height: 12px;
    width: 12px;
    background-color: var(--primary-color);
    border-radius: 50%;
}
</style>
