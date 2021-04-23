import React from 'react';

import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    makeStyles,
} from '@material-ui/core';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Typography from '@material-ui/core/Typography';



const useStyles = makeStyles((theme) => ({
    closeButton: {
        position: 'absolute',
        right: theme.spacing(1),
        top: theme.spacing(1),
        color: theme.palette.grey[500],
    },
}));


function MuiModal(props) {
    const classes = useStyles();


    return (
        <Dialog
            open={true}
            onClose={props.onClose}
            maxWidth="xs"
            fullWidth={true}
        >
            <DialogTitle>
                <Typography variant="h6">{props.title}</Typography>
                {props.onClose &&
                    <IconButton onClick={props.onClose} className={classes.closeButton}>
                        <CloseIcon />
                    </IconButton>
                }
            </DialogTitle>
            <DialogContent dividers>
                <Typography gutterBottom>{props.content}</Typography>
            </DialogContent>
            <DialogActions>
                <Button
                    onClick={props.onClose}
                    color="secondary"  
                >
                    {props.cancelText}
                </Button>

                <Button
                    onClick={props.onConfirm}
                    color="primary"     
                    autoFocus
                >
                    {props.okText}
                </Button>
            </DialogActions>
        </Dialog>
    )
}

export default MuiModal;
