
declare namespace EstablishmentService {
  type GetDetailParams = {
    cnpj: string;
  };

  type ResponseStatus = 'LIBERADO'| 'BLOQUEADO'|'INEXISTENTE'|'EM_ANALISE'|'REPROVAR';

  type GetDetailResponse = {
    id: string;
    idProposta: string;
    codigoNeurotech: number;
    cnpj: string;
    razaoSocial: string;
    nomeFantasia: string;
    inscricaoEstadual: string;
    isenta: boolean;
    cnae: string;
    cnaesSecundarios: Array<string>;
    endereco: {
      logradouro: string;
      complemento: string;
      cep: string;
      bairro: string;
      numero: string;
      uf: string;
      cidade: string;
      localidade: string;
    };
    socios: Array<{
      nome: string;
      cpf: string;
    }>;
    status: ResponseStatus;
    statusCode: string;
    dataConsulta: string;
    erros: Array<{
      'Código do Erro': string;
      'Mensagem do Erro': string;
    }>;
  }

  type GetDetailDTO = {
    id: string;
    cnpj: string;
    cnae: string;
    secondaryCnaes: Array<string>;
    name: string;
    tradingName: string;
    status: CartDataStore.EstablishmentStatus;
    owners: Array<{
      name: string;
      cpf: string;
    }>;
    address: {
      additionalInfo: string;
      city: string;
      neighborhood: string;
      number: string;
      street: string;
      state: string;
      zipCode: string;
      uf: string;
    }
  };

  type GetAddressByZipCodeParams = {
    zipCode: string;
  };

  type GetAddressByZipCodeResponse = {
    logradouro: string;
    complemento: string;
    cep: string;
    bairro: string;
    uf: string;
    cidade: string;
  };

  type GetAddressByZipCodeDTO = {
    street: string;
    neighborhood: string;
    uf: string;
    city: string;
  };
}
