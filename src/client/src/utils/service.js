import axios from "axios";
import Vue from "vue";
import store from "@/store";
import router from "@/router";
import { removeToken } from "@/utils/token";

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
  res => res.data,
  error => {
    var response = error.response;
    var message = "";
    if (response && response.data && response.data) {
      var data = response.data;
      if (data && data.errors) {
        message = Object.keys(response.data.errors).map(item => response.data.errors[item]).join("<br/>");
      } else if (data.failureMessage) {
        message = data.failureMessage;
      } else {
        message = error.message;
      }
    }
    else
      message = error.message;
    Vue.toasted.global.network_error({
      message: message
    });

    if (error.response.status == 401) {
      removeToken();
      router.push("/login");
    }

    return Promise.reject(error);
  }
);

export default service;
