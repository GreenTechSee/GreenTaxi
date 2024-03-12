<template>
	<nav class="navbar navbar-expand fixed-bottom bg-body-tertiary">
		<div class="container-fluid justify-content-around mx-3">
			<a class="btn" href="#/alerts" :style="isItemActive('alerts') ? 'color: #1D424D;' : 'color: #387f93;'">
				<i class="fa-bell" :class="isItemActive('alerts') ? 'fa-solid': 'fa-light'"></i><br>
				VARSEL
			</a>
			<a class="btn" href="#/myHome" :style="isItemActive('myhome') ? 'color: #1D424D;' : 'color: #387f93;'">
				<i class="fa-home" :class="isItemActive('myHome') ? 'fa-solid': 'fa-light'"></i><br>
				MITT HJEM
			</a>
			<a class="btn" href="#/map" :style="isItemActive('map') ? 'color: #1D424D;' : 'color: #387f93;'">
				<i class="fa-map" :class="isItemActive('map') ? 'fa-solid': 'fa-light'"></i><br>
				KART
			</a>
			<a class="btn" href="#/help" :style="isItemActive('help') ? 'color: #1D424D;' : 'color: #387f93;'">
				<i class="fa-info" :class="isItemActive('help') ? 'fa-solid': 'fa-light'"></i><br>
				INFO
			</a>
		</div>
	</nav>
</template>

<style>

</style>

<script setup lang="ts">
import { useRouter } from "vue-router";

const router = useRouter();

function isItemActive(item: string) {
	if (router) {
		return ("./#" + router.currentRoute.value.fullPath).toLowerCase().includes(item.toLowerCase());
	}
}

</script>