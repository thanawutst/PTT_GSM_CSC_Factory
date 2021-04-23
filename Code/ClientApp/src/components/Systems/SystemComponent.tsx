import Swal, { SweetAlertIcon } from "sweetalert2";
import { useDispatch } from "react-redux";
import axios from "axios";
import Crypto from "crypto-js";
import parse from 'html-react-parser';


export const arrShortMonth = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

export const ColumnWidthInput = {
    Delete: 50,
    No: 70,
    General: 200,
    Specific: 300,
    Unit: 150,
    Source: 250,
    Month: 160,
}

export const AlertTitle = {
    Success: "Action Completed",
    Warning: "Warning",
    Error: "Error",
    Info: "Information",
    Confirm: "Confirmation",
    Hint: "Hint",
    Duplicate: "Duplicate",
    Session: 'Session Timeout',
};

export const AlertMsg = {
  SaveComplete: "Data is already saved.",
  RejectComplete: "Data is already rejected.",  
  CanceledComplete: "Data is already canceled.",       
  DeleteComplete: "Delete is completed.",
  Warning: "Warning",
  Error: "Some thing went wrong",
  Info: "Info",
  ConfirmSave: "Do you want to save?",
  ConfirmSaveDraft: "Do you want to save draft?",
  ConfirmDelete: "Do you want to delete?",
  ConfirmApprove: "Do you want to approve?",
  Confirmapprove: "Do you want to approve?",
  ConfirmRevisit: "Do you want to revisit data?",
  ConfirmReject: "Do you want to reject?",
  ConfirmCancel: "Do you want to cancel?",
  ConfirmSubmit: "Do you want to submit?",
  NoPermission: "Access denied",
  SessionTimeOut: "Session time out",
  Duplicate: "Duplicate Data",
  DeleteFail: 'Please select <i class="far fa-check-square"></i>',
  UploadImage: "Please upload image",
  OverTime: "Please select Start Time less than End Time.",
  ConfirmForgot: "Do you want to Email Forgot Password?",
};
export const Process_System = {
    process_SessionExpire: "SSEXP",
    process_Duplicate: "DUP",
    process_Success: "Success",
    process_Error: "Error",
};

export const AlertIcon = {
    info: "info" as SweetAlertIcon,
    success: "success" as SweetAlertIcon,
    error: "error" as SweetAlertIcon,
    warning: "warning" as SweetAlertIcon,
    question: "question" as SweetAlertIcon,
};

export const AlertButtonText = {
    OK: "OK",
    Cancel: "Cancel",
    Close: "Close",
    Yes: "Yes",
    Confirm: "Confirm",
};

export const TooltipsMSG = {
    Add: "Add",
    Edit: "Edit",
    Delete: "Delete",
    View: "View",
    Search: "Search",
    Save: "Save",
    Approve: "Approve",
    Reject: "Reject",
    Cancel: "Cancel",
    Logout: "Logout",
    ReviewContent: "Review Content",
    File: "File",
    DownloadTemplate: "Download Template",
    Download: "Download",
    Close: "Close",
    MasterData: "Master Data",
    DocRef: "Document reference",
    Check: "Check",
    Submit: "Submit",
    NewRegistration: "Registration",
    Withdraw: "Withdraw",
    SendToVendor: "Send To Vendor"
};

export const ResultDeleteAPI = (
    IsLoadData = false,
    response: any,
    MSG_Success: any,
    fnOnSuccess?: any,
    fnOther?: any
) => {
    let { sStatus, sMsg, nPermission } = response;
    const {
        process_SessionExpire,
        process_Duplicate,
        process_Success,
        process_Error,
    } = Process_System;
    if (sStatus === process_Success) {
        if (!nPermission) nPermission = 1;
        if (nPermission === 0) {
            NoPermission();
        } else {
            if (!IsLoadData) {
                if (!MSG_Success) MSG_Success = AlertMsg.DeleteComplete;
                SwAlert.Success(AlertTitle.Info, MSG_Success, fnOnSuccess);
            } else {
                if (fnOnSuccess) fnOnSuccess();
            }
        }
    } else if (sStatus === process_SessionExpire) {
        SwAlert.Warning(AlertTitle.Warning, AlertMsg.SessionTimeOut, function () {
            lnkToLogin()
        });
    } else if (sStatus === process_Duplicate) {
        let tempMsg = AlertMsg.Duplicate;
        if (!sMsg) {
            sMsg = tempMsg;
        }
        SwAlert.Warning(AlertTitle.Warning, sMsg, () => {
            if (fnOther) fnOther();
        });
    } else if (sStatus === process_Error) {
        SwAlert.Warning(AlertTitle.Warning, sMsg, () => {
            if (fnOther) fnOther();
        });
    } else {
        if (!nPermission) nPermission = 1;
        if (nPermission === 0) {
            NoPermission();
        } else {
            if (fnOnSuccess) fnOnSuccess();
        }
    }
};

export const ResultAPI = (
    IsLoadData = false,
    response: any,
    MSG_Success: any,
    fnOnSuccess?: any,
    fnOther?: any
) => {
    let { sStatus, sMsg, nPermission } = response;
    const {
        process_SessionExpire,
        process_Duplicate,
        process_Success,
        process_Error,
    } = Process_System;
    if (sStatus === process_Success) {
        if (!nPermission) nPermission = 1;
        if (nPermission === 0) {
            NoPermission();
        } else {
            if (!IsLoadData) {
                if (!MSG_Success) MSG_Success = AlertMsg.SaveComplete;
                SwAlert.Success(AlertTitle.Info, MSG_Success, fnOnSuccess);
            } else {
                if (fnOnSuccess) fnOnSuccess();
            }
        }
    } else if (sStatus === process_SessionExpire) {
        SwAlert.Warning(AlertTitle.Warning, AlertMsg.SessionTimeOut, function () {
            lnkToLogin()
        });
    } else if (sStatus === process_Duplicate) {
        let tempMsg = AlertMsg.Duplicate;
        if (!sMsg) {
            sMsg = tempMsg;
        }
        SwAlert.Warning(AlertTitle.Warning, sMsg, () => {
            if (fnOther) fnOther();
        });
    } else if (sStatus === process_Error) {
        SwAlert.Warning(AlertTitle.Warning, sMsg, () => {
            if (fnOther) fnOther();
        });
    } else {
        if (!nPermission) nPermission = 1;
        if (nPermission === 0) {
            NoPermission();
        } else {
            if (fnOnSuccess) fnOnSuccess();
        }
    }
};

export const DialogConfirm = (funcYes: any, funcNo?: any, sMsg?: any) => {
    SwAlert.Confirm(AlertTitle.Confirm, sMsg != undefined && sMsg != "" ? sMsg : AlertMsg.ConfirmSave, funcYes, funcNo);
};

export const DialogConfirmDelete = (funcYes: any, funcNo?: any) => {
    SwAlert.Confirm(AlertTitle.Confirm, AlertMsg.ConfirmDelete, funcYes, funcNo);
};
export const DialogConfirmForgot = (funcYes: any, funcNo?: any) => {
    SwAlert.Confirm(AlertTitle.Confirm, AlertMsg.ConfirmForgot, funcYes, funcNo);
};
export const SwAlert_Title = (sTitle: any) => {
    return "<strong>" + sTitle + "</strong>";
};

export const SwAlert = {
    Common: (sTitle: any, sMessage: any, fnOK?: any) => {
        Swal.fire({
            title: SwAlert_Title(!sTitle ? "" : sTitle + ""),
            html: sMessage,
            confirmButtonText: AlertButtonText.Close,
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.value) {
                if (fnOK) fnOK();
            }
        });
    },
    Info: (sTitle: any, sMessage: any, fnOK?: any) => {
        Swal.fire({
            title: SwAlert_Title(!sTitle ? AlertTitle.Success : sTitle + ""),
            html: sMessage,
            icon: AlertIcon.info,
            confirmButtonText: AlertButtonText.Close,
            allowOutsideClick: false,
            allowEscapeKey: false,
        }).then((result) => {
            if (result.value) {
                if (fnOK) {
                    fnOK();
                } else {
                    Swal.close();
                }
            }
        });
    },
    Success: function (sTitle: any, sMessage: any, fnOK?: any) {
        Swal.fire({
            title: SwAlert_Title(!sTitle ? AlertTitle.Success : sTitle + ""),
            html: sMessage,
            icon: AlertIcon.success,
            confirmButtonText: AlertButtonText.Close,
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
    Error: function (sTitle: any, sMessage: any, fnOK?: any) {
        Swal.fire({
            icon: AlertIcon.error,
            title: SwAlert_Title(!sTitle ? AlertTitle.Error : sTitle + ""),
            html: sMessage,
            confirmButtonText: AlertButtonText.Close,
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
    Warning: function (sTitle: any, sMessage: any, fnOK?: any) {
        Swal.fire({
            icon: AlertIcon.warning,
            title: SwAlert_Title(!sTitle ? AlertTitle.Warning : sTitle + ""),
            html: sMessage,
            confirmButtonText: AlertButtonText.Close,
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
    Confirm: function (sTitle: any, sMessage: any, fnYes?: any, fnNo?: any) {
        Swal.fire({
            title: sTitle,
            text: sMessage,
            // icon: AlertIcon.question,
            icon: AlertIcon.question,
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: AlertButtonText.Confirm,
            showLoaderOnConfirm: true,
            allowOutsideClick: false,
            preConfirm: function () {
                return new Promise(function () {
                    //resolve, reject
                    Swal.disableButtons();

                    if (fnYes) {
                        (document.getElementsByClassName("swal2-cancel") as any)[0].style.display = "none";
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
    ConfirmYN: function (sTitle: any, sMessage: any, fnYes?: any, fnNo?: any) {
        Swal.fire({
            title: SwAlert_Title(!sTitle ? AlertTitle.Confirm : sTitle + ""),
            html: sMessage,
            icon: AlertIcon.question,
            allowOutsideClick: false,
            allowEscapeKey: false,
            showCancelButton: true,
            confirmButtonColor: "#5cb85c", //"#2098D1",
            confirmButtonText: "Yes",
            cancelButtonText: "Cancel",

            // closeOnConfirm: false,
            // closeOnCancel: false
        }).then((result) => {
            if (result.value) {
                if (fnYes) {
                    fnYes();
                    var d = Swal.getCloseButton();
                    console.log("ew", d);

                } else { Swal.close() };
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                if (fnNo) fnNo();
                else Swal.close();
            }
        });
    },
};

export const actionBlockUI = () => {
    return { type: "BLOCK_UI" };
};

export const actionUnBlockUI = () => {
    return { type: "UNBLOCK_UI" };
};

export const FnBlock_UI = () => {
    const Dispatch = useDispatch();
    const BlockUI = () => {
        Dispatch(actionBlockUI())
    };
    const UnBlockUI = () => Dispatch(actionUnBlockUI());
    return { BlockUI, UnBlockUI };
};

export const NoPermission = () => {
    SwAlert.Error(undefined, "No Permission", () => {
        // window.location.href = "/";
        // window.location.href = `${process.env.REACT_APP_API_URL}`;
        lnkToNotPermission()
    });
};

export const AxiosPost = (
    sWebMetodName: any,
    objJSON: any,
    fnSuccess?: any,
    fnError?: any,
    BlockUI?: any,
    UnBlockUI?: any,
    Async?: any
) => {

    if (BlockUI) BlockUI();
    if (Async) {
        const fnAsync = async () => {
            try {
                let res = await axios.post(process.env.REACT_APP_API_URL + "api/" + sWebMetodName, objJSON)
                if (fnSuccess) fnSuccess(res.data);
            } catch (errors) {
                if (fnError) {
                    await fnError();
                    if (UnBlockUI) UnBlockUI()
                } else {
                    if (UnBlockUI) UnBlockUI()
                    SwAlert.Error(undefined, errors);
                }
            }
        }
        fnAsync()
    } else {
        axios
            .post(
                process.env.REACT_APP_API_URL + "api/" + sWebMetodName,
                objJSON
            )
            .then((res) => {
                if (fnSuccess) fnSuccess(res.data);
            })
            .catch((errors) => {
                if (fnError) {
                    fnError();
                } else {
                    SwAlert.Error(undefined, errors);
                }
            })
            .then((res) => {
                if (UnBlockUI) UnBlockUI();
            });
    }
};

export const closeSwAlert = () => (
    Swal.close()
)

export const scrollToElementValidate = (formID?: any) => {
    let elForm: any = formID
        ? (document.querySelector("form[id=" + formID + "]") as any)
        : document.querySelector("form");
    let elByClass = elForm.querySelectorAll("div.Mui-error") as any;
    if (elByClass && elByClass.length > 0)
        elByClass[0].scrollIntoView({ behavior: "smooth", block: "center" });
}

export const lnkToLogin = () => {
    let el = document.getElementById("lnkToLogin") as any
    el && el.click()
}

export const lnkToNotPermission = () => {
    let el = document.getElementById("NotPermission") as any
    el && el.click()
}

export const Effect = (props: any) => {
    const effect = () => {
        if (props.formik.submitCount > 0 && !props.formik.isValid) {
            props.onSubmissionError();
        }
    };
    props.useEffect(effect, [props.formik.submitCount, props.formik.errors]);
    return null;
}


export const CheckTextInput = (nVal: any) => {
    if (nVal != "" && nVal != null) {
        nVal = (nVal + "").replace(/ /g, '').replace(/,/g, '');
        if (IsNumberic(nVal)) {
            var nCheck = parseFloat(nVal);
            if (nCheck > 0) {
                let arrSplit = nVal.split('.')
                if (arrSplit.length > 1) {
                    if (arrSplit[1]) {
                        nVal = `${arrSplit[0]}.${arrSplit[1]}`
                    } else {
                        nVal = arrSplit[0]
                    }
                }
                nVal = addCommas(nVal);
            }
            else if (nCheck < 0) {
                nVal = "";
            }
            else {
                nVal = nCheck;
            }
        }
        else {
            if (nVal.toLowerCase() == "na" || nVal.toLowerCase() == "n/a" || nVal.toLowerCase() == "n" || nVal.toLowerCase() == "a") {
                nVal = "N/A";
            } else {
                nVal = "";
            }
        }
    }
    nVal = nVal + "";
    return nVal + "";
}

export const CheckTextOutput = (nValue: any) => {
    var nVal = nValue + "";
    if (nVal != "" && nVal != null) {
        if (IsNumberic(nVal)) {
            nVal = nVal.replace(/,/g, '');
            if (nVal.toLowerCase() != "na" && nVal.toLowerCase() != "n/a") {
                var nDecimal = 3;
                var sEmpty = "-";
                if (IsNumberic(nVal)) {

                    var sv2 = parseFloat(nVal);
                    var arrValue = nVal.split('.');
                    if (sv2 >= 1 || sv2 == 0) { // 1 ถึง Infinity
                        nVal = SetFormatNumber(sv2, nDecimal, sEmpty)
                    }
                    else if (sv2 > 0 && sv2 < 1 && arrValue.length == 2) {
                        if (arrValue[1].length > 4) {
                            nVal = (sv2.toExponential(3)).replace(/e/g, 'E');
                        }
                    }
                    else if (sv2 <= -1) {
                        nVal = SetFormatNumber(sv2, nDecimal, sEmpty)
                    }
                }
            }
            else {
                nVal = "N/A";
            }
        }
        else {
            nVal = "";
        }

    }

    nVal = nVal + "";
    return nVal + "";
}
export const IsNumberic = (sVal: any) => {
    sVal = (sVal + "").replace(/,/g, '');
    return !isNaN(sVal);
}
export const addCommas = (nStr: any) => {
    nStr += '';
    let x = nStr.split('.');
    let x1 = x[0];
    let x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
export const SetFormatNumber = (nNumber: any, nDecimal: any, sEmpty: any) => {
    if (IsNumberic(nNumber)) {
        if (IsNumberic(nDecimal))
            return addCommas(nNumber.toFixed(nDecimal));
        else
            return addCommas(nNumber);
    }
    else {
        return !nNumber ? (sEmpty === undefined ? "" : sEmpty) : nNumber;
    }
}

const URL_API = process.env.REACT_APP_API_URL + "api/";
export const AutocompleteDataSource = async (Controller: any, MethodName: any, ObjSearch: any) => {
    const res = await axios.post(URL_API + Controller + '/' + MethodName, ObjSearch)
    return res;
};

const keyCrypto = "P4ssw0rd3ndCr7pt"
export const Encrypt = (dataEncrypt: any) => {
    let data = dataEncrypt + "";

    let result = Crypto.AES.encrypt(data, keyCrypto).toString();
    ;
    result = result.replace(/\//g, "s14sh").replace(/\+/g, 'p1u5');

    return result
}
export const Decrypt = (dataDecrypt: any) => {
    if (dataDecrypt != null) {
        dataDecrypt = dataDecrypt + "";
        dataDecrypt = dataDecrypt.replace(/s14sh/g, '/').replace(/p1u5/g, '+')
        let bytes = Crypto.AES.decrypt(dataDecrypt, keyCrypto);
        let result = bytes.toString(Crypto.enc.Utf8)
        return result
    } else {
        return "";
    }
}

export const ParseHtml = (val: any) => {
    if (val)
        return parse(val);
    else
        return val;
}


export const TableRowColor = {
    OutputTotal: "rgb(167, 215, 113)",
    RowCollapse: "rgb(167, 215, 113)",
    OutputSubTotal1: "rgb(223, 237, 161)"
}

export const BackToComponent = (lnkBack: any) => {
    var elLink = document.getElementById(lnkBack) as any;
    if (elLink) {
        elLink.click();
    }
}

export const RegexpPasswork = () => {

    let regexp1 = "((?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,}))";//พิมพ์ใหญ่และตัวเลขและอักขระพิเศษ
    let regexp2 = "((?=.*[A-Z])(?=.*[0-9])(?=.*[a-z])(?=.{8,}))";
    let regexp3 = "((?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,}))";
    let regexp4 = "((?=.*[a-z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,}))";
    let regexp5 = "((?=.*[a-z])(?=.*[A-Z])(?=.*[!@#\$%\^&\*])(?=.{8,}))";
    let regexp6 = "((?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,}))";//พิมพ์เล็กและพิมพ์ใหญ่และตัวเลขและอักขระพิเศษ 
    let egexp = "^(" + regexp1 + "|" + regexp2 + "|" + regexp3 + "|" + regexp4 + "|" + regexp5 + "|" + regexp6 + ")"
    return egexp;
}
export const MsgValidate = {
    PleaseSpecify: "Please Specify ",
    Passwork: "Passwords must have at least 8 characters and contain at least three of the following: uppercase letters, lowercase letters, numbers, symbols.",
    InvalidEmail: "Invalid Email",
    ConfirmPassword: "Invalid Password"
}

export const MsgValidateMaxLength = (sField: any, nMaxLength: any) => {
    return sField + " must be at most " + nMaxLength + " characters";
};

export const resetFromData = (formProps: any, sFild: any, sValue: any) => {
    formProps.setFieldValue(sFild, sValue, false)
    formProps.setFieldError(sFild, '', false)
}
export const resetFromSubmitData = (setFieldValue: any, setFieldError: any, sFild: any, sValue: any) => {
    setFieldValue(sFild, sValue, false)
    setFieldError(sFild, '', false)
}

export const AxiosPostExportFile = (
    sWebMetodName: any,
    objJSON: any,
    filename: any,
    fnError?: any,
    BlockUI?: any,
    UnBlockUI?: any
) => {
    if (BlockUI) BlockUI();
    axios
        .post(
            process.env.REACT_APP_API_URL + "api/" + sWebMetodName,
            objJSON, {
            responseType: 'blob',
        }
        )
        .then((res) => {
            exportsFileSave(res.data, filename);
        })
        .catch((errors) => {
            if (fnError) {
                fnError();
            } else {
                SwAlert.Error(undefined, errors);
            }
        })
        .then((res) => {
            if (UnBlockUI) UnBlockUI();
        });
};

export const exportsFileSave = (data: any, filename: any, mime?: any, bom?: any) => {
    var blobData = (typeof bom !== 'undefined') ? [bom, data] : [data]
    var blob = new Blob(blobData, { type: mime || 'application/octet-stream' });
    if (typeof window.navigator.msSaveBlob !== 'undefined') {
        window.navigator.msSaveBlob(blob, filename);
    }
    else {
        var blobURL = (window.URL && window.URL.createObjectURL) ? window.URL.createObjectURL(blob) : window.webkitURL.createObjectURL(blob);
        var tempLink = document.createElement('a');
        tempLink.style.display = 'none';
        tempLink.href = blobURL;
        tempLink.setAttribute('download', filename);
        if (typeof tempLink.download === 'undefined') {
            tempLink.setAttribute('target', '_blank');
        }
        document.body.appendChild(tempLink);
        tempLink.click();
        setTimeout(function () {
            document.body.removeChild(tempLink);
            window.URL.revokeObjectURL(blobURL);
        }, 200)
    }
}