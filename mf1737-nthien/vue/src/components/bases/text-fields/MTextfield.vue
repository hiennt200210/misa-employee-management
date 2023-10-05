<template>
    <div class="m-textfield">
        <!-- Nhãn -->
        <MLabel
            v-if="label"
            class="m-textfield__label"
            :id="id"
            :label="label"
            :tooltip="tooltip"
            :required="required"
            @clickLabel="focus"
        />

        <!-- Ô nhập -->
        <div :class="inputClass">
            <input
                type="text"
                ref="input"
                :id="id"
                :placeholder="placeHolder"
                :value="modelValue"
                @input="emitValue"
                @blur="validateInput"
            />

            <!-- Icon trên ô nhập -->
            <div v-if="icon" :class="iconClass"></div>
        </div>

        <!-- Thông báo xác thực đầu vào -->
        <div v-if="showErrorMessage" class="error-message">
            {{ `${label} ${message}` }}
        </div>
    </div>
</template>

<script>
import {
    validateEmpty,
    validateMaxLength,
    validateEmail,
} from "@helpers/helpers";
import MLabel from "@components/bases/labels/MLabel.vue";

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
        maxLength: Number,

        // Hàm xác thực dữ liệu đầu vào
        validate: {
            type: Function,
            default: validateEmail,
        },
    },

    data() {
        return {
            value: "",
            showErrorMessage: false,
            message: "",
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
            return `icon icon-${this.icon}`;
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
            this.$refs["input"].select();
        },

        /**
         * Kiểm tra dữ liệu hợp lệ.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        validateInput(e) {
            if (this.required) {
                if (!validateEmpty(e.target.value)) {
                    this.message = this.$resx.CannotBeEmpty;
                    this.showErrorMessage = true;
                } else {
                    if (!validateMaxLength(e.target.value, this.maxLength)) {
                        this.message = `${this.$resx.MaxLengthExceeded} ${this.maxLength} ${this.$resx.Character}`;
                        this.showErrorMessage = true;
                    } else {
                        const result = this.validate(e.target.value);
                        if (this.label === "Email") {
                            if (result !== "") {
                                this.message = result;
                                this.showErrorMessage = true;
                            }
                        } else {
                            this.showErrorMessage = false;
                        }
                    }
                }
            } else {
                if (!validateMaxLength(e.target.value, this.maxLength)) {
                    this.message = `${this.$resx.MaxLengthExceeded} ${this.maxLength} ${this.$resx.Character}`;
                    this.showErrorMessage = true;
                } else {
                    const result = this.validate(e.target.value);
                    if (this.label === "Email") {
                        if (result !== "") {
                            this.message = result;
                            this.showErrorMessage = true;
                        }
                    } else {
                        this.showErrorMessage = false;
                    }
                }
            }
        },

        /**
         * Xác thực dữ liệu được nhập cho input.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        emitValue(e) {
            this.$emit("update:modelValue", e.target.value);
            this.validateInput(e);
        },

        /**
         * Xác thực dữ liệu được nhập cho input khi mất focus.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        onBlur(e) {
            // this.validateInput(e.target.value);
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
