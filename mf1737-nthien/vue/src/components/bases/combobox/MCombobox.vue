<template>
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

    <div class="m-combobox">
        <!-- Ô nhập của combobox -->
        <input
            ref="input"
            :value="textInput"
            type="text"
            class="m-combobox__input"
            @input="onInput"
            @keydown="onUpDown"
        />

        <!-- Nút hiển thị danh sách -->
        <button
            class="m-combobox__button"
            @click="btnSelectDataOnClick"
            @keydown="onUpDown"
            tabindex="-1"
        >
            <div ref="icon" class="icon icon-expand"></div>
        </button>

        <!-- Danh sách item -->
        <div
            v-if="isShowListData"
            class="m-combobox__data"
            ref="m-combobox__data"
            v-clickoutside="hideListData"
        >
            <!-- Item -->
            <a
                class="m-combobox__item"
                v-for="(item, index) in dataFilter"
                :class="{
                    'm-combobox__item--focus': index == indexItemFocus,
                    'm-combobox__item--selected': index == indexItemSelected,
                }"
                :key="item[this.propValue]"
                :ref="'toFocus_' + index"
                :value="item[this.propValue]"
                @click="itemOnSelect(item, index)"
                @focus="saveItemFocus(index)"
                @keydown="onUpDown(index)"
                @keyup="onUpDown(index)"
                tabindex="1"
            >
                <!-- Icon check -->
                <div class="m-combobox__item-icon">
                    <span id="check-icon" class="material-symbols-rounded"
                        >check</span
                    >
                </div>

                <!-- Text -->
                <span>{{ item[this.propText] }}</span>
            </a>
        </div>
    </div>
</template>

<script>
import MLabel from "@components/bases/labels/MLabel.vue";
/* eslint-disable */
/**
 * Gán sự kiện nhấn click chuột ra ngoài combobox data (ẩn data list đi)
 * NVMANH (31/07/2022)
 */
const clickoutside = {
    mounted(el, binding, vnode, prevVnode) {
        el.clickOutsideEvent = (event) => {
            // Nếu element hiện tại không phải là element đang click vào
            // Hoặc element đang click vào không phải là button trong combobox hiện tại thì ẩn đi.
            if (
                !(
                    el === event.target || // click phạm vi của m-combobox__data
                    el.contains(event.target) || //click vào element con của m-combobox__data
                    el.previousElementSibling.contains(event.target)
                )
            ) {
                //click vào element button trước combobox data (nhấn vào button thì hiển thị)
                // Thực hiện sự kiện tùy chỉnh:
                binding.value(event, el);
                // vnode.context[binding.expression](event); // vue 2
            }
        };
        document.body.addEventListener("click", el.clickOutsideEvent);
    },
    beforeUnmount: (el) => {
        document.body.removeEventListener("click", el.clickOutsideEvent);
    },
};

/**
 * Hàm xóa dấu tiếng việt
 * NVMANH (31/07/2022)
 */
function removeVietnameseTones(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
    str = str.replace(/ + /g, " ");
    str = str.trim();
    // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
    str = str.replace(
        /!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g,
        " "
    );
    return str;
}

const keyCode = {
    Enter: 13,
    ArrowUp: 38,
    ArrowDown: 40,
    ESC: 27,
};

export default {
    name: "MCombobox",

    components: {
        MLabel,
    },

    directives: {
        clickoutside,
    },

    props: {
        // Props cho nhãn
        id: String,
        label: String,
        tooltip: String,
        required: Boolean,

        // Props cho ô nhập
        url: String,
        propValue: String,
        propText: String,
        formValue: String,
    },

    emits: ["getValue"],

    data() {
        return {
            data: [], // data gốc
            textInput: "",
            dataFilter: [], // data đã được filter
            isShowListData: false, // Hiển thị list data hay không
            indexItemFocus: null, // Index của item focus hiện tại
            indexItemSelected: null, // Index của item được selected
        };
    },

    methods: {
        /**
         * Focus vào input.
         * CreatedBy: hiennt200210 (04/10/2023)
         */
        focus() {
            this.$refs["input"].focus();
            this.$refs["input"].select();
        },
        /**
         * Lưu lại index của item đã focus
         * NVMANH (31/07/2022)
         */
        saveItemFocus(index) {
            this.indexItemFocus = index;
        },

        /**
         * Ẩn danh sách item
         * NVMANH (31/07/2022)
         */
        hideListData() {
            this.isShowListData = false;
            this.$refs.icon.style.transform = "rotate(0deg)";
        },

        /**
         * Nhấn vào button thì hiển thị hoặc ẩn List Item
         * NVMANH (31/07/2022)
         */
        btnSelectDataOnClick() {
            this.dataFilter = this.data;
            // this.isShowListData = !this.showListData;
            this.isShowListData = !this.isShowListData;
            if (this.isShowListData) {
                this.$refs.icon.style.transform = "rotate(180deg)";
            } else {
                this.$refs.icon.style.transform = "rotate(0deg)";
            }
        },

        /**
         * Click chọn item trong danh sách
         * NVMANH (31/07/2022)
         */
        itemOnSelect(item, index) {
            const text = item[this.propText];
            const value = item[this.propValue];
            this.textInput = text; // Hiển thị text lên input.
            this.indexItemSelected = index;
            this.isShowListData = false;
            this.$emit("getValue", value, text, item);
        },

        /**
         * Nhập text thì thực hiện filter dữ liệu và hiển thị
         * NVMANH (31/07/2022)
         */
        onInput() {
            var me = this;
            this.textInput = event.target.value;
            // Thực hiện lọc các phần tử phù hợp trong data:
            this.dataFilter = this.data.filter((e) => {
                let text = removeVietnameseTones(me.textInput)
                    .toLowerCase()
                    .replace(" ", "");
                let textOfItem = removeVietnameseTones(e[me.propText])
                    .toLowerCase()
                    .replace(" ", "");
                return textOfItem.includes(text);
            });
            this.isShowListData = true;
        },

        /**
         * Lựa chọn item bằng cách nhấn các phím lên, xuống trên bàn phím
         * NVMANH (31/07/2022)
         */
        onUpDown() {
            var me = this;
            var keyCodePress = event.keyCode;
            var elToFocus = null;
            switch (keyCodePress) {
                case keyCode.ESC:
                    this.isShowListData = false;
                    break;
                case keyCode.ArrowDown:
                    this.isShowListData = true;
                    elToFocus = this.$refs[`toFocus_${me.indexItemFocus + 1}`];
                    if (
                        this.indexItemFocus == null ||
                        !elToFocus ||
                        elToFocus.length == 0
                    ) {
                        this.indexItemFocus = 0;
                    } else {
                        this.indexItemFocus += 1;
                    }
                    break;
                case keyCode.ArrowUp:
                    this.isShowListData = true;
                    elToFocus = this.$refs[`toFocus_${me.indexItemFocus - 1}`];
                    if (
                        this.indexItemFocus == null ||
                        !elToFocus ||
                        elToFocus.length == 0
                    ) {
                        this.indexItemFocus = this.dataFilter.length - 1;
                    } else {
                        this.indexItemFocus -= 1;
                    }
                    break;
                case keyCode.Enter:
                    elToFocus = this.$refs[`toFocus_${me.indexItemFocus}`];
                    if (elToFocus && elToFocus.length > 0) {
                        elToFocus[0].click();
                        this.isShowListData = false;
                    }
                    break;
                default:
                    break;
            }
        },
    },

    created() {
        // Thực hiện lấy dữ liệu từ api:
        if (this.url) {
            fetch(this.url)
                .then((res) => res.json())
                .then((res) => {
                    this.data = res;
                    this.dataFilter = res;
                    if (this.formValue) {
                        this.textInput = res.find(
                            (e) => e[this.propValue] == this.formValue
                        )[this.propText];
                    }
                    this.$emit("getValue", this.formValue, this.textInput);
                })
                .catch((res) => {
                    console.log(res);
                });
        }
    },
};
</script>

<style scoped>
@import url(combobox.css);
</style>
