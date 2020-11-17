<template>
  <div class="hello">
    <h1 v-text="msg"></h1>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Axios from 'axios';

@Component
export default class HelloWorld extends Vue {
  @Prop() private baseURL!: string;

  private msg = '';

  private mounted() {
    Axios.get(this.baseURL)
      .then((res) => {
        this.msg = res.data;
      })
      .catch((error) => {
        this.msg = error;
      });
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->

