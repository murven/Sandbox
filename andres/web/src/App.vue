<template>
  <div id="app">
      http://localhost:37851/:
   <holamundo/>
  </div>
</template>

<script>
import holamundo from './components/holamundo'

export default {
  name: 'App',
  components: {
    holamundo
  }
}
</script>

<style>
#app {
  text-align: center;
}
</style>
