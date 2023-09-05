<template>
    <router-link @click="onClickMenuItem" :class="styleClass" :to="item.path">
        <div :class="`icon icon-${item.icon}`"></div>
        {{ item.title }}
    </router-link>
</template>

<script>
export default {
    name: "MMenuItem",
    props: ["item"],
    computed: {
        styleClass() {
            if (this.isActived) {
                return "m-menu-item m-menu-item--active";
            }
            return "m-menu-item";
        },
    },
    methods: {
        onClickMenuItem() {
            this.isActived = !this.isActived;
        },
    },
    data() {
        return {
            isActived: false,
        };
    },
};
</script>

<style scoped>
@import url(./menu-item.css);
</style>
