import React from "react";
import { useDispatch, useSelector } from 'react-redux';
import * as LayoutStore from "src/store/redux/LayoutStore";
import UserInfo from "src/layout/CommonLayout/UserInfo";
import LayoutSelectors from "src/store/selectors/LayoutSelectors";
import { AppBar, Toolbar, IconButton, makeStyles } from "@material-ui/core";
import MenuIcon from "@material-ui/icons/Menu";
import { Link } from "react-router-dom";
import { i18n } from "src/i18n";

const useStyles = makeStyles((theme) => ({
  appBar: {
    color: theme.palette.getContrastText(theme.palette.primary.main),
    zIndex: theme.zIndex.drawer + 1,
  },
  logo: {
    paddingLeft: theme.spacing(1),
    fontWeight: 500,
    fontSize: "1.5em",
    color: theme.palette.getContrastText(theme.palette.primary.main),
    textDecoration: "none",
  },
  grow: {
    flex: "1 1 auto",
  },
}));

export default function Header(props) {
  const dispatch = useDispatch();
  const classes = useStyles();

  const doToggleMenu = () => {
    dispatch(LayoutStore.LayoutActionCreators.doToggleMenu());
  };

  return (
    <AppBar className={classes.appBar} position="fixed">
      <Toolbar>
        <IconButton edge="start" color="inherit" onClick={doToggleMenu}>
          <MenuIcon />
        </IconButton>

        <Link className={classes.logo} to="/">
          <>{i18n("app.title")}</>
        </Link>

        <div className={classes.grow} />
        <UserInfo />
      </Toolbar>
    </AppBar>
  );
}
