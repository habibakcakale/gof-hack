<script>
import service from "@/utils/service";
import { mapActions } from "vuex";
export default {
  name: "Register",
  layout: "public",
  data() {
    return {
      registerForm: {}
    };
  },
  methods: {
    ...mapActions("auth", ["SET_TOKEN"]),
    handleRegister() {
      // todo validation

      service.post("user/register", this.registerForm).then(({ isSuccess }) => {
        // once back-end completed, we can check here.
        // todo redirect dashboard after login
        if (isSuccess) {
          service
            .post("user/login", {
              username: this.registerForm.username,
              password: this.registerForm.password
            })
            .then(({ isSuccess, token }) => {
              if (isSuccess) {
                this.SET_TOKEN(token);
                this.$router.push("dashboard");
              }
            });
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
              <v-toolbar-title
                >Register - Let's make estimation great again!</v-toolbar-title
              >
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field
                  v-model="registerForm.username"
                  name="name"
                  label="Email"
                  type="text"
                ></v-text-field>
                <v-text-field
                  v-model="registerForm.password"
                  append-icon="visibility"
                  name="password"
                  label="Password"
                  type="password"
                ></v-text-field>
                <v-text-field
                  v-model="registerForm.passwordConfirm"
                  append-icon="visibility"
                  name="password-confirm"
                  label="Password Confirm"
                  type="password"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="success" @click="handleRegister">Login</v-btn>
            </v-card-actions>
          </v-card>

          {{ registerForm }}
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>
