<template>
    <div class="m-select" @focus="open = true" @blur="open = false">
        <div class="m-selected" :class="{ open: open }" @click="open = !open">
            {{ selected }}
        </div>
        <div class="m-select-items" :class="{ selectHide: !open }">
            <div
                class="m-select-item"
                v-for="(option, i) of options"
                :key="i"
                @click="onClick(option)"
            >
                {{ option }}
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "MSelect",
    props: {
        options: {
            type: Array,
            required: true,
        },
    },
    data() {
        return {
            selected: this.options.length > 0 ? this.options[0] : null,
            open: false,
        };
    },
    methods: {
        onClick(option) {
            this.selected = option;
            this.open = false;
            this.$emit("input", option);
        },
    },
    mounted() {
        this.$emit("input", this.selected);
    },
};
</script>

<style scoped>
@import url(./select.css);
</style>
