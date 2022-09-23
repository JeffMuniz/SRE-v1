
declare namespace UserService {

  interface GetClientIpDTO {
    ip: string;
    country: string;
    state: string;
    city: string;
    geolocation: {
      latitude: number;
      longitude: number;
    };
  }

  interface GetClientIpResponse {
    query: string;
    country: string;
    region: string;
    city: string;
    lat: number;
    lon: number;
  }

}
