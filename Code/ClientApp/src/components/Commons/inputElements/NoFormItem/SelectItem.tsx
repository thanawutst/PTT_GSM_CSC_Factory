import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import Select from 'react-select';
import { i18n } from 'src/i18n';
import {
    components as materialUiComponents,
    styles as materialUiStyles,
} from 'src/components/Commons/inputElements/FormItems/style/reactSelectMaterialUi';
import { makeStyles } from '@material-ui/core/styles';


const useStyles = makeStyles(materialUiStyles as any);

function SelectFormItem(props) {
    const {
        label,
        name,
        hint,
        options,
        required,
        mode,
        placeholder,
        isClearable,
        externalErrorMessage,
        value
    } = props;

    const classes = useStyles();

    const controlStyles = {
        container: (provided) => ({
            ...provided,
            width: '100%',
            marginTop: '16px',
            marginBottom: '8px',
        }),
        control: (provided) => ({
            ...provided,
            borderColor: externalErrorMessage ? 'red' : undefined,
        }),
    };

    return (
        <Select
            styles={controlStyles}
            classes={classes}
            value={value}
            onChange={(e) => { props.onchange && props.onchange(e) }}
            inputId={name}
            TextFieldProps={{
                label,
                required,
                variant: 'outlined',
                fullWidth: true,
                size: 'small',
                error: Boolean(externalErrorMessage),
                helperText: externalErrorMessage || hint,
                InputLabelProps: {
                    shrink: true,
                },
            }}
            components={materialUiComponents}
            onBlur={(event) => {
                props.onBlur && props.onBlur(event);
            }}
            options={options}
            isMulti={mode === 'multiple'}
            placeholder={placeholder || ''}
            isClearable={isClearable}
            loadingMessage={() => i18n('autocomplete.loading')}
            noOptionsMessage={() =>
                i18n('autocomplete.noOptions')
            }
        />
    );
}

SelectFormItem.defaultProps = {
    required: false,
    isClearable: true,
};

SelectFormItem.propTypes = {
    name: PropTypes.string,
    options: PropTypes.array.isRequired,
    label: PropTypes.string,
    hint: PropTypes.string,
    required: PropTypes.bool,
    externalErrorMessage: PropTypes.string,
    mode: PropTypes.string,
    isClearable: PropTypes.bool,
    value: PropTypes.string,
    onchange: PropTypes.func,
};

export default SelectFormItem;
