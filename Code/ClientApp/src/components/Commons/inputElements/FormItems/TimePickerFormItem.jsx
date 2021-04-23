import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import { TextField } from "@material-ui/core";
import DateFnsUtils from "@material-ui/pickers/adapter/date-fns";
import {
    TimePicker,
    LocalizationProvider,
} from "@material-ui/pickers";
import { getLanguage } from 'src/i18n';
import { useFormContext } from 'react-hook-form';
import FormErrors from 'src/components/Commons/inputElements/formErrors';

export function TimePickerFormItem(props) {
    const {
        label,
        name,
        hint,
        externalErrorMessage,
        autoComplete,
        inputProps,
        required,
        minTime,
        maxTime,
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

    const errorMessage = FormErrors.errorMessage(
        name,
        errors,
        touched,
        isSubmitted,
        externalErrorMessage,
    );

  

    return (
        <LocalizationProvider
            dateAdapter={DateFnsUtils}
            locale={getLanguage().dateFns}
        >
            <TimePicker
                id={name}
                label={label}
                ampm={false}
                autoComplete={autoComplete || undefined}
                autoOk={true}
                minTime={minTime || undefined}
                maxTime={maxTime || undefined}
                onChange={(value) => {
                    setValue(name, value, { shouldValidate: true });
                    props.onChange && props.onChange(value);
                }}
                onBlur={(event) => {
                    props.onBlur && props.onBlur(event);
                }}
                value={watch(name) || null}
                // placeholder={placeholder || undefined}
                // autoFocus={autoFocus || undefined}
                // autoComplete={autoComplete || undefined}
                // InputLabelProps={{
                //     shrink: true,
                // }}
                //autoOk
                {...inputProps}
                renderInput={(props) => (
                    <TextField
                        margin="normal"
                        variant="outlined"
                        size="small"
                        fullWidth
                        {...props}
                        required={required}
                        error={Boolean(errorMessage)}
                        helperText={errorMessage || hint}
                    />
                )}
            />
        </LocalizationProvider>
    );
}

TimePickerFormItem.defaultProps = {
    required: false,
};

TimePickerFormItem.propTypes = {
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
    onBlur: PropTypes.func
};

export default TimePickerFormItem;
