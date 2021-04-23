import { createSelector } from 'reselect';

const selectRaw = (state) => state.authen;

const selectCurrentUser = createSelector(
    [selectRaw],
    (authen) => authen.currentUser,
);

const selectLoadingInit = createSelector(
    [selectRaw],
    (authen) => Boolean(authen.loadingInit)

);


const selectLoading = createSelector(
    [selectRaw],
    (authen) => Boolean(authen.loading),
);

const selectErrorMessage = createSelector(
    [selectRaw],
    (authen) => authen.Message,
);

const selectMenu = createSelector(
    [selectRaw],
    (authen) => authen.MenuArray,
);

const selectInputLogin = createSelector(
    [selectRaw],
    (authen) => authen.InputLogin,
);

const AuthenSelectors = {
    selectRaw,
    selectCurrentUser,
    selectLoading,
    selectErrorMessage,
    selectLoadingInit,
    selectMenu,
    selectInputLogin,

}
export default AuthenSelectors;