import AxiosConfig from "src/Service/Config/AxiosConfig";

export const AxiosPostJson = async (path, data = {}) => {
    const response = await AxiosConfig.post(path, data);
    return response;
}

export const AxiosGetJson = async (path, data = {}) => {
    const config = {} as any;
    if (data) config.params = data;
    const response = await AxiosConfig.get(path, config); 
    console.log(response)
    return response.data;
}