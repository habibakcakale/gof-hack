import axios from "axios";
import Vue from "vue";
import store from "@/store";

const service = axios.create({
  baseURL: "http://gof-hack.azurewebsites.net/api",
  timeout: 5000
});

// request interceptor
service.interceptors.request.use(
  config => {
    // sending token in every request in need
    if (store.getters.token) {
      config.headers["Authorization"] = "Bearer " + store.getters.token;
    }
    return config;
  },
  error => {
    // do something with request error
    console.log(error); // for debug
    return Promise.reject(error);
  }
);

service.interceptors.response.use(
  ({ data }) => {
    const { code, isSuccess, failureMessage } = data;

    if (isSuccess == false) {
      Vue.toasted.global.network_error({
        message: failureMessage
      });
    }

    if (code === 401) {
      alert("Auth Failed");
    }
    if (code === 405) {
      alert("NetWork Error");
    }
    if (code === 500) {
      alert("NetWork Error");
    }

    return data;
  },
  error => {
    Vue.toasted.global.network_error({
      message: error.message
    });
    return Promise.reject(error);
  }
);

export default service;
