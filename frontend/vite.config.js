import path from "path";
import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [vue()],
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "src"),
            "@constants": path.resolve(__dirname, "src", "constants"),
            "@components": path.resolve(__dirname, "src", "components"),
            "@views": path.resolve(__dirname, "src", "views"),
            "@assets": path.resolve(__dirname, "src", "assets"),
            "@router": path.resolve(__dirname, "src", "router"),
            "@services": path.resolve(__dirname, "src", "services"),
            "@utils": path.resolve(__dirname, "src", "utils"),
        },
    },
});
