<template>
    <div class="m-radio-box">
        <MLabel :label="label" />
        <div class="m-radio-list">
            <slot></slot>
        </div>
    </div>
</template>

<script>
import MLabel from "@components/bases/labels/MLabel.vue";

export default {
    name: "MRadioBox",
    components: {
        MLabel,
    },
    props: ["label"],

    
};
</script>

<style>
.m-radio-box {
    height: fit-content;
    display: flex;
    flex-direction: column;
}

.m-radio-box>label {
    font-family: var(--default-font-family);
    font-size: var(--default-font-size);
}

.m-radio-box .m-radio-list {
    margin-top: 16px;
    display: flex;
}

.m-radio-box .m-radio-list>*:not(:first-child) {
    margin-left: 16px;
}
</style>
