import React from "react";
import { Route } from "react-router-dom";

function _blankRoute({ component: Component, ...rest }) {
  return (
    <Route
      {...rest}
      render={(props) => {
        return (
          <React.Fragment {...props}>
            <Component {...props} />
          </React.Fragment>
        );
      }}
    />
  );
}

export default _blankRoute;
