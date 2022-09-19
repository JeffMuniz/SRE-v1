declare namespace FeesAndTariffsPage {

  namespace Table {
    type State = {
      showErrorModal: boolean;
    };

    type TableData = Array<Array<{
      main?: string;
      title?: string;
      titleDescription?: string;
      text?: string;
    }>>;
  }

}
