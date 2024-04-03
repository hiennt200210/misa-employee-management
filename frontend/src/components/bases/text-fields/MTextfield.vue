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
                @blur="onBlur"
            />

            <!-- Icon trên ô nhập -->
            <div v-if="icon" :class="iconClass"></div>
        </div>

        <!-- Thông báo lỗi khi validate dữ liệu -->
        <div v-if="showErrorMessages" class="error-message">
            {{ `${label} ${message}` }}
        </div>
    </div>
</template>

<script>
import {
    validateEmpty,
    validateMaxLength,
    validateEmail,
} from "@utils/validate";
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
        type: {
            type: String,
            default: "text",
        },
        placeHolder: String,
        icon: String,
        modelValue: String,
        maxLength: Number,

        validate: {
            type: Function,
            default: () => "",
        },
    },

    data() {
        return {
            value: "",
            showErrorMessages: false,
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
                    this.showErrorMessages = true;
                } else {
                    if (!validateMaxLength(e.target.value, this.maxLength)) {
                        this.message = `${this.$resx.MaxLengthExceeded} ${this.maxLength} ${this.$resx.Character}`;
                        this.showErrorMessages = true;
                    } else {
                        if (this.type === "email") {
                            if (e.target.value !== "" && !validateEmail(e.target.value)) {
                                this.message = this.$resx.EmailInvalid;
                                this.showErrorMessages = true;
                            } else {
                                this.showErrorMessages = false;
                            }
                        } else {
                            this.showErrorMessages = false;
                        }
                    }
                }
            } else {
                if (!validateMaxLength(e.target.value, this.maxLength)) {
                    this.message = `${this.$resx.MaxLengthExceeded} ${this.maxLength} ${this.$resx.Character}`;
                    this.showErrorMessages = true;
                } else {
                    if (this.type === "email") {
                        if (e.target.value !== "" && !validateEmail(e.target.value)) {
                            this.message = this.$resx.EmailInvalid;
                            this.showErrorMessages = true;
                        } else {
                            this.showErrorMessages = false;
                        }
                    } else {
                        this.showErrorMessages = false;
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
            this.validateInput(e);
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
