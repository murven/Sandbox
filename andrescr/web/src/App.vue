<template>
  <div id="app">
    <HelloWorld v-bind:baseURL="baseURL"/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from './components/HelloWorld.vue';

@Component({
  components: {
    HelloWorld,
  },
})
export default class App extends Vue {
  private baseURL = 'http://localhost:37851/';
}

</script>


