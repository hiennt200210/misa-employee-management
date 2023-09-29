import { createApp } from "vue";
import App from "./App.vue";
import axios from "axios";
import emitter from "tiny-emitter/instance";
import enums from "./helpers/enums.js";
import resources from "./helpers/resources.js";
import helpers from "./helpers/helpers";
import vueRouter from "./routers/index";

const app = createApp(App);

app.config.globalProperties.$axios = axios;
app.config.globalProperties.$emitter = emitter;
app.config.globalProperties.$enums = enums;
app.config.globalProperties.$helpers = helpers;
app.config.globalProperties.$langCode = "VN";
app.config.globalProperties.$resx = resources;

app.use(vueRouter);

app.mount("#app");
