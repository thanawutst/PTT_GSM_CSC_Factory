import React, { useLayoutEffect, useEffect } from "react";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import * as LayoutStore from "src/store/redux/LayoutStore";
import * as AuthenStore from "src/store/redux/AuthenStore";
import AuthenSelectors from "src/store/selectors/AuthenSelectors";
import LayoutSelectors from "src/store/selectors/LayoutSelectors";
import clsx from "clsx";
import {
  makeStyles,
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
} from "@material-ui/core";
import Icon from '@material-ui/core/Icon';
import Collapse from "@material-ui/core/Collapse";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";

const drawerWidth = 250;

const useStyles = makeStyles((theme) => ({
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  drawerOpen: {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerClose: {
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: theme.spacing(6),
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing(7),
    },
  },
  active: {
    color: theme.palette.primary.main,
    fontWeight: theme.typography.fontWeightMedium,
  },
  toolbar: theme.mixins.toolbar,
  listItemIcon: {
    minWidth: "48px",
  },
  listItemDisabled: {
    opacity: 0.5,
  },
  nested: {
    paddingLeft: theme.spacing(4),
  },

}));

function Menu(props) {
  const classes = useStyles();
  const dispatch = useDispatch();

  const menuVisible = useSelector(
    LayoutSelectors.selectMenuVisible,
  );

  const menuCollapse = useSelector(
    LayoutSelectors.selectCollapseMenu,
  );


  // useEffect(() => {
  //   dispatch(AuthenStore.AuthenActionCreators.doInitMenu());
  // }, [dispatch]);

  useLayoutEffect(() => {
    const toggleMenuOnResize = () => {
      (window as any).innerWidth < 576
        ? dispatch(LayoutStore.LayoutActionCreators.doHideMenu())
        : dispatch(LayoutStore.LayoutActionCreators.doShowMenu());
    };

    toggleMenuOnResize();

    (window as any).addEventListener(
      'resize',
      toggleMenuOnResize,
    );

    return () => {
      (window as any).removeEventListener(
        'resize',
        toggleMenuOnResize,
      );
    };
  }, [dispatch]);

  const MenuArray = useSelector(AuthenSelectors.selectMenu);

  const selectedKeys = (dataMenu) => {
    const url = props.url;
    if (dataMenu.lstSubMenu) {
      return dataMenu.lstSubMenu?.find((option) => {
        return url === option.sPath || url.startsWith(option.sPath + "/");
      });

    } else {
      return url === dataMenu.sPath || url.startsWith(dataMenu.sPath + "/");
    }
  };

  const CustomRouterLink = (props) => (
    <div
      style={{
        flexGrow: 1,
      }}
    >
      <Link key={props.key}
        style={{
          textDecoration: "none",
          color: "inherit",
        }}
        {...props}
      />
    </div>
  );

  const handleCollapse = key => () => {
    const dataCollapse = { ...menuCollapse, [key]: !menuCollapse[key] }

    dispatch(LayoutStore.LayoutActionCreators.doCollapseMenu(dataCollapse))
  };

  if (!menuVisible) {
    return null
  }

  return (
    <Drawer
      className={clsx(classes.drawer, {
        [classes.drawerOpen]: menuVisible,
        [classes.drawerClose]: !menuVisible,
      })}
      variant="permanent"
      anchor="left"
      open={true}
      classes={{
        paper: clsx(classes.drawer, {
          [classes.drawerOpen]: menuVisible,
          [classes.drawerClose]: !menuVisible,
        })
      }}
    >
      <div className={classes.toolbar}></div>
      <List>
        {MenuArray.map((menu) => {
          const activeHead = selectedKeys(menu);
          const open = menuCollapse[menu.sLabel] || false;

          return (
            <CustomRouterLink key={menu.sPath} to={!menu.lstSubMenu && menu.sPath}>
              <ListItem button onClick={handleCollapse(menu.sLabel)}>
                <ListItemIcon
                  className={clsx({
                    [classes.listItemIcon]: true,
                    [classes.active]: activeHead,
                  })}
                >
                  <Icon>{menu.sIcon}</Icon>
                </ListItemIcon>
                <ListItemText
                  className={clsx({
                    [classes.active]: activeHead,
                  })}
                >
                  {menuVisible && menu.sLabel}
                </ListItemText>
                {(menu.isHeadMenu) && (open ? <ExpandLess /> : <ExpandMore />)}
              </ListItem>
              <Collapse in={(open || activeHead)} timeout="auto" unmountOnExit>
                <List component="div" disablePadding>
                  {menu.lstSubMenu?.map((sub) => {
                    const activeSub = selectedKeys(sub);
                    return (
                      <CustomRouterLink key={sub.sPath} to={sub.sPath}>
                        <ListItem button className={classes.nested}>
                          <ListItemIcon
                            className={clsx({
                              [classes.listItemIcon]: true,
                              [classes.active]: activeSub,
                            })}
                          >
                            <Icon>{sub.sIcon}</Icon>
                          </ListItemIcon>
                          <ListItemText
                            className={clsx({
                              [classes.active]: activeSub,
                            })}
                          >
                            {menuVisible && sub.sLabel}
                          </ListItemText>
                        </ListItem>
                      </CustomRouterLink>
                    )
                  })}
                </List>
              </Collapse>
            </CustomRouterLink>
          )
        })}
      </List>
    </Drawer>
  );
}

export default Menu;
