import React, { Fragment, useEffect, useState } from 'react';
import { yupResolver } from '@hookform/resolvers/yup';
import { Box, Button, Grid, Link, StepContent, TextField } from '@material-ui/core';
import { Label, SaveAlt, } from '@material-ui/icons';
import EventIcon from '@material-ui/icons/Event';
import { FormProvider, useForm } from 'react-hook-form';
import { makeStyles, Theme, createStyles, withStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import Check from '@material-ui/icons/Check';
import SettingsIcon from '@material-ui/icons/Settings';
import GroupAddIcon from '@material-ui/icons/GroupAdd';
import VideoLabelIcon from '@material-ui/icons/VideoLabel';
import StepConnector from '@material-ui/core/StepConnector';
import Typography from '@material-ui/core/Typography';
import { StepIconProps } from '@material-ui/core/StepIcon';
import { Icon } from '@material-ui/core';
import { AxiosGetJson } from 'src/Service/Config/AxiosMethod';
import { Encrypt } from 'src/components/Systems/SystemComponent';
import PersonIcon from '@material-ui/icons/Person';
import moment from 'moment';
import PropTypes from 'prop-types';
import SpringModal from './Modal';
import { ButtonBack } from '../ButtonAll/ButtonAll';
let Stepp = "";

const FlowStep = (props) => {
    const { RequestID, PageID } = props;
    const [DataFlow, SetDataFlow] = useState<any>([]);
    const classes = useStyles();
    const [activeStep, setActiveStep] = React.useState(0);
    const [TextModal, setTextModal] = React.useState<any>();
    const [Modal, setModal] = React.useState<any>();
    const [IsOpenModal, setIsOpenModal] = React.useState(false);
    useEffect(() => {
        GetFlow();
    }, [])
    useEffect(() => {

    }, [TextModal])
    useEffect(() => {
        OpenModalComment(IsOpenModal);
    }, [IsOpenModal])
    const BoxMassage = (Data) => {
        let ColorCode = "ffa726";
        switch (Data.cApprove) {
            case "A": ColorCode = "#00e676"; break
            case "W": ColorCode = "#ffa726"; break
            case "R": ColorCode = "#c62828"; break
        }
        let dApprove = Data.dApprove;
        let SdApprove = dApprove == null ? "" : moment(dApprove).format("DD/MM/yyyy");

        let Comment;
        if (Data.Desc) {
            if (Data.Desc.toString().length > 10) {
                let Word = Data.Desc.toString().substring(0, 10);
                Comment = <Fragment>{Word}<Link style={{ cursor: "pointer" }} onClick={(c => { SetText(Data.Desc) })}>...</Link></Fragment>
            } else {
                Comment = Data.Desc;
            }
        }
        return (
            <Fragment >
                <Box borderColor={ColorCode} border={1} style={{ backgroundColor: ColorCode, height: "30px", }} >
                    <Grid style={{ textAlign: "center", verticalAlign: "middle", paddingTop: "5px" }} lg={12} md={12} sm={12} xs={12}>
                        {Data.step == 1 ? "Requester" : Data.SRole}
                    </Grid >
                </Box>
                <Box borderColor={ColorCode} border={1} >
                    <Grid container item lg={12} md={12} sm={12} xs={12} style={{ paddingTop: 2 }}>
                        <Grid style={{ textAlign: "left" }} lg={12} md={12} sm={12} xs={12}>
                            <div style={{ paddingLeft: 20 }}>Name: {Data.Name}</div>
                        </Grid >
                        {/* <Grid style={{ textAlign: "left" }} lg={12} md={12} sm={12} xs={12}>
                            <div style={{ paddingLeft: 20 }}>Status: {Data.SStatus}</div>
                        </Grid > */}
                        {SdApprove != "" ?
                            <Grid style={{ textAlign: "left" }} lg={12} md={12} sm={12} xs={12}>
                                <div style={{ paddingLeft: 20 }}>Date: {SdApprove}</div>
                            </Grid > : <div></div>
                        }
                        {Data.Desc != "" && Data.Desc != null ?
                            <Grid style={{ textAlign: "left" }} lg={12} md={12} sm={12} xs={12}>
                                <div style={{ paddingLeft: 20 }}>Comment: {Comment}   </div>
                            </Grid >
                            : <div></div>}

                    </Grid>
                </Box >
            </Fragment >
        );
    }
    const OpenModalComment = (prop) => {
        setModal(<SpringModal isOpen={setIsOpenModal} Open={IsOpenModal} MsgHeader={'Comment'} MsgBody={TextModal} />)
    }
    const Open = () => {
        setIsOpenModal(true)
    }
    const SetText = (prop) => {
        setTextModal(prop)
        Open();
    }

    const GetFlow = async () => {
        let resultStatus: any = await AxiosGetJson("FlowStep/GetFlow", { sID: props.RequestID == null || props.RequestID == undefined || props.RequestID == "" ? "0" : props.RequestID + "", sPageID: props.PageID == null || props.PageID == undefined || props.PageID == "" ? "0" : props.PageID + "" });
        if (resultStatus.length > 0) {
            SetDataFlow(resultStatus.map(o => { return { Icon: "", step: o.nStep, Desc: o.sComment, cApprove: o.cApprove, Name: o.sName, SRole: o.SRole, SStatus: o.SStatus, dApprove: o.dApprove } }))
            let StepActive = resultStatus.filter(f => f.cApprove == "A").sort((n1, n2) => n2.nStep - n1.nStep);
            if (StepActive && StepActive.length > 0) {
                setActiveStep(StepActive[0].nStep);
                // setActiveStep(2)
            }
        }
    }
    function ColorlibStepIcon(props: StepIconProps, step) {
        const classes = useColorlibStepIconStyles();
        // const { active, completed } = props;
        const { cApprove } = step;
        return (
            <Fragment>
                <div
                    className={clsx(classes.root, {
                        [classes.Approve]: cApprove == "A",
                        [classes.Waiting]: cApprove == "W",
                        [classes.Reject]: cApprove == "R",
                    })}  >
                    <PersonIcon style={{ fontSize: "45" }} />
                </div>

            </Fragment>
        );
    }

    return (

        // <div className={classes.mobile}>
        //     <Stepper activeStep={activeStep} orientation="vertical">
        //         {DataFlow.map((Data) => (
        //             <Step key={Data.step}>
        //                 <StepLabel StepIconComponent={(o => ColorlibStepIcon(o, Data))}>{BoxMassage(Data)}</StepLabel>
        //                 <StepContent>
        //                     {/* <Typography>{getStepContent(index)}</Typography> */}
        //                 </StepContent>
        //             </Step>
        //         ))}
        //     </Stepper>
        // </div>

        <div className={classes.root}>
            <Stepper alternativeLabel activeStep={activeStep} connector={<ColorlibConnector />}>
                {DataFlow.map((Data): any => (
                    <Step key={Data.step}>
                        <SetSteps set={Data.step}></SetSteps>
                        <StepLabel classes={{ labelContainer: classes.labelContainer }} StepIconComponent={(o => ColorlibStepIcon(o, Data))}>{BoxMassage(Data)} </StepLabel>
                    </Step>
                ))}
            </Stepper>
            {Modal}
        </div >
    );
}

export function SetSteps(prop) {
    Stepp = prop.set;
    return <div></div>
}
const QontoConnector = withStyles({
    alternativeLabel: {
        top: 10,
        left: 'calc(-50% + 16px)',
        right: 'calc(50% + 16px)',

    },
    active: {
        '& $line': {
            borderColor: '#784af4',
        },
    },
    completed: {
        '& $line': {
            borderColor: '#784af4',
        },
    },
    line: {
        borderColor: '#eaeaf0',
        borderTopWidth: 3,
        borderRadius: 1,
    },
})(StepConnector);

// const useQontoStepIconStyles = makeStyles({
//     root: {
//         color: '#eaeaf0',
//         display: 'flex',
//         height: 22,
//         alignItems: 'center',
//     },
//     active: {
//         color: '#784af4',
//     },
//     circle: {
//         width: 8,
//         height: 8,
//         borderRadius: '50%',
//         backgroundColor: 'currentColor',
//     },
//     completed: {
//         color: '#784af4',
//         zIndex: 1,
//         fontSize: 18,
//     },
// });

const ColorlibConnector = withStyles({
    alternativeLabel: {
        top: 22,
    },
    active: {
        '& $line': {
            backgroundImage:
                'linear-gradient(79deg, rgba(126,255,81,1) 0%, rgba(212,210,92,1) 51%, rgba(250,173,102,1) 100%);',
            //  backgroundColor: "#ffa726"
        },
    },
    completed: {
        '& $line': {
            // backgroundImage:
            //     'linear-gradient(79deg, rgba(62,193,31,0.9951330874146533) 0%, rgba(74,200,30,1) 53%, rgba(0,255,76,1) 100%)',
            backgroundColor: "#00e676"

        },
    },
    line: {
        height: 3,
        border: 0,
        paddingLeft: "10",
        paddingRight: "10",
        backgroundColor: '#eaeaf0',
        borderRadius: 1,
    },
})(StepConnector);

const useColorlibStepIconStyles = makeStyles({
    root: {
        backgroundColor: '#ccc',
        zIndex: 1,
        color: '#fff',
        width: 50,
        height: 50,
        display: 'flex',
        borderRadius: '50%',
        justifyContent: 'center',
        alignItems: 'center',
    },
    active: {
        // backgroundImage:
        //     'linear-gradient( 136deg, rgb(242,113,33) 0%, rgb(233,64,87) 50%, rgb(138,35,135) 100%)',
        // boxShadow: '0 4px 10px 0 rgba(0,0,0,.25)',
    },
    completed: {
        backgroundImage:
            'linear-gradient( 136deg, rgb(242,113,33) 0%, rgb(233,64,87) 50%, rgb(138,35,135) 100%)',
    },
    Approve: {
        backgroundColor:
            "#00e676",
    },
    Waiting: {
        backgroundColor:
            "#ffa726",
    },
    Reject: {
        backgroundColor:
            "#c62828",
    },
});



const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            width: '100%',
        },
        button: {
            marginRight: theme.spacing(1),
        },
        instructions: {
            marginTop: theme.spacing(1),
            marginBottom: theme.spacing(1),
        },
        labelContainer: {
            width: "200px",
            "& $alternativeLabel": {
                marginTop: 0
            }
        },
        mobile: {
            width: '300px',
        }
    }),

);

FlowStep.prototype = {
    RequestID: PropTypes.number.isRequired,
    PageID: PropTypes.number.isRequired,
}
export default FlowStep;
