<template>
    <div>
        {{ api }}        
    </div>
</template>
<script>
import Vue from 'vue';
import axios from 'axios';
import VueAxios from 'vue-axios';
Vue.use(VueAxios, axios)
export default {
    name:"holamundo",
    data(){
        return {api:undefined}
    },
    mounted(){
        Vue.axios.get('http://localhost:37851/').then((resp)=>{
            console.dir(resp);
            this.api=resp.data;
        })
    }
}
</script>