import { AuthToken } from "src/Service/Config/AuthToken";
import Axios from "axios";
import Qs from "qs";
import moment from "moment";
import { apiurl, url } from "src/Service/Config/ApiUrl";

const AxiosConfig = Axios.create({
  baseURL: apiurl,
  paramsSerializer: function (params) {
    return Qs.stringify(params, {
      arrayFormat: "brackets",
      filter: (prefix, value) => {
        if (moment.isMoment(value) || value instanceof Date) {
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
    options.headers["Accept"] = "application/json";
    options.headers["Content-Type"] = "application/json";

    if (token) {
      options.headers["Authorization"] = `Bearer ${token}`;
    }
    return options;
  },
  function (error) {
    console.log("Request error: ", error);
    return Promise.reject(error);
  }
);

export default AxiosConfig;
