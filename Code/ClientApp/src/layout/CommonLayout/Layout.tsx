import React from "react";
import Header from "src/layout/CommonLayout/Header";
import Menu from "src/layout/CommonLayout/Menu";
import MenuBack from "src/layout/CommonLayout/MenuBack";
import { makeStyles } from "@material-ui/core";
import { useRouteMatch } from "react-router-dom";
// import Layout_MP_Back from "src/layout/CommonLayout/Layout_MP_Back";

const useStyles = makeStyles((theme) => ({
  root: {
    color: "rgba(0, 0, 0, 0.65)",
    backgroundColor: "#f0f2f5",
    display: "flex",
    fontFamily: `'Roboto', sans-serif`,

    "& h1, h2, h3, h4, h5, h6": {
      color: "rgba(0, 0, 0, 0.85)",
    },
  },

  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
    minHeight: "100vh",
    overflowX: "hidden",
  },

  toolbar: theme.mixins.toolbar,
}));

function Layout(props) {
  const classes = useStyles();
  const match = useRouteMatch();
  return (
    <div className={classes.root}>
      <Header />
      <Menu />
      <div className={classes.content}>
        <div className={classes.toolbar}></div>
        {props.children}
      </div>
      {/* <Layout_MP_Back /> */}
    </div>
  );
}

export default Layout;
