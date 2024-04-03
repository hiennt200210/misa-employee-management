import { createRouter, createWebHistory } from "vue-router";
import HomePage from "@views/homepage/HomePage.vue";
import EmployeeList from "@views/employee/employee-list/EmployeeList.vue";

const routes = [
    { path: "/", name: "HomeRouter", component: HomePage },
    { path: "/employees", name: "EmployeeRouter", component: EmployeeList },
];

const vueRouter = createRouter({
    history: createWebHistory(),
    routes,
});

export default vueRouter;
