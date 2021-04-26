import React, { Fragment, useEffect, useState, useCallback } from "react";
import "../../Styles/Fonts/Kanit/css/fonts.css";
import "../../Styles/fontawesome-5.13.0/css/all.css";
import "./MP_Back.css";
import "../../Styles/perfect-scrollbar/perfect-scrollbar.css";
import { List as linq } from "linqts";
import { Link } from "react-router-dom";
import { Badge } from "reactstrap";
import MenuIcon from "@material-ui/icons/Menu";
import Tooltip from "@material-ui/core/Tooltip";
import HomeIcon from "@material-ui/icons/Home";
import logo from "../../images/ico-ptt.png";
// import { FnBlock_UI, AxiosPost, ResultAPI, lnkToLogin } from 'components/SysComponent/SystemComponent';
import { List as LINQ } from "linqts";
import Avatar from "@material-ui/core/Avatar";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemAvatar from "@material-ui/core/ListItemAvatar";
import ListItemText from "@material-ui/core/ListItemText";
import DialogTitle from "@material-ui/core/DialogTitle";
import Dialog from "@material-ui/core/Dialog";
import PersonIcon from "@material-ui/icons/Person";
import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import { blue } from "@material-ui/core/colors";
import { makeStyles, Button, IconButton } from "@material-ui/core";

const emails = ["username@gmail.com", "user02@gmail.com"];
const useStyles = makeStyles({
  avatar: {
    backgroundColor: blue[100],
    color: blue[600],
  },
});

export interface SimpleDialogProps {
  open: boolean;
  selectedValue: string;
  data: any[];
  onClose: (value: string) => void;
}

function SimpleDialog(props: SimpleDialogProps) {
  const classes = useStyles();
  const { onClose, selectedValue, open, data } = props;

  const handleClose = () => {
    onClose(selectedValue);
  };

  const handleListItemClick = (value: string) => {
    onClose(value);
  };

  return (
    <Dialog
      onClose={handleClose}
      aria-labelledby="simple-dialog-title"
      open={open}
    >
      <DialogTitle id="simple-dialog-title">Group User</DialogTitle>
      <List>
        {data.map((item, index) => (
          <ListItem key={"dilog_" + index}>
            <ListItemAvatar>
              <Avatar className={classes.avatar}>
                <PersonIcon />
              </Avatar>
            </ListItemAvatar>
            <ListItemText primary={item.sGroupName} />
          </ListItem>
        ))}
      </List>
    </Dialog>
  );
}

const sController = "MP_Back";
const lstMenu = [
  {
    cPRMS: "N",
    nHeaderID: null,
    nLavel: 0,
    nMenuID: 1,
    nOrderBy: 1,
    sHeaderName: "Home",
    sIcon: "fas fa-home",
    sLink: "/b_home",
    sMenuName: "Home",
  },
  {
    cPRMS: "N",
    nHeaderID: null,
    nLavel: 0,
    nMenuID: 2,
    nOrderBy: 2,
    sHeaderName: "Test Component Form",
    sIcon: "fa fa-flag",
    sLink: "/b_test_comp",
    sMenuName: "Test Component Form",
  },
  {
    cPRMS: "N",
    nHeaderID: null,
    nLavel: 0,
    nMenuID: 3,
    nOrderBy: 3,
    sHeaderName: "Menu3",
    sIcon: "fas fa-comment-dots",
    sLink: "/b_menu3",
    sMenuName: "Menu3",
  },
  {
    cPRMS: "N",
    nHeaderID: null,
    nLavel: 0,
    nMenuID: 4,
    nOrderBy: 4,
    sHeaderName: "Menu4",
    sIcon: "fas fa-comment-dots",
    sLink: "/",
    sMenuName: "Menu4",
  },
  {
    cPRMS: "N",
    nHeaderID: null,
    nLavel: 0,
    nMenuID: 5,
    nOrderBy: 5,
    sHeaderName: "Menu5",
    sIcon: "fas fa-comment-dots",
    sLink: "/",
    sMenuName: "Menu5",
  },
  {
    cPRMS: "N",
    nHeaderID: null,
    nLavel: 0,
    nMenuID: 6,
    nOrderBy: 6,
    sHeaderName: "Menu6",
    sIcon: "fas fa-comment-dots",
    sLink: "/",
    sMenuName: "Menu6",
  },
];

const Layout_MP_Back = (Props: any) => {
  // const { BlockUI, UnBlockUI } = FnBlock_UI()
  const [objMenu, setObjMenu] = useState({
    isVisible: true,
    IsopenPage: "",
    isSideNav: true,
    sFullUserName: "",
    sCompanyName: "",
    sGroupUserName: "",
    lstGroupUser: [],
    lstMenu: [] as any,
    lstMenuAll: [] as any,
  });
  const [open, setOpen] = React.useState(false);
  const [selectedValue, setSelectedValue] = React.useState(emails[1]);
  const [openMenu, setOpenMenu] = React.useState(true);
  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = (value: string) => {
    setOpen(false);
    setSelectedValue(value);
  };

  const LoadMenu = () => {
    //   AxiosPost(sController + "/LoadMP_Back", {}, (response) => {
    //       ResultAPI(false, response, null, () => {
    //           setObjMenu({
    //               ...objMenu,
    //               lstMenu: new LINQ<any>(response.lstMenuData).Where(w => w.nLavel === 0).OrderBy(o => o.nOrderBy).ToArray(),
    //               lstMenuAll: new LINQ<any>(response.lstMenuData).OrderBy(o => o.nOrderBy).ToArray(),
    //               sFullUserName: response.sFullUserName,
    //               sCompanyName: response.sCompanyName,
    //               lstGroupUser: response.lstGroupUser,
    //               sGroupUserName: response.sGroupUserName,
    //           })
    //       })
    //   }, () => { }, BlockUI, UnBlockUI)
    setObjMenu({
      ...objMenu,
      IsopenPage: "",
      isSideNav: true,
      isVisible: true,
      lstMenu: lstMenu,
      lstMenuAll: lstMenu,
      sCompanyName: "PTT",
      sFullUserName: "Admin",
      sGroupUserName: "Corporate Admin",
    });
  };
  useEffect(() => {
    LoadMenu();
  }, []);

  const toggleMenu = () => {
    setObjMenu({
      ...objMenu,
      isVisible: !objMenu.isVisible ? true : false,
    });

    var arrMenuLabel = document.getElementsByClassName("menu-label");
    var n = arrMenuLabel.length;
    if (!objMenu.isVisible) {
      setTimeout(() => {
        for (var i = 0; i < n; i++) arrMenuLabel[i].className = "menu-label"; //arrMenuLabel[i].style.display = "block";
      }, 100);
    } else {
      for (var i = 0; i < n; i++)
        arrMenuLabel[i].className = "menu-label display-none"; //arrMenuLabel[i].style.display = "none";
    }
  };

  const toggleSideNav = () => {
    setObjMenu({
      ...objMenu,
      isSideNav: !objMenu.isSideNav,
    });
    if (document.body.className === "nav-expanded") {
      document.body.classList.remove("nav-expanded");
    } else {
      document.body.classList.add("nav-expanded");
    }
  };

  const toggleMobilemenu = () => {
    if (document.body.className === "nav-expanded") {
      document.body.classList.remove("nav-expanded");
    }
  };

  const toggleArea = () => {
    document.body.addEventListener("click", toggleMobilemenu);
  };

  const setActiveLink = (sLink: any) => {
    let sActive = "";
    let sPath = window.location.pathname.replace(
      process.env.REACT_APP_API_URL + "",
      "/"
    );
    // sActive = sLink === sPath ? "active" : ""

    if (sLink !== sPath) {
      var q = new linq<any>(objMenu.lstMenuAll).FirstOrDefault(
        (f) => f.sLink === sLink && f.nLavel === 0
      );
      if (q != null) {
        let qSub = new linq<any>(objMenu.lstMenuAll).FirstOrDefault(
          (f) =>
            f.nHeaderID === q.nMenuID && f.sLink === sPath && f.nLavel !== 0
        );
        if (qSub != null) sActive = "active";
      } else {
        sActive = sLink === sPath ? "active" : "";
      }
    } else {
      sActive = sLink === sPath ? "active" : "";
    }
    return sActive;
  };
  const setActiveManuName = () => {
    let sActive = "";
    let sPath = window.location.pathname.replace(
      process.env.REACT_APP_API_URL + "",
      "/"
    );
    let q = new linq<any>(objMenu.lstMenuAll).FirstOrDefault(
      (f) => f.sLink === sPath
    );
    if (q != null) {
      sActive = q.sHeaderName;
    }
    return sActive;
  };

  // const LogOut = () => {
  //     AxiosPost("Login/ClearSession", {}, (response) => {
  //         ResultAPI(false, response, null, () => {
  //             lnkToLogin();
  //         })
  //     }, () => { }, BlockUI, UnBlockUI)
  // };
  return (
    <Fragment>
      <Link to="/" id="lnkToLogin" />
      <Link to="/" id="lnkTo" />
      <Link to="/Admin-Home" id="NotPermission" />
      <SimpleDialog
        selectedValue={selectedValue}
        open={open}
        onClose={handleClose}
        data={objMenu.lstGroupUser}
      />
      <div id="PAGE_CONTENT" onClick={toggleArea}>
        <div className="content-header">
          <div className="header-item">
            <div className="nav">
              <a className="NAV_TRIGGER" onClick={toggleSideNav}>
                <MenuIcon />
              </a>
            </div>
            <div className="main-title">
              <div
                className="title-image"
                style={{ cursor: "pointer" }}
                onClick={(e) => {
                  let el = document.getElementById("toHome") as any;
                  el && el.click();
                }}
              >
                <img src={logo} style={{ width: "80px" }} />
              </div>
              <Link to="/b_home" id={"toHome"}></Link>
              <div className="title">
                <div className="label-2">GSM CSC Factory</div>
              </div>
            </div>
          </div>
          <div className="main-info">
            <div className="main-home">
              {/* <button type="button" className="btn btn-primary badge badge-pill font-weight-normal"><HomeIcon style={{ fontSize: 15 }} /></button> */}
              {/* <Tooltip title={"Home"} placement="bottom">
                <Link to="/Create-Report">
                  <IconButton
                    color="primary"
                    aria-label="add an alarm"
                    className="badge badge-pill font-weight-normal"
                  >
                    <HomeIcon />
                  </IconButton>
                </Link>
              </Tooltip> */}
              {/* <button type="button" className="btn  badge badge-pill font-weight-normal"><ExitToAppIcon style={{ fontSize: 15 }} /></button> */}
              <Tooltip title={"Logout"} placement="bottom">
                <IconButton
                  color="secondary"
                  aria-label="add an alarm"
                  className="badge badge-pill font-weight-normal"
                >
                  <ExitToAppIcon />
                </IconButton>
              </Tooltip>
            </div>
            <div className="info-content">
              <div className="info-title">{objMenu.sFullUserName}</div>
              <div className="info-desc">
                {/* <Badge color="info">{objMenu.sCompanyName}</Badge> */}
                {objMenu.lstGroupUser && objMenu.lstGroupUser.length > 1 ? (
                  <a onClick={handleClickOpen}>{objMenu.sGroupUserName}...</a>
                ) : (
                  objMenu.sGroupUserName
                )}
              </div>
            </div>
          </div>
        </div>
        <div
          className={
            objMenu.isVisible ? "content-body" : "content-body menu-collapse"
          }
        >
          <div className="body-side">
            <div className="main-nav">
              <div className="menu-toggle" onClick={(e) => toggleMenu()}>
                <a className="manu-togglebar">
                  <MenuIcon />
                </a>
              </div>
              <ul id="menuMain" className="menu">
                {objMenu.lstMenu.map((items: any, index: number) => {
                  return (
                    <li key={`menu_${index}_${items.nMenuID}`}>
                      <Link
                        to={items.sLink}
                        className={setActiveLink(items.sLink)}
                      >
                        <Tooltip title={items.sHeaderName} placement="right">
                          <div className="menu-icon">
                            <i className={items.sIcon}></i>
                          </div>
                        </Tooltip>
                        <div className="menu-label">{items.sHeaderName}</div>
                      </Link>
                    </li>
                  );
                })}
              </ul>
            </div>
          </div>
          <div className="body-main">
            <div className="main-head">
              <div className="head-flag">
                <div className="flag-item">
                  <div id="headIcon" className="head-icon"></div>
                </div>
                <div className="flag-item">
                  <div id="headTitle" className="head-title"></div>
                  <div id="headNav" className="head-nav">
                    <h5>{setActiveManuName()}</h5>
                  </div>
                </div>
              </div>
            </div>
            <div className="main-body">{Props.children}</div>
          </div>
        </div>

        {/* <div className="content-footer">
                    <div className="footer-panel">
                        <div className="footer-content">
                            <div className="footer-title">PTT PUBLIC COMPANY LIMITED</div>
                            <div className="footer-desc">Copyright © 2020 PTT Public Company Limited All rights reserved</div>
                        </div>
                    </div>
                </div> */}
        {/* <Overlay /> */}
      </div>

      <div className="SIDE_NAV">
        <ul id="menuMain" className="menu">
          {objMenu.lstMenu.map((items: any, index: number) => {
            return (
              <li key={`menu_NV_${index}_${items.nMenuID}`}>
                <Link to={items.sLink} className={setActiveLink(items.sLink)}>
                  <Tooltip title={items.sHeaderName} placement="right">
                    <div className="menu-icon">
                      <i className={items.sIcon}></i>
                    </div>
                  </Tooltip>
                  <div className="menu-label">{items.sHeaderName}</div>
                </Link>
              </li>
            );
          })}
        </ul>
      </div>
    </Fragment>
  );
};

const Overlay = () => {
  return (
    <Fragment>
      <div id="SITE_OVERLAY">
        <div className="loader">
          <div className="lds-ring">
            <div></div>
            <div></div>
            <div></div>
            <div></div>
          </div>
          <div className="loader-text">Loading</div>
        </div>
      </div>
    </Fragment>
  );
};

export default Layout_MP_Back;
