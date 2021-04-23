import { Action, Reducer } from 'redux';
import AuthenService from "src/Service/AuthenService";
import { AuthToken } from "src/Service/Config/AuthToken";
import { getHistory } from "src/App";

interface LoginInitial {
    sUsername: string,
    sPassword: string
}
export interface AuthenInitialState {
    currentUser: any,
    Message: string,
    loading: boolean,
    loadingInit: boolean,
    MenuArray: any,
    InputLogin: LoginInitial,
}

export const AuthenInitialState: AuthenInitialState = {
    currentUser: {},
    Message: "",
    loading: false,
    loadingInit: true,
    MenuArray: [],
    InputLogin: {} as LoginInitial,
}

const prefix = 'AUTHEN';
export const authenActions = {
    ERROR_MESSAGE_CLEARED: `${prefix}_ERROR_MESSAGE_CLEARED`,

    AUTH_INIT_SUCCESS: `${prefix}_INIT_SUCCESS`,
    AUTH_INIT_ERROR: `${prefix}_INIT_ERROR`,

    AUTH_INITMENU_SUCCESS: `${prefix}_INITMENU_SUCCESS`,
    AUTH_INITMENU_ERROR: `${prefix}_INITMENU_ERROR`,

    AUTH_START: `${prefix}_AUTH_START`,
    AUTH_SUCCESS: `${prefix}_AUTH_SUCCESS`,
    AUTH_ERROR: `${prefix}_AUTH_ERROR`,


}

export const AuthenActionCreators = {
    doClearErrorMessage: () => {
        return {
            type: authenActions.ERROR_MESSAGE_CLEARED,
        };
    },
    doLoginWithUsernameAndPassword: (dataLogin) => async (dispatch) => {
        try {
            let InputLogin = {} as LoginInitial;
            InputLogin = dataLogin;
            dispatch({
                type: authenActions.AUTH_START,
                payload: {
                    InputLogin,
                },
            });

            let currentUser = null;

            const dataCreatedToken = await AuthenService.CreateTokenLogin(dataLogin);
            if (dataCreatedToken.data.code === 200) {
                const token = dataCreatedToken.data.token;

                AuthToken.set(token)
                const DataRes = await AuthenService.DataUserAppBar();

                currentUser = DataRes.data;
                InputLogin.sUsername = dataLogin.sUsername;
                InputLogin.sPassword = "";
                await dispatch({
                    type: authenActions.AUTH_SUCCESS,
                    payload: {
                        currentUser,
                        InputLogin,
                    },
                });
                getHistory().push('/');
            } else {
                let Message = dataCreatedToken.data.message;
                InputLogin = dataLogin;
                dispatch({
                    type: authenActions.AUTH_ERROR,
                    payload: {
                        Message,
                        InputLogin,
                    },
                });
            }

        } catch (error) {
            await AuthenService.SignOut();

            dispatch({
                type: authenActions.AUTH_ERROR,
                payload: {
                    currentUser: {},
                    Message: error,
                }

            });
        }
    },
    doInit: () => async (dispatch) => {
        try {
            const token = AuthToken.get();
            let currentUser = null;

            if (token) {

                const DataInit = await AuthenService.DataUserAppBar();
                currentUser = DataInit.data;

                dispatch({
                    type: authenActions.AUTH_INIT_SUCCESS,
                    payload: {
                        currentUser,
                    },
                });
            }
        } catch (error) {
            await AuthenService.SignOut();
            dispatch({
                type: authenActions.AUTH_INIT_ERROR,
                payload: error,
            });
        }
    },
    doInitMenu: () => async (dispatch) => {
        try {
            let MenuArray = null;
            const resultMenu = await AuthenService.GetMenuWithPermission();
            MenuArray = resultMenu.data.data;

            dispatch({
                type: authenActions.AUTH_INITMENU_SUCCESS,
                payload: {
                    MenuArray,
                },
            });
        } catch (error) {
            await AuthenService.SignOut();
            dispatch({
                type: authenActions.AUTH_INITMENU_ERROR,
                payload: error,
            });
        }
    },
    doSignout: () => async (dispatch) => {
        try {
            dispatch({
                type: authenActions.AUTH_START,
                payload: {
                    InputLogin: {},
                },
            });
            await AuthenService.SignOut();

            dispatch({
                type: authenActions.AUTH_SUCCESS,
                payload: {
                    currentUser: null,
                    MenuArray: [],
                    InputLogin: {},
                },
            });
        } catch (error) {


            dispatch({
                type: authenActions.AUTH_ERROR,
            });
        }
    },



}

export const AuthenReducer = (state = AuthenInitialState, { type, payload }) => {

    if (state === undefined) {
        return AuthenInitialState;
    }
    switch (type) {
        case authenActions.ERROR_MESSAGE_CLEARED:
            return {
                ...state,
                loading: false,
            };
        case authenActions.AUTH_START:
            return {
                ...state,
                currentUser: {},
                MenuArray: [],
                InputLogin: payload.InputLogin || {},
                Message: "",
                loading: true
            }
        case authenActions.AUTH_SUCCESS:

            return {
                ...state,
                currentUser: payload.currentUser || {},
                InputLogin: payload.InputLogin || {},
                Message: "",
                loading: true,
                loadingInit: false,
            }
        case authenActions.AUTH_ERROR:
            return {
                ...state,
                currentUser: {},
                Message: payload.Message,
                InputLogin: payload.InputLogin || {},
                loading: false
            }
        case authenActions.AUTH_INIT_SUCCESS:

            return {
                ...state,
                Message: payload.Message,
                loadingInit: false,
                currentUser: payload.currentUser || {},
            }
        case authenActions.AUTH_INITMENU_SUCCESS:
            return {
                ...state,
                MenuArray: payload.MenuArray || [],
            }
        case authenActions.AUTH_INITMENU_ERROR:
            return {
                ...state,
                MenuArray: [],
            }
        default:
            return {
                ...state,
                loadingInit: false
            };
    }
}