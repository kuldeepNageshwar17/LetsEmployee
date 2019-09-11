<template>
  <div class="header-right-menu">
    <ul>
      <li v-if="!$store.state.IsLogedIn||$store.state.Role==='user'">
        <a href="/letsemploy/pages/postjob.php" class="post-jobs">Post jobs</a>
      </li>
      <li v-if="!$store.state.IsLogedIn">
        <a href="index.html">
          <i class="fa fa-user"></i>sign up
        </a>
      </li>
      <li v-if="!$store.state.IsLogedIn" @click.prevent="Login()">
        <a href="index.html">
          <i class="fa fa-lock"></i>login
        </a>
      </li>
      <li v-if="$store.state.IsLogedIn">
        <a href="index.html" style>
          <i class="fa fa-lock"></i>
          {{$store.state.UserName}}
        </a>
      </li>
      <li v-if="$store.state.IsLogedIn">
        <a href="index.html" @click.prevent="LogOut()">
          <i class="fa fa-lock"></i>Logout
        </a>
      </li>
    </ul>
  </div>
</template>
<script>
export default {

  name: 'leftheader',
  methods: {

    LogOut () {
      this.$store.dispatch('Logout')
    },
    Login () {
      this.$store.dispatch('LogedIn')
    }
  },

}
</script>
<style scoped>

</style>