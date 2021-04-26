import React, { useEffect, useCallback, useMemo } from "react";
import {
  makeStyles,
  createStyles,
  Theme,
  withStyles,
} from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import axios from "axios";
import IconButton from "@material-ui/core/IconButton";
import VisibilityIcon from "@material-ui/icons/Visibility";
import GetAppIcon from "@material-ui/icons/GetApp";
import DeleteIcon from "@material-ui/icons/Delete";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableRow from "@material-ui/core/TableRow";
import Table from "@material-ui/core/Table";
import AttachFileIcon from "@material-ui/icons/AttachFile";
import { List as LINQ } from "linqts";
import Swal from "sweetalert2";
import {
  Process_System,
  AxiosPost,
  FnBlock_UI,
  DialogConfirmDelete,
  ResultDeleteAPI,
  DialogConfirm,
  ResultAPI,
  SwAlert,
  AlertMsg,
  AlertIcon,
} from "src/components/Systems/SystemComponent";
import Avatar from "@material-ui/core/Avatar";
import Grid from "@material-ui/core/Grid";
import { AxiosPostJson } from "../../../Service/Config/AxiosMethod";
import {
  Responsestart,
  Sweetalert,
} from "src/components/Systems/MaterialComponent";

export const Extension = {
  Image: ["jpg", "jpeg", "png", "gif"],
  Video: ["mov", "wmv", "avi", "mp4"],
  PDF: ["pdf"],
  Document: ["doc", "docx", "xls", "xlsx", "txt"],
  Word: ["doc", "docx"],
  Excel: ["xls", "xlsx"],
  Powpoint: ["pptx", "pdf", "ppt"],
  txt: ["txt"],
  Email: ["msg"],
  Other: ["rar", "zip"],
  AllType: null,
};

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      margin: theme.spacing(1),
      textTransform: "none",
      textAlign: "center",
      "box-shadow":
        "0px 2px 1px -1px rgba(0,0,0,0.2), 0px 1px 1px 0px rgba(0,0,0,0.14), 0px 1px 3px 0px rgba(0,0,0,0.12)",
    },
    media: {
      height: 0,
      paddingTop: "56.25%", // 16:9
    },
    input: {
      display: "none",
    },
    demo: {
      width: "300px",
      wordSpacing: "nowrap",
      overflow: "hidden",
      textOverflow: "ellipsis",
    },
    textMsg: {
      "font-size": "11px",
      "text-align": "start",
      padding: "5px",
      color: "#f60a0a",
    },
  })
);

const StyledTableCell = withStyles((theme: Theme) =>
  createStyles({
    root: {
      padding: 1,
      paddingBottom: 0,
      paddingTop: 0,
      height: "38px",
    },
    head: {
      backgroundColor: "rgba(0, 120, 255, 0.1)",
      color: theme.palette.common.black,
    },
    body: {
      fontSize: 14,
    },
  })
)(TableCell);

const CheckFilesize = (_size: any, Max: any) => {
  if (_size == 0) return "0 Byte";

  let k = 1024,
    sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"],
    i = Math.floor(Math.log(_size) / Math.log(k));
  let result = "";
  let nSize = _size / Math.pow(k, i);
  result = nSize + " " + sizes[i];
  if (sizes[i] == "MB" && nSize > Max) {
    return false;
  } else {
    return true;
  }
};

const CheckFileType = (sName: any, sType: any) => {
  let LF = sType.replace(/,/g, "").split(".");
  let arrFileYupe = sName.toUpperCase().split(".");
  let getLNAme = LF.filter(
    (w: any) => w.toUpperCase() == arrFileYupe[arrFileYupe.length - 1]
  );
  if (getLNAme != null && getLNAme.length > 0) {
    return true;
  } else {
    return false;
  }
};
export const UploadFile = (props: any) => {
  const { BlockUI, UnBlockUI } = FnBlock_UI();
  const {
    size,
    multiple,
    arrData,
    folder,
    defaultFileData,
    isClearFile,
    ResultClearFile,
    name,
    isView,
    AfterBind,
    ...OtherProps
  } = props;

  const classes = useStyles();
  const [FileData, setFileData] = React.useState([] as any);
  const [FileDataErr, setFileDataErr] = React.useState([] as any[]);
  const [selectedFile, setselectedFile] = React.useState([] as File[]);
  const [selectedFileCompleted, setselectedFileCompleted] = React.useState(
    [] as any[]
  );
  let sFileType =
    ".jpeg,.png,.gif,.jpg,.txt,.doc,.docx,.xls,.xlsx,.pdf,.rar,.zip,.mov,.wmv,.avi,.mp4";
  let sFileTypeImage = ".jpeg,.png,.gif,.jpg";
  const REACT_APP_API_URL_File = process.env.REACT_APP_API_URL;
  const REACT_URL_File = process.env.REACT_APP_URL;
  const [arrDataEmp, setDataEmp] = React.useState([] as any[]);

  //#region Change file
  const handleInputChange = (event: any) => {
    let files = event.target.files;
    for (let i = 0; i < files.length; i++) {
      selectedFile.push(files[i]);
    }
    setselectedFile(selectedFile);
    let min = 1;
    selectedFile.forEach((f) => {
      if (!CheckFilesize(f.size, size || 50)) {
        var rand = min + Math.random() * min;
        let obj = {
          id: rand,
          name: f.name,
          size: f.size,
          msg: "Maximum file size " + size + " MB.",
        };
        Sweetalert.Warning(
          AlertIcon.warning,
          "Maximum file size " + size + " MB.",
          null
        );
        FileDataErr.push(obj);
      } else if (!CheckFileType(f.name, sFileType)) {
        var rand = min + Math.random() * min;
        let obj = {
          id: rand,
          name: f.name,
          size: f.size,
          msg: "Wrong type",
        };
        Sweetalert.Warning(
          AlertIcon.warning,
          "This file type is not supported.",
          null
        );

        FileDataErr.push(obj);
      } else {
        selectedFileCompleted.push(f);
      }
      min++;
    });
    if (selectedFileCompleted.length > 0) {
      setselectedFileCompleted([...selectedFileCompleted]);
      onUpload();
    }
    setFileDataErr([...FileDataErr]);
    setselectedFile([]);
    event.target.files = null;
    event.target.value = null;
  };

  const deleteFileErr = (id: any) => {
    setFileDataErr(
      new LINQ<any>(FileDataErr).Where((w) => w.id !== id).ToArray()
    );
  };
  const deleteFileCompleted = (id: any) => {
    console.log("id", id);
    console.log("FileData", FileData);
    DialogConfirmDelete(async () => {
      let q = new LINQ<any>(FileData)
        .Where((w) => w.nID === id)
        .FirstOrDefault();
      if (q != null) {
        if (q.IsNewFile == true) {
          let result: any = await AxiosPostJson("UploadFile/DeleteFile", q);
          if (result.data.sStatus === Responsestart.success) {
            let f = new LINQ<any>(FileData)
              .Where((w) => w.nID !== id)
              .ToArray();
            setFileData([...f]);
            Sweetalert.Success(AlertIcon.success, "", null);
          }
        } else {
          q.IsDelete = true;
          setFileData([...FileData]);
          Swal.close();
        }
      }
      setDataEmp([]);
    });
  };
  const ClearFile = (id: any) => {
    // DialogConfirm(() => {
    let q = new LINQ<any>(FileData).Where((w) => w.nID === id).FirstOrDefault();
    if (q != null) {
      if (q.IsNewFile == true) {
        let obj = JSON.stringify(q);
        AxiosPost(
          "UploadFile/DeleteFile",
          q,
          (response: any) => {
            let f = new LINQ<any>(FileData)
              .Where((w) => w.nID !== id)
              .ToArray();
            setFileData([...f]);
            Swal.close();
          },
          () => {},
          BlockUI,
          UnBlockUI
        );
      } else {
        q.IsDelete = true;
        setFileData([...FileData]);
        Swal.close();
      }
    }
    //     setDataEmp([]);
    // }, null, "Do you want to clear this data?");
  };
  useMemo(() => {
    //call function after rendeer complete
    setFileData(defaultFileData);
  }, [props.defaultFileData]);
  //#region  Updata FileData
  useEffect(() => {
    arrData(FileData);
  }, [FileData]);

  useEffect(() => {
    if (isClearFile) {
      setFileData([]);
      setFileDataErr([]);
      setDataEmp([]);
    }
    if (ResultClearFile != undefined) ResultClearFile(false);
  }, [isClearFile]);
  //#endregion

  const onUpload = useCallback(() => {
    if (selectedFileCompleted.length) {
      let idex = 0;

      selectedFileCompleted.forEach(async (file) => {
        idex++;
        // BlockUI();
        const formPayload = new FormData();
        formPayload.append("file", file);
        formPayload.append("sFolder", folder);
        try {
          await axios({
            baseURL: REACT_APP_API_URL_File,
            url: "/UploadFile/Upload", // "api/UploadFile/Upload"
            method: "post",
            data: formPayload,
            onUploadProgress: (progress) => {
              const { loaded, total } = progress;
              const percentageProgress = Math.floor((loaded / total) * 100);
            },
          })
            .then(function (response) {
              let d = response.data;
              if (d.sStatusUpload == Process_System.process_Success) {
                //if (FileData.filter(f => !f.IsDelete).length > 0) {
                //    let max = new LINQ<any>(FileData).Max(m => m.nID)
                //    let q = new LINQ<any>(FileData).Where(w => w.nID === max).FirstOrDefault();
                //    if (q != null) {
                //        q.IsDelete = true;
                //    }
                //}

                if (d.FileData != null) {
                  let fi = d.FileData;
                  let MaxID =
                    FileData.length > 0
                      ? new LINQ<any>(FileData).Max((m) => m.nID) + 1
                      : 1;
                  let obj = {
                    nID: MaxID,
                    sPath: fi.sPath,
                    sSystemFileName: fi.sSystemFileName,
                    sFileName: fi.sFileName,
                    sSaveToPath: fi.sSaveToPath,
                    sUrl: fi.sUrl,
                    IsDelete: fi.IsDelete,
                    IsNewFile: fi.IsNewFile,
                    sFileType: fi.sFileType,
                  };
                  FileData.push(obj);
                  setFileData(FileData);
                }
              } else if (d.sStatusUpload == Process_System.process_Error) {
                var rand = 10 + Math.random() * 1;
                let obj = {
                  id: rand,
                  name: file.name,
                  size: file.size,
                  msg: "file error",
                };
                FileDataErr.push(obj);
                setFileDataErr([...FileDataErr]);
              }
              if (selectedFileCompleted.length === idex)
                setselectedFileCompleted([]);

              // UnBlockUI();
            })
            .catch(function (error) {
              setselectedFileCompleted([]);
              // UnBlockUI();
            })
            .then(function () {
              setselectedFileCompleted([]);
              // UnBlockUI();
            });
        } catch (error) {}
      });
    }
  }, [selectedFileCompleted, FileData]);

  const TableSus = useCallback(() => {
    return (
      <div className="table-responsive" style={{ maxHeight: "287px" }}>
        <Table aria-label="custom pagination table">
          <TableBody>
            {FileData.map((row, index) => (
              <TableCell
                key={"_" + index}
                style={{ backgroundColor: "#ebffed" }}
              >
                <StyledTableCell
                  component="th"
                  scope="row"
                  style={{ width: "1%" }}
                >
                  <AttachFileIcon />
                </StyledTableCell>
                <StyledTableCell component="th" scope="row">
                  <div className={classes.demo}> {row.sFileName}</div>
                </StyledTableCell>
              </TableCell>
            ))}
          </TableBody>
        </Table>
      </div>
    );
  }, [FileData]);

  const TableCompleted = useCallback(() => {
    console.log("FileData", FileData);
    return (
      <div>
        <div className="table-responsive" style={{ maxHeight: "287px" }}>
          <Table aria-label="custom pagination table">
            <TableBody>
              {FileData.map(
                (row, index) =>
                  !row.IsDelete && (
                    <TableRow key={"_" + index}>
                      <StyledTableCell
                        component="th"
                        scope="row"
                        style={{ width: "1%" }}
                      >
                        <AttachFileIcon />
                      </StyledTableCell>
                      <StyledTableCell component="th" scope="row">
                        <div className={classes.demo}> {row.sFileName}</div>
                      </StyledTableCell>
                      <StyledTableCell
                        component="th"
                        scope="row"
                        style={{ color: "#e30f0f" }}
                      >
                        <a
                          href={REACT_URL_File + "Uploadfiles/" + row.sUrl}
                          target="_blank"
                          download
                        >
                          <IconButton
                            edge="end"
                            color="primary"
                            aria-label="upload picture"
                            component="span"
                            onClick={() => {
                              deleteFileErr(row.nID);
                            }}
                          >
                            <GetAppIcon />
                          </IconButton>
                        </a>
                      </StyledTableCell>
                      <StyledTableCell component="th" scope="row">
                        {(isView || false) === false && (
                          <IconButton
                            edge="end"
                            color="secondary"
                            aria-label="delete"
                            onClick={() => {
                              deleteFileCompleted(row.nID);
                            }}
                          >
                            <DeleteIcon />
                          </IconButton>
                        )}
                      </StyledTableCell>
                    </TableRow>
                  )
              )}
            </TableBody>
          </Table>
        </div>
      </div>
    );
  }, [FileData]);
  return (
    <div className={classes.root}>
      {(isView || false) === false && (
        <div style={{ paddingTop: "23px" }}>
          <input
            className={classes.input}
            id={"contained-button-file" + name}
            // multiple={multiple || true}
            multiple={false}
            type="file"
            onChange={handleInputChange}
            accept={sFileType}
          />
          <label htmlFor={"contained-button-file" + name}>
            <Button
              variant="contained"
              color="primary"
              component="span"
              style={{ textTransform: "none" }}
              startIcon={<i className="fa fa-upload"></i>}
            >
              Upload File{" "}
            </Button>
            <p className={classes.textMsg}>
              ระบบอนุญาตให้อัพโหลดได้เฉพาะไฟล์ .jpeg .png .gif .jpg เท่านั้น
              ขนาดไฟล์ไม่เกิน {size || 50} MB.
            </p>
          </label>
        </div>
      )}
      {FileData.length > 0 && <TableCompleted />}
    </div>
  );
};
