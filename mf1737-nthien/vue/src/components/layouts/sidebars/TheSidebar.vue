<template>
    <div class="sidebar">
        <div class="sidebar__menu">
            <MMenuItem v-for="item in items" :key="item.id" :item="item" />
        </div>
        <div class="sidebar__footer">
            <div class="icon icon-arrow-left"></div>
            Rút gọn
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
            items: [
                {
                    title: "Tổng quan",
                    icon: "tongquan",
                    path: "/",
                    isActived: true,
                },
                {
                    title: "Tiền mặt",
                    icon: "tienmat",
                    path: "/employee",
                    isActived: false,
                },
                {
                    title: "Tiền gửi",
                    icon: "tiengui",
                    path: "/",
                    isActived: false,
                },
                {
                    title: "Mua hàng",
                    icon: "muahang",
                    path: "/",
                    isActived: false,
                },
                {
                    title: "Bán hàng",
                    icon: "banhang",
                    path: "/",
                    isActived: false,
                },
                {
                    title: "Quản lý hóa đơn",
                    icon: "quanlyhoadon",
                    path: "/",
                    isActived: false,
                },
                { title: "Kho", icon: "kho", path: "/", isActived: false },
                {
                    title: "Công cụ dụng cụ",
                    icon: "congcudungcu",
                    path: "/",
                    isActived: false,
                },
                {
                    title: "Tài sản cố định",
                    icon: "taisancodinh",
                    path: "/",
                    isActived: false,
                },
                { title: "Thuế", icon: "thue", path: "/", isActived: false },
                {
                    title: "Giá thành",
                    icon: "giathanh",
                    path: "/",
                    isActived: false,
                },
                {
                    title: "Tổng hợp",
                    icon: "tonghop",
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
