<template>
  <div>
    <v-toolbar fixed app :color="`elevation-0`">
      <v-title>JAPE</v-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat>Login</v-btn>
        <v-btn flat>Register</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <router-view />
  </div>
</template>
