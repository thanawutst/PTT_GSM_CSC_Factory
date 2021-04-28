import React, { useEffect, useState } from "react";
import { i18n } from "src/i18n";
import * as yup from "yup";
import yupFormSchemas from "src/components/Commons/yup/yupFormSchemas";
import { AxiosGetJson, AxiosPostJson } from "src/Service/Config/AxiosMethod";
import ContentWrapper from "src/components/Commons/style/ContentWrapper";
import { FormProvider, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import Grid from "@material-ui/core/Grid";
import { Typography } from "@material-ui/core";
import { ButtonAdd } from "src/components/Commons/ButtonAll/ButtonAll";
import DistributorStaffModal from "./DistributorStaffModal";
import Paper from "@material-ui/core/Paper";
import { Theme, createStyles, makeStyles } from "@material-ui/core/styles";
import Fileuploader, {
  Extension,
} from "src/components/Commons/UploadFile/Fileuploader";

const DistributorStaff = (props) => {
  const [openModal, setOpenModal] = useState(false as any);
  //   const [arrStaff, setArrStaff] = useState([] as any);
  const { arrStaff, setArrStaff } = props;
  const schema = yup.object().shape({});

  const [initialValues] = useState(() => {
    return {
      sProject: null,
    };
  });

  const form = useForm({
    resolver: yupResolver(schema),
    mode: "all",
    defaultValues: initialValues as any,
  });

  const onAddStaff = (value) => {
    let arr = arrStaff;
    arr.push(value);
    setArrStaff(arr);
    setOpenModal(false);
  };

  // FileUploader Avatar
  const onUploadComplete = (value, index) => {
    let arr = arrStaff;
    arr[index].sAvatarPath = value[value.length - 1].sSaveToPath || null;
    arr[index].sAvatarName = value[value.length - 1].sFileName || null;
    arr[index].sAvatarSysFileName =
      value[value.length - 1].sSaveToFileName || null;
    setArrStaff(arr);
  };
  const onRemoveComplete = (value, index) => {
    let arr = arrStaff;
    arr[index].sAvatarPath = null;
    arr[index].sAvatarName = null;
    arr[index].sAvatarSysFileName = null;
    setArrStaff(arr);
  };

  useEffect(() => {}, [arrStaff]);

  const onCloseModal = () => {
    setOpenModal(!openModal);
  };

  return (
    <ContentWrapper>
      <Grid spacing={1} container>
        <FormProvider {...form}>
          <Grid item lg={1} md={1} sm={1} xs={1}>
            <Typography variant="subtitle1">Staff</Typography>
          </Grid>
          <Grid item lg={11} md={11} sm={11} xs={11}>
            <ButtonAdd onClick={() => setOpenModal(true)}></ButtonAdd>
          </Grid>

          {/* arrStaff */}
          {arrStaff.length > 0 &&
            arrStaff.map((item, index) => (
              <Grid key={index + 1} item lg={6} md={6} sm={6} xs={6}>
                <ContentWrapper>
                  <Fileuploader
                    limit="1"
                    fileList={
                      [
                        {
                          sSaveToPath: item.sAvatarPath || "",
                          sFileName: item.sAvatarName || "",
                          sSaveToFileName: item.sAvatarSysFileName || "",
                        },
                      ] || []
                    }
                    onComplete={(e) => onUploadComplete(e, index)}
                    onRemoveComplete={(e) => onRemoveComplete(e, index)}
                    fileMaxSize="10"
                    extensions={Extension.Image}
                    readOnly={false}
                  />
                  <Grid item lg={12} md={12} sm={12} xs={12}>
                    <Typography variant="subtitle1">
                      Name : {item.sName}
                    </Typography>
                  </Grid>
                  <Grid item lg={12} md={12} sm={12} xs={12}>
                    <Typography variant="subtitle1">
                      Email : {item.sEmail}
                    </Typography>
                  </Grid>
                  <Grid item lg={12} md={12} sm={12} xs={12}>
                    <Typography variant="subtitle1">
                      Username : {item.sUserName}
                    </Typography>
                  </Grid>
                  <Grid item lg={12} md={12} sm={12} xs={12}>
                    <Typography variant="subtitle1">
                      Password : {item.sPassword}
                    </Typography>
                  </Grid>
                  <Grid item lg={12} md={12} sm={12} xs={12}>
                    <Typography variant="subtitle1">
                      Status : {item.cStatus ? "Enable" : "Disable"}
                    </Typography>
                  </Grid>
                </ContentWrapper>
              </Grid>
            ))}

          {/* modal */}
          <Grid item lg={12} md={12} sm={12} xs={12}>
            <DistributorStaffModal
              open={openModal}
              onAddStaff={onAddStaff}
              onCloseModal={onCloseModal}
            />
          </Grid>
        </FormProvider>
      </Grid>
    </ContentWrapper>
  );
};

export default DistributorStaff;
