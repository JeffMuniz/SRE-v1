
import {httpClientGet, httpClientPost} from '~/clients';

const {REACT_APP_API_DOMAIN} = process.env;

type GetOwnerDetailParams = OwnerInformationService.GetOwnerDetailParams;
type GetOwnerDetailDTO = OwnerInformationService.GetOwnerDetailDTO;
type GetOwnerDetailResponse = OwnerInformationService.GetOwnerDetailResponse;
type PostOwnerIdentityConfirmationParams = OwnerInformationService.PostOwnerIdentityConfirmationParams;

export const getOwnerDetail = async({
  cnpj,
  cpf,
}: GetOwnerDetailParams): Promise<GetOwnerDetailDTO> => {

  const {data} = await httpClientGet<GetOwnerDetailResponse>({
    url: REACT_APP_API_DOMAIN,
    path: `clientes/${cnpj}/socios/${cpf}/validar`,
  });

  return {
    motherNames: data.nomesMae,
    birthDates: data.datasNascimento,
  };
};

export const postOwnerIdentityConfirmation = async({
  cnpj,
  cpf,
  name,
  motherName,
  birthDate,
}: PostOwnerIdentityConfirmationParams): Promise<string> => {
  const {data: id} = await httpClientPost<string>({
    url: REACT_APP_API_DOMAIN,
    path: `clientes/${cnpj}/socios/${cpf}/validar`,
    params: {
      cpf,
      nomeMae: motherName,
      nome: name,
      dataNascimento: birthDate,
    },
  });

  return id;
};
