<template>
    <div class="m-date-picker">
        <MLabel :label="label" :forInput="id" @clickLabel="focus" />
        <input
            ref="input"
            type="date"
            :id="id"
            :value="dateString"
            @input="
                $emit('update:modelValue', reformatDate($event.target.value))
            "
            @blur="onBlur"
        />

        <!-- Thông báo lỗi khi validate dữ liệu -->
        <div v-if="showErrorMessages" class="error-message">
            {{ `${label} ${message}` }}
        </div>
    </div>
</template>

<script>
import { formatDate } from "@utils/format";
import MLabel from "@components/bases/labels/MLabel.vue";

export default {
    name: "MDatePicker",
    components: {
        MLabel,
    },
    props: ["label", "id", "modelValue"],
    emits: ["update:modelValue"],

    data() {
        return {
            value: this.modelValue,
            showErrorMessages: false,
            message: "",
        };
    },

    computed: {
        /**
         * Ngày tháng năm định dạng lại
         */
        dateString() {
            return this.reformatDate(this.modelValue);
        },
    },

    watch: {
        modelValue(newValue) {
            this.value = newValue;
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

        /**
         * Validate dữ liệu
         * CreatedBy: hiennt2002106 (04/10/2023)
         */
        validateInput() {
            if (new Date(this.value) > new Date()) {
                this.showErrorMessages = true;
                this.message = this.$resx.DateInvalid;
            } else {
                this.showErrorMessages = false;
            }
        },

        /**
         * Xử lý sự kiện input
         * CreatedBy: hiennt2002106 (04/10/2023)
         */
        onInput(event) {
            this.validateInput();
            this.$emit(
                "update:modelValue",
                this.reformatDate(event.target.value)
            );
        },

        /**
         * Validate dữ liệu khi blur khỏi input
         * CreatedBy: hiennt2002106 (04/10/2023)
         */
        onBlur() {
            this.validateInput();
        },
    },
};
</script>

<style scoped>
@import url("./date-picker.css");
</style>
