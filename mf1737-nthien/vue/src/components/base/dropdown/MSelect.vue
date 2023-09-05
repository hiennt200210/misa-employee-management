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
                @click="
                    selected = option;
                    open = false;
                    $emit('input', option);
                "
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
    mounted() {
        this.$emit("input", this.selected);
    },
};
</script>

<style scoped>
@import url(./select.css);
</style>
