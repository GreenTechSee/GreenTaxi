<template>
	<top-nav></top-nav>
	<div class="container-fluid mt-3 mb-5 pb-5">
		<router-view></router-view>
	</div>
	<bottom-nav></bottom-nav>
</template>
<script lang="ts" setup>
import topNav from "@/scripts/components/topNav.vue";
import bottomNav from "@/scripts/components/bottomNav.vue";
</script>