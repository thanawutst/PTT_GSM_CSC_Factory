import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import Backdrop from '@material-ui/core/Backdrop';
import { useSpring, animated } from 'react-spring'
//import { useSpring, animated } from 'react-spring/web.cjs'; // web.cjs is required for IE 11 support
import PropTypes from 'prop-types';
import MessageIcon from '@material-ui/icons/Message';
import { Grid } from '@material-ui/core';
const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            outline: "none",
        },
        modal: {
            display: 'flex',
            alignItems: 'center',
            border: '0px solid #fff',
            justifyContent: 'center',
            outline: "none",
            "*:focus": {
                outline: "none",
            }
        },
        paper: {
         //   borderRadius: "12px",
            backgroundColor: theme.palette.background.paper,
            border: '0px solid #fff',
            boxShadow: theme.shadows[5],
            //  padding: theme.spacing(2, 4, 3),
        },
        Header: {
            borderTop: "12px",
            padding: "12px",
            backgroundColor: "#b3e5fc !important",
            textAlign: "left",
            fontSize: "16px"

        }, body: {
            paddingBottom: "40px",
            paddingRight: "50px",
            paddingLeft: "10px"
        }
    }),
);

interface FadeProps {
    children?: React.ReactElement;
    in: boolean;
    onEnter?: () => {};
    onExited?: () => {};
}

const Fade = React.forwardRef<HTMLDivElement, FadeProps>(function Fade(props, ref) {
    const { in: open, children, onEnter, onExited, ...other } = props;
    const classes = useStyles();
    const style = useSpring({
        from: { opacity: 0 },
        to: { opacity: open ? 1 : 0 },
        onStart: () => {
            if (open && onEnter) {
                onEnter();
            }
        },
        onRest: () => {
            if (!open && onExited) {
                onExited();
            }
        },
    });

    return (
        <animated.div ref={ref} style={style} {...other} className={classes.root}>
            {children}
        </animated.div>
    );
});

export default function SpringModal(Props) {
    const classes = useStyles();
    const [open, setOpen] = React.useState();

    const handleClose = () => {
        Props.isOpen(false);
    };

    return (
        <div>
            <Modal
                aria-labelledby="spring-modal-title"
                aria-describedby="spring-modal-description"
                className={classes.modal}
                open={Props.Open}
                onClose={handleClose}
                closeAfterTransition
                BackdropComponent={Backdrop}
                BackdropProps={{
                    timeout: 500,
                }}
            >
                <Fade in={Props.Open}>
                    <div className={classes.paper}>
                        <h2 className={classes.Header} id="spring-modal-title"><Grid direction="row"><MessageIcon style={{ marginBottom: "-7px" }} />&nbsp;&nbsp;{Props.MsgHeader || ''}</Grid></h2>
                        <p className={classes.body} id="spring-modal-description">{Props.MsgBody || ''}</p>
                    </div>
                </Fade>
            </Modal>
        </div>
    );
}
SpringModal.propTypes = {
    MsgHeader: PropTypes.string,
    MsgBody: PropTypes.string,
    Open: PropTypes.bool,
    isOpen: PropTypes.any,
}