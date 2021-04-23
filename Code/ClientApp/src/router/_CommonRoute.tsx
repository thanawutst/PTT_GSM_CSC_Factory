import React from "react";
import { Route, useLocation, Redirect } from "react-router-dom";
import Layout from "src/layout/CommonLayout/Layout";

function _CommonRoute({ component: Component, currentUser, ...rest }) {
  const location = useLocation();
  return (
    <Route
      {...rest}
      render={(props) => {
        // if (!currentUser.sLogonName) {
        //   return (
        //     <Redirect
        //       to={{
        //         pathname: "/login",
        //         state: { from: location },
        //       }}
        //     />
        //   );
        // }
        return (
          <Layout {...props}>
            <Component {...props} />
          </Layout>
        );
      }}
    />
  );
}

export default _CommonRoute;
