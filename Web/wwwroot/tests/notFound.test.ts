// @ts-nocheck // trenger ikke typecheck i testene

import { mount, flushPromises } from "@vue/test-utils";
import { test, expect } from "vitest";
import NotFound from "@/scripts/notFound.vue";

test("mount notFound", async () => {

	//////////////////////////////////////////////////////////////////////
	// mocking av data

	//////////////////////////////////////////////////////////////////////
	// mount komponenten

	const wrapper = mount(NotFound, {
	});

	//////////////////////////////////////////////////////////////////////
	// vent på at mounting blir ferdig
	await flushPromises();

	//////////////////////////////////////////////////////////////////////
	// her kommer testene

	// bruk denne ved skriving av flere tester
	// console.log(wrapper.html());

	expect(wrapper.text()).toBe(`Fant ikke siden.Tilbake til forsiden`);
});
