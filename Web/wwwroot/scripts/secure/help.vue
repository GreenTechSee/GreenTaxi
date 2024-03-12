<template>
	<div class="container mb-5">
		<h1 class="text-center">INFO</h1>
		<div class="d-flex justify-content-center mt-3">
			<div>
				<iframe 
					src="https://www.youtube.com/embed/e75m35UZW3E?si=ZWfsv9WE8QCirihZ" 
					title="YouTube video player" 
					frameborder="0" 
					allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
					allowfullscreen>
				</iframe>
				<div class="text-center"><small>Video: hjerte- og lungeredning</small></div>
			</div>
		</div>
		
		<div class="d-flex justify-content-center mt-5">
			<div>
				<iframe 
					src="https://www.youtube.com/embed/lLkw4BXa7pQ?si=Ktj8JybjUmXEJMWU" 
					title="YouTube video player" 
					frameborder="0" 
					allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
					allowfullscreen>
				</iframe>
				<div class="text-center"><small>Video: Behandling av hoggormbitt (på engelsk)</small></div>
			</div>
		</div>

		<div class="d-flex justify-content-center mt-5">
			<div>
				<iframe 
					src="https://www.youtube.com/embed/FwerEKvJjSA?si=7tjEonIKqUdC58MT" 
					title="YouTube video player" 
					frameborder="0" 
					allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
					allowfullscreen>
				</iframe>
				<div class="text-center"><small>Video: Beredskap fra Røde Kors</small></div>
			</div>
		</div>
		<div class="d-flex justify-content-center mt-5">
			<div>
				<iframe 
					src="https://www.youtube.com/embed/ZB6LIlCTrC4?si=aYj4dlQTpqZ4WYZp" 
					title="YouTube video player" 
					frameborder="0" 
					allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" 
					allowfullscreen>
				</iframe>
				<div class="text-center"><small>Video: Stabilt sideleie</small></div>
			</div>
		</div>
		<div v-if="showChat" style="position: fixed; bottom: 0; right: 0; z-index: 100; margin-bottom: 165px; margin-right: 10px;">
			<div class="bg-white border rounded p-3" style="height: 500px; width: 300px;">
				<div class="overflow-auto" style="height: 420px;">
					<div v-for="chat in myChats" :key="chat.text" class="alert" :class="chat.isFromMe ? 'alert-light' : 'alert-info'">{{ chat.text }}</div>
				</div>
				<input type="text" class="form-control mt-3" placeholder="Skriv spørsmål her" v-model="message" @keyup.enter="myChats.push(message); message = ''">
			</div>
		</div>
		<div style="position: fixed; bottom: 0; right: 0; z-index: 100; margin-bottom: 100px; margin-right: 10px;">
			<button type="button" class="btn btn-primary btn-lg rounded-circle" aria-label="Chat" style="height: 60px; width: 60px;" @click="showChat = !showChat">
				<i class="fa-solid fa-robot"></i>
			</button>
		</div>
	</div>
</template>

<script lang="ts" setup>
	import { ref } from "vue";

	const showChat = ref(false);
	const myChats = ref<{ text: string; isFromMe: boolean; }[]>([{ isFromMe: false, text:"Hei, jeg er chat gpt. Jeg kan svare på spørsmål om beredskap og førstehjelp. Det er viktig å huske at jeg bare er en robot og du alltid snakke med erfarende fag folk først. Det er mer jeg ikke vet enn du tror."} ]);
	const message = ref("");
</script>