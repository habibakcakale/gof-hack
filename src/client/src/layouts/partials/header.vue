<template>
  <header>
    <v-toolbar color="indigo" dark fixed app>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>GanG Of Four</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat @click="handleLogout">
          <v-icon class="ml-2 mr-2">exit_to_app</v-icon> Logout
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>
  </header>
</template>

<script lang="js">
  import { mapActions } from "vuex";
  export default  {
    name: 'Header',
    methods: {
      ...mapActions("auth", ["REMOVE_TOKEN"]),
      handleLogout(){
        this.REMOVE_TOKEN();
        this.$router.push('/');
      }
    }
}
</script>
