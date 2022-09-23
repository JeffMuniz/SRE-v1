
declare namespace CartDataStore {

  interface Range {
    min: number;
    max: number;
  }

  type AcquirerName = 'getnet'
    | 'safrapay'
    | 'macna'
    | 'stone'
    | 'pag-seguro'
    | 'rede';

  type EstablishmentStatus = 'OK' | 'BLOCKED' | 'ANALYZING' | 'NONEXISTENT' | 'FAIL';

  type ContactType = 'phone' | 'email';

  type State = {
    creationDate: Date;
    id: string;
    updateDate: Date;
    selectedOwner: {
      cpf: string;
      email: string;
      name: string;
      phone: string;
    };
    establishment: {
      address: {
        additionalInfo: string;
        city: string;
        neighborhood: string;
        number: string;
        state: string;
        street: string;
        zipCode: string;
        uf: string;
      };
      status: EstablishmentStatus;
      cnpj: string;
      name: string;
      tradingName: string;
      phone: string;
      owners: Array<{
        cpf: string;
        name: string;
      }>;
    };
    patQuestions: {
      cashRegisters: Range;
      dailyServings: Range;
      diningPlaces: Range;
      fruitOnMenu: boolean;
      servingArea: Range;
    };
    products: {
      vr: boolean;
      va: boolean;
    };
    termsAcceptance: {
      nutritional: boolean;
      truthfulInfo: boolean;
    };
    acquirers: Array<{
      name: AcquirerName;
      affiliationCodes: Array<string>;
    }>;
    bankAccount: {
      bank: string;
      agency: string;
      account: string;
      digit: string;
    };
    contactType: ContactType,
    fees: Array<{
      name: string;
      value: number;
    }>;
  };
}
