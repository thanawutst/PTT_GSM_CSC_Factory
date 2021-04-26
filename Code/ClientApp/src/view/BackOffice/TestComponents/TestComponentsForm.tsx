import React, { useEffect, useState } from "react";
import { i18n } from "src/i18n";
import * as yup from "yup";
import yupFormSchemas from "src/components/Commons/yup/yupFormSchemas";
import { Button, Grid, IconButton } from "@material-ui/core";
import { AxiosGetJson, AxiosPostJson } from "src/Service/Config/AxiosMethod";
import ContentWrapper, {
  FormButtons,
} from "src/components/Commons/style/ContentWrapper";
import { FormProvider, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import SelectFormItem from "src/components/Commons/inputElements/FormItems/SelectFormItem";
import TextAreaFormItem from "src/components/Commons/inputElements/FormItems/TextAreaFormItem";
import DatePickerFormItem from "src/components/Commons/inputElements/FormItems/DatePickerFormItem";
import CheckboxFormItem from "src/components/Commons/inputElements/FormItems/CheckboxFormItem";
import InputNumberFormItem from "src/components/Commons/inputElements/FormItems/InputNumberFormItem";
import InputFormItem from "src/components/Commons/inputElements/FormItems/InputFormItem";
import FroalaBasicMoreTools from "src/components/Commons/inputElements/FormItems/FroalaBasicMoreTools";
import TimePickerFormItem from "src/components/Commons/inputElements/FormItems/TimePickerFormItem";
import SwitchFormItem from "src/components/Commons/inputElements/FormItems/SwitchFormItem";
import AutocompleteFormItem from "src/components/Commons/inputElements/FormItems/AutocompleteFormItem";
import {
  ButtonAddHead,
  ButtonEditTB,
  ButtonViewTB,
} from "src/components/Commons/ButtonAll/ButtonAll";
import { makeStyles } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import MUIDataTable from "mui-datatables";
import { UploadFile } from "src/components/Commons/UploadFile/Fileuploader";
import { Cancel, SaveAlt, Save, Add } from "@material-ui/icons";

const arrSelect = [
  { value: "1", label: "selection 1" },
  { value: "2", label: "selection 2" },
  { value: "3", label: "selection 3" },
  { value: "4", label: "selection 4" },
  { value: "5", label: "selection 5" },
  { value: "6", label: "selection 6" },
  { value: "7", label: "selection 7" },
  { value: "8", label: "selection 8" },
];

const arrDataList = [
  {
    nReqTravelID: 1,
    nYear: 2020,
    sMonth: "April",
    nItem: 2,
    sRequester: "Test 1",
    dRequestDate: "19/04/2021",
    sStatus: "1",
  },
  {
    nReqTravelID: 2,
    nYear: 2020,
    sMonth: "April",
    nItem: 1,
    sRequester: "Test 2",
    dRequestDate: "13/04/2021",
    sStatus: "2",
  },
  {
    nReqTravelID: 3,
    nYear: 2021,
    sMonth: "May",
    nItem: 3,
    sRequester: "Test 3",
    dRequestDate: "19/04/2021",
    sStatus: "11",
  },
  {
    nReqTravelID: 4,
    nYear: 2021,
    sMonth: "May",
    nItem: 4,
    sRequester: "Test 4",
    dRequestDate: "13/04/2021",
    sStatus: "12",
  },
];

const TestComponentsForm = (props) => {
  const lable = makeStyles({
    Active: {
      color: "green",
    },
    Inactive: {
      color: "red",
    },
    icon: {
      color: "white",
    },
    bticon: {
      backgroundColor: "#2196f3",
      "&:hover": {
        backgroundColor: "#218af3",
      },
    },
    bticonEdit: {
      backgroundColor: "#72cff8",
      "&:hover": {
        backgroundColor: "#72cff8",
      },
    },
    Alignicon: {
      textAlign: "center",
    },
    setHead: {
      // marginLeft: '4%',
      textAlign: "center",
      marginTop: 5,
    },
    FontHeadTable: {
      width: "10%",
      fontWeight: "bold",
      textAlign: "center",
    },
  });
  const classes = lable();
  const [arrData, setarrData] = useState([] as any[]);
  const [arrSelectForm, setArrSelectForm] = useState([] as any);

  const schema = yup.object().shape({
    sInput: yupFormSchemas.string("Input Form Item", {
      required: true,
    }),
    nInputNumber: yupFormSchemas.string("Input Form Item", {
      required: true,
    }),
    sSelect: yupFormSchemas.integer("Select Form Item", {
      required: true,
    }),
    sMultiSelect: yupFormSchemas.string("Multi Select", {
      required: true,
    }),
    dDatePicker: yupFormSchemas.date("Date Picker", {
      required: true,
    }),
    dTimePicker: yupFormSchemas.date("Time Picker", {
      required: true,
    }),
    sTextArea: yupFormSchemas.string("Text Area", {
      required: true,
    }),
  });

  const [initialValues] = useState(() => {
    return {};
  });

  const form = useForm({
    resolver: yupResolver(schema),
    mode: "all",
    defaultValues: initialValues as any,
  });

  useEffect(() => {
    // GetProjectLst();
    setArrSelectForm(arrSelect);
    setarrData(arrDataList);
  }, []);

  const onValidate = () => {
    console.log("validate => Complete");
  };

  const columnsTable = [
    {
      name: "nYear",
      options: {
        // setCellProps: () => {
        //   return { style: { textAlign: "center" } };
        // },
        customHeadLabelRender: () => <b>Year</b>,
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
      },
    },
    {
      name: "sMonth",
      label: "Month",
      options: {
        // setCellProps: () => {
        //   return { style: { textAlign: "center" } };
        // },
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
      },
    },
    {
      name: "nItem",
      label: "Item",
      options: {
        // setCellProps: () => {
        //   return { style: { textAlign: "center" } };
        // },
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
      },
    },
    {
      name: "sRequester",
      label: "Requester",
      options: {
        filter: true,
      },
    },
    {
      name: "dRequestDate",
      label: "Request Date",
      options: {
        // setCellProps: () => {
        //   return { style: { textAlign: "center" } };
        // },
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
      },
    },
    {
      name: "sStatus",
      label: "Status",
      options: {
        // setCellProps: () => {
        //   return { style: { textAlign: "center" } };
        // },
        setCellHeaderProps: () => {
          return { style: { textAlign: "center", width: "170px" } };
        },
        customBodyRender: (value, tableMeta, updateValue) => {
          return tableMeta.rowData[5] == 1
            ? "Save Draft"
            : tableMeta.rowData[5] == 2
            ? "Cancel By Requester"
            : tableMeta.rowData[5] == 11
            ? "Waiting HR"
            : tableMeta.rowData[5] == 12
            ? "Cancel By HR"
            : tableMeta.rowData[5] == 13
            ? "Complete"
            : "";
        },
      },
    },
    {
      name: "nReqTravelID",
      label: "Action",
      options: {
        filter: false,
        sort: false,
        customHeadLabelRender: () => (
          <IconButton
            className={classes.bticon}
            aria-label="Add"
            // onClick={() => {
            //   setIsRedirect(true);
            // }}
          >
            <AddIcon className={classes.icon} />
          </IconButton>
        ),
        setCellHeaderProps: () => {
          return {
            style: {
              width: 1,
            },
          };
        },
        customBodyRender: (value, tableMeta, updateValue) => (
          <>
            {tableMeta.rowData[5] == 1 || tableMeta.rowData[5] == 11 ? (
              <div>
                <ButtonEditTB></ButtonEditTB>
              </div>
            ) : (
              <div>
                <ButtonViewTB></ButtonViewTB>
              </div>
            )}
          </>
        ),
      },
    },
  ];

  const options = {
    responsive: "stacked",
    fixedSelectColumn: false,
    fixedHeader: false,
    print: false,
    download: false,
    viewColumns: false,
    // search: false,
    // filter: false,
    textLabels: {
      body: {
        noMatch: "No Data",
      },
    },
    rowsPerPage: 10,
    rowsPerPageOptions: [10, 20, 50, 100],
    // customToolbarSelect: () => {},
    // onRowSelectionChange: (rowsSelectedData, allRows, rowsSelected) => {
    //   SetSelectedRows(() => rowsSelected);
    // },
    // rowsSelected: selectedRows,
  };

  return (
    <Grid spacing={1} container>
      <FormProvider {...form}>
        <Grid item lg={6} md={6} sm={6} xs={6}>
          <InputFormItem name="sInput" label={"Input Form"} />
        </Grid>
        <Grid item lg={6} md={6} sm={6} xs={6}>
          <InputNumberFormItem
            name="nInputNumber"
            required={true}
            label={"Input Number Form"}
          />
        </Grid>
        <Grid item lg={6} md={6} sm={6} xs={6}>
          <SelectFormItem
            name="sSelect"
            label={"Select Form"}
            options={arrSelectForm}
            required={true}
          />
        </Grid>
        <Grid item lg={6} md={6} sm={6} xs={6}>
          <SelectFormItem
            name="sMultiSelect"
            label={"Multi Select Form"}
            options={arrSelectForm}
            mode={"multiple"}
          />
        </Grid>
        {/* <Grid item lg={12} md={12} sm={12} xs={12}>
          <FroalaBasicMoreTools id="sTextEditor" labal={"Text Editor"} />
        </Grid> */}
        <Grid item lg={6} md={6} sm={6} xs={6}>
          <AutocompleteFormItem name="sAutoComplete" label={"Auto Complete"} />
        </Grid>
        <Grid item lg={3} md={3} sm={3} xs={3}>
          <DatePickerFormItem
            name="dDatePicker"
            required={true}
            label={"Date Picker"}
          />
        </Grid>
        <Grid item lg={3} md={3} sm={3} xs={3}>
          <TimePickerFormItem
            name="dTimePicker"
            required={true}
            label={"Time Picker"}
          />
        </Grid>
        <Grid item lg={3} md={3} sm={3} xs={3}>
          <CheckboxFormItem
            name="bCheckBox"
            label={"Checkbox"}
            labelPlacement="end"
          />
        </Grid>
        <Grid item lg={3} md={3} sm={3} xs={3}>
          <SwitchFormItem name="bSwitch" label={"Switch"} />
        </Grid>
        <Grid item lg={12} md={12} sm={12} xs={12}>
          <TextAreaFormItem
            name="sTextArea"
            label={"Text Area Form"}
            required={true}
          />
        </Grid>

        {/* UploadFile */}
        <Grid item lg={12} md={12} sm={12} xs={12}>
          {/* <UploadFile
            name="KM"
            defaultFileData={{}}
            size={50}
            folder={"KM"}
            arrData={(e: any) => {
              setFileData_(e);
            }}
            isClearFile={false}
            ResultClearFile={() => setisClearFile(false)}
          /> */}
        </Grid>

        <Grid item lg={12} md={12} sm={12} xs={12}>
          <Button
            variant="contained"
            onClick={(e) => {
              form.handleSubmit(onValidate)(e);
            }}
            color="primary"
            size="large"
            startIcon={<Save />}
          >
            {"Validate"}
          </Button>
        </Grid>

        {/* Table */}
        <Grid item lg={12} md={12} sm={12} xs={12}>
          <MUIDataTable
            data={arrData || []}
            columns={columnsTable}
            options={options}
          />
        </Grid>
      </FormProvider>
    </Grid>
  );
};

export default TestComponentsForm;
