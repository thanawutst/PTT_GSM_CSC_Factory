import { i18n } from "src/i18n";
import PropTypes from "prop-types";
import React, { useEffect } from "react";
import FormErrors from "src/components/Commons/inputElements/formErrors";
import AsyncSelect from "react-select/async";
import {
  components as materialUiComponents,
  styles as materialUiStyles,
} from "src/components/Commons/inputElements/FormItems/style/reactSelectMaterialUi";
import { makeStyles } from "@material-ui/core/styles";
import { IconButton } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import { useFormContext } from "react-hook-form";

const AUTOCOMPLETE_SERVER_FETCH_SIZE = 100;

const useStyles = makeStyles(materialUiStyles as any);

function AutocompleteFormItem(props) {
  const {
    label,
    name,
    hint,
    options,
    placeholder,
    autoFocus,
    externalErrorMessage,
    mode,
    required,
    isClearable,
    fetchFn,
    mapper,
  } = props;

  const {
    errors,
    watch,
    setValue,
    register,
    formState: { touched, isSubmitted },
  } = useFormContext();

  const errorMessage = FormErrors.errorMessage(
    name,
    errors,
    touched,
    isSubmitted,
    externalErrorMessage
  );

  const originalValue = watch(name);

  useEffect(() => {
    register({ name });
  }, [register, name]);

  const value = () => {
    const { mode } = props;
    if (mode === "multiple") {
      return valueMultiple();
    } else {
      return valueOne();
    }
  };

  const valueMultiple = () => {
    const { mapper } = props;

    if (originalValue) {
      return originalValue.map((value) => mapper.toAutocomplete(value));
    }

    return [];
  };

  const valueOne = () => {
    const { mapper } = props;

    if (originalValue) {
      return mapper.toAutocomplete(originalValue);
    }

    return null;
  };

  const handleSelect = (value) => {
    if (mode === "multiple") {
      return handleSelectMultiple(value);
    } else {
      return handleSelectOne(value);
    }
  };

  const handleSelectMultiple = (values) => {
    if (!values) {
      setValue(name, [], { shouldValidate: true });
      props.onChange && props.onChange([]);
      return;
    }

    const newValue = values.map((value) => mapper.toValue(value));
    setValue(name, newValue, { shouldValidate: true });
    props.onChange && props.onChange(newValue);
  };

  const handleSelectOne = (value) => {
    if (!value) {
      setValue(name, null, { shouldValidate: true });
      props.onChange && props.onChange(null);
      return;
    }

    const newValue = mapper.toValue(value);

    if (props.onSetValues) {
      props.onSetValues(value, newValue); // SET VALUE ตรงนี้!
    }

    setValue(name, newValue, { shouldValidate: true });
    props.onChange && props.onChange(newValue);

    mapper.toValue(value);
  };

  const handleSearch = async (value) => {
    try {
      const results = await fetchFn(value, AUTOCOMPLETE_SERVER_FETCH_SIZE);
      if (results) {
        return results.map((result) => mapper.toAutocomplete(result));
      }
    } catch (error) {
      console.error(error);
      return [];
    }
  };

  const classes = useStyles();

  const controlStyles = {
    container: (provided) => ({
      ...provided,
      width: "100%",
      marginTop: "16px",
      marginBottom: "8px",
    }),
    control: (provided) => ({
      ...provided,
      borderColor: errorMessage ? "red" : undefined,
    }),
  };

  return (
    <div style={{ display: "flex", alignItems: "center" }}>
      <AsyncSelect
        styles={controlStyles}
        classes={classes}
        value={value()}
        onChange={handleSelect}
        inputId={name}
        TextFieldProps={{
          label,
          required,
          variant: "outlined",
          fullWidth: true,
          size: "small",
          error: Boolean(errorMessage),
          helperText: errorMessage || hint,
          InputLabelProps: {
            shrink: true,
          },
        }}
        components={materialUiComponents}
        defaultOptions={true}
        isMulti={mode === "multiple" ? true : false}
        loadOptions={handleSearch}
        // options={options}
        placeholder={placeholder || ""}
        autoFocus={autoFocus || undefined}
        onBlur={() => props.onBlur && props.onBlur(null)}
        isClearable={isClearable}
        loadingMessage={() => i18n("autocomplete.loading")}
        noOptionsMessage={() => i18n("autocomplete.noOptions")}
      />

      {props.showCreate && props.hasPermissionToCreate ? (
        <IconButton
          style={{
            marginLeft: "16px",
            marginTop: "16px",
            marginBottom: "8px",
            flexShrink: 0,
          }}
          color="secondary"
          onClick={props.onOpenModal}
        >
          <AddIcon />
        </IconButton>
      ) : null}
    </div>
  );
}

AutocompleteFormItem.defaultProps = {
  isClearable: true,
  mode: "default",
  required: false,
};

AutocompleteFormItem.propTypes = {
  fetchFn: PropTypes.func.isRequired,
  mapper: PropTypes.object.isRequired,
  required: PropTypes.bool,
  mode: PropTypes.string,
  name: PropTypes.string,
  options: PropTypes.array,
  label: PropTypes.string,
  hint: PropTypes.string,
  autoFocus: PropTypes.bool,
  placeholder: PropTypes.string,
  externalErrorMessage: PropTypes.string,
  isClearable: PropTypes.bool,
  showCreate: PropTypes.bool,
  hasPermissionToCreate: PropTypes.bool,
};

export default AutocompleteFormItem;
