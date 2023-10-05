import { createApp } from "vue";
import App from "./App.vue";
import enums from "@helpers/enums";
import resources from "@helpers/resources";
import vueRouter from "@routers/index";

const app = createApp(App);

app.config.globalProperties.$enums = enums;
app.config.globalProperties.$resx = resources["VN"];

app.use(vueRouter);

app.mount("#app");
