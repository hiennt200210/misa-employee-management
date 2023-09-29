<template>
    <div class="m-textfield">
        <!-- Nhãn -->
        <MLabel
            v-if="label"
            class="m-textfield__label"
            :id="id"
            :label="label"
            :tooltip="tooltip"
            :required="required" />

        <!-- Ô nhập -->
        <div :class="inputClass">
            <input
                type="text"
                ref="input"
                :id="id"
                :placeholder="placeHolder"
                :value="modelValue"
                @input="emitValue"
                @blur="validateInput" />

            <!-- Icon trên ô nhập -->
            <span v-if="icon" class="material-symbols-rounded">{{ icon }}</span>
        </div>

        <!-- Thông báo xác thực đầu vào -->
        <div v-if="showErrorMessage" class="error-message">
            {{
                `${label} ${$resx[$langCode].Component.Form.Warning.CannotBeEmpty}`
            }}
        </div>
    </div>
</template>

<script>
import MLabel from "../labels/MLabel.vue";

export default {
    name: "MTextfield",
    components: {
        MLabel,
    },

    emits: ["update:modelValue"],

    props: {
        // Props cho nhãn
        id: String,
        label: String,
        tooltip: String,
        required: Boolean,

        // Props cho ô nhập
        placeHolder: String,
        icon: String,
        modelValue: String,

        // Hàm xác thực dữ liệu đầu vào
        validator: {
            type: Function,
        }
    },

    data() {
        return {
            value: "",
            showErrorMessage: false,
        };
    },

    computed: {
        /**
         * Xác định style class phù hợp cho textfield.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        inputClass() {
            if (this.icon) {
                return "m-textfield__input m-textfield__input--icon";
            }
            return "m-textfield__input";
        },

        /**
         * Xác định các class phù hợp cho icon.
         * CreatedBy: hiennt200210 (23/08/2023)
         */
        iconClass() {
            return `fa-${this.icon.style} fa-${this.icon.name}`;
        },
    },

    watch: {
        modelValue(oldValue, newValue) {
            this.value = newValue;
        },
    },

    methods: {
        /**
         * Focus vào phần tử input.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        focus() {
            this.$refs["input"].focus();
        },

        /**
         * Kiểm tra dữ liệu hợp lệ.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        validateInput() {
            if (this.required)
                this.showErrorMessage = this.value === "";
        },

        /**
         * Xác thực dữ liệu được nhập cho input.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        emitValue(e) {
            this.$emit("update:modelValue", e.target.value);

            if (this.required)
                this.showErrorMessage = e.target.value === "";
        },

        /**
         * Tuỳ chỉnh kích thước chiều ngang cho ô nhập
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        setWidth(width) {
            this.$refs["input"].style.width = width;
        },
    },
};
</script>

<style scoped>
@import url(./text-field.css);
</style>
