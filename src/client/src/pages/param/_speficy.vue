<template>
  <div class="about">
    <h1>{{ $route.params.speficy }}</h1>
  </div>
</template>

<script>
export default {
  name: "Params",
  mounted() {
    alert(this.$route.params.speficy);
  }
};
</script>
