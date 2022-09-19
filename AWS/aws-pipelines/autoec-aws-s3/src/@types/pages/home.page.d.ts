
declare namespace HomePage {
  interface State { }

  namespace Form {
    interface FormValues {
      cnpj: string;
    }

    type State = {
      title: string;
      text: string;
      show: boolean;
      buttonText: string;
      cnpjStatus: string;
    };
  }
}
