import React from "react";
import PropTypes from "prop-types";
import Button from "@material-ui/core/Button";
import { ArrowBack, Save, SaveAlt, Add } from "@material-ui/icons";
import UndoIcon from "@material-ui/icons/Undo";
import CloseIcon from "@material-ui/icons/Close";
import { i18n } from "src/i18n";
import { Grid, IconButton, makeStyles } from "@material-ui/core";
import { blue, green, red, grey } from "@material-ui/core/colors";
import CheckIcon from "@material-ui/icons/Check";
import ClearIcon from "@material-ui/icons/Clear";
import { getMuiTheme, classToLabel } from "src/components/Systems/StyleTable";
import EditOutlinedIcon from "@material-ui/icons/EditOutlined";
import VisibilityICON from "@material-ui/icons/Visibility";
import AddIcon from "@material-ui/icons/Add";
import DeleteForeverIcon from "@material-ui/icons/DeleteForever";
const useStyles = makeStyles((theme) => ({
  SaveDraftButton: {
    marginRight: "5px",
    color: "#fff",
    backgroundColor: blue[500],
    "&:hover": {
      backgroundColor: blue[600],
    },
  },
  RejectButton: {
    marginRight: "5px",
    color: "#fff",
    backgroundColor: red[700],
    "&:hover": {
      backgroundColor: red[900],
    },
  },
  SubmitButton: {
    marginRight: "5px",
    color: "#fff",
    backgroundColor: green[500],
    "&:hover": {
      backgroundColor: green[700],
    },
  },
  CancelButton: {
    color: "#fff",
    marginRight: "5px",
    backgroundColor: "#e91e63",
    "&:hover": {
      backgroundColor: "#c2185b",
    },
  },
  Back: {
    color: "#000000",
    marginRight: "5px",
  },
  Delete: {
    color: "#ff0000",
    marginRight: "5px",
  },
}));
const LabelStyle = makeStyles(classToLabel);
export function ButtonSaveDraft(props) {
  const classes = useStyles();
  return (
    <Button
      className={classes.SaveDraftButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<Save />}
      {...props}
    >
      {i18n("common.saveDarft")}
    </Button>
  );
}
ButtonSaveDraft.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonSubmit(props) {
  const classes = useStyles();
  return (
    <Button
      style={{ marginLeft: "5px" }}
      className={classes.SubmitButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<SaveAlt />}
      {...props}
    >
      {i18n("common.submit")}
    </Button>
  );
}
ButtonSubmit.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonCancel(props) {
  const classes = useStyles();
  return (
    <Button
      style={{ marginLeft: "5px" }}
      className={classes.CancelButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<CloseIcon />}
      {...props}
    >
      {i18n("common.cancel")}
    </Button>
  );
}
ButtonCancel.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};
export function ButtonBack(props) {
  const classes = useStyles();
  return (
    <Button
      style={{ marginLeft: "5px" }}
      className={classes.Back}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<ArrowBack />}
      {...props}
    >
      {i18n("common.back")}
    </Button>
  );
}
ButtonBack.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonApprove(props) {
  const classes = useStyles();
  return (
    <Button
      style={{ marginLeft: "5px" }}
      className={classes.SubmitButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<CheckIcon />}
      {...props}
    >
      {i18n("common.approve")}
    </Button>
  );
}
ButtonApprove.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonReject(props) {
  const classes = useStyles();
  return (
    <Button
      style={{ marginLeft: "5px" }}
      className={classes.RejectButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<ClearIcon />}
      {...props}
    >
      {i18n("common.reject")}
    </Button>
  );
}
ButtonReject.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonSave(props) {
  const classes = useStyles();
  return (
    <Button
      style={{ marginLeft: "5px" }}
      className={classes.SaveDraftButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<SaveAlt />}
      {...props}
    >
      {i18n("common.save")}
    </Button>
  );
}
ButtonSave.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonAdd(props) {
  const classes = useStyles();
  return (
    <Button
      // style={{ marginLeft: "5px" }}
      className={classes.SaveDraftButton}
      variant="contained"
      type="button"
      size="medium"
      startIcon={<Add />}
      {...props}
    >
      {i18n("common.add")}
    </Button>
  );
}
ButtonAdd.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

export function ButtonAddHead(props) {
  const classes = LabelStyle();
  return (
    //  style={{ marginLeft: "19%" }}
    <Grid>
      <IconButton className={classes.bticon} aria-label="Add" {...props}>
        <AddIcon className={classes.icon} />
      </IconButton>
    </Grid>
  );
}

export function ButtonEditTB(props) {
  const classes = LabelStyle();
  return (
    <IconButton
      color="primary"
      className={classes.bticonEdit}
      aria-label="Edit"
      {...props}
    >
      <EditOutlinedIcon className={classes.icon} />
    </IconButton>
  );
}
export function ButtonViewTB(props) {
  const classes = LabelStyle();
  return (
    <IconButton
      color="primary"
      className={classes.bticonEdit}
      aria-label="Edit"
      {...props}
    >
      <VisibilityICON className={classes.icon} />
    </IconButton>
  );
}

export function ButtonDelete(props) {
  const classes = LabelStyle();
  return (
    <IconButton
      size="medium"
      color="secondary"
      className={classes.bticonDel}
      aria-label="Delete"
      {...props}
    >
      <DeleteForeverIcon />
    </IconButton>
  );
}
