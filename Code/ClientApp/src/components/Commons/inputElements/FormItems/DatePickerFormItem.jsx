import React, { Fragment, useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { InputAdornment } from "@material-ui/core";
import DateFnsUtils from "@material-ui/pickers/adapter/date-fns";
import DateRangeOutlinedIcon from '@material-ui/icons/DateRangeOutlined';
import {
    MobileDatePicker,
    MobileDateTimePicker,
    LocalizationProvider,
} from "@material-ui/pickers";
import { getLanguage } from 'src/i18n';
import TextField from "@material-ui/core/TextField";
import { useFormContext } from 'react-hook-form';
import FormErrors from 'src/components/Commons/inputElements/formErrors';
import IconButton from '@material-ui/core/IconButton';
export function DatePickerFormItem(props) {

    const {
        label,
        name,
        hint,
        placeholder,
        autoFocus,
        autoComplete,
        externalErrorMessage,
        required,
        showTime,
        inputProps,
        disabled
    } = props;

    const {
        register,
        errors,
        formState: { touched, isSubmitted },
        setValue,
        watch,
    } = useFormContext();

    useEffect(() => {
        register({ name });
    }, [register, name]);
    const [IsOpen, SetIsOpen] = useState(false)
    const errorMessage = FormErrors.errorMessage(
        name,
        errors,
        touched,
        isSubmitted,
        externalErrorMessage,
    );

    const DateTimePickerComponent = showTime
        ? MobileDateTimePicker
        : MobileDatePicker;

    const format = showTime ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
    const handleClick = () => {
        SetIsOpen(true)
    }
    return (
        <LocalizationProvider
            dateAdapter={DateFnsUtils}
            locale={getLanguage().dateFns}
        >
            <DateTimePickerComponent
                disableOpenPicker={disabled}
                disabled={disabled}
                clearable
                inputFormat={format}
                label={label}
                open={IsOpen}
                id={name}
                onChange={(value) => {
                    setValue(name, value, { shouldValidate: true });
                    props.onChange && props.onChange(value);
                    SetIsOpen(false)
                }}
                onBlur={(event) => {
                    props.onBlur && props.onBlur(event);
                    SetIsOpen(false)
                }}
                value={watch(name) || null}
                placeholder={placeholder || undefined}
                autoFocus={autoFocus || undefined}
                autoComplete={autoComplete || undefined}
                // InputLabelProps={{
                //     shrink: true,
                // }}
                autoOk
                onClose={(event) => {
                    SetIsOpen(false)
                }}
                {...inputProps}
                renderInput={(props) => (
                    <TextField
                        disabled={disabled}
                        margin="normal"
                        variant="outlined"
                        size="small"
                        fullWidth
                        {...props}
                        required={required}
                        error={Boolean(errorMessage)}
                        helperText={errorMessage || hint}
                        InputProps={{
                            endAdornment: (
                                <Fragment>
                                    <InputAdornment>
                                        <IconButton disabled={disabled} position="end" style={{ color: "#747474", marginRight: "-11px" }} onClick={(f => handleClick())}>
                                            <DateRangeOutlinedIcon />
                                        </IconButton>
                                    </InputAdornment>
                                </Fragment>
                            ),
                        }}
                    />
                )}
            />
        </LocalizationProvider>
    );
}

DatePickerFormItem.defaultProps = {
    required: false,
};

DatePickerFormItem.propTypes = {
    name: PropTypes.string.isRequired,
    required: PropTypes.bool,
    label: PropTypes.string,
    hint: PropTypes.string,
    autoFocus: PropTypes.bool,
    size: PropTypes.string,
    prefix: PropTypes.string,
    placeholder: PropTypes.string,
    externalErrorMessage: PropTypes.string,
    showTime: PropTypes.bool,
    inputProps: PropTypes.object,
    onChange: PropTypes.func,
    onBlur: PropTypes.func,
    disabled: PropTypes.bool,
};

export default DatePickerFormItem;
