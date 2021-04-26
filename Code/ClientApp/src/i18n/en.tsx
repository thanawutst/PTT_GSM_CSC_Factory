const en = {
  common: {
    or: 'or',
    cancel: 'Cancel',
    reset: 'Reset',
    save: 'Save',
    search: 'Search',
    edit: 'Edit',
    new: 'New',
    export: 'Export to Excel',
    noDataToExport: 'No data to export',
    import: 'Import',
    discard: 'Discard',
    yes: 'Yes',
    no: 'No',
    pause: 'Pause',
    areYouSure: 'Are you sure?',
    view: 'View',
    destroy: 'Delete',
    mustSelectARow: 'Must select a row',
    start: 'Start',
    end: 'End',
    select: 'Select',
    continue: 'Continue',
    filters: 'Filters',
    add: 'Add',
    clear: 'Clear',
    back: 'Back',
    create: 'Create',
    default: 'Default',
  },

  app: {
    title: 'PTT GSM CSC Factory',
  },

  entities: {
    auth: {
      login: {
        fields: {
          username: "Username",
          password: "Password",
          showPassword: "Show Password",
          btnLogin: "Login"
        }
      }
    },
    simplePage: {
      fields: {
        exText1: "Input Hook Form 1",
        exDate1: "Datetime Hook Form 1",
        exDateRange1: "Date Range Hook Form 1",
        exDateRange1Start: "Start Date",
        exDateRange1End: "End Date",
      }
    },
    FormInput: {
      fields: {
        arrProject: "Project",
        dRequestDate: "Request Date",
        StartDate: "StartDate",
        StartTime: "StartTime",
        EndDate: "EndDate",
        EndTime: "EndTime",
        Descrition: "Descrition",
        CerDate: "Date of certification",
        benefit: "Benefits requested  ( get off work after 22.00 only )",
        Timeattendance: "Time attendance",
        OvertimeDate: "OvertimeDate",
        Status: "Status",
        EmployeeID: "Employee",
        Requester: "Requester",
        Comment: "Comment"
      }
    },
    FormInputHoliday: {
      fields: {
        Date: "Date",
        Holiday_Name: "Holiday Name",
        Substitution_Day: "Substitution Day",
        Please: "Please Request",
        Status: "Status",
      }
    },
    Project_Add: {
      fields: {
        Type: "Type",
        Project_Old: "Project Old",
        Project_Name: "Project Name",
        Project_Abbr: "Project Abbr",
        Year: "Year",
        dDate: "Date",
        Progress: "Progress",
        Status: "Status",
        Name: "Name",
        Role: "Role",

      }
    },
    FormInputMeeting: {
      fields: {
        sMeetingRoomName: "Room Name",
        nFloorID: "Floor",
        sActive: "Status"
      }
    },
    allowance: {
      fields: {
        meet: "Meeting Detail",
        project: "Project",
        date: "Transaction date",
        location: "Location",
        date2: "Date",
        roundtrip: "Round trip",
        overnight: "Overnight",
        starttime: "Start Time",
        endtime: "End Time",
        allowance: "Allowance",
        typetime: "Round trip/Overnight",
        time: "00.00",
        comment: "comment",
        requester: "Requester",
        status: "Status",
        dateRange1Start: "Start Date",
        dateRange1End: "End Date",
        requestDate: "Request Date",
        total: "Total",
        edit: "Edit",
      }
    },
    FormInputCategory: {
      fields: {
        sCategoryName: "Category Name",
        sDetail: "Category Detail",
        sActive: "Status"
      }
    },
    FormInputTaskType: {
      fields: {
        sTaskTypeName: "Task Type Name",
        sActive: "Status"
      }
    },
    FormInputRequestType: {
      fields: {
        sRequestTypeName: "Request Type Name",
        sActive: "Status"
      }
    },
    FormInputDestination: {
      fields: {
        sDestinationName: "Destination Name",
        sActive: "Status"
      }
    },
    FormTravel: {
      fields: {
        sProject: "Project",
        dRequestDate: "Request Date",
        dTravelDate: "Travel Date",
        sVehicle: "Vehicle",
        cRoundTrip: "Round Trip",
        sOtherFrom: "Other From",
        sOtherTo: "Other To",
        sFrom: "From",
        sTo: "To",
        nDistance: "Distance",
        nTollway: "Tollway",
        nAmount: "Amount",
        nTaxiAmount: "Amount",
        nTaxiTollway: "Tollway",
      },
    },
    FormLeaveQuota: {
      fields: {
        sEmployee: "Employee",
        sYear: "Year",
        sField: "Field",
        nSickLeave: "Sick Leave",
        nPersonalLeave: "Personal Leave",
        nVacationLeave: "Vacation Leave",
        nMaternityLeave: "Maternity Leave",
        nOrdinationLeave: "Ordination Leave",
        nBirthDayLeave: "Birth Day Leave",
      },
    },
  },
  errors: {
    backToHome: 'Back to home',
    403: `Sorry, you don't have access to this page`,
    404: 'Sorry, the page you visited does not exist',
    500: 'Sorry, the server is reporting an error',
    429: 'Too many requests. Please try again later.',
    forbidden: {
      message: 'Forbidden',
    },
    validation: {
      message: 'An error occurred',
    },
    defaultErrorMessage: 'Ops, an error occurred',
  },
  // See https://github.com/jquense/yup#using-a-custom-locale-dictionary
  /* eslint-disable */
  validation: {
    mixed: {
      default: '${path} is invalid',
      required: '${path} is required',
      oneOf:
        '${path} must be one of the following values: ${values}',
      notOneOf:
        '${path} must not be one of the following values: ${values}',
      notType: ({ path, type, value, originalValue }) => {
        return `${path} must be a ${type}`;
      },
    },
    string: {
      length:
        '${path} must be exactly ${length} characters',
      min: '${path} must be at least ${min} characters',
      max: '${path} must be at most ${max} characters',
      matches:
        '${path} must match the following: "${regex}"',
      email: '${path} must be a valid email',
      url: '${path} must be a valid URL',
      trim: '${path} must be a trimmed string',
      lowercase: '${path} must be a lowercase string',
      uppercase: '${path} must be a upper case string',
      selected: '${path} must be selected',
    },
    number: {
      min:
        '${path} must be greater than or equal to ${min}',
      max: '${path} must be less than or equal to ${max}',
      lessThan: '${path} must be less than ${less}',
      moreThan: '${path} must be greater than ${more}',
      notEqual: '${path} must be not equal to ${notEqual}',
      positive: '${path} must be a positive number',
      negative: '${path} must be a negative number',
      integer: '${path} must be an integer',
    },
    date: {
      min: '${path} field must be later than ${min}',
      max: '${path} field must be at earlier than ${max}',
    },
    boolean: {},
    object: {
      noUnknown:
        '${path} field cannot have keys not specified in the object shape',
    },
    array: {
      min: ({ min, path }) =>
        min === 1
          ? `${path} is required`
          : `${path} field must have at least ${min} items`,
      max:
        '${path} field must have less than or equal to ${max} items',
    },
  },

  autocomplete: {
    loading: 'Loading...',
    noOptions: 'No data found',
  },


  table: {
    noData: 'No records found',
    loading: 'Loading...',
  },

  pagination: {
    labelDisplayedRows: '{0}-{1} of {2}',
    labelRowsPerPage: 'Per page:',
  },
};

export default en;