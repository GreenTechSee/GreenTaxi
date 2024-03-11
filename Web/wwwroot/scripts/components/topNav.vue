<template>
	<div class="d-flex w-100">
		<div class="me-3">Logo her</div>
		<h1>Status - 
			<span v-if="status == 0">Laster</span>
			<span v-if="status == 1">Normal</span>
			<span v-if="status == 2">Risikabel</span>
			<span v-if="status == 3">Kritisk</span>
		</h1>
		
	</div>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue";

const status = ref(0);

onMounted(async () => {
	const api = new ApiClient("");

	const statusString = sessionStorage.getItem("topnav::status");
	status.value = statusString ? JSON.parse(statusString) : await api.getStatus();
	if (!statusString) {
		sessionStorage.setItem("topnav::status", JSON.stringify(status.value));
	}
});
</script>