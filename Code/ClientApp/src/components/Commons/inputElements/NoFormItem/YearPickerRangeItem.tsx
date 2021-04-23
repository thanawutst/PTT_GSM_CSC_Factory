import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { TextField, InputAdornment, Grid } from "@material-ui/core";
import DateFnsUtils from "@material-ui/pickers/adapter/date-fns";
import DateRangeOutlinedIcon from '@material-ui/icons/DateRangeOutlined';
import {
    MobileDatePicker,
    MobileDateTimePicker,
    LocalizationProvider,
    DatePicker,
    DateRangeDelimiter,
} from "@material-ui/pickers";
import { getLanguage } from 'src/i18n';
import IconButton from '@material-ui/core/IconButton';

function YearPickerRangeItem(props) {
    const {
        label,
        name,
        placeholder,
        autoFocus,
        autoComplete,
        startText,
        endText,
        showTime,
        inputProps,
        value
    } = props;

    const [StartDate, handleStartDateChange] = useState(new Date() as any);
    const [EndDate, handleEndDateChange] = useState(new Date() as any);
    const [IsOpenStart, setIsOpenStart] = useState(false);
    const [IsOpenEnd, setIsOpenEnd] = useState(false);

    const handleChangedStart = async (value) => {
        await handleStartDateChange(value);

    };
    const handleChangedEnd = async (value) => {
        await handleEndDateChange(value);
        let Range = { StartYear: StartDate, EndYear: value }
        props.onchange && props.onchange(Range);

    };

    useEffect(() => {
        if (value) {
            handleStartDateChange(value.StartYear);
            handleEndDateChange(value.EndYear)
        } else {
            handleStartDateChange(null);
            handleEndDateChange(null)
        }
    }, [value])

    const DateTimePickerComponent = showTime
        ? MobileDateTimePicker
        : MobileDatePicker;

    return (

        <LocalizationProvider
            dateAdapter={DateFnsUtils}
            locale={getLanguage().dateFns}
        >
            <Grid container direction="row" item lg={12} md={12} sm={12} xs={12} alignItems="center">
                <Grid item lg={5} md={12} sm={12} xs={12}>
                    <MobileDatePicker
                        disableOpenPicker={true}
                        onChange={(value) => {
                            setIsOpenStart(false)
                            setIsOpenEnd(true)
                            handleChangedStart(value)
                            props.onChange && props.onChange(value);
                        }}
                        onBlur={(event) => {
                            props.onBlur && props.onBlur(event);
                        }}
                        openTo={"year"}
                        views={["year"]}
                        inputFormat={"yyyy"}
                        {...inputProps}
                        label={startText ? startText : label}
                        id={name}
                        autoOk={true}
                        onOpen={() => { setIsOpenStart(true) }}
                        onClose={() => { setIsOpenStart(false) }}
                        open={IsOpenStart}
                        value={StartDate}
                        renderInput={(props) => (
                            <TextField
                                style={{ textAlign: "center" }}
                                margin="normal"
                                variant="outlined"
                                size="small"
                                fullWidth
                                {...props}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="start" style={{ color: "#747474" }} >
                                            <IconButton style={{ color: "#747474", marginRight: "-20px" }} onClick={() => { setIsOpenStart(!IsOpenStart) }}>
                                                <DateRangeOutlinedIcon />
                                            </IconButton>

                                        </InputAdornment>
                                    ),
                                }}
                            />
                        )}
                    />
                </Grid>
                <Grid item lg={2} md={12} sm={12} xs={12}>
                    <DateRangeDelimiter translate={'p'} align="center"> to </DateRangeDelimiter>
                </Grid>
                <Grid item lg={5} md={12} sm={12} xs={12} >
                    <MobileDatePicker
                        onChange={(value) => {
                            setIsOpenEnd(false)
                            handleChangedEnd(value)
                            props.onChange && props.onChange(value);
                        }}
                        onBlur={(event) => {
                            props.onBlur && props.onBlur(event);
                        }}
                        minDate={StartDate}
                        openTo={"year"}
                        views={["year"]}
                        inputFormat={"yyyy"}
                        {...inputProps}
                        label={endText ? endText : label}
                        id={name}
                        autoOk={true}
                        onOpen={() => { setIsOpenEnd(true) }}
                        onClose={() => { setIsOpenEnd(false) }}
                        open={IsOpenEnd}
                        value={EndDate}
                        renderInput={(props) => (
                            <TextField
                                style={{ textAlign: "center" }}
                                margin="normal"
                                variant="outlined"
                                size="small"
                                fullWidth
                                {...props}
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="start" style={{ color: "#747474" }} >
                                            <IconButton style={{ color: "#747474", marginRight: "-20px" }} onClick={() => { setIsOpenEnd(!IsOpenEnd) }}>
                                                <DateRangeOutlinedIcon />
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                            />
                        )}
                    />
                </Grid>
            </Grid>
        </LocalizationProvider>

    );
}

YearPickerRangeItem.defaultProps = {
    required: false,
};

YearPickerRangeItem.propTypes = {
    name: PropTypes.string.isRequired,
    required: PropTypes.bool,
    label: PropTypes.string.isRequired,
    startText: PropTypes.string,
    endText: PropTypes.string,
    hint: PropTypes.string,
    autoFocus: PropTypes.bool,
    size: PropTypes.string,
    prefix: PropTypes.string,
    placeholder: PropTypes.string,
    externalErrorMessage: PropTypes.string,
    showTime: PropTypes.bool,
    inputProps: PropTypes.object,
    onchange: PropTypes.func,
    onBlur: PropTypes.func,
    value: PropTypes.object,

};


export default YearPickerRangeItem;
