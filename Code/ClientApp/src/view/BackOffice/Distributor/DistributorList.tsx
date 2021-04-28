import React, { useEffect, useState } from "react";
import { AxiosGetJson } from "src/Service/Config/AxiosMethod";
import { Button, Grid, IconButton } from "@material-ui/core";
import BackdropLoading from "src/view/Shared/BackdropLoading";
import MUIDataTable from "mui-datatables";
import AddIcon from "@material-ui/icons/Add";
import {
  ButtonAddHead,
  ButtonEditTB,
  ButtonViewTB,
} from "src/components/Commons/ButtonAll/ButtonAll";
import { makeStyles } from "@material-ui/core";
import { useHistory } from "react-router";
import Breadcrumb from "src/view/Shared/Breadcrumb";
import { FormWrapper } from "src/components/Commons/style/ContentWrapper";
import { MuiIconButtonItem } from "src/components/Systems/MaterialComponent";
import ItemStaffModal from "./ItemStaffModal";

const DistributorList = (props) => {
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

  const [ArrDist, setArrDist] = useState([] as any);
  console.log("ArrDist =>", ArrDist);
  const [loading, setloading] = useState(false as any);
  const [selectedRows, SetSelectedRows] = useState([]);
  const [OpenStaffModal, SetOpenStaffModal] = useState(false);
  const [DistIDOpenModal, SetDistIDOpenModal] = useState(false);

  const GetDistList = async () => {
    setloading(true);
    let resp: any = await AxiosGetJson(`BackOffice/GetDistributorList`);
    if (resp) {
      setArrDist(resp);
    }
    setloading(false);
  };

  const onCloseModal = () => {
    SetOpenStaffModal(!OpenStaffModal);
  };

  useEffect(() => {
    GetDistList();
  }, []);

  const columnsTable = [
    {
      name: "sDistributorName",
      options: {
        customHeadLabelRender: () => <b>Name</b>,
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
      },
    },
    {
      name: "sSoldTo",
      options: {
        customHeadLabelRender: () => <b>Sold to</b>,
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
      },
    },
    {
      name: "cStatus",
      options: {
        customHeadLabelRender: () => <b>Status</b>,
        customBodyRender: (value, tableMeta, updateValue) => {
          return value == "Y" ? "Enable" : "Disable";
        },
      },
    },
    {
      name: "nStaff",
      options: {
        customHeadLabelRender: () => <b>Staff</b>,
        setCellHeaderProps: () => {
          return { style: { textAlign: "center" } };
        },
        customBodyRender: (value, tableMeta, updateValue) => (
          <MuiIconButtonItem
            rowCount={value}
            onClick={(e) => {
              onClickStaffModal(tableMeta.rowData[4]);
            }}
          />
        ),
      },
    },
    {
      name: "nDistributorID",
      options: {
        filter: false,
        sort: false,
        customHeadLabelRender: () => (
          <IconButton
            className={classes.bticon}
            aria-label="Add"
            onClick={() => {
              toAdd();
            }}
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
          <div>
            <ButtonEditTB onClick={(o) => toEdit(value)}></ButtonEditTB>
          </div>
        ),
      },
    },
  ];

  const onClickStaffModal = (value) => {
    console.log("value =>", value);
    SetOpenStaffModal(true);
    SetDistIDOpenModal(value);
  };

  const history = useHistory();

  const toAdd = () => {
    let sUrl = `/b_distributor`;
    history.push(sUrl);
  };

  const toEdit = (value) => {
    let sUrl = `/b_distributor?ID=${value}`;
    history.push(sUrl);
  };

  const options = {
    responsive: "stacked",
    fixedSelectColumn: false,
    fixedHeader: false,
    print: false,
    download: false,
    viewColumns: false,
    textLabels: {
      body: {
        noMatch: "No Data",
      },
    },
    rowsPerPage: 10,
    rowsPerPageOptions: [10, 20, 50, 100],
    customToolbarSelect: () => {},
    onRowSelectionChange: (rowsSelectedData, allRows, rowsSelected) => {
      SetSelectedRows(() => rowsSelected);
    },
    rowsSelected: selectedRows,
  };

  return (
    <>
      <BackdropLoading open={loading} />
      <Breadcrumb
        titlePage={"Distributor List"}
        items={[["Home", "/"], ["Distributor List"]]}
      />
      <FormWrapper>
        <Grid spacing={1} container>
          <Grid item lg={12} md={12} sm={12} xs={12}>
            {/* Table */}
            <MUIDataTable
              data={ArrDist || []}
              columns={columnsTable}
              options={options}
            />
          </Grid>

          {/*staff modal */}
          <Grid item lg={12} md={12} sm={12} xs={12}>
            <ItemStaffModal
              open={OpenStaffModal}
              nDistID={DistIDOpenModal}
              onCloseModal={onCloseModal}
            />
          </Grid>
        </Grid>
      </FormWrapper>
    </>
  );
};

export default DistributorList;
