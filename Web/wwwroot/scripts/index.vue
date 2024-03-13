<template>
	<div class="container mt-5">
		<h1>SafeHaven</h1>
		<p>Velkommen til ditt innloggings-dashboard</p>
		<div class="row">
			<div class="col-3 border rounded p-5 m-3 text-center">
				I dag er det<br />
				{{ moment().format("DD.MM.YYYY") }}
			</div>
			<div class="col-3 border rounded p-5 m-3 text-center">
				<a href="/Access/Login" class="btn btn-primary mt-3">Logg inn</a>
			</div>
			<div class="col-3 border rounded p-5 m-3 text-center">
				Klokken er<br />
				{{ timeStr }}
			</div>
		</div>
	</div>
</template>
<script lang="ts" setup>
import moment from "moment";
import { ref } from "vue";

const timeStr = ref("");

function updateClock() {
	timeStr.value = moment().format("HH:mm:ss");
}

updateClock();
setInterval(updateClock, 1000);
</script>