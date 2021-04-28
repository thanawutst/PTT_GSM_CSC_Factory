import React from "react";
import PropTypes from "prop-types";
import { TextField } from "@material-ui/core";
import { useFormContext } from "react-hook-form";
import FormErrors from "src/components/Commons/inputElements/formErrors";
import { watch } from "fs";

export function InputFormItem(props) {
  const {
    label,
    name,
    hint,
    type,
    placeholder,
    autoFocus,
    autoComplete,
    required,
    externalErrorMessage,
    disabled,
    startAdornment,
    endAdornment,
    maxLength,
    Aligntext,
  } = props;

  const {
    register,
    errors,
    formState: { touched, isSubmitted },
    getValues,
  } = useFormContext();

  const errorMessage = FormErrors.errorMessage(
    name,
    errors,
    touched,
    isSubmitted,
    externalErrorMessage
  );
  // const shrinked = getValues(name) == "" ? false : true;
  const shrinked = true;
  return (
    <TextField
      id={name}
      name={name}
      type={type}
      label={label}
      required={required}
      inputRef={register}
      onChange={(event) => {
        props.onChange && props.onChange(event.target.value);
      }}
      onBlur={(event) => {
        props.onBlur && props.onBlur(event);
      }}
      margin="normal"
      fullWidth
      variant="outlined"
      size="small"
      placeholder={placeholder || undefined}
      autoFocus={autoFocus || undefined}
      autoComplete={autoComplete || "off"}
      InputLabelProps={{
        shrink: shrinked,
      }}
      error={Boolean(errorMessage)}
      helperText={errorMessage || hint}
      InputProps={startAdornment ? { startAdornment } : { endAdornment }}
      inputProps={{
        maxLength: maxLength ? maxLength : null,
        name,
        style: { textAlign: Aligntext ? Aligntext : "start" }, //fee add
      }}
      disabled={disabled}
      // value={props.value || ""}
    />
  );
}

InputFormItem.defaultProps = {
  type: "text",
  required: false,
};

InputFormItem.propTypes = {
  name: PropTypes.string.isRequired,
  required: PropTypes.bool,
  type: PropTypes.string,
  label: PropTypes.string,
  Aligntext: PropTypes.string,
  hint: PropTypes.string,
  autoFocus: PropTypes.bool,
  disabled: PropTypes.bool,
  prefix: PropTypes.string,
  placeholder: PropTypes.string,
  autoComplete: PropTypes.string,
  externalErrorMessage: PropTypes.string,
  onChange: PropTypes.func,
  startAdornment: PropTypes.any,
  endAdornment: PropTypes.any,
  maxLength: PropTypes.number,
  // value: PropTypes.string,
};

export default InputFormItem;
