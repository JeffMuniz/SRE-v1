
import {httpClientGet} from '~/clients';

const {REACT_APP_API_DOMAIN} = process.env;

type GetFeesAndTariffsParams = FeesAndTariffsService.GetFeesAndTariffsParams;
type GetFeesAndTariffsResponse = FeesAndTariffsService.GetFeesAndTariffsResponse;
type GetFeesAndTariffsDTO = FeesAndTariffsService.GetFeesAndTariffsDTO;

export const getFeesAndTariffs = async({
  cnpj,
}: GetFeesAndTariffsParams): Promise<GetFeesAndTariffsDTO> => {

  const {data} = await httpClientGet<GetFeesAndTariffsResponse>({
    url: REACT_APP_API_DOMAIN,
    path: `clientes/${cnpj}/taxas`,
  });

  return data.taxas.map(tax => ({
    value: tax.valor,
    name: tax.nome,
  }));
};
