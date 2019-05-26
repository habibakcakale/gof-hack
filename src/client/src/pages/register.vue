<script>
import service from "@/utils/service";
import { mapActions } from "vuex";
export default {
  name: "Register",
  layout: "public",
  data() {
    return {
      e1: 0,
      registerForm: {},
      textField: "label",
      valueField: "value",
      roles: [
        { label: "Frontend Developer", value: "FD" },
        { label: "Backend Developer", value: "BD" },
        { label: "Quality Assurance", value: "QA" },
        { label: "Bussiness Analyst", value: "BA" },
        { label: "Solution Architect", value: "SA" },
        { label: "Project Manager", value: "PM" }
      ],
      levels: [
        { label: "Junior 0", value: "J0" },
        { label: "Junior 1", value: "J1" },
        { label: "Junior 2", value: "J2" },
        { label: "Intermediate 1", value: "I1" },
        { label: "Intermediate 2", value: "I2" },
        { label: "Senior 1", value: "S1" },
        { label: "Senior 2", value: "S2" }
      ],
      emailRules: [
        v => !!v || "E-mail is required",
        v => /.+@.+/.test(v) || "E-mail must be valid"
      ],
      isRequired: [v => !!v || "This input is required"]
    };
  },
  methods: {
    ...mapActions("auth", ["SET_TOKEN"]),
    handleContinue() {
      if (this.$refs.registerForm1.validate()) {
        this.e1 = 2;
      }
    },
    handleRegister() {
      // todo validation

      if (this.$refs.registerForm2.validate()) {
        service
          .post("user/register", this.registerForm)
          .then(({ isSuccess }) => {
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
  }
};
</script>

<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-toolbar dark color="primary">
            <v-toolbar-title
              >Register - Let's make estimation great again!</v-toolbar-title
            >
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-stepper v-model="e1">
            <v-stepper-header>
              <v-stepper-step :complete="e1 > 1" step="1"
                >Register Credentials</v-stepper-step
              >

              <v-divider></v-divider>

              <v-stepper-step :complete="e1 > 2" step="2"
                >Project Role</v-stepper-step
              >
            </v-stepper-header>

            <v-stepper-items>
              <v-stepper-content step="1">
                <v-card class="elevation-12">
                  <v-card-text>
                    <v-form ref="registerForm1">
                      <v-text-field
                        v-model="registerForm.username"
                        :rules="emailRules"
                        validate-on-blur
                        name="name"
                        label="Email"
                        type="text"
                      ></v-text-field>
                      <v-text-field
                        v-model="registerForm.password"
                        :rules="isRequired"
                        validate-on-blur
                        append-icon="visibility"
                        name="password"
                        label="Password"
                        type="password"
                      ></v-text-field>
                      <v-text-field
                        v-model="registerForm.passwordConfirm"
                        :rules="isRequired"
                        validate-on-blur
                        append-icon="visibility"
                        name="password-confirm"
                        label="Password Confirm"
                        type="password"
                      ></v-text-field>
                    </v-form>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="primary" @click="handleContinue"
                      >Continue</v-btn
                    >
                  </v-card-actions>
                </v-card>
              </v-stepper-content>

              <v-stepper-content step="2">
                <v-card class="elevation-12">
                  <v-card-text>
                    <v-form ref="registerForm2">
                      <v-select
                        v-model="registerForm.role"
                        :rules="isRequired"
                        validate-on-blur
                        :items="roles"
                        :item-value="valueField"
                        :item-text="textField"
                        label="Role"
                      ></v-select>

                      <v-select
                        v-model="registerForm.level"
                        :rules="isRequired"
                        validate-on-blur
                        :items="levels"
                        :item-value="valueField"
                        :item-text="textField"
                        label="Level"
                      ></v-select>
                    </v-form>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="success" @click="handleRegister"
                      >Submit</v-btn
                    >
                    <v-btn flat @click="e1 = 1">Cancel</v-btn>
                  </v-card-actions>
                </v-card>
              </v-stepper-content>
            </v-stepper-items>
          </v-stepper>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>
