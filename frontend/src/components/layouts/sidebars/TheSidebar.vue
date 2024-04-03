<template>
    <div class="sidebar">
        <!-- List items -->
        <div class="sidebar__menu">
            <MMenuItem v-for="(item, index) in menus" :item="item" />
        </div>

        <!-- Sidebar footer -->
        <div class="sidebar__footer">
            <div class="icon icon-arrow-left"></div>
            {{ $resx.Shrink }}
        </div>
    </div>
</template>

<script>
import MMenuItem from "@components/bases/menus/MMenuItem.vue";

export default {
    name: "TheSidebar",
    components: {
        MMenuItem,
    },
    data() {
        return {
            menus: [
                {
                    title: this.$resx.Overview,
                    icon: "overview",
                    path: "/",
                    isActived: true,
                },
                {
                    title: this.$resx.Cash,
                    icon: "cash",
                    path: "/employees",
                    isActived: false,
                },
                {
                    title: this.$resx.Deposits,
                    icon: "deposits",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Purchase,
                    icon: "purchase",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Sales,
                    icon: "sales",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.ManageInvoices,
                    icon: "manage-invoices",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Warehouse,
                    icon: "warehouse",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Tools,
                    icon: "tools",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.FixedAssets,
                    icon: "fixed-assets",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Tax,
                    icon: "tax",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Prices,
                    icon: "prices",
                    path: "/",
                    isActived: false,
                },
                {
                    title: this.$resx.Summarization,
                    icon: "summarization",
                    path: "/",
                    isActived: false,
                },
            ],
        };
    },
};
</script>

<style scoped>
@import url(sidebar.css);
</style>
