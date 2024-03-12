import "@/core";
import { createApp } from "vue";
import FloatingVue from "floating-vue";
import { createRouter, createWebHistory } from "vue-router";
import NotFound from "@/scripts/notFound.vue";
import SecureIndexLayout from "@/scripts/secure/secureIndexLayout.vue";
import Alerts from "@/scripts/secure/alerts.vue";
import MyProfile from "@/scripts/secure/myProfile.vue";
import Help from "@/scripts/secure/help.vue";
import Map from "@/scripts/secure/map.vue";
import MyHome from "@/scripts/secure/myHome.vue";

const routes = [
	{ path: "/:pathMatch(.*)", component: NotFound },
	{ path: "/", component: Alerts },
	{ path: "/myProfile", component: MyProfile },
	{ path: "/alerts", component: Alerts },
	{ path: "/myHome", component: MyHome },
	{ path: "/map", component: Map },
	{ path: "/help", component: Help }
];

const router = createRouter({
	history: createWebHistory("/Secure/#/"),
	routes
});

const app = createApp(SecureIndexLayout);
app.use(FloatingVue);
app.use(router);
app.mount("#entrypoint");
