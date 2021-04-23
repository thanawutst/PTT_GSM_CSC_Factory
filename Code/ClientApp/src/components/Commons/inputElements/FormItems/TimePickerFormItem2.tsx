import React from "react";
import PropTypes from "prop-types";
import { useFormContext } from "react-hook-form";
import FormErrors from "src/components/Commons/inputElements/formErrors";
import { TextField } from "@material-ui/core";

function TimePickerFormItem2(props) {
  const {
    label,
    name,
    hint,
    placeholder,
    autoFocus,
    autoComplete,
    externalErrorMessage,
    required,
    value,
    defaultValue,
    inputProps,
  } = props;

  const {
    register,
    errors,
    formState: { touched, isSubmitted },
  } = useFormContext();

  const errorMessage = FormErrors.errorMessage(
    name,
    errors,
    touched,
    isSubmitted,
    externalErrorMessage
  );

  return (
    <TextField
      id={name}
      name={name}
      label={label}
      required={required}
      margin="normal"
      fullWidth
      variant="outlined"
      size="small"
      inputRef={register}
      placeholder={placeholder || undefined}
      autoFocus={autoFocus || undefined}
      autoComplete={autoComplete || undefined}
      InputLabelProps={{
        shrink: true,
      }}
      onChange={(event) => {
        props.onChange && props.onChange(event.target.value);
      }}
      onBlur={(event) => {
        props.onBlur && props.onBlur(event);
      }}
      type="time"
      defaultValue={defaultValue}
      error={Boolean(errorMessage)}
      helperText={errorMessage || hint}
      InputProps={inputProps}
      {...inputProps}
    />
  );
}

TimePickerFormItem2.defaultProps = {
  type: "text",
  required: false,
};

TimePickerFormItem2.propTypes = {
  name: PropTypes.string.isRequired,
  required: PropTypes.bool,
  label: PropTypes.string,
  hint: PropTypes.string,
  value: PropTypes.string,
  defaultValue: PropTypes.string,
  autoFocus: PropTypes.bool,
  prefix: PropTypes.string,
  placeholder: PropTypes.string,
  externalErrorMessage: PropTypes.string,
  rows: PropTypes.number,
  inputProps: PropTypes.object,
};

export default TimePickerFormItem2;
