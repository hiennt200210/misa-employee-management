import { createApp } from "vue";
import App from "./App.vue";
import enums from "@constants/enums";
import resources from "@constants/resources";
import vueRouter from "@router/index";

const app = createApp(App);

app.config.globalProperties.$enums = enums;
app.config.globalProperties.$resx = resources["VN"];

app.use(vueRouter);

app.mount("#app");
