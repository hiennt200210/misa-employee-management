import { createRouter, createWebHistory } from "vue-router";
import HomePage from "../views/HomePage.vue";
import EmployeeList from "../views/employee/EmployeeList.vue";

const routes = [
    { path: "/", name: "HomeRouter", component: HomePage },
    { path: "/employee", name: "EmployeeRouter", component: EmployeeList },
];

const vueRouter = createRouter({
    history: createWebHistory(),
    routes,
});

export default vueRouter;
