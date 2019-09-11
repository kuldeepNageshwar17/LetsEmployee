import axios from 'axios'

export default {

  LogedIn({
    commit
  }, payload) {

    commit('LogedIn', payload)
  },
  Logout({
    commit
  }) {
    console.log('Mutation Call');
    commit('Logout');
  },
  setLoginState: ({
    commit

  }) => {
    if (this.localStorage.getItem())
      axios
      .get('/Api/account/CheckLogin')
      .then(response => (this.info = response))
  },
  addPet: ({
    commit
  }, payload) => {
    commit('appendPet', payload)
  }
}
