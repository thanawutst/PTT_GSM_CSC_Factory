import { AxiosPostJson, AxiosGetJson } from "src/Service/Config/AxiosMethod";
import { AuthToken } from "src/Service/Config/AuthToken";
import { getHistory } from "src/App";

export default class AuthenService {
    static CreateTokenLogin(data) {
        const response = AxiosPostJson('/Authen/CreateTokenLogin', data);
       
        return response;
    }

    static DataUserAppBar() {
        const response = AxiosPostJson('/Authen/DataUserAppBar');
        return response;
    }

    static async SignOut() {
        await AuthToken.set(null);
        getHistory().push("/login");
    }

    static GetMenuWithPermission() {
        const response = AxiosPostJson('/Authen/GetMenuWithPermission');
        return response;
    }

}