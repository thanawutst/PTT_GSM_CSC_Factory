import FormControl from '@material-ui/core/FormControl';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormLabel from '@material-ui/core/FormLabel';
import Switch from '@material-ui/core/Switch';
import React from 'react'
import { useEffect } from 'react';
import { useFormContext } from 'react-hook-form';
import FormErrors from '../formErrors';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';

function SwitchActiveFormItem(props) {
    const {
        label,
        name,
        hint,
        required,
        externalErrorMessage,
        disabled,
    } = props;

    const {
        register,
        errors,
        formState: { touched, isSubmitted },
        watch,
        setValue
    } = useFormContext();

    useEffect(() => {
        register({ name });
    }, [register, name]);

    const AntSwitch = withStyles((theme) => ({
        root: {
            width: 42,
            height: 26,
            padding: 0,
            margin: theme.spacing(1),
        },
        switchBase: {
            padding: 1,
            '&$checked': {
                transform: 'translateX(16px)',
                color: theme.palette.common.white,
                '& + $track': {
                    backgroundColor: '#52d869',
                    opacity: !disabled ? 1 : 0.5,
                    border: 'none',
                },
            },

        },
        thumb: {
            width: 24,
            height: 24,
        },
        track: {
            borderRadius: 26 / 2,
            border: `1px solid #c41016`,
            backgroundColor: '#c41016',
            opacity: !disabled ? 1 : 0.5,
            transition: theme.transitions.create(['background-color', 'border']),
        },
        checked: {},
    }))(Switch);

    const errorMessage = FormErrors.errorMessage(
        name,
        errors,
        touched,
        isSubmitted,
        externalErrorMessage,
    );

    const formHelperText = errorMessage || hint;

    return (
        <FormControl
            required={required}
            fullWidth
            error={Boolean(errorMessage)}
            component="fieldset"
            size="medium"
        >
            <FormLabel
                component="legend"
                style={{ marginBottom: '8px' }}
            >
                {label}
            </FormLabel>

            <Typography component="div">
                <Grid component="label" container alignItems="center" spacing={1}>
                    <Grid item>Active</Grid>
                    <Grid item>
                        <AntSwitch disabled={disabled} checked={watch(name) || false} onChange={(e) => {
                            setValue(name, e.target.checked, { shouldValidate: true });
                            props.onChange &&
                                props.onChange(e.target.checked);
                        }}
                            onBlur={(event) => {
                                props.onBlur && props.onBlur(event);
                            }}
                            id={name}
                            name={name} />
                    </Grid>
                </Grid>
            </Typography>

            {/* <Switch
                id={name}
                name={name}
                checked={watch(name) || false}
                onChange={(e) => {
                    setValue(name, e.target.checked, { shouldValidate: true });
                    props.onChange &&
                        props.onChange(e.target.checked);
                }}
                onBlur={(event) => {
                    props.onBlur && props.onBlur(event);
                }}
                color="primary"
                size="medium"
            ></Switch> */}
            {formHelperText && (
                <FormHelperText style={{ marginTop: 0 }}>
                    {formHelperText}
                </FormHelperText>
            )}
        </FormControl>
    );
}

SwitchActiveFormItem.defaultProps = {
    disabled:false
};

SwitchActiveFormItem.propTypes = {
    name: PropTypes.string.isRequired,
    required: PropTypes.bool,
    label: PropTypes.string,
    hint: PropTypes.string,
    externalErrorMessage: PropTypes.string,
    disabled: PropTypes.bool,
};

export default SwitchActiveFormItem;
