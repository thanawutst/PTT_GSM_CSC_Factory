let notistakEnqueueSnackbar;

export default class SnackbarComs {
  static registerNotistakEnqueueSnackbar(instance) {
    notistakEnqueueSnackbar = instance;
  }

  static success(arg) {
    notistakEnqueueSnackbar(arg, { variant: 'success' });
  }

  static error(arg) {
    notistakEnqueueSnackbar(arg, { variant: 'error' });
  }
}
