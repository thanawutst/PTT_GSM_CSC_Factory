import React, { Fragment, useEffect, useState } from "react";
import { Card, CardHeader, CardContent, Collapse } from "@material-ui/core";
import { CardFooter } from "reactstrap";
import { SaveAlt, KeyboardArrowRight } from "@material-ui/icons";
import SaveIcon from "@material-ui/icons/Save";
import {
  Theme,
  createStyles,
  makeStyles,
  withStyles,
} from "@material-ui/core/styles";
import {
  green,
  blue,
  lightBlue,
  orange,
  red,
  amber,
  grey,
} from "@material-ui/core/colors";
import ClearOutlinedIcon from "@material-ui/icons/ClearOutlined";
import ArrowBackIosIcon from "@material-ui/icons/ArrowBackIos";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";
import Swal, { SweetAlertIcon } from "sweetalert2";
import { i18n } from "src/i18n";
import DeleteIcon from "@material-ui/icons/Delete";
import { AxiosGetJson } from "src/Service/Config/AxiosMethod";
import Icon from "@material-ui/core/Icon";
import { string } from "yup";
import Badge from "@material-ui/core/Badge";
import Tooltip from "@material-ui/core/Tooltip";
import { Button, IconButton } from "@material-ui/core";
import SubjectIcon from "@material-ui/icons/Subject";
import PersonIcon from "@material-ui/icons/Person";

const i18nField = "entities.Project_Add.fields";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    button: {
      textTransform: "none",
      margin: theme.spacing(1),
      height: "42px",
      "margin-top": "6px",
    },
    buttonSearch: {
      textTransform: "none",
      backgroundColor: "#000",
      margin: theme.spacing(0),
      height: "42px",
      "margin-top": "6px",
      color: "#fff",
      "&:hover": {
        backgroundColor: "#3f3d3d",
        borderColor: "#0062cc",
        boxShadow: "none",
      },
      "&:active": {
        boxShadow: "none",
        backgroundColor: "#3f3d3d",
        borderColor: "#005cbf",
      },
    },
    buttonEdite: {
      textTransform: "none",
      backgroundColor: "#e5a109",
      margin: theme.spacing(1),
      color: "#fff",
      "&:hover": {
        backgroundColor: "#cc9541",
        borderColor: "#0062cc",
        boxShadow: "none",
      },
      "&:active": {
        boxShadow: "none",
        backgroundColor: "#cc9541",
        borderColor: "#005cbf",
      },
    },
    buttonView: {
      textTransform: "none",
      backgroundColor: "#6f88a9",
      margin: theme.spacing(1),
      color: "#fff",
      "&:hover": {
        backgroundColor: "#305b93",
        borderColor: "#0062cc",
        boxShadow: "none",
      },
      "&:active": {
        boxShadow: "none",
        backgroundColor: "#305b93",
        borderColor: "#005cbf",
      },
    },
    buttonAdd: {
      textTransform: "none",
      backgroundColor: "#1081e3",
      margin: theme.spacing(1),
      color: "#fff",
      "&:hover": {
        backgroundColor: "#3f93db",
        borderColor: "#0062cc",
        boxShadow: "none",
      },
      "&:active": {
        boxShadow: "none",
        backgroundColor: "#3f93db",
        borderColor: "#3f93db",
      },
    },
    buttonDelete: {
      textTransform: "none",
      backgroundColor: "#1081e3",
      margin: theme.spacing(1),
      color: "#fff",
      "&:hover": {
        backgroundColor: "#3f93db",
        borderColor: "#0062cc",
        boxShadow: "none",
      },
      "&:active": {
        boxShadow: "none",
        backgroundColor: "#3f93db",
        borderColor: "#3f93db",
      },
    },
    ButtonClear: {
      textTransform: "none",
      backgroundColor: "#959392",
      margin: theme.spacing(1),
      color: "#fff",
      height: "42px",
      "margin-top": "6px",
      "&:hover": {
        backgroundColor: "##959392",
        borderColor: "#0062cc",
        boxShadow: "none",
      },
      "&:active": {
        boxShadow: "none",
        backgroundColor: "##959392",
        borderColor: "##959392",
      },
    },
    buttonF: {
      "margin-right": "0px!important",
      "&:MuiButton-startIcon": {
        "margin-right": "0px!important",
      },
    },
    buttonExport: {
      textTransform: "none",
      height: "42px",
      "margin-top": "6px",
      // margin: theme.spacing(0),

      // backgroundColor: "#2a8000",
      // margin: theme.spacing(0),
      // "height": "42px",
      // "margin-top": "6px",
      // color: "#fff",
      // '&:hover': {
      //     backgroundColor: '#3f3d3d',
      //     borderColor: '#0062cc',
      //     boxShadow: 'none',
      // },
      // '&:active': {
      //     boxShadow: 'none',
      //     backgroundColor: '#3f3d3d',
      //     borderColor: '#005cbf',
      // },
    },
  })
);

export const MR_Card = (props) => {
  const { Color } = props;
  const [expanded, setExpanded] = useState(true);
  const useStyle = makeStyles({
    root: {
      borderRadius: 0,
    },
  });
  const backgroundColor = Color ? Color : "#d2f0c5";
  const useStyleHeader = makeStyles({
    root: {
      color: "#ffffff",
      backgroundColor: backgroundColor,
      borderColor: "#d6e9c6",
      padding: "10px",
      fontSize: "16px",
    },
  });

  const classes = useStyle();
  const classesHeader = useStyleHeader();

  useEffect(() => {}, [props.title]);
  return (
    <Card
      className={classes.root}
      style={
        props.scopeName
          ? {
              overflow: "unset",
            }
          : undefined
      }
    >
      <CardHeader
        className={classesHeader.root}
        style={
          props.scopeName
            ? {
                top: "-30px",
                position: "sticky",
                zIndex: 5,
              }
            : undefined
        }
        title={
          <React.Fragment>
            {props.title ? (
              <Fragment>
                {props.headicon}&nbsp;{props.title}
              </Fragment>
            ) : (
              <Fragment>
                {props.headicon}&nbsp;{props.scopeName}
              </Fragment>
            )}
          </React.Fragment>
        }
        disableTypography
      />
      <Collapse in={expanded}>
        <CardContent style={{ minHeight: props.minHeight || 300 }}>
          {props.children}
        </CardContent>
      </Collapse>
      <CardFooter
        hidden={
          props.IsShowFooter === undefined
            ? true
            : props.IsShowFooter === true
            ? false
            : true
        }
      >
        {props.footer}
      </CardFooter>
    </Card>
  );
};
export const MR_Card_Header = (props) => {
  const { Color } = props;
  const [expanded, setExpanded] = useState(true);
  const useStyle = makeStyles({
    root: {
      borderRadius: 0,
    },
  });
  const backgroundColor = Color ? Color : "#d2f0c5";
  const useStyleHeader = makeStyles({
    root: {
      color: "#ffffff",
      backgroundColor: backgroundColor,
      borderColor: "#d6e9c6",
      padding: "10px",
      fontSize: "16px",
    },
  });

  const classes = useStyle();
  const classesHeader = useStyleHeader();
  const [sHeader, setHeader] = useState();
  const [sIcon, setIcon] = useState();
  const [sRounter, setRounter] = useState(string as any);

  const GetHeader = async () => {
    let result: any = await AxiosGetJson("Header/GetHeaderName", {
      nMenuID: props.nMenuID,
    });
    if (result) {
      setHeader(result.sMenuName);
      setIcon(result.sIcon);
      setRounter(result.sRounter);
      //props.url = result.sRounter;
    }
  };
  useEffect(() => {
    GetHeader();
  }, [props.title]);
  return (
    <Card
      className={classes.root}
      style={
        props.scopeName
          ? {
              overflow: "unset",
            }
          : undefined
      }
    >
      <CardHeader
        className={classesHeader.root}
        style={
          props.scopeName
            ? {
                top: "-30px",
                position: "sticky",
                zIndex: 5,
              }
            : undefined
        }
        title={
          <React.Fragment>
            {props.isModeEdit == true ? (
              sHeader ? (
                <Fragment>
                  <Icon style={{ verticalAlign: "middle" }}>{sIcon} </Icon>
                  &nbsp;
                  <Link
                    to={sRounter}
                    style={{
                      color: "inherit",
                      textDecoration: "inherit",
                      verticalAlign: "middle",
                    }}
                  >
                    {sHeader}
                  </Link>
                  <KeyboardArrowRight style={{ verticalAlign: "middle" }} />
                  &nbsp;
                  <label style={{ verticalAlign: "middle" }}>
                    {"Add/Edit"}
                  </label>
                </Fragment>
              ) : (
                <Fragment>
                  <Icon style={{ verticalAlign: "middle" }}>{sIcon}</Icon>
                  &nbsp;{props.scopeName}
                </Fragment>
              )
            ) : props.isModeApprove == true ? (
              sHeader ? (
                <Fragment>
                  <Icon style={{ verticalAlign: "middle" }}>{sIcon} </Icon>
                  &nbsp;
                  <Link
                    to={sRounter}
                    style={{
                      color: "inherit",
                      textDecoration: "inherit",
                      verticalAlign: "middle",
                    }}
                  >
                    {sHeader}
                  </Link>
                  <KeyboardArrowRight style={{ verticalAlign: "middle" }} />
                  &nbsp;
                  <label style={{ verticalAlign: "middle" }}>{"Approve"}</label>
                </Fragment>
              ) : (
                <Fragment>
                  <Icon style={{ verticalAlign: "middle" }}>{sIcon}</Icon>
                  &nbsp;{props.scopeName}
                </Fragment>
              )
            ) : sHeader ? (
              <Fragment>
                <Icon style={{ verticalAlign: "middle" }}>{sIcon}</Icon>
                &nbsp;
                <label style={{ verticalAlign: "middle" }}>{sHeader}</label>
              </Fragment>
            ) : (
              <Fragment>
                <Icon style={{ verticalAlign: "middle" }}>{sIcon}</Icon>
                &nbsp;{props.scopeName}
              </Fragment>
            )}
          </React.Fragment>
        }
        disableTypography
      />
      <Collapse in={expanded}>
        <CardContent style={{ minHeight: props.minHeight || 300 }}>
          {props.children}
        </CardContent>
      </Collapse>
      <CardFooter
        hidden={
          props.IsShowFooter === undefined
            ? true
            : props.IsShowFooter === true
            ? false
            : true
        }
      >
        {props.footer}
      </CardFooter>
    </Card>
  );
};
export const useStylesIconButton = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      "& > *": {
        margin: theme.spacing(1),
      },
    },
    extendedIcon: {
      marginRight: theme.spacing(1),
    },
  })
);

export const MuiButtonblue = (props) => {
  const useStyles = makeStyles((theme: Theme) =>
    createStyles({
      buttonSuccess: {
        color: "#ffffff",
        borderRadius: 0,
        margin: theme.spacing(1),
        textTransform: "none",
        backgroundColor: blue[500],
        "&:hover": {
          backgroundColor: blue[700],
        },
      },
    })
  );
  const classes = useStyles();
  return (
    <div>
      <Button
        variant="contained"
        size="small"
        type="button"
        className={classes.buttonSuccess}
        {...props}
      >
        {props.children}
      </Button>
    </div>
  );
};

export const MuiButtonGreen = (props) => {
  const useStyles = makeStyles((theme: Theme) =>
    createStyles({
      buttonSuccess: {
        color: "#ffffff",
        borderRadius: 0,
        margin: theme.spacing(1),
        textTransform: "none",
        backgroundColor: green[500],
        "&:hover": {
          backgroundColor: green[700],
        },
      },
    })
  );
  const classes = useStyles();
  return (
    <div>
      <Button
        variant="contained"
        size="small"
        type="button"
        className={classes.buttonSuccess}
        {...props}
      >
        {props.children}
      </Button>
    </div>
  );
};

export const MuiButtonCreatebt = (props) => {
  const useStyles = makeStyles((theme: Theme) =>
    createStyles({
      buttonSuccess: {
        color: "#ffffff",
        borderRadius: 0,
        margin: theme.spacing(1),
        textTransform: "none",
        backgroundColor: blue[500],
        "&:hover": {
          backgroundColor: blue[700],
        },
      },
    })
  );
  const classes = useStyles();
  return (
    <Link to={props.Linkto}>
      <Button
        variant="contained"
        size="small"
        type="button"
        className={classes.buttonSuccess}
        {...props}
      >
        {props.children}
      </Button>
    </Link>
  );
};

export const MuiButtonBackbt = (props) => {
  const useStyles = makeStyles((theme: Theme) =>
    createStyles({
      buttonSuccess: {
        color: "#ffffff",
        borderRadius: 0,
        margin: theme.spacing(1),
        textTransform: "none",
        backgroundColor: grey[500],
        "&:hover": {
          backgroundColor: grey[700],
        },
      },
    })
  );
  const classes = useStyles();
  return (
    <Button
      variant="contained"
      size="small"
      type="button"
      className={classes.buttonSuccess}
      {...props}
    >
      {props.children}
    </Button>
  );
};

export const MuiButtonCustom = (props) => {
  let btcolor;
  switch (props.event) {
    case "save":
      btcolor = green;
      break;
    case "edit":
      btcolor = amber;
      break;
    case "clear":
      btcolor = grey;
      break;
  }

  const useStyles = makeStyles((theme: Theme) =>
    createStyles({
      buttonSuccess: {
        color: props.btcolor,
        borderRadius: 0,
        margin: theme.spacing(1),
        textTransform: "none",
        backgroundColor: btcolor[500],
        "&:hover": {
          backgroundColor: btcolor[700],
        },
      },
    })
  );
  const classes = useStyles();
  return (
    <Button
      variant="contained"
      size="small"
      type="button"
      className={classes.buttonSuccess}
      {...props}
    >
      {props.children}
    </Button>
  );
};
export const MuiButtonBack1 = (props) => {
  const classes = useStylesIconButton();
  return (
    <StyledButton
      name={props.name || "btnCancel"}
      variant="contained"
      size="medium"
      {...props}
    >
      <ArrowBackIosIcon className={classes.extendedIcon} /> Back
      {props.children}
    </StyledButton>
  );
};
const StyledButton = withStyles((theme: Theme) =>
  createStyles({
    root: {
      borderRadius: 0,
      margin: theme.spacing(1),
      textTransform: "none",
    },
  })
)(Button);
export const MuiButtonSave = (props) => {
  const classes = useStylesIconButton();
  return (
    <MuiButtonblue size="medium" {...props}>
      <SaveAlt className={classes.extendedIcon} />
      Save
      {props.children}
    </MuiButtonblue>
  );
};
export const MuiButtonCreate = (props) => {
  const classes = useStylesIconButton();
  return (
    <MuiButtonCreatebt size="medium" {...props}>
      <AddIcon className={classes.extendedIcon} />
      Create
      {props.children}
    </MuiButtonCreatebt>
  );
};
export const MuiButtonBack = (props) => {
  const classes = useStylesIconButton();
  return (
    <MuiButtonBackbt size="medium" {...props}>
      <ArrowBackIosIcon className={classes.extendedIcon} />
      Back
      {props.children}
    </MuiButtonBackbt>
  );
};
export const MuiButtonCancel = (props) => {
  const classes = useStylesIconButton();
  return (
    <StyledButton
      name={props.name || "btnCancel"}
      variant="contained"
      size="medium"
      {...props}
    >
      <ClearOutlinedIcon className={classes.extendedIcon} />
      Cancel
      {props.children}
    </StyledButton>
  );
};

export const MuiButtonAdd = (props) => {
  const classes = useStylesIconButton();
  return (
    <MuiButtonGreen size="medium" {...props}>
      <SaveIcon className={classes.extendedIcon} />
      {props.label ? props.label : "Add"}
      {props.children}
    </MuiButtonGreen>
  );
};

export const MuiButtonClear = (props) => {
  const classes = useStylesIconButton();
  return (
    <StyledButton
      name={props.name || "btnClear"}
      variant="contained"
      size="medium"
      {...props}
    >
      <ClearOutlinedIcon className={classes.extendedIcon} />
      {props.label ? props.label : "Clear"}
      {props.children}
    </StyledButton>
  );
};
export const MuiButtonDelete = (props) => {
  const classes = useStylesIconButton();
  return (
    <StyledButton
      name={props.name || "btnDelete"}
      variant="contained"
      size="medium"
      color="secondary"
      {...props}
    >
      <DeleteIcon className={classes.extendedIcon} />
      Delete
      {props.children}
    </StyledButton>
  );
};

export const DialogConfirm = (funcYes, funcNo?, title = "", message = "") => {
  Sweetalert.Confirm(
    title === "" ? AlertTitle.Confirm : title,
    message === "" ? "Do you want to save data ?" : message,
    funcYes,
    funcNo
  );
};
export const DialogConfirmDelete = (
  funcYes,
  funcNo?,
  title = "",
  message = ""
) => {
  Sweetalert.Confirm(
    title === "" ? AlertTitle.Confirm : title,
    message === "" ? "Do you want to delete data ?" : message,
    funcYes,
    funcNo
  );
};
export const AlertIcon = {
  info: "info" as SweetAlertIcon,
  success: "success" as SweetAlertIcon,
  error: "error" as SweetAlertIcon,
  warning: "warning" as SweetAlertIcon,
  question: "question" as SweetAlertIcon,
  confirm: "Confirmation" as SweetAlertIcon,
};
export const Responsestart = {
  success: "Success",
  error: "Failed",
  warning: "Warning",
  SSEXP: "SSEXP",
};
export const AlertTitle = {
  Success: "Action Completed",
  Warning: "Warning",
  Error: "Error",
  Info: "Information",
  Confirm: "Confirmation",
  Hint: "Hint",
  Duplicate: "Duplicate",
  Desc_Confirm_Delete: "Do you want to delete data?",
  Desc_Delete_Success: "Delete Success.",
};
export const Sweetalert = {
  Warning: function (sTitle, sMessage, fnOK?) {
    Swal.fire({
      icon: AlertIcon.warning,
      title: sTitle,
      html: sMessage,
      confirmButtonText: "Close",
      allowOutsideClick: false,
      allowEscapeKey: false,
    }).then(function (result) {
      if (result.value) {
        if (fnOK) {
          fnOK();
        } else {
          Swal.close();
        }
      }
    });
  },
  Success: function (sTitle, sMessage, fnOK?) {
    Swal.fire({
      title: sTitle == "" ? "Action Completed" : sTitle,
      html: sMessage == "" ? "Action Completed" : sMessage,
      icon: AlertIcon.success,
      confirmButtonText: "Close",
      allowOutsideClick: false,
      allowEscapeKey: false,
    }).then(function (result) {
      if (result.value) {
        if (fnOK) {
          fnOK();
        } else {
          Swal.close();
        }
      }
    });
  },
  Error: function (sTitle, sMessage, fnOK?) {
    Swal.fire({
      icon: AlertIcon.error,
      title: sTitle == "" ? "Error" : sTitle,
      html: sMessage,
      confirmButtonText: "Close",
      allowOutsideClick: false,
      allowEscapeKey: false,
    }).then(function (result) {
      if (result.value) {
        if (fnOK) {
          fnOK();
        } else {
          Swal.close();
        }
      }
    });
  },
  Confirm: function (sTitle, sMessage, fnYes?, fnNo?) {
    Swal.fire({
      title: sTitle,
      text: sMessage,
      icon: AlertIcon.question,
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Confirm",
      showLoaderOnConfirm: true,
      allowOutsideClick: false,
      preConfirm: function () {
        return new Promise(function () {
          //resolve, reject
          Swal.disableButtons();

          if (fnYes) {
            fnYes();
          } else {
            Swal.close();
          }
        });
      },
    }).then((result) => {
      if (result.dismiss === Swal.DismissReason.cancel) {
        if (fnNo) {
          fnNo();
        } else {
          Swal.close();
        }
      }
    });
  },
};

export type _btName = "save" | "edit" | "clear" | "submit" | "add";

export const MuiButton = (props) => {
  const classes = useStylesIconButton();
  return (
    <MuiButtonCustom size="medium" {...props}>
      <ArrowBackIosIcon className={classes.extendedIcon} />
      {i18n("common." + props.event)}
      {props.children}
    </MuiButtonCustom>
  );
};

export const MuiIconButtonItem = (props) => {
  const { type, rowCount, ...OtherProps } = props;
  const classes = useStyles();
  const StyledBadge = withStyles((theme: Theme) =>
    createStyles({
      badge: {
        right: -3,
        top: 13,
        border: `2px solid ${theme.palette.background.paper}`,
        padding: "0 4px",
      },
    })
  )(Badge);
  return (
    <Tooltip
      title={rowCount && rowCount + " Person(s)"}
      placement="bottom"
      arrow
    >
      <IconButton color="primary" {...OtherProps} aria-label="cart">
        <StyledBadge badgeContent={rowCount} color="secondary">
          <PersonIcon />
        </StyledBadge>
      </IconButton>
    </Tooltip>
  );
};
