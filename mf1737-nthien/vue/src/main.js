import { createApp } from "vue";
import App from "./App.vue";

import axios from "axios";
import emitter from "tiny-emitter/instance";

import MLabel from "./components/base/label/MLabel.vue";
import MCheckbox from "./components/base/checkbox/MCheckbox.vue";
import MSelect from "./components/base/dropdown/MSelect.vue";
import MSelectWithLabel from "./components/base/dropdown/MSelectWithLabel.vue";

import MISAEnum from "./scripts/MISAEnum";
import MISAResource from "./scripts/MISAResource";
import helpers from "./scripts/helpers";

import vueRouter from "./router/index";

const app = createApp(App);

app.component("MLabel", MLabel);
app.component("MCheckbox", MCheckbox);
app.component("MSelect", MSelect);
app.component("MSelectWithLabel", MSelectWithLabel);

app.config.globalProperties.$MISAEnum = MISAEnum;
app.config.globalProperties.$MISAResource = MISAResource;
app.config.globalProperties.$helpers = helpers;
app.config.globalProperties.$languageCode = "VN";
app.config.globalProperties.$language = "VN";
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$emitter = emitter;
app.config.globalProperties.$Resource = MISAResource["$language"];

app.use(vueRouter);

app.mount("#app");

export default app;
