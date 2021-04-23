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
import { Search } from '@material-ui/icons';
import { grey } from '@material-ui/core/colors';
import MenuItem from '@material-ui/core/MenuItem';
import { Checkbox, Chip, TextField } from '@material-ui/core';
import CancelIcon from '@material-ui/icons/Cancel';
import clsx from 'clsx';

const useStyles = makeStyles(materialUiStyles as any);

function DropdownIndicator(props) {
    const {
        getStyles,
        innerProps: { ref, ...restInnerProps },
    } = props;
    const useStyles = makeStyles((theme) => ({
        Icon: {
            paddingRight: "10",
            color: grey[500],
            '&:hover': {
                color: grey[700],
            },
        },
    }));
    const classes = useStyles();
    return (
        <components.DropdownIndicator
            {...restInnerProps}
            ref={ref}
            style={getStyles('clearIndicator', props)}
        >
            <Search style={{ paddingRight: "7", paddingLeft: "7", fontSize: 40 }} className={classes.Icon} />
        </components.DropdownIndicator>
    )
}
function Option(props) {
    return (
        <MenuItem
            ref={props.innerRef}
            selected={props.isFocused}
            component="div"
            style={{
                fontWeight: props.isSelected ? 500 : 400,
            }}
            {...props.innerProps}
        >
            <Checkbox checked={props.isSelected} color={"primary"} />
            {props.children}
        </MenuItem>
    );
}

function MultiValue(props) {
    return (
        <Chip
            tabIndex={0}
            label={props.children}
            className={clsx(props.selectProps.classes.chip, {
                [props.selectProps.classes.chipFocused]:
                    props.isFocused,
            })}
            style={{ textOverflow: 'ellipsis' }}
            onDelete={props.removeProps.onClick}
            deleteIcon={<CancelIcon {...props.removeProps} />}
            size="small"
        />
    );
}

function inputComponent({ inputRef, ...props }) {
    return <div ref={inputRef} {...props} />;
}

function Control(props) {
    const {
        children,
        innerProps,
        innerRef,
        selectProps: { classes, TextFieldProps },
    } = props;

    return (
        <TextField
            InputProps={{
                inputComponent,
                inputProps: {
                    className: classes.input,
                    ref: innerRef,
                    children,
                    ...innerProps,
                },
            }}
            {...TextFieldProps}
        />
    );
}

function SelectFormItemCuctom(props) {
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
            closeMenuOnSelect={false}
            hideSelectedOptions={false}
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
            components={{ DropdownIndicator: DropdownIndicator, Option: Option, Control: Control, MultiValue: MultiValue }}
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

SelectFormItemCuctom.defaultProps = {
    required: false,
    isClearable: true,
};

SelectFormItemCuctom.propTypes = {
    name: PropTypes.string,
    options: PropTypes.array.isRequired,
    label: PropTypes.string,
    hint: PropTypes.string,
    required: PropTypes.bool,
    externalErrorMessage: PropTypes.string,
    mode: PropTypes.string,
    isClearable: PropTypes.bool,
    value: PropTypes.any,
    onchange: PropTypes.func,
};

export default SelectFormItemCuctom;
