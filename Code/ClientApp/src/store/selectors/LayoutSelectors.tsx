import { createSelector } from 'reselect';

const selectRaw = (state) => state.layout;

const selectMenuVisible = createSelector(
    [selectRaw],
    (layout) => Boolean(layout.menuVisible),
);

const selectCollapseMenu = createSelector(
    [selectRaw],
    (layout) => layout.collapse,
);

const selectLoading = createSelector(
    [selectRaw],
    (layout) => Boolean(layout.loading),
);

const LayoutSelectors = {
    selectRaw,
    selectMenuVisible,
    selectLoading,
    selectCollapseMenu,

};
export default LayoutSelectors;

