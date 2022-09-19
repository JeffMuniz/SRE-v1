
declare namespace ContactInfoPage {
  namespace Form {
    interface FormValues {
      email: string;
      phone: string;
    }

    type State = {
      showModal: boolean;
    };
  }
}
