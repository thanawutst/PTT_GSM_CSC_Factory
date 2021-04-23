import React, { useEffect, useState } from 'react';
import PropTypes from "prop-types";
import FormErrors from 'src/components/Commons/inputElements/formErrors';
import { TextField } from "@material-ui/core";
import DateFnsUtils from "@material-ui/pickers/adapter/date-fns";
import { TimePicker, LocalizationProvider, MobileDatePicker, MobileDateTimePicker, } from "@material-ui/pickers";
import { getLanguage } from 'src/i18n';
import { useFormContext } from 'react-hook-form';
import { es } from 'date-fns/locale';
import { invalid } from 'moment';
import moment from 'moment';



export function TimePickerFormItem(props) {
    const [MinTime, SetMinTime] = useState(props.minTime);
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
        minTime,
        maxTime,
    } = props;

    const {
        register,
        errors,
        formState: { touched, isSubmitted },
        setValue,
        setError,
        watch,
        handleSubmit,
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

    const DateTimePickerComponent = showTime
        ? MobileDateTimePicker
        : MobileDatePicker;

    const format = "HH:mm";

    return (
        <LocalizationProvider
            dateAdapter={DateFnsUtils}
            locale={getLanguage().dateFns}
        >
            <TimePicker
                clearable
                inputFormat={format}
                label={label}
                id={name}
                ampm={false}
                minTime={minTime || undefined}
                maxTime={maxTime || undefined}
                autoComplete={autoComplete || undefined}
                onChange={(date: Date, keyboardInputValue?: string) => {
                    let Key = keyboardInputValue
                    let Ispass = true;
                    if (Key) {
                        if (Key.length == 1 && Number(Key.substr(0, 1)) > 2) {
                            Ispass = false
                            // setError(name, {
                            //     type: "validate",
                            //     message: "Invalid Date"
                            // })
                        }
                        else if (Key.length > 2) {
                            if (Number(Key.substr(0, 2)) >= 24) {
                                Ispass = false
                                // setError(name, {
                                //     type: "validate",
                                //     message: "Invalid Date"
                                // })
                            }
                        }
                     if (Key.length >= 4) {
                            if (Number(Key.substr(3, 2)) >= 59) {
                                Ispass = false
                                // setError(name, {
                                //     type: "validate",
                                //     message: "Invalid Date"
                                // })
                            }
                        }

                    }
                    if (date && Key != undefined) {
                        if (minTime || maxTime) {
                            let valueTime = new Date().setHours(moment(date).hour(), moment(date).minute(), 0, 0);
                            let MinTime = new Date().setHours(moment(minTime).hour(), moment(minTime).minute(), 0, 0);
                            if (valueTime < MinTime) {
                                Ispass = false
                                // setError(name, {
                                //     message: "Invalid Date",

                                // })
                            }
                            if (maxTime != undefined) {
                                let MaxTime = new Date().setHours(moment(maxTime).hour(), moment(maxTime).minute(), 0, 0);
                                if (valueTime > MaxTime) {
                                    Ispass = false
                                    // setError(name, {
                                    //     type: "validate",
                                    //     message: "Invalid Date"
                                    // })
                                }
                            }
                        }
                    }
                    if (Ispass) {
                        props.onChange && props.onChange(date);
                        // if ((date && Key?.length == 5) || (date && Key == undefined)) {
                        setValue(name, date)
                        // } else {
                        //     setValue(name, "")
                        // }

                    } else {
                        props.onChange && props.onChange(null);
                        // if ((date && Key?.length == 5) || (date && Key == undefined)) {
                        setValue(name, null)
                        // }

                    }
                }}
                value={watch(name) || null}
                autoOk
                {...inputProps}
                renderInput={(prop) =>
                (
                    <TextField
                        margin="normal"
                        variant="outlined"
                        size="small"
                        fullWidth
                        // value={props.value || ''}
                        {...prop}
                        inputProps={{ ...prop.inputProps, value: (Number(prop.inputProps?.value.substr(0, 2)) >= 24 || Number(prop.inputProps?.value.substr(3, 2)) >= 59 ? "" : prop.inputProps?.value) }}
                        required={required}
                        // error={Boolean(errorMessage)}
                        helperText={errorMessage || hint}

                    />
                )
                }
            />
            {/* <DateTimePickerComponent
                clearable
                inputFormat={format}
                label={label}
                id={name}
                onChange={(value) => {
                    setValue(name, value, { shouldValidate: true });
                    props.onChange && props.onChange(value);
                }}
                onBlur={(event) => {
                    props.onBlur && props.onBlur(event);
                }}
                value={watch(name) || null}
                placeholder={placeholder || undefined}
                autoFocus={autoFocus || undefined}
                autoComplete={autoComplete || undefined}
            // InputLabelProps={{
            //     shrink: true,
            // }}

            /> */}
        </LocalizationProvider>
    );
}

TimePickerFormItem.defaultProps = {
    required: false,
};

TimePickerFormItem.propTypes = {
    form: PropTypes.object,
    name: PropTypes.string.isRequired,
    required: PropTypes.bool,
    label: PropTypes.string,
    hint: PropTypes.string,
    autoFocus: PropTypes.bool,
    size: PropTypes.string,
    prefix: PropTypes.string,
    placeholder: PropTypes.string,
    errorMessage: PropTypes.string,
    inputProps: PropTypes.object,
    onChange: PropTypes.func,
    value: PropTypes.string,

};

export default TimePickerFormItem;
