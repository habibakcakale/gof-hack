import { getToken, setToken, removeToken } from "@/utils/token";

const state = {
  token: getToken()
};

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token;
  }
};

const actions = {
  SET_TOKEN: ({ commit }, token) => {
    commit("SET_TOKEN", token);
    setToken(token);
  },
  REMOVE_TOKEN: ({ commit }) => {
    commit("SET_TOKEN", null);
    removeToken("");
  }
};

export default {
  namespaced: true,
  state,
  mutations,
  actions
};
