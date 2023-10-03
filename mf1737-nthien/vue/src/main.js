import { createApp } from "vue";
import App from "./App.vue";
import axios from "axios";
import emitter from "tiny-emitter/instance";
import enums from "@helpers/enums";
import resources from "@helpers/resources";
import helpers from "@helpers/helpers";
import vueRouter from "./routers/index";

const app = createApp(App);

app.config.globalProperties.$axios = axios;
app.config.globalProperties.$emitter = emitter;
app.config.globalProperties.$enums = enums;
app.config.globalProperties.$helpers = helpers;
app.config.globalProperties.$resxLang = resources["VN"];

app.use(vueRouter);

app.mount("#app");
