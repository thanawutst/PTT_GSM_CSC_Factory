import * as AuthenStore from "src/store/redux/AuthenStore";
export default (store) => {
  store.dispatch(AuthenStore.AuthenActionCreators.doInit());
  
};