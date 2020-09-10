import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';


Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: 'http://localhost:3000',
    name: 'api',
    component: Home
  }
];


const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
