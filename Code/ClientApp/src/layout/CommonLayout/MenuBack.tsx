import React, { useLayoutEffect, useState, useEffect } from "react";
import logo from "../../images/ico-ptt.png";
import { Link } from "react-router-dom";
import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import MenuIcon from "@material-ui/icons/Menu";
import "./MP_Front.css";

export default function MenuBack(Props) {
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
  const [isSideNav, setIsSideNav] = useState(true);

  const toggle_Mobilemenu = () => {
    if (document.body.className === "nav-expanded") {
      document.body.classList.remove("nav-expanded");
    }
  };
  const toggle_Area = () => {
    document.body.addEventListener("click", toggle_Mobilemenu);
  };

  const [display, setdisplay] = useState("none");
  const handleClickOpen = () => {
    setOpen(true);
  };

  const setActiveLink = (sLink: any) => {
    let sActive = "";
    let sPath = window.location.pathname.replace(
      process.env.REACT_APP_API_URL + "",
      "/"
    );
    //let sPath = window.location.pathname.replace("/PTT_Event_Report/", "/");
    sActive = sLink === sPath ? "active-LI" : "";

    return sActive;
  };

  const toggleSideNav = () => {
    setIsSideNav(!isSideNav);
    if (document.body.className === "nav-expanded") {
      document.body.classList.remove("nav-expanded");
    } else {
      document.body.classList.add("nav-expanded");
    }
  };

  const LogOut = () => {
    // AxiosPost("Login/ClearSession", {}, (response) => {
    //     ResultAPI(false, response, null, () => {
    //         lnkToLogin();
    //     })
    // }, () => { }, BlockUI, UnBlockUI)
  };

  return (
    <>
      <div id="CONTENT-Front" onClick={toggle_Area}>
        <div className="content-header-Front">
          {/* <div className="header-item"> */}
          <div className="container">
            <div className="d-flex">
              <div className="main-title-Front">
                <div className="title-image">
                  <img src={logo} style={{ width: "80px" }} />
                </div>

                <div className="title-label">
                  <div className="label-1">EVENT</div>
                </div>
                <div className="title">
                  <div className="label-2">REPORT</div>
                </div>
              </div>
              <div style={{ display: display }}>
                <div className="head-menu-Front">
                  <ul id="MENU_TOP" className="menu-Front">
                    {objMenu.lstMenu.map((items: any, index: number) => {
                      return (
                        <li
                          key={`menu_NV_${index}_${items.nMenuID}`}
                          className={setActiveLink(items.sLink)}
                        >
                          <Link to={"" + items.sLink}>
                            <div className="menu-label-Front">
                              {items.sHeaderName}
                            </div>
                          </Link>
                        </li>
                      );
                    })}
                  </ul>
                </div>
              </div>
              {/* <div style={{ display: display }}>
                  <div className="head-info-Front">
                      <div className="user-info">
                          <PersonIcon />
                      </div>
                  </div>
              </div> */}
              <div style={{ display: display }}>
                <div className="main-info-Front">
                  <div className="info-content">
                    <div className="content">WELCOME</div>
                    <div className="info-title">{objMenu.sFullUserName}</div>
                    <div className="info-desc">
                      ROLE:
                      {objMenu.lstGroupUser &&
                      objMenu.lstGroupUser.length > 1 ? (
                        <a onClick={handleClickOpen}>
                          {objMenu.sGroupUserName}...
                        </a>
                      ) : (
                        objMenu.sGroupUserName
                      )}
                    </div>
                  </div>
                  <div className="info-tool">
                    <button
                      type="button"
                      className="btn btn-danger badge badge-pill font-weight-normal"
                      onClick={LogOut}
                    >
                      <ExitToAppIcon style={{ fontSize: 15 }} />
                    </button>
                  </div>
                </div>
              </div>
              <div style={{ display: display }}>
                <div className="head-tool">
                  <a
                    id="MENU_TRIGGER"
                    role="button"
                    className="nav-menu"
                    onClick={toggleSideNav}
                  >
                    <MenuIcon />
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div id="OVERLAY"></div>

        <div className="content-body-Front">{Props.children}</div>

        <div className="content-footer-Front">
          <div className="footer-panel">
            <div className="footer-flag">
              <img src={logo} style={{ height: "30px", marginTop: "5px" }} />
            </div>
            <div className="footer-content">
              <div className="footer-title">PTT PUBLIC COMPANY LIMITED</div>
              <div className="footer-desc">
                Copyright © 2020 PTT Public Company Limited All rights reserved
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="NAV">
        <div id="MENU_SIDE" className="menu-Front">
          {/* <ul id="MENU_TOP" className="menu-Front" > */}
          {objMenu.lstMenu.map((items: any, index: number) => {
            return (
              <li
                key={`menu_NV_${index}_${items.nMenuID}`}
                className={setActiveLink(items.sLink)}
              >
                <Link to={"" + items.sLink}>
                  <div className="menu-label-Front">{items.sHeaderName}</div>
                </Link>
              </li>
            );
          })}
          {/* </ul> */}
        </div>
      </div>
      ;
    </>
  );
}
