<template>
    <div class="m-radio">
        <input type="radio" :id="id" :name="name" />
        <MLabel class="label" v-if="label" :label="label" :for="id" />
    </div>
</template>

<script>
import MLabel from "../label/MLabel.vue";

export default {
    name: "MRadio",
    components: {
        MLabel,
    },
    props: ["label", "id", "name"],
};
</script>

<style scoped>
.m-radio {
    display: flex;
    align-items: center;
}

input[type="radio"] {
    height: 20px;
    width: 20px;
    margin-right: 8px;
    appearance: none;
    background-color: #fff;
    border: 2px solid #707070;
    border-radius: 50%;
}

input[type="radio"]:hover {
    background-color: var(--secondary-color--hover);
}

input[type="radio"]:checked {
    height: 20px;
    width: 20px;
    margin-right: 8px;
    appearance: none;
    background-color: #fff;
    border: 2px solid var(--primary-color);
    border-radius: 50%;
    position: relative;
}

input[type="radio"]:checked:after {
    content: "";
    height: 12px;
    width: 12px;
    background-color: var(--primary-color);
    border-radius: 50%;
    position: absolute;
    top: 2px;
    left: 2px;
}

.m-radio .label {
    font-family: "Google Sans Regular", sans-serif;
}
</style>
