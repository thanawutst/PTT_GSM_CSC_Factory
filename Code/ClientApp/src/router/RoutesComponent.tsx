import React, { useEffect, useRef } from "react";
import { useSelector, useDispatch } from 'react-redux';
import AuthenSelectors from "src/store/selectors/AuthenSelectors";
import LayoutSelectors from "src/store/selectors/LayoutSelectors";
import * as AuthenStore from "src/store/redux/AuthenStore";
import { Switch, Route, useLocation } from "react-router-dom";
import { ProgressBar } from "src/router/LoadableComponent/LoadingComponent";
import _blankRoute from "src/router/_blankRoute";
import _CommonRoute from "src/router/_CommonRoute";
import NotMatch from "src/router/NotMatch";
import Customloadable from "src/router/LoadableComponent/Customloadable";
import routes from "src/router/routes";



function RoutesComponent() {
    const isInitialMount = useRef(true);
    const dispatch = useDispatch();

    const authLoading = useSelector(
        AuthenSelectors.selectLoadingInit,
    );

    const layoutLoading = useSelector(
        LayoutSelectors.selectLoading,
    );

    const currentUser = useSelector(
        AuthenSelectors.selectCurrentUser,
    );

    const loading = authLoading || layoutLoading;
    useEffect(() => {
        if (isInitialMount.current) {
            isInitialMount.current = false;
            ProgressBar.start();

            return;
        }

        if (!loading) {

            ProgressBar.done();
        }

    }, [loading, authLoading, layoutLoading]);


    if (loading) {
        return <div />;
    }

    return (
        <Switch>
            {routes._blankRoute.map((route) => (
                <_blankRoute
                    exact
                    key={route.path}
                    path={route.path}
                    component={Customloadable({
                        loader: route.loader,
                    })}
                />
            ))}
            {routes._CommonRoute.map((route) => (
                <_CommonRoute
                    key={route.path}
                    path={route.path}
                    currentUser={currentUser}
                    component={Customloadable({
                        loader: route.loader,
                    })}
                    exact={Boolean(route.exact)}
                />
            ))}
            <Route path="*">
                <NotMatch />
            </Route>
        </Switch>
    );
}

export default RoutesComponent;
