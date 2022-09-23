
import {httpClientGet} from '~/clients';

const {REACT_APP_API_DOMAIN} = process.env;

type GetDetailParams = EstablishmentService.GetDetailParams;
type GetDetailDTO = EstablishmentService.GetDetailDTO;
type GetDetailResponse = EstablishmentService.GetDetailResponse;
type GetAddressByZipCodeParams = EstablishmentService.GetAddressByZipCodeParams;
type GetAddressByZipCodeResponse = EstablishmentService.GetAddressByZipCodeResponse;
type GetAddressByZipCodeDTO = EstablishmentService.GetAddressByZipCodeDTO;

export const getDetail = async({
  cnpj,
}: GetDetailParams): Promise<GetDetailDTO> => {

  const {data} = await httpClientGet<GetDetailResponse>({
    url: REACT_APP_API_DOMAIN,
    path: `clientes/${cnpj}`,
  });

  let status: CartDataStore.EstablishmentStatus;

  switch (data.status) {
    case 'LIBERADO':
      status = 'OK';
      break;
    case 'EM_ANALISE':
      status = 'ANALYZING';
      break;
    case 'INEXISTENTE':
      status = 'NONEXISTENT';
      break;
    case 'REPROVAR':
      status = 'FAIL';
      break;
    default:
      status = 'BLOCKED';
      break;
  }

  return {
    id: data.id,
    cnpj: data.cnpj,
    cnae: data.cnae,
    secondaryCnaes: data.cnaesSecundarios,
    name: data.razaoSocial,
    tradingName: data.nomeFantasia,
    status,
    owners: data.socios?.map(owner => ({
      cpf: owner.cpf,
      name: owner.nome,
    })),
    address: {
      additionalInfo: data.endereco.complemento,
      city: data.endereco.cidade,
      neighborhood: data.endereco.bairro,
      number: data.endereco.numero,
      street: data.endereco.logradouro,
      state: data.endereco.localidade,
      zipCode: data.endereco.cep,
      uf: data.endereco.uf,
    },
  };
};

export const getAddressByZipCode = async({
  zipCode,
}: GetAddressByZipCodeParams): Promise<GetAddressByZipCodeDTO> => {
  const {data} = await httpClientGet<GetAddressByZipCodeResponse>({
    url: REACT_APP_API_DOMAIN,
    path: `codigopostal/${zipCode}`,
  });

  return {
    street: data.logradouro,
    neighborhood: data.bairro,
    uf: data.uf,
    city: data.cidade,
  };
};
