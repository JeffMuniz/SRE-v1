
declare namespace IdentityConfirmationPageStore {
  interface State {
    showPageLoading: boolean;
    showErrorModal: boolean;
    showButtonLoading: boolean;
    options: {
      motherNames: Array<string>;
      birthDates: Array<string>;
    };
  }
}
