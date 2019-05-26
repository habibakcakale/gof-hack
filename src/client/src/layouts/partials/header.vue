<template lang="html">
  <header>
    <v-navigation-drawer v-model="drawer" fixed app>
      <v-list dense>
        <v-list-tile :to="`dashboard`">
          <v-list-tile-action>
            <v-icon>fa-chart-line</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Dashboard</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile :to="`ab`">
          <v-list-tile-action>
            <v-icon>fa-project-diagram</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>About Product</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar color="indigo" dark fixed app>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>GanG Of Four</v-toolbar-title>
    </v-toolbar>
  </header>
</template>

<script lang="js">
  export default  {
    name: 'Header',
    props: [],
    data: () => ({
      drawer: false
    }),
    computed: {

    },
    mounted() {

    },
    methods: {

    }
}
</script>

<style scoped lang="scss"></style>
