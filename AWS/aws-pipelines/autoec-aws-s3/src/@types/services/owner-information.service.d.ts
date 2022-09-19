
declare namespace OwnerInformationService {
  type GetOwnerDetailParams = {
    cnpj: string;
    cpf: string;
  };

  type GetOwnerDetailResponse = {
    id: string;
    cpf: string;
    nome: string;
    nomes: Array<string>;
    nomeMae: string;
    nomesMae: Array<string>;
    sexo: string;
    status: string;
    dataConsulta: string;
    dataNascimento: string;
    datasNascimento: Array<string>;
    erros: [
      {
        'Código do Erro': string;
        'Mensagem do Erro': string
      }
    ]
  }

  type GetOwnerDetailDTO = {
    birthDates: Array<string>;
    motherNames: Array<string>;
  };

  type PostOwnerIdentityConfirmationParams = {
    cnpj: string;
    cpf: string;
    name: string;
    motherName: string;
    birthDate: string;
  };
}
