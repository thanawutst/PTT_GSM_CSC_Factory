import * as React from "react";
import { Route, Switch, Router } from "react-router";
import { createBrowserHistory } from "history";
import routes from "src/router/routes";

const baseUrl = document
  .getElementsByTagName("base")[0]
  .getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });

export default () => (
  <Router history={history}>
    <Switch>
      {routes.map((o) => {
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
