<script>
import service from "@/utils/service";
import { mapActions } from "vuex";
export default {
  name: "Login",
  layout: "public",
  data() {
    return {
      loginForm: {}
    };
  },
  methods: {
    ...mapActions("auth", ["SET_TOKEN"]),
    handleLogin() {
      // todo validation

      service
        .post("user/login", this.loginForm)
        .then(({ isSuccess, token }) => {
          console.log("[login.vue] Request successful");
          // once back-end completed, we can check here.
          // todo redirect dashboard after login
          if (isSuccess) {
            this.SET_TOKEN(token);
            this.$router.push("dashboard");
          }
        });
    }
  }
};
</script>

<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Login form</v-toolbar-title>
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field
                  v-model="loginForm.username"
                  prepend-icon="fa-facebook"
                  name="login"
                  label="Login"
                  type="text"
                ></v-text-field>
                <v-text-field
                  id="password"
                  v-model="loginForm.password"
                  prepend-icon="fa-twitter"
                  name="password"
                  label="Password"
                  type="password"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="handleLogin">Login</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>
