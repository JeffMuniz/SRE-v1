
declare namespace DataConfirmationPage {
  namespace Form {
    type FormValues = {
      name: string;
      cpf: string;
      email: string;
      phone: string;
      cnpj: string;
      additionalInfo: string;
      city: string;
      companyName: string;
      establishmentPhone: string;
      neighborhood: string;
      number: string;
      state: string;
      street: string;
      tradingName: string;
      zipCode: string;
      account: string;
      agency: string;
      bank: string;
      digit: string;
    }
  }

  namespace ProductInformation {
    type Props = {
      type: 'vr' | 'va';
    }
  }

}
