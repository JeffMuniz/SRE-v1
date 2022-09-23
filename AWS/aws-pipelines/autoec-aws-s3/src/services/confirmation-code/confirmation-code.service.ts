
import {httpClientPatch, httpClientPost} from '~/clients';

const {REACT_APP_API_DOMAIN} = process.env;

type PostSendConfirmationCodeParams = ConfirmationCode.PostSendConfirmationCodeParams;
type PatchConfirmCodeParams = ConfirmationCode.PatchConfirmCodeParams;

export const postSendConfirmationCode = async({
  contactType,
  email,
  phone,
  resend,
}: PostSendConfirmationCodeParams): Promise<void> => {

  await httpClientPost({
    url: REACT_APP_API_DOMAIN,
    path: 'token',
    params: contactType === 'phone' ? {
      ddd: phone.slice(0, 2),
      telefone: phone.slice(2),
      reenvio: resend,
    } : {
      email,
      reenvio: resend,
    },
  });
};

export const patchConfirmCode = async({
  contactType,
  code,
  email,
  phone,
}: PatchConfirmCodeParams): Promise<void> => {

  await httpClientPatch({
    url: REACT_APP_API_DOMAIN,
    path: 'token',
    params: contactType === 'phone' ? {
      ddd: phone.slice(0, 2),
      telefone: phone.slice(2),
      codigoSeguranca: code,
    } : {
      email,
      codigoSeguranca: code,
    },
  });
};
