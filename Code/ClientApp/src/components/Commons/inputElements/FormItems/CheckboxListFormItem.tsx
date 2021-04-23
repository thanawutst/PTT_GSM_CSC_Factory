import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import Select from 'react-select';
import { i18n } from 'src/i18n';
import {
    components as materialUiComponents,
    styles as materialUiStyles,
} from 'src/components/Commons/inputElements/FormItems/style/reactSelectMaterialUi';
import { makeStyles } from '@material-ui/core/styles';
import { useFormContext } from 'react-hook-form';
import FormErrors from 'src/components/Commons/inputElements/formErrors';
import { FormControl, FormControlLabel, FormHelperText, FormLabel, FormGroup } from "@material-ui/core";
import Checkbox from "@material-ui/core/Checkbox";
import Grid from "@material-ui/core/Grid";

export function CheckboxListFormItem(props) {

    const {
        label,
        name,
        form,
        hint,
        inputProps,
        required,
        dataProvider,
        AllowCheckAll,
        labelAll,
        defaultAll,
        validateTextOnTop,
        forceShowError,
        selectAll,
        selectAllTime,
        externalErrorMessage,
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

    const originalValue = watch(name) || [];
    const handleSelect = (values) => {
        const { name, AllowCheckAll, dataProvider } = props;
        const datas = watch(name) ? watch(name) : [];
        const index = datas.indexOf(values.target.name);
        if (AllowCheckAll) {
        }
        if (index === -1) {
            datas.push(values.target.name);
        } else {
            datas.splice(index, 1);
        }

        if (!datas || !datas.length) {
            setValue(name, null, { shouldValidate: true });
            props.onChange && props.onChange(null);
            return;
        }
        const newValue = datas.map((data) => (data ? data : undefined)).filter(Boolean);

        setValue(name, newValue, { shouldValidate: true });
        props.onChange && props.onChange(newValue);
        // if (values.target.checked) {
        //     setValue(name, values.target.name, { shouldValidate: true });
        //     props.onChange && props.onChange(values.target.name);
        // } else {
        //     setValue(name, null, { shouldValidate: true });
        //     props.onChange && props.onChange(null);
        //     return;
        // }
    };
    const value = () => {
        const { AllowCheckAll } = props;
        if (AllowCheckAll) {
            return valueAll();
        } else {
            return valueOne();
        }
    };
    const valueOne = () => {
        const { dataProvider } = props;
        debugger
        if (originalValue != null) {
            return Boolean(dataProvider.find((option) => option.id === originalValue));
        }

        return false;
    };
    const valueAll = () => {
        debugger
        if (originalValue) {
            return Boolean(originalValue.map((value) => dataProvider.find((option) => option.id === value)));
        }
        return false;
    };


    const handleSelectAll = (event) => {
        if (event.target.checked) {
            const newValue = dataProvider
                .map((data) => (data ? data.id : undefined))
                .filter(Boolean);

            setValue(name, newValue, { shouldValidate: true });
            props.onChange && props.onChange(newValue);
        }
        else {
            setValue(name, null, { shouldValidate: true });
            props.onChange && props.onChange(null);
            return;
        }
    };
    const errorMessage = FormErrors.errorMessage(
        name,
        errors,
        touched,
        isSubmitted,
        externalErrorMessage,
    );
    const formHelperText = errorMessage || hint;
        //selectAll && selectAllTime == 1 ? "" : errorMessage || hint;
  
    return (
        <FormControl
            style={{ marginTop: "16px" }}
            required={required}
            fullWidth
            error={errorMessage}
            component="fieldset"
        >
            <FormLabel component="legend">{label}</FormLabel>
            {formHelperText && validateTextOnTop && (
                <FormHelperText style={{ marginTop: 0 }}>
                    {formHelperText}
                </FormHelperText>
            )}
            <FormGroup row>
                <Grid container>
                    {AllowCheckAll && (
                        <FormControlLabel
                            key={"all"}
                            control={
                                <Checkbox
                                    id={name + "_All"}
                                    name={name + "_All"}
                                    checked={originalValue.length === dataProvider.length}
                                    onChange={(e) => handleSelectAll(e)}
                                    color="primary"
                                    {...inputProps}
                                />
                            }
                            label={labelAll ? labelAll : "All " + label}
                        />

                    )}

                    {dataProvider.map((row, index) => (
                        <FormControlLabel
                            key={index}
                            control={
                                <Checkbox
                                    id={name + "_" + row.id}
                                    name={row.id}
                                    checked={originalValue.indexOf(row.id) !== -1}
                                    onChange={handleSelect}
                                    color="primary"
                                    {...inputProps}
                                />
                            }
                            label={row.label}
                        />

                    ))}
                </Grid>
            </FormGroup>
            {formHelperText && !validateTextOnTop && (
                <FormHelperText style={{ marginTop: 0 }}>
                    {formHelperText}
                </FormHelperText>
            )}
        </FormControl>

    );
}
CheckboxListFormItem.defaultProps = {
    selectAll: false,
    selectAllTime: 0,
    required: false,
};
CheckboxListFormItem.propTypes = {
    //form: PropTypes.object.isRequired,
    name: PropTypes.string.isRequired,
    required: PropTypes.bool,
    label: PropTypes.string,
    hint: PropTypes.string,
    errorMessage: PropTypes.string,
    inputProps: PropTypes.object,
    dataProvider: PropTypes.array,
    AllowCheckAll: PropTypes.bool,
    labelAll: PropTypes.string,
    defaultAll: PropTypes.bool,
    selectAll: PropTypes.bool,
    selectAllTime: PropTypes.number,
    externalErrorMessage: PropTypes.string,
    onChange: PropTypes.func,
};
export default CheckboxListFormItem;