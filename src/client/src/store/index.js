import Vue from "vue";
import Vuex from "vuex";

import auth from "./modules/auth";
Vue.use(Vuex);

import getters from "./getters";

const store = new Vuex.Store({
  modules: {
    auth
  },
  getters
});

export default store;
