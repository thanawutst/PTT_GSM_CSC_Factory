import { TextField } from "@material-ui/core";
import PropTypes from "prop-types";
import React from "react";
import { useFormContext } from "react-hook-form";
import FormErrors from "src/components/Commons/inputElements/formErrors";

export function InputNumberFormItem(props) {
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
    style,
    startAdornment,
    endAdornment,
    maxLength,
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
      type={type}
      label={label || undefined}
      required={required}
      onChange={(event) => {
        props.onChange && props.onChange(event.target.value);
      }}
      onBlur={(event) => {
        props.onBlur && props.onBlur(event);
      }}
      style={style}
      inputRef={register}
      margin="normal"
      fullWidth
      variant="outlined"
      size="small"
      placeholder={placeholder || undefined}
      autoFocus={autoFocus || undefined}
      autoComplete={autoComplete || "off"}
      InputLabelProps={
        {
          //shrink: true,
        }
      }
      inputProps={{
        style: { textAlign: "right" },
      }}
      error={Boolean(errorMessage)}
      helperText={errorMessage || hint}
      InputProps={startAdornment ? { startAdornment } : { endAdornment }}
      disabled={disabled}
    />
  );
}

InputNumberFormItem.defaultProps = {
  type: "number",
  required: false,
};

InputNumberFormItem.propTypes = {
  name: PropTypes.string.isRequired,
  required: PropTypes.bool,
  type: PropTypes.string,
  label: PropTypes.string,
  hint: PropTypes.string,
  autoFocus: PropTypes.bool,
  disabled: PropTypes.bool,
  prefix: PropTypes.string,
  placeholder: PropTypes.string,
  externalErrorMessage: PropTypes.string,
  style: PropTypes.object,
  onChange: PropTypes.func,
  startAdornment: PropTypes.any,
  endAdornment: PropTypes.any,
  maxLength: PropTypes.number
};

export default InputNumberFormItem;
