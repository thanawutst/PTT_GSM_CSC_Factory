import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import FormErrors from 'src/components/Commons/inputElements/formErrors';
import {
  FormLabel,
  RadioGroup,
  Radio,
  FormControlLabel,
  FormControl,
  FormHelperText,
} from '@material-ui/core';
import { useFormContext } from 'react-hook-form';

function RadioFormItem(props) {
  const {
    label,
    name,
    hint,
    options,
    externalErrorMessage,
    required,
    disable
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

  const formHelperText = errorMessage || hint;

  return (
    <FormControl
      required={required}
      error={Boolean(errorMessage)}
      component="fieldset"
      size="small"
      disabled={disable}
    >
      <FormLabel component="legend">{label}</FormLabel>
      <RadioGroup
        id={name}
        name={name}
        value={String(watch(name) || '')}
        onChange={(e) => {
          console.log(" e.target.value", e.target.value)
          console.log(" name", name)
          setValue(name, e.target.value, { shouldValidate: true });
          props.onChange && props.onChange(e.target.value);
        }}
        onBlur={(event) => {
          props.onBlur && props.onBlur(event);
        }}
        row
      >
        {options.map((option) => (
          <FormControlLabel
            key={option.value}
            value={String(option.value)}
            control={<Radio size="small" />}
            label={option.label}
            checked={String(props.value) !== "" ? (String(props.value) === String(option.value) ? true : false) : false}
            disabled={props.disabled}          
          />
        ))}
      </RadioGroup>
      {formHelperText && (
        <FormHelperText style={{ marginTop: 0 }}>
          {formHelperText}
        </FormHelperText>
      )}
    </FormControl>
  );
}

RadioFormItem.defaultProps = {
  required: false,
};

RadioFormItem.propTypes = {
  name: PropTypes.string.isRequired,
  options: PropTypes.array.isRequired,
  label: PropTypes.string,
  hint: PropTypes.string,
  required: PropTypes.bool,
  externalErrorMessage: PropTypes.string,
  onChange: PropTypes.func,
  value: PropTypes.string,
  disable: PropTypes.bool
};

export default RadioFormItem;
