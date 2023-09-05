<template>
    <div class="sidebar">
        <div class="sidebar__menu">
            <MMenuItem v-for="item in items" :key="item.id" :item="item" />
            <router-link
                @click="onClickMenuItem"
                class="m-menu-item"
                :to="items[0].path">
                <div :class="`icon icon-${items[0].icon}`"></div>
                {{ items[0].title }}
            </router-link>
        </div>
        <div class="sidebar__footer">
            <i class="fa-solid fa-angle-left"></i>
            Rút gọn
        </div>
    </div>
</template>

<script>
import MMenuItem from "../base/menu-item/MMenuItem.vue";

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
.sidebar {
    height: 100%;
    width: var(--sidebar-width);
    background-color: #041434;
}

.sidebar__menu {
    height: calc(100% - 44px);
    padding: 0 12px;
    padding-top: 20px;
    overflow: auto;
}

.sidebar__menu::-webkit-scrollbar {
    height: 8px;
    width: 8px;
    border-radius: 4px;
    background-color: var(--secondary-color);
}

.sidebar__menu::-webkit-scrollbar-thumb {
    border-radius: 4px;
    background-color: #869ab8;
}

.sidebar__menu::-webkit-scrollbar-track {
    border-radius: 4px;
    background-color: #041434;
}

.m-menu-item {
    width: 100%;
    height: 44px;
    border-radius: var(--border-radius);
    padding: 0 12px;
    display: flex;
    align-items: center;
    color: #869ab8;
    background-color: #041434;
    text-decoration: none;
}

.m-menu-item:hover {
    background-color: #17274b;
    cursor: pointer;
}

.m-menu-item:active {
    background-color: #1e3155;
    color: #e0e0e0;
}

.m-menu-item .icon {
    width: 24px;
    height: 24px;
    margin-right: 12px;
}

.m-menu-item--active {
    background-color: #1e3155;
    color: #e0e0e0;
}

.sidebar__footer {
    display: flex;
    align-items: center;
    height: 44px;
    width: 100%;
    padding: 0 24px;
    background-color: #041434;
    border-top: 1px solid #213553;
    color: #869ab8;
}

.sidebar__footer:hover {
    background-color: #17274b;
    cursor: pointer;
}

.sidebar__footer:active {
    background-color: #1e3155;
    color: #e0e0e0;
}

.sidebar__footer i {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 24px;
    width: 24px;
    margin-right: 16px;
    font-size: 18px;
}
</style>
