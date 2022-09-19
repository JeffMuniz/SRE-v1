
declare namespace FeesAndTariffsService {
  type GetFeesAndTariffsParams = {
    cnpj: string;
  };

  type GetFeesAndTariffsResponse = {
    taxas: Array<{
      valor: number;
      nome: string;
      idProduto: number;
    }>;
  }

  type GetFeesAndTariffsDTO = Array<{
    value: number;
    name: string;
  }>;
}
