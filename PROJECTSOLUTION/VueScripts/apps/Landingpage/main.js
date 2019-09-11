import Vue from 'vue'
// import FirstComponent from './FirstComponent.vue'
// import UserSelector from './UserSelector.vue'
import leftheader from './../../Components/LeftInnerHeader.vue'
import store from './../../store'
var app = new Vue({
  el: '#app',
  store,
  // VueRouter,
  data: {
    tittle: "New Tittle",
    message: "Hello"
  },
  methods: {
    Login() {}

  },
  components: {
    // UserSelector,
    leftheader

  },
  // created() {
  //   this.$store.dispatch('setLoginState')
  // }

})
