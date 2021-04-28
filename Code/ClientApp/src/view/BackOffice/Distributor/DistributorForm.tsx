import React, { useEffect, useState } from "react";
import { i18n } from "src/i18n";
import * as yup from "yup";
import yupFormSchemas from "src/components/Commons/yup/yupFormSchemas";
import { AxiosGetJson, AxiosPostJson } from "src/Service/Config/AxiosMethod";
import ContentWrapper, {
  FormWrapper,
  FormButtons,
} from "src/components/Commons/style/ContentWrapper";
import { FormProvider, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  Grid,
  Typography,
  Switch,
  FormControl,
  FormGroup,
  FormControlLabel,
} from "@material-ui/core";
import InputFormItem from "src/components/Commons/inputElements/FormItems/InputFormItem";
import AutoCompleteDistName from "src/components/Commons/inputElements/AutoComplete/AutoCompleteDistName";
import TextAreaFormItem from "src/components/Commons/inputElements/FormItems/TextAreaFormItem";
import Fileuploader, {
  Extension,
} from "src/components/Commons/UploadFile/Fileuploader";
import { withStyles } from "@material-ui/core/styles";
import { green, red } from "@material-ui/core/colors";
import BackdropLoading from "src/view/Shared/BackdropLoading";
import {
  ButtonBack,
  ButtonCancel,
  ButtonSaveDraft,
  ButtonSubmit,
  ButtonSave,
} from "src/components/Commons/ButtonAll/ButtonAll";
import {
  Sweetalert,
  AlertIcon,
  DialogConfirm,
} from "src/components/Systems/MaterialComponent";
import { AlertMsg, AlertTitle } from "src/components/Systems/SystemComponent";
import DistributorStaff from "./DistributorStaff";
import Breadcrumb from "src/view/Shared/Breadcrumb";

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

const DistributorForm = (props) => {
  const IsModeEdit = props.nDistID ? true : false;
  const [logo, setLogo] = useState([] as any);
  const [orgChart, setOrgChart] = useState([] as any);
  const [loading, setloading] = useState(false as any);
  const [status, setStatus] = useState(false as any);
  const [arrStaff, setArrStaff] = useState([] as any);

  const schema = yup.object().shape({
    sEmail: yupFormSchemas.string("Email", {
      required: true,
    }),
    sDistName: yupFormSchemas.object("Distributor Name", {
      required: true,
    }),
    sCode: yupFormSchemas.string("Code", {
      required: true,
    }),
    sSoldTo: yupFormSchemas.string("Sold To", {
      required: true,
    }),
    sAddress: yupFormSchemas.string("Address", {
      required: false,
    }),
    sArea: yupFormSchemas.string("เขต", {
      required: false,
    }),
    sMeter: yupFormSchemas.string("Meter", {
      required: false,
    }),
    sInitial: yupFormSchemas.string("ชื่อย่อ", {
      required: false,
    }),
    sWebsite: yupFormSchemas.string("Website", {
      required: false,
    }),
    sDetail: yupFormSchemas.string("Detail", {
      required: false,
    }),
  });

  const [initialValues] = useState(() => {
    return {
      sEmail: null,
      sDistName: null,
      sCode: null,
      sSoldTo: null,
      sAddress: null,
      sArea: null,
      sMeter: null,
      sInitial: null,
      sWebsite: null,
      sDetail: null,
    };
  });

  const form = useForm({
    resolver: yupResolver(schema),
    mode: "all",
    defaultValues: initialValues as any,
  });

  // FileUploader Logo
  const onUploadLogoComplete = (value) => {
    setLogo(value);
  };
  const onRemoveLogoComplete = (value) => {
    setLogo([]);
  };

  // FileUploader Org Chart
  const onUploadChartComplete = (value) => {
    setOrgChart(value);
  };
  const onRemoveChartComplete = (value) => {
    setOrgChart([]);
  };

  let arrSelect = [{ value: "1", label: "selection 1" }];

  const onSetAutoComplete = async (values, arrData) => {
    if (values) {
      arrSelect = arrData;
      setloading(true);
      let result: any = await AxiosGetJson(
        `BackOffice/GetDistributionDetail/${values.value}`
      );
      if (result) {
        let Data: any = result || {};

        //Set Form
        form.setValue("sSoldTo", result.CUST_NUMBER);
        form.setValue("sAddress", result.HOUSE_NUMBER);
        form.setValue("sArea", result.DIFFERENT_CITY);
        form.setValue("sInitial", result.SEARCH_TERM2);
      }
      setloading(false);
    }
  };

  const SaveData = async (saveStatus) => {
    let FormValue = form.getValues();
    let dataSend = {
      nDistributorID: props.nDistID || 0,
      sEmail: FormValue.sEmail,
      sDistributorCode: FormValue.sDistName.value,
      sDistributorName: FormValue.sDistName.label,
      sCode: FormValue.sCode,
      sSoldTo: FormValue.sSoldTo,
      sAddressSoldTo: FormValue.sAddress,
      sDistrict: FormValue.sArea,
      sMeterName: FormValue.sMeter,
      sABDistributorName: FormValue.sInitial,
      sWebsite: FormValue.sWebsite,

      sLogoName: logo.length > 0 ? logo[logo.length - 1].sFileName : null,
      sSysLogoName:
        logo.length > 0 ? logo[logo.length - 1].sSaveToFileName : null,
      sLogoPath: logo.length > 0 ? logo[logo.length - 1].sSaveToPath : null,

      sOrgChartName:
        orgChart.length > 0 ? orgChart[orgChart.length - 1].sFileName : null,
      sSysOrgChartName:
        orgChart.length > 0
          ? orgChart[orgChart.length - 1].sSaveToFileName
          : null,
      sOrgChartPath:
        orgChart.length > 0 ? orgChart[orgChart.length - 1].sSaveToPath : null,
      sDetail: FormValue.sDetail,
      cStatus: status == true ? "Y" : "N",
      arrStaff: arrStaff,
    };
    DialogConfirm(async () => {
      let result: any = await AxiosPostJson(
        "BackOffice/SaveDistributor",
        dataSend
      );
      if (result.status === 200) {
        Sweetalert.Success(AlertTitle.Success, AlertMsg.SaveComplete, null);
      } else {
        Sweetalert.Error(AlertIcon.error, result.sMsg, null);
      }
    });
  };

  const GetDistDetail = async () => {
    setloading(true);
    let resp: any = await AxiosGetJson(
      `BackOffice/GetDistributorDetail/${props.nDistID}`
    );
    if (resp) {
      SetDetailEditMode(resp);
    }

    let respStaff: any = await AxiosGetJson(
      `BackOffice/GetDistributorStaff/${props.nDistID}`
    );
    if (resp) {
      setArrStaff(respStaff);
    }
    setloading(false);
  };

  const SetDetailEditMode = async (rec) => {
    form.setValue("sEmail", rec.sEmail);
    form.setValue("sDistName", {
      value: rec.sDistributorCode,
      label: rec.sDistributorName,
    });
    form.setValue("sCode", rec.sCode);
    form.setValue("sSoldTo", rec.sSoldTo);
    form.setValue("sAddress", rec.sAddressSoldTo);
    form.setValue("sArea", rec.sDistrict);
    form.setValue("sMeter", rec.sMeterName);
    form.setValue("sInitial", rec.sABDistributorName);
    form.setValue("sWebsite", rec.sWebsite);
    form.setValue("sDetail", rec.sDetail);

    setLogo([
      {
        sSaveToPath: rec.sLogoPath || "",
        sFileName: rec.sLogoName || "",
        sSaveToFileName: rec.sSysLogoName || "",
      },
    ]);

    setOrgChart([
      {
        sSaveToPath: rec.sOrgChartPath || "",
        sFileName: rec.sOrgChartName || "",
        sSaveToFileName: rec.sSysOrgChartName || "",
      },
    ]);

    setStatus(rec.cStatus == "Y" ? true : false);
  };

  useEffect(() => {
    if (IsModeEdit) {
      GetDistDetail();
    }
  }, []);

  return (
    <>
      <BackdropLoading open={loading} />
      <Breadcrumb
        titlePage={"Distributor Add/Edit"}
        items={[
          ["Home", "/"],
          ["Distributor List", `/b_distributor/list`],
          ["Distributor Add/Edit"],
        ]}
      />
      <FormWrapper>
        <Grid spacing={1} container>
          <FormProvider {...form}>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sEmail" label={"Email"} required={true} />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <AutoCompleteDistName
                name="sDistName"
                label={"Distributor Name"}
                onSetValues={onSetAutoComplete}
                options={arrSelect}
                required={true}
              />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sCode" label={"Code"} required={true} />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sSoldTo" label={"Sold To"} required={true} />
            </Grid>
            <Grid item lg={12} md={12} sm={12} xs={12}>
              <InputFormItem name="sAddress" label={"Address"} />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sArea" label={"เขต"} />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sMeter" label={"Meter"} />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sInitial" label={"ชื่อย่อ"} />
            </Grid>
            <Grid item lg={6} md={6} sm={6} xs={6}>
              <InputFormItem name="sWebsite" label={"Website"} />
            </Grid>
            <Grid item lg={12} md={12} sm={12} xs={12}>
              <Typography variant="subtitle1">Logo</Typography>
              <Fileuploader
                limit="1"
                fileList={logo || []}
                onComplete={onUploadLogoComplete}
                onRemoveComplete={onRemoveLogoComplete}
                fileMaxSize="10"
                extensions={Extension.Image}
                readOnly={false}
              />
            </Grid>
            <Grid item lg={12} md={12} sm={12} xs={12}>
              <Typography variant="subtitle1">Organization Chart</Typography>
              <Fileuploader
                limit="1"
                fileList={orgChart || []}
                onComplete={onUploadChartComplete}
                onRemoveComplete={onRemoveChartComplete}
                fileMaxSize="10"
                extensions={Extension.Image}
                readOnly={false}
              />
            </Grid>
            <Grid item lg={12} md={12} sm={12} xs={12}>
              <TextAreaFormItem name="sDetail" label={"Detail"} rows={3} />
            </Grid>
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

            {/* Staff */}
            <Grid item lg={12} md={12} sm={12} xs={12}>
              <DistributorStaff
                nDistID={props.nDistID}
                arrStaff={arrStaff}
                setArrStaff={setArrStaff}
              />
            </Grid>

            <Grid item lg={12} md={12} sm={12} xs={12}>
              <div style={{ height: 50 }}></div>
            </Grid>

            {/* Button */}
            <Grid
              container
              item
              justify="center"
              lg={12}
              md={12}
              sm={12}
              xs={12}
              spacing={1}
            >
              <ButtonSave
                onClick={form.handleSubmit((o) => {
                  SaveData(1);
                })}
                //   onClick={() => {
                //     SaveData(1);
                //   }}
              ></ButtonSave>
              {/* <ButtonSubmit
              onClick={form.handleSubmit((o) => {
                SaveData(2);
              })}
            ></ButtonSubmit> */}
            </Grid>
          </FormProvider>
        </Grid>
      </FormWrapper>
    </>
  );
};

export default DistributorForm;
