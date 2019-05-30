import Vue from "vue";
import Toasted from "vue-toasted";

Vue.use(Toasted);

// options
let options = {
  type: "error",
  icon: "error_outline",
  duration: 3000,
  position: "top-center"
};

Vue.toasted.register(
  "network_error",
  payload => {
    if (!payload.message) {
      return "Oops.. Something Went Wrong..";
    }
    return "Oops.. " + payload.message;
  },
  options
);
