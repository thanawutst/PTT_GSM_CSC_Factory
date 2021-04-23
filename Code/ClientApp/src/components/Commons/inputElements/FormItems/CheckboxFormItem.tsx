import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormErrors from 'src/components/Commons/inputElements/formErrors';
import {
  Checkbox,
  FormControl,
  FormHelperText,
  FormLabel,
} from '@material-ui/core';
import { useFormContext } from 'react-hook-form';

export function CheckboxFormItem(props) {
  const {
    label,
    name,
    hint,
    required,
    labelPlacement,
    externalErrorMessage,
    disable,
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
  const positionLabel = labelPlacement != "" ? labelPlacement : "end";
  const formHelperText = errorMessage || hint;

  return (
    <FormGroup>
      <FormControl
        required={required}
        fullWidth
        error={Boolean(errorMessage)}
        component="fieldset"
        size="small"
      >
        <div>
          <FormControlLabel
            control={<Checkbox
              id={name}
              name={name}
              checked={watch(name) || false}
              onChange={(e) => {
                setValue(name, Boolean(e.target.checked), { shouldValidate: true });
                props.onChange &&
                  props.onChange(e.target.checked);
              }}
              onBlur={() => props.onBlur && props.onBlur(null)}
              color="secondary"
              size="small"
            ></Checkbox>}
            label={label}
            labelPlacement={positionLabel}
            disabled={disable}
          />
        </div>
        {formHelperText && (
          <FormHelperText style={{ marginTop: 0 }}>
            {formHelperText}
          </FormHelperText>
        )}
      </FormControl>
    </FormGroup>
  );
}

CheckboxFormItem.defaultProps = {};

CheckboxFormItem.propTypes = {
  name: PropTypes.string.isRequired,
  required: PropTypes.bool,
  label: PropTypes.string,
  hint: PropTypes.string,
  labelPlacement: PropTypes.string,
  onChange: PropTypes.func,
  externalErrorMessage: PropTypes.string,
  disable: PropTypes.bool
};

export default CheckboxFormItem;
