import React, { Component } from "react";
import AutocompleteFormItem from "src/components/Commons/inputElements/FormItems/AutocompleteFormItem";
import { AxiosGetJson, AxiosPostJson } from "src/Service/Config/AxiosMethod";

const AutoCompleteDistName = (props) => {
  const fetchFn = async (value, limit) => {
    if (value.length >= 3) {
      let resp: any = await AxiosGetJson(
        `AutoComplete/GetDistributionName/${value}`
      );
      return resp; // REST API
    }
  };

  const mapper = {
    toAutocomplete(originalValue) {
      if (!originalValue) {
        return undefined;
      }

      let value = originalValue.value;
      let label = originalValue.label;

      return {
        key: value,
        value,
        label,
      };
    },

    toValue(originalValue) {
      if (!originalValue) {
        return undefined;
      }

      return {
        value: originalValue.value,
        label: originalValue.label,
      };
    },
  };
  return (
    <React.Fragment>
      <AutocompleteFormItem
        fetchFn={fetchFn}
        mapper={mapper}
        name={props.name}
        label={props.label}
        required={props.required}
        {...props}
      />
    </React.Fragment>
  );
};

export default AutoCompleteDistName;
