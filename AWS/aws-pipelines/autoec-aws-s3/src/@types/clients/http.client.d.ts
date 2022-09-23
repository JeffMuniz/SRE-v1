
declare namespace HttpClient {

  type GetParams = {
    client?: ExternalModules.Axios.AxiosInstance;
    options?: ExternalModules.Axios.AxiosRequestConfig;
    params?: any,
    path: string;
    url: string;
  }

  type PostParams = {
    client?: ExternalModules.Axios.AxiosInstance;
    options?: ExternalModules.Axios.AxiosRequestConfig;
    params?: any,
    path: string;
    url: string;
  }

  type PutParams = {
    client?: ExternalModules.Axios.AxiosInstance;
    options?: ExternalModules.Axios.AxiosRequestConfig;
    params?: any,
    path: string;
    url: string;
  }

  type PatchParams = {
    client?: ExternalModules.Axios.AxiosInstance;
    options?: ExternalModules.Axios.AxiosRequestConfig;
    params?: any,
    path: string;
    url: string;
  }

  type DelParams = {
    client?: ExternalModules.Axios.AxiosInstance;
    options?: ExternalModules.Axios.AxiosRequestConfig;
    params?: any,
    path: string;
    url: string;
  }

  type Response<T> = ExternalModules.Axios.AxiosResponse<T>;

}
