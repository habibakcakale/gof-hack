<script>
import service from "@/utils/service";
import { mapActions } from "vuex";
export default {
  name: "Login",
  layout: "public",
  data() {
    return {
      loginForm: {},
      isRequired: [v => !!v || "Password is required"],
      emailRules: [
        v => !!v || "E-mail is required",
        v => /.+@.+/.test(v) || "E-mail must be valid"
      ]
    };
  },
  methods: {
    ...mapActions("auth", ["SET_TOKEN"]),
    handleLogin() {
      // todo validation
      if (this.$refs.loginForm.validate()) {
        service
          .post("user/login", this.loginForm)
          .then(({ isSuccess, token }) => {
            if (isSuccess) {
              this.SET_TOKEN(token);
              this.$router.push("/dashboard");
            }
          });
      }
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
              <v-form ref="loginForm">
                <v-text-field
                  v-model="loginForm.username"
                  validate-on-blur
                  prepend-icon="account_circle"
                  :rules="emailRules"
                  name="login"
                  label="Login"
                  type="text"
                ></v-text-field>
                <v-text-field
                  id="password"
                  v-model="loginForm.password"
                  validate-on-blur
                  :rules="isRequired"
                  prepend-icon="fingerprint"
                  name="password"
                  label="Password"
                  type="password"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <div class="ml-3 mr-3">
                <v-btn
                  color="primary"
                  class="text-lowercase"
                  to="/register"
                  flat
                >
                  Create Account
                </v-btn>
              </div>
              <v-btn color="success text-lowercase" @click="handleLogin"
                >Login</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>
