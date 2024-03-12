<template>
	<top-nav></top-nav>
	<router-view></router-view>
	<bottom-nav></bottom-nav>
</template>
<script lang="ts" setup>
import topNav from "@/scripts/components/topNav.vue";
import bottomNav from "@/scripts/components/bottomNav.vue";
</script>