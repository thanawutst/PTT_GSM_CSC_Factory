import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {
    Button,
    Menu,
    MenuItem,
    makeStyles,
    Avatar,
} from '@material-ui/core';
import AccountCircleIcon from '@material-ui/icons/AccountCircle';
import PersonOutlineIcon from '@material-ui/icons/PersonOutline';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import LockIcon from '@material-ui/icons/Lock';
import AuthenSelectors from "src/store/selectors/AuthenSelectors";
import { AuthenActionCreators } from "src/store/redux/AuthenStore";


const useStyles = makeStyles((theme) => ({
    button: {
        color: theme.palette.getContrastText(
            theme.palette.primary.main,
        ),
    },
    buttonInner: {
        display: 'flex',
        alignItems: 'center',
    },
    text: {
        margin: theme.spacing(0, 0.5, 0, 1),
        display: 'none',
        flexDirection: 'column',
        alignItems: 'flex-start',
        color: theme.palette.getContrastText(
            theme.palette.primary.main,
        ),
        [theme.breakpoints.up('md')]: {
            display: 'flex',
        },
    },
    optionText: {
        margin: theme.spacing(0, 0.5, 0, 1),
    },
}));

function UserInfo() {
    const [anchorEl, setAnchorEl] = useState(null);
    const dispatch = useDispatch();
    const classes = useStyles();
    const currentUser = useSelector(AuthenSelectors.selectCurrentUser);

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const doSignout = () => {
        dispatch(AuthenActionCreators.doSignout());
        
    };
  
    return (
        <React.Fragment>
            <Button
                className={classes.button}
                onClick={handleClick}
            >
                <div className={classes.buttonInner}>
                    {currentUser.sLogonName && (
                        <>
                            <Avatar >{currentUser.sLogonName[0] + "" + currentUser.sLogonName[currentUser.sLogonName.length - 1]}</Avatar>
                            <div className={classes.text}>
                                {currentUser.sFullname}
                            </div>
                        </>
                    )}

                </div>
            </Button>
            <Menu
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                {/* <MenuItem onClick={() => { }}>
                    <PersonOutlineIcon />
                    <span className={classes.optionText}>
                        {"Profile"}
                    </span>
                </MenuItem> */}

                <MenuItem onClick={doSignout}>
                    <ExitToAppIcon />
                    <span className={classes.optionText}>
                        {"Log out"}
                    </span>
                </MenuItem>
            </Menu>
        </React.Fragment>
    );

}

export default UserInfo;
