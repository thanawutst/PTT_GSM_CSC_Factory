import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import Select from 'react-select';
import { i18n } from 'src/i18n';
import {
    components,
    components as materialUiComponents,
    styles as materialUiStyles,
} from 'src/components/Commons/inputElements/FormItems/style/reactSelectMaterialUi';
import { emphasize, makeStyles } from '@material-ui/core/styles';
import grey from '@material-ui/core/colors/grey';
import { Search } from '@material-ui/icons';


const useStyles = makeStyles(materialUiStyles as any);
export const styles = (theme) => ({
    root: {
        flexGrow: 1,
        height: 250,
        minWidth: 290,
        fontSize: 14
    },
    input: {
        display: 'flex',
        padding: 0,
        height: 'auto',
        minHeight: '40px',
    },
    valueContainer: {
        display: 'flex',
        flexWrap: 'wrap',
        flex: 1,
        alignItems: 'center',
        overflow: 'hidden',
        paddingLeft: theme.spacing(1),
    },
    chip: {
        margin: theme.spacing(0.5, 0.25),
    },
    chipFocused: {
        backgroundColor: emphasize(
            theme.palette.type === 'light'
                ? theme.palette.grey[300]
                : theme.palette.grey[700],
            0.08,
        ),
    },
    noOptionsMessage: {
        padding: theme.spacing(1, 2),
    },
    singleValue: {
        fontSize: 14,
    },
    placeholder: {
        position: 'absolute',
        left: 2,
        bottom: 6,
        fontSize: 14,
    },
    paper: {
        position: 'absolute',
        zIndex: 2,
        marginTop: theme.spacing(1),
        left: 0,
        right: 0,
    },
    divider: {
        height: theme.spacing(2),
    },

});
const indicatorSeparatorStyle = {
    alignSelf: 'stretch',
    marginBottom: 8,
    marginTop: 8,
    width: 1,
};

const IndicatorSeparator = ({ innerProps }) => {
    return <span style={indicatorSeparatorStyle} {...innerProps} />;
};



function SelectFormItemCustom(props) {
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

    return (
        <Select
            styles={styles}
            classes={classes}
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
            components={IndicatorSeparator}
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

SelectFormItemCustom.defaultProps = {
    required: false,
    isClearable: true,
};

SelectFormItemCustom.propTypes = {
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

export default SelectFormItemCustom;
