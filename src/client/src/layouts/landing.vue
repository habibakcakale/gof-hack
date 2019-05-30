<template>
  <div>
    <landingNav />
    <router-view />
  </div>
</template>

<script>
import landingNav from "./partials/landing-nav";
export default {
  components: {
    landingNav
  }
};
</script>
