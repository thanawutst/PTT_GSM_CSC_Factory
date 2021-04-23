import {
    MobileDateRangePicker,
    DateRangeDelimiter,
    LocalizationProvider,
    DateTimePicker,
    DateRangePicker,
    DatePicker,
} from "@material-ui/pickers";
import DateFnsUtils from "@material-ui/pickers/adapter/date-fns";
import TextField from "@material-ui/core/TextField";
import PropTypes from 'prop-types';
import React, { Fragment, useEffect, useState } from 'react';
import { useFormContext } from 'react-hook-form';
import { i18n } from 'src/i18n';
import FormErrors from 'src/components/Commons/inputElements/formErrors';
import { InputAdornment } from "@material-ui/core";
import { getLanguage } from 'src/i18n';
import IconButton from '@material-ui/core/IconButton';
import DateRangeOutlinedIcon from '@material-ui/icons/DateRangeOutlined';


function DatePickerRangeFormItem(props) {
    const [IsOpen, SetIsOpen] = useState(false)
    const {
        label,
        name,
        hint,
        placeholder,
        autoFocus,
        autoComplete,
        required,
        showTime,
        externalErrorMessage,
        inputProps,
        startText,
        endText,
        disabled
    } = props;

    const {
        register,
        errors,
        formState: { touched, isSubmitted },
        setValue,
        watch,
    } = useFormContext();

    const errorMessage = FormErrors.errorMessage(
        name,
        errors,
        touched,
        isSubmitted,
        externalErrorMessage,
    );

    const originalValue = watch(name) || [null, null];

    useEffect(() => {
        SetIsOpen(false)
        register({ name });
    }, [register, name]);

    const handleStartChanged = (value) => {
        setValue(name, [value, endValue()], { shouldValidate: true });
        props.onChange && props.onChange([value, endValue()]);
    };

    const handleEndChanged = (value) => {
        setValue(name, [startValue(), value], { shouldValidate: true });
        props.onChange && props.onChange([startValue(), value]);
    };

    const handleChangeValue = (value) => {

        setValue(name, value, { shouldValidate: true });
        props.onChange && props.onChange(value);

    };


    const value = () => {

        if (!startValue() && !endValue()) {
            return [null, null];
        }

        return [startValue(), endValue()];
    };

    const startValue = () => {

        if (!originalValue) {
            return null;
        }

        if (Array.isArray(!originalValue)) {
            return null;
        }

        if (!originalValue.length) {
            return null;
        }

        return originalValue[0] || null;
    };

    const endValue = () => {

        if (!originalValue) {
            return null;
        }

        if (Array.isArray(!originalValue)) {
            return null;
        }

        if (originalValue.length < 2) {
            return null;
        }

        return originalValue[1] || null;
    };


    const format = showTime ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";

    return (
        <LocalizationProvider
            dateAdapter={DateFnsUtils}
            locale={getLanguage().dateFns}
        >
            <DateRangePicker
                disabled={disabled}
                inputFormat={format}
                startText={startText}
                endText={endText}
                open={IsOpen}
                id={name}
                name={name}
                value={originalValue}
                onChange={(value) => {
                    handleChangeValue(value);
                    props.onChange && props.onChange(value)
                }}
                onBlur={(event) => {
                    props.onBlur && props.onBlur(event);
                    SetIsOpen(false)
                }}

                onClose={() => { SetIsOpen(false) }}
                required={required}
                {...inputProps}
                autoOk={true}
                renderInput={(startProps, endProps) => {
                    return (
                        <React.Fragment>
                            <TextField
                                disabled={disabled}
                                size="small"
                                margin="normal"
                                variant="outlined"
                                {...startProps}
                                fullWidth={true}
                                required={required}
                                autoComplete={autoComplete || undefined}
                                error={Boolean(errorMessage)}
                                helperText={errorMessage || hint}
                                InputProps={{
                                    endAdornment: (
                                        <Fragment>
                                            <InputAdornment>
                                                <IconButton disabled={disabled} position="end" style={{ color: "#747474", marginRight: "-11px" }} onClick={() => { SetIsOpen(!IsOpen) }}>
                                                    <DateRangeOutlinedIcon />
                                                </IconButton>
                                            </InputAdornment>
                                        </Fragment>
                                    ),
                                }}
                            />
                            <DateRangeDelimiter> to </DateRangeDelimiter>
                            <TextField
                                size="small"
                                margin="normal"
                                variant="outlined"
                                {...endProps}
                                fullWidth={true}
                                required={required}
                                autoComplete={autoComplete || undefined}
                                error={Boolean(errorMessage)}
                                helperText={errorMessage || hint}
                                InputProps={{
                                    endAdornment: (
                                        <Fragment>
                                            <InputAdornment>
                                                <IconButton disabled={disabled} position="end" style={{ color: "#747474", marginRight: "-11px" }} onClick={() => { SetIsOpen(!IsOpen) }}>
                                                    <DateRangeOutlinedIcon />
                                                </IconButton>
                                            </InputAdornment>
                                        </Fragment>
                                    ),
                                }}
                            />
                        </React.Fragment>
                    );
                }}

            // placeholder={placeholder || undefined}
            // autoFocus={autoFocus || undefined}
            // autoComplete={autoComplete || undefined}      
            // InputLabelProps={{
            //   shrink: true,
            // }}

            />
        </LocalizationProvider>
    );
}

DatePickerRangeFormItem.defaultProps = {
    required: false,
};

DatePickerRangeFormItem.propTypes = {
    name: PropTypes.string.isRequired,
    label: PropTypes.string,
    hint: PropTypes.string,
    autoFocus: PropTypes.bool,
    required: PropTypes.bool,
    size: PropTypes.string,
    prefix: PropTypes.string,
    placeholder: PropTypes.string,
    externalErrorMessage: PropTypes.string,
    formItemProps: PropTypes.object,
    showTime: PropTypes.bool,
    startText: PropTypes.string,
    endText: PropTypes.string,
    inputProps: PropTypes.object,
    onChange: PropTypes.func,
    onBlur: PropTypes.func,
    disabled: PropTypes.bool,
};

export default DatePickerRangeFormItem;
