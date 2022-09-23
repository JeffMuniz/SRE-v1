
import axios, {AxiosInstance, AxiosRequestConfig} from 'axios';

type Response<T> = HttpClient.Response<T>;

export const buildClient = (config: AxiosRequestConfig) => {
  const client = axios.create(config);
  return client;
};

const defaultClient: AxiosInstance = buildClient({
 timeout: 12000,
});

export const get = async <T>({
  client = defaultClient,
  options = { },
  params = { },
  path,
  url,
}: HttpClient.GetParams): Promise<Response<T>> => {
  const response = await client.get(`${url}/${path}`, {
    ...options,
    params,
  });
  return response;
};

export const post = async <T>({
  client = defaultClient,
  options = { },
  params = { },
  path,
  url,
}: HttpClient.PostParams): Promise<Response<T>> => {
  const response = await client.post(`${url}/${path}`, params, options);
  return response;
};

export const put = async <T>({
  client = defaultClient,
  options = { },
  params = { },
  path,
  url,
}: HttpClient.PutParams): Promise<Response<T>> => {
  const response = await client.put(`${url}/${path}`, params, options);
  return response;
};

export const patch = async <T>({
  client = defaultClient,
  options = { },
  params = { },
  path,
  url,
}: HttpClient.PatchParams): Promise<Response<T>> => {
  const response = await client.patch(`${url}/${path}`, params, options);
  return response;
};

export const del = async <T>({
  client = defaultClient,
  options = { },
  params = { },
  path,
  url,
}: HttpClient.DelParams): Promise<Response<T>> => {
  const response = await client.delete(`${url}/${path}`, {
    params,
    ...options,
  });
  return response;
};
