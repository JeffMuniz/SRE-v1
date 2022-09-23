
declare namespace CartService {
  interface PatchCartOrderParams {
    id: string;
    establishment?: {
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
      status: string;
      cnpj: string;
      name: string;
      tradingName: string;
      phone: string;
    };
    selectedOwner?: {
      cpf: string;
      email: string;
      name: string;
      phone: string;
    };
    patQuestions?: {
      cashRegisters: {
        max: number,
        min: number,
      };
      dailyServings: {
        max: number,
        min: number,
      };
      diningPlaces: {
        max: number,
        min: number,
      };
      fruitOnMenu: boolean;
      servingArea: {
        max: number,
        min: number,
      };
    };
    products?: {
      vr: boolean;
      va: boolean;
    };
    termsAcceptance?: {
      nutritional: boolean;
      truthfulInfo: boolean;
    };
    acquirers?: Array<{
      name: AcquirerName;
      affiliationCodes: Array<string>;
    }>;
    bankAccount?: {
      bank: string;
      agency: string;
      account: string;
      digit: string;
    };
    fees: Array<{
      name: string;
      value: number;
    }>;
  };
}
