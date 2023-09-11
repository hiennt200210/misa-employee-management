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
        <div :class="inputStyle">
            <input
                type="text"
                ref="input"
                :id="id"
                :placeholder="placeHolder"
                :value="modelValue"
                @input="emitValue"
                @blur="validateInput" />

            <!-- Icon trên ô nhập -->
            <i v-if="icon" :class="iconClass"></i>
        </div>

        <!-- Thông báo xác thực đầu vào -->
        <div v-if="showErrorMessage" class="error-message">
            {{
                `${label} ${$Resource.Component.Form.Warning.CannotBeEmpty}`
            }}
        </div>
    </div>
</template>

<script>
import MLabel from "../label/MLabel.vue";

export default {
    name: "MTextfield",
    components: {
        MLabel,
    },

    emits: ["update:modelValue"],

    props: {
        // Props for MLabel
        id: String,
        label: String,
        tooltip: String,
        required: Boolean,

        // Props for input element
        placeHolder: String,
        icon: Object,
        modelValue: String,
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
        inputStyle() {
            if (this.icon) {
                return "m-textfield__input m-textfield__input--icon";
            }
            return "m-textfield__input";
        },

        /**
         * Xác định các class phù hợp cho icon trên nút.
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
         * Đặt kích thước cho input.
         * CreatedBy: hiennt200210 (24/08/2023)
         */
        setWidth(width) {
            this.$refs["input"].style.width = width;
        },
    },
};
</script>

<style scoped>
.m-textfield {
    display: flex;
    flex-direction: column;
}

.m-textfield__label {
    width: fit-content;
    margin-bottom: 8px;
}

.m-textfield__input {
    position: relative;
}

.m-textfield__input input {
    height: var(--control-height);
    width: 100%;
    padding: 0 12px;
    background-color: var(--secondary-color);
    border: 1px solid var(--border-color);
    border-radius: var(--border-radius);
    font-family: var(--default-font-family);
    font-size: var(--default-font-size);
    color: var(--input-text-color);
}

.m-textfield__input input:hover {
    background-color: var(--secondary-color--hover);
    cursor: pointer;
}

.m-textfield__input input:focus {
    background-color: var(--secondary-color);
    border: 1px solid var(--primary-color);
    outline: none;
}

.m-textfield__input input::placeholder {
    color: var(--input-placeholder-color);
    font-family: var(--default-font-family);
    font-size: 14px;
}

.m-textfield .error-message {
    max-width: 160px;
    margin-top: 8px;
    color: var(--danger-color);
    white-space: wrap;
}

/* Textfield with icon */
.m-textfield__input--icon input {
    padding-right: 36px;
}

.m-textfield__input--icon i {
    position: absolute;
    top: 6px;
    right: 6px;
    display: flex;
    align-items: center;
    justify-content: center;
    height: 24px;
    width: 24px;
    font-size: 18px;
    color: var(--input-placeholder-color);
    text-align: center;
}
</style>
