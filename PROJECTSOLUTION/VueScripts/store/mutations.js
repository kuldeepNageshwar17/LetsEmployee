import state from './state'

export default {
  LogedIn: (state, ({
    UserName,
    Role
  }) => {
    state.UserName = UserName;
    state.Role = Role;
    state.IsLogedIn = true;
  }),
  Logout: (state) => {
    console.log('Mutation Call');
    state.UserName = '';
    state.Role = '';
    state.IsLogedIn = false;
  }
}
