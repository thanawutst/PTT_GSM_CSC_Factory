import { AuthToken } from 'src/Service/Config/AuthToken';
import Axios from 'axios';
import Qs from 'qs';
import moment from 'moment';

const AxiosConfig = Axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  paramsSerializer: function (params) {
    return Qs.stringify(params, {
      arrayFormat: 'brackets',
      filter: (prefix, value) => {
        console.log("paramsSerializer", value)
        if (moment.isMoment(value) || value instanceof Date) {
          console.log("paramsSerializer", value)
          return value.toISOString();
        }

        return value;
      },
    });
  },
});

AxiosConfig.interceptors.request.use(
  async function (options) {
    const token = AuthToken.get();
    options.headers['Accept'] = "application/json";
    options.headers['Content-Type'] = "application/json";

    if (token) {
      options.headers['Authorization'] = `Bearer ${token}`;
    }
    return options;
  },
  function (error) {
    console.log('Request error: ', error);
    return Promise.reject(error);
  },
);

export default AxiosConfig;
