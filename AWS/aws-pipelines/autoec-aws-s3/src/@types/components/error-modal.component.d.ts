
declare namespace ErrorModal {
  interface Props {
    title?: string;
    text: string;
    show: boolean;
    buttonText?: string;
    onCloseButtonClick: () => void;
  }
}
