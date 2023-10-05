import path from 'path';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "src"),
      "@configs": path.resolve(__dirname, "src", "configs"),
      "@components": path.resolve(__dirname, "src", "components"),
      "@views": path.resolve(__dirname, "src", "views"),
      "@assets": path.resolve(__dirname, "src", "assets"),
      "@routers": path.resolve(__dirname, "src", "routers"),
      "@services": path.resolve(__dirname, "src", "services"),
      "@helpers": path.resolve(__dirname, "src", "helpers"),
    },
  },
});
