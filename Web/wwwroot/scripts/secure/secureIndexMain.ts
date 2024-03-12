import "@/core";
import { createApp } from "vue";
import FloatingVue from "floating-vue";
import { createRouter, createWebHistory } from "vue-router";
import NotFound from "@/scripts/notFound.vue";
import SecureIndexLayout from "@/scripts/secure/secureIndexLayout.vue";
import Alerts from "@/scripts/secure/alerts.vue";

const routes = [
	{ path: "/:pathMatch(.*)", component: NotFound },
	{ path: "/", component: Alerts }
];

const router = createRouter({
	history: createWebHistory("/Secure/#/"),
	routes
});

const app = createApp(SecureIndexLayout);
app.use(FloatingVue);
app.use(router);
app.mount("#entrypoint");
