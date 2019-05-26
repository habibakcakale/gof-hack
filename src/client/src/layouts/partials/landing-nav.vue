<template>
  <header class="land_header">
    <nav class="land_header_nav">
      <div class="land_header_nav_block">
        <router-link class="land_header_nav__link" :to="`/`">MEGA</router-link>
      </div>
      <div class="land_header_nav_block">
        <router-link class="land_header_nav__link" :to="`/about-product`"
          >About Product</router-link
        >
        <router-link class="land_header_nav__link" :to="`/login`"
          >Login</router-link
        >
        <router-link class="land_header_nav__link" :to="`/register`"
          >Register</router-link
        >
      </div>
    </nav>
  </header>
</template>

<style lang="scss" scoped>
.land_header {
  border: none;
  margin: 0;
  padding: 30px 0 20px;
  max-width: 1157px;
  width: 100%;
  margin: 0 auto;

  &_nav {
    display: flex;
    justify-content: space-between;
    &_block {
      display: block;
    }
    &__link {
      text-decoration: none;
      color: #5f7d95;
      margin-right: 2rem;
      font-size: 1.3rem;
      &:hover {
        color: #1273eb;
      }
    }
  }
}
</style>
