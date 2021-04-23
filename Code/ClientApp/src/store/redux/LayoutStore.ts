import { Action, Reducer } from 'redux';

export interface LayoutInitalState {
    menuVisible: boolean;
    loading: boolean,
    collapse: object,
}
export const LayoutInitalState = {
    menuVisible: true, loading: false, collapse: {}
}
const prefix = 'LAYOUT';
export const layoutActions = {
    MENU_TOGGLE: `${prefix}_MENU_TOGGLE`,
    MENU_HIDE: `${prefix}_MENU_HIDE`,
    MENU_SHOW: `${prefix}_MENU_SHOW`,
    MENU_COLLAPSE: `${prefix}_MENU_COLLAPSE`,

    
}


export const LayoutActionCreators = {
    doToggleMenu: () => ({ type: layoutActions.MENU_TOGGLE }),
    doShowMenu: () => ({ type: layoutActions.MENU_SHOW }),
    doHideMenu: () => ({ type: layoutActions.MENU_HIDE }),
    doCollapseMenu: (key) => (dispatch) => {
        dispatch({
            type: layoutActions.MENU_COLLAPSE,
            payload: {
                key
            }
        });
    },
    
}

export const layoutReducer = (state, { type, payload }) => {
    if (state === undefined) {
        return LayoutInitalState;
    }

    switch (type) {
        case layoutActions.MENU_TOGGLE:
            return {
                ...state,
                menuVisible: !state.menuVisible,
            };
        case layoutActions.MENU_SHOW:
            return {
                ...state,
                menuVisible: true,
            };
        case layoutActions.MENU_HIDE:
            return {
                ...state,
                menuVisible: false,
            };
        case layoutActions.MENU_COLLAPSE:
            return {
                ...state,
                collapse: payload.key,
            };
        default:
            return state;
    }
};

