<template>
    <div class="m-date-picker">
        <MLabel :label="label" :forInput="id" />
        <input
            type="date"
            :id="id"
            :value="dateString"
            @input="
                $emit('update:modelValue', reformatDate($event.target.value))
            "
        />
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
    data() {
        value: this.modelValue;
    },
    computed: {
        dateString() {
            return this.reformatDate(this.modelValue);
        },
    },
    watch: {
        value(newValue) {
            this.value = newValue;
        },
    },
    methods: {
        /**
         * Định dạng lại ngày tháng năm
         * @param {String} dateString Ngày tháng năm
         * @returns {String} Ngày tháng năm định dạng lại
         * CreatedBy: hiennt2002106 (28/09/2023)
         */
        reformatDate(dateString) {
            let date = new Date(dateString);
            let year = date.getFullYear();
            let month = date.getMonth() + 1;
            let day = date.getDate();
            return `${year}-${month < 10 ? `0${month}` : month}-${
                day < 10 ? `0${day}` : day
            }`;
        },
    },
};
</script>

<style scoped>
@import url("./date-picker.css");
</style>
