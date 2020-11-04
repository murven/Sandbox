<template>
  <div id="app">
    <HelloWorld v-bind:baseURL="baseURL"/>
    <VideoAsk1/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from './components/HelloWorld.vue';
import VideoAsk1 from './components/VideoAsk1.vue';

@Component({
  components: {
    HelloWorld,
    VideoAsk1
  },
})
export default class App extends Vue {
  private baseURL = 'http://localhost:37851/';
}

</script>


