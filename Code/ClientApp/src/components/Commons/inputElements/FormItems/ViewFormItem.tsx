import { TextField } from '@material-ui/core';
import PropTypes from 'prop-types';
import React from 'react';
import { useFormContext } from 'react-hook-form';

function ViewFormItem(props) {
  const { label, name, disabled } = props;

  const { register } = useFormContext();

  return (
    <TextField
      id={name}
      name={name}
      label={label}
      disabled={disabled}
      fullWidth
      inputRef={register}
      margin="normal"
      InputProps={{
        readOnly: true,
      }}
      InputLabelProps={{
        shrink: true,
      }}
      variant="outlined"
      size="small"
    />
  );
}

ViewFormItem.defaultProps = {
  disabled: false

};

ViewFormItem.propTypes = {
  name: PropTypes.string.isRequired,
  label: PropTypes.string,
  disabled: PropTypes.bool
};

export default ViewFormItem;
