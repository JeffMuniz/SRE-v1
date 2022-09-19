
import {httpClientPatch} from '~/clients';
import {onlyNumbersMask} from '~/utils';

const {REACT_APP_API_DOMAIN} = process.env;

type PatchCartOrderParams = CartService.PatchCartOrderParams;

export const patchCartOrder = async({
  id,
  selectedOwner,
  establishment,
  patQuestions,
  products,
  acquirers,
  bankAccount,
  fees,
}: PatchCartOrderParams): Promise<void> => {

  const selectedProducts = [
    products?.vr ? {
      tipo: 'VALE_REFEICAO',
      aceiteTermos: true,
    } : null,
    products?.va ? {
      tipo: 'VALE_ALIMENTACAO',
      aceiteTermos: true,
    } : null,
  ];

  await httpClientPatch({
    url: REACT_APP_API_DOMAIN,
    path: `clientes/${id}`,
    params: {
      proprietario: selectedOwner ? {
        nome: selectedOwner.name,
        cpf: selectedOwner.cpf ? onlyNumbersMask(selectedOwner?.cpf) : null,
        email: selectedOwner.email,
        telefone: selectedOwner.phone ? onlyNumbersMask(selectedOwner?.phone) : null,
      } : null,
      estabelecimento: establishment ? {
        statusCadastro: establishment?.status,
        cnpj: establishment?.cnpj ? onlyNumbersMask(establishment.cnpj) : null,
        razaoSocial: establishment?.name,
        nomeFantasia: establishment?.tradingName,
        telefone: onlyNumbersMask(establishment?.phone),
        endereco: {
          logradouro: establishment?.address?.street,
          complemento: establishment?.address?.additionalInfo,
          cep: establishment?.address?.zipCode ? onlyNumbersMask(establishment.address.zipCode) : null,
          bairro: establishment?.address?.neighborhood,
          numero: establishment?.address?.number,
          uf: establishment?.address?.uf,
          cidade: establishment?.address?.city,
          localidade: establishment?.address?.state,
        },
      } : null,
      questionarioPAT: patQuestions ? {
        lugaresEstabelecimento: {
          min: patQuestions.diningPlaces?.min,
          max: patQuestions.diningPlaces?.max,
        },
        numeroRefeicoesDiarias: {
          min: patQuestions.dailyServings?.min,
          max: patQuestions.dailyServings?.max,
        },
        areaAtendimento: {
          min: patQuestions.servingArea?.min,
          max: patQuestions.servingArea?.max,
        },
        possuiFrutaCardapio: patQuestions.fruitOnMenu,
        numeroCaixasRegistradoras: {
          min: patQuestions.cashRegisters?.min,
          max: patQuestions.cashRegisters?.max,
        },
      } : null,
      produtos: selectedProducts.filter(product => product !== null),
      adquirentes: acquirers?.map(acquirer => ({
        nome: acquirer?.name.toUpperCase(),
        numerosEstabelecimento: acquirer?.affiliationCodes,
      })),
      dadosBancarios: bankAccount ? {
        banco: bankAccount?.bank,
        agencia: bankAccount?.agency,
        contaCorrente: bankAccount?.account,
        digito: bankAccount?.digit,
      } : null,
      tarifas: fees ? fees.map(fee => ({
        tipo: fee.name,
        valor: fee.value,
      })) : null,
    },
  });
};
