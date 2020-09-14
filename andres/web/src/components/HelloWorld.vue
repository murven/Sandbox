<template>
  <div class="hello">
    '<div id="app">{{ message }}</div>'
  </div>
</template>

<script>
import Vue from 'vue';
import axios from 'axios'

new Vue({
  el: '#app',
  data () {
    return {
      info: 'NULL'
    }
  },
  mounted () {
    axios
      .get('http://localhost:37851/')
      .then(response => (this.info = response))
  }
})

export default {
  name: 'HelloWorld',
  props: {
    msg: String
  }
}
</script>
