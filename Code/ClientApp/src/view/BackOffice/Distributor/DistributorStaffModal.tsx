import React, { useEffect, useState } from "react";
import { i18n } from "src/i18n";
import { AxiosGetJson, AxiosPostJson } from "src/Service/Config/AxiosMethod";
import { FormButtons } from "src/components/Commons/style/ContentWrapper";
import {
  Grid,
  Typography,
  Switch,
  FormControl,
  FormGroup,
  FormControlLabel,
  Button,
} from "@material-ui/core";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
import EventNoteIcon from "@material-ui/icons/EventNote";
import BackdropLoading from "src/view/Shared/BackdropLoading";
import InputFormItem from "src/components/Commons/inputElements/FormItems/InputFormItem";
import { FormProvider, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import yupFormSchemas from "src/components/Commons/yup/yupFormSchemas";
import { withStyles } from "@material-ui/core/styles";
import { green, red } from "@material-ui/core/colors";
import Fileuploader, {
  Extension,
} from "src/components/Commons/UploadFile/Fileuploader";

const ColorSwitch = withStyles({
  switchBase: {
    color: red[500],
    "&$checked": {
      color: green[500],
    },
    "&$checked + $track": {
      backgroundColor: green[500],
    },
  },
  checked: {},
  track: {},
})(Switch);

const StyledDialog = withStyles({
  root: {
    position: "fixed",
    zIndex: 1,
    right: "0px",
    bottom: "0px",
    top: "0px",
    left: "0px",
  },
})(Dialog);

const DistributorStaffModal = (props) => {
  const [loading, setloading] = useState(false as any);
  const [status, setStatus] = useState(false as any);
  const [avatar, setAvatar] = useState([] as any);

  const schema = yup.object().shape({
    sName: yupFormSchemas.string("sName", {
      required: true,
    }),
    sEmail: yupFormSchemas.string("Email", {
      required: true,
    }),
    sUsername: yupFormSchemas.string("sUsername", {
      required: true,
    }),
    sPassword: yupFormSchemas.string("sPassword", {
      required: true,
    }),
  });

  const [initialValues] = useState(() => {
    return {
      sName: null,
      sEmail: null,
      sUsername: null,
      sPassword: null,
    };
  });

  const form = useForm({
    resolver: yupResolver(schema),
    mode: "all",
    defaultValues: initialValues as any,
  });

  // FileUploader Avatar
  const onUploadComplete = (value) => {
    setAvatar(value);
  };
  const onRemoveComplete = (value) => {
    setAvatar([]);
  };

  const onAdd = (form) => {
    let objStaff = {
      sName: form.sName,
      sEmail: form.sEmail,
      sUserName: form.sUsername,
      sPassword: form.sPassword,
      cStatus: status,
      sAvatarPath: "",
      sAvatarName: "",
      sAvatarSysFileName: "",
    };
    props.onAddStaff(objStaff);
  };

  useEffect(() => {}, []);

  return (
    <React.Fragment>
      <BackdropLoading open={loading} />
      <Grid container spacing={1}>
        <FormProvider {...form}>
          {!loading && (
            <StyledDialog
              key={"dialog-edit"}
              aria-labelledby="form-dialog-title"
              open={props.open}
              onClose={props.onCloseModal}
              scroll="paper"
              maxWidth="md"
              fullWidth={true}
            >
              <DialogTitle>
                <FormControlLabel control={<EventNoteIcon />} label={"Staff"} />
              </DialogTitle>
              <DialogContent dividers={true}>
                <Grid container spacing={1}>
                  <Grid item lg={6} md={6} sm={6} xs={6}>
                    <InputFormItem
                      name="sName"
                      label={"Name"}
                      required={true}
                    />
                  </Grid>
                  <Grid item lg={6} md={6} sm={6} xs={6}>
                    <InputFormItem
                      name="sEmail"
                      label={"Email"}
                      required={true}
                    />
                  </Grid>
                  <Grid item lg={6} md={6} sm={6} xs={6}>
                    <InputFormItem
                      name="sUsername"
                      label={"Username"}
                      required={true}
                    />
                  </Grid>
                  <Grid item lg={6} md={6} sm={6} xs={6}>
                    <InputFormItem
                      name="sPassword"
                      label={"Password"}
                      required={true}
                    />
                  </Grid>

                  {/* file upload */}
                  {/* <Grid item lg={12} md={12} sm={12} xs={12}>
                    <Typography variant="subtitle1">Avatar</Typography>
                    <Fileuploader
                      limit="1"
                      fileList={avatar || []}
                      onComplete={onUploadComplete}
                      onRemoveComplete={onRemoveComplete}
                      fileMaxSize="10"
                      extensions={Extension.Image}
                      modalMode={modalMode}
                      readOnly={false}
                    />
                  </Grid> */}

                  {/* status */}
                  <Grid item lg={12} md={12} sm={12} xs={12}>
                    <FormControl component="fieldset">
                      <FormGroup aria-label="position" row>
                        <FormControlLabel
                          value="start"
                          control={
                            <ColorSwitch
                              onChange={(e) => {
                                setStatus(e.target.checked);
                              }}
                              checked={status}
                              size="medium"
                            />
                          }
                          label="Status   :"
                          labelPlacement="start"
                        />
                      </FormGroup>
                    </FormControl>
                  </Grid>
                </Grid>
              </DialogContent>
              <DialogActions>
                <FormButtons>
                  <Button
                    onClick={form.handleSubmit(onAdd)}
                    variant="outlined"
                    color="primary"
                  >
                    {i18n("common.save")}
                  </Button>
                  <Button
                    onClick={props.onCloseModal}
                    variant="outlined"
                    color="secondary"
                  >
                    {i18n("common.cancel")}
                  </Button>
                </FormButtons>
              </DialogActions>
            </StyledDialog>
          )}
        </FormProvider>
      </Grid>
    </React.Fragment>
  );
};

export default DistributorStaffModal;
