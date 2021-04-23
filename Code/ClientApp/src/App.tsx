import * as React from "react";
import { Route, Switch, Router } from "react-router";
import { createBrowserHistory } from "history";
// import ReduxBlockUi from "react-block-ui/redux";
// import "react-block-ui/style.css";
// import "loaders.css/loaders.min.css";
//brack
import Home from "src/view/BackOffice/Home/Home";
import TestComponentsForm from "src/view/BackOffice/TestComponents/TestComponentsForm";
import Menu2 from "src/view/BackOffice/Menu2/Menu2";
import Menu3 from "src/view/BackOffice/Menu3/Menu3";
import Layout_MP_Back from "src/layout/CommonLayout/Layout_MP_Back";
// var Loader = require("react-loaders").Loader;

const baseUrl = document
  .getElementsByTagName("base")[0]
  .getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });

const lstRoute = [
  {
    exact: true,
    path: "/",
    component: Home,
    layout: Layout_MP_Back,
  },
  {
    exact: true,
    path: "/b_home",
    component: Home,
    layout: Layout_MP_Back,
  },
  {
    exact: true,
    path: "/b_test_comp",
    component: TestComponentsForm,
    layout: Layout_MP_Back,
  },
  {
    exact: true,
    path: "/b_menu3",
    component: Menu3,
    layout: Layout_MP_Back,
  },
];

export default () => (
  <Router history={history}>
    <Switch>
      {lstRoute.map((o) => {
        return (
          <Route path={o.path} key={o.path} exact={o.exact}>
            {o.layout ? (
              <o.layout>
                <o.component />
              </o.layout>
            ) : (
              <o.component />
            )}
          </Route>
        );
      })}
    </Switch>
  </Router>
);
