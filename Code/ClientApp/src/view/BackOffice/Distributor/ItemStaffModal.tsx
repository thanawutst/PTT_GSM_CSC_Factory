import React, { useEffect, useState } from "react";
import { i18n } from "src/i18n";
import { AxiosGetJson, AxiosPostJson } from "src/Service/Config/AxiosMethod";
import { FormButtons } from "src/components/Commons/style/ContentWrapper";
import Grid from "@material-ui/core/Grid";
import { Button, FormControlLabel } from "@material-ui/core";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
import EventNoteIcon from "@material-ui/icons/EventNote";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import BackdropLoading from "src/view/Shared/BackdropLoading";

const ItemStaffModal = (props) => {
  const { nDistID } = props;
  const [arrStaff, setArrStaff] = useState([] as any);
  const [loading, setloading] = useState(false as any);
  const GetStaff = async () => {
    setloading(true);
    let resp: any = await AxiosGetJson(
      `BackOffice/GetDistributorStaff/${nDistID}`
    );
    if (resp) {
      setArrStaff(resp);
    }
    setloading(false);
  };

  useEffect(() => {
    if (nDistID) {
      GetStaff();
    }
  }, [nDistID]);

  return (
    <React.Fragment>
      <Grid container spacing={1}>
        <BackdropLoading open={loading} />
        {!loading && (
          <Dialog
            aria-labelledby="form-dialog-title"
            open={props.open}
            onClose={props.onCloseModal}
            scroll="paper"
            maxWidth="md"
            fullWidth={true}
          >
            <DialogTitle>
              <FormControlLabel
                control={<EventNoteIcon />}
                label={" Distributor Staff"}
              />
            </DialogTitle>
            <DialogContent dividers={true}>
              <Grid item xs={12} sm={12}>
                <TableContainer component={Paper}>
                  <Table aria-label="simple table">
                    <TableHead>
                      <TableRow>
                        <TableCell
                          align="center"
                          style={{
                            backgroundColor: "#76CAFF",
                          }}
                        >
                          Name
                        </TableCell>
                        <TableCell
                          align="center"
                          style={{
                            backgroundColor: "#76CAFF",
                          }}
                        >
                          Username
                        </TableCell>
                        <TableCell
                          align="center"
                          style={{
                            backgroundColor: "#76CAFF",
                          }}
                        >
                          Password
                        </TableCell>
                        <TableCell
                          align="center"
                          style={{
                            backgroundColor: "#76CAFF",
                          }}
                        >
                          Email
                        </TableCell>
                        <TableCell
                          align="center"
                          style={{
                            backgroundColor: "#76CAFF",
                          }}
                        >
                          Status
                        </TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {arrStaff.map((row) => (
                        <TableRow key={row.id}>
                          <TableCell align="center">{row.sName}</TableCell>
                          <TableCell align="center">{row.sUserName}</TableCell>
                          <TableCell align="center">{row.sPassword}</TableCell>
                          <TableCell align="center">{row.sEmail}</TableCell>
                          <TableCell align="center">
                            {row.cStatus ? "Enable" : "Disable"}
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              </Grid>
            </DialogContent>
            <DialogActions>
              <FormButtons>
                <Button onClick={props.onCloseModal} variant="outlined">
                  {i18n("common.close")}
                </Button>
              </FormButtons>
            </DialogActions>
          </Dialog>
        )}
      </Grid>
    </React.Fragment>
  );
};

export default ItemStaffModal;
