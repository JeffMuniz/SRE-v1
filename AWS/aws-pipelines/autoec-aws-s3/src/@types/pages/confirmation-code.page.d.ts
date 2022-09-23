
declare namespace ConfirmationCodePage {
  namespace Form {
    interface FormValues {
      code: string;
    }

    type State = {
      showErrorText: boolean;
      showErrorModal: boolean;
    }
  }
}
