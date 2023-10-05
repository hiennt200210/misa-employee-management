<template>
    <div class="m-radio">
        <input
            ref="input"
            type="radio"
            :id="id"
            :name="name"
            :value="value"
            :checked="checked"
            @input="$emit('update:modelValue', Number($event.target.value))"
        />
        <MLabel v-if="label" :label="label" :for="id" @clickLabel="focus" />
    </div>
</template>

<script>
import MLabel from "../labels/MLabel.vue";

export default {
    name: "MRadio",
    components: {
        MLabel,
    },
    props: ["label", "id", "name", "value", "modelValue"],
    data() {
        return {
            radioValue: this.modelValue,
        };
    },
    computed: {
        checked() {
            return this.modelValue === this.value;
        },
    },
    watch: {
        modelValue(newValue) {
            this.radioValue = newValue;
        },
    },
    methods: {
        /**
         * Focus vào input
         * CreatedBy: hiennt2002106 (04/10/2023)
         */
        focus() {
            this.$refs["input"].focus();
        },
    },
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

.m-radio .m-label {
    font-family: var(--default-font-family);
    font-weight: 400;
}
</style>
