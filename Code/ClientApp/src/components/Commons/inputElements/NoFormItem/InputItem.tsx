import React from 'react';
import PropTypes from 'prop-types';
import { TextField } from '@material-ui/core';


export function InputItem(props) {
    const {
        label,
        name,
        hint,
        type,
        placeholder,
        autoFocus,
        autoComplete,
    
        disabled,
        endAdornment,
        maxLength,
    } = props;

   

    return (
        <TextField
            id={name}
            name={name}
            type={type}
            label={label}
            onChange={(event) => {
                props.onChange &&
                    props.onChange(event.target.value);
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
                //shrink: false,
            }}
           
            InputProps={{ endAdornment }}
            inputProps={{
                maxLength: maxLength ? maxLength : null,
                name,
            }}
            disabled={disabled}
        />
    );
}

InputItem.defaultProps = {
    type: 'text',
};

InputItem.propTypes = {
    name: PropTypes.string.isRequired,
    type: PropTypes.string,
    label: PropTypes.string,
    hint: PropTypes.string,
    autoFocus: PropTypes.bool,
    disabled: PropTypes.bool,
    prefix: PropTypes.string,
    placeholder: PropTypes.string,
    autoComplete: PropTypes.string,
    onChange: PropTypes.func,
    endAdornment: PropTypes.any,
    maxLength: PropTypes.number
};

export default InputItem;
