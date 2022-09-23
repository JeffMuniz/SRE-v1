
declare namespace LoadingButton {
  interface Props {
    className?: string;
    disabled?: boolean;
    id?: string;
    isLoading?: boolean;
    onClick?: MouseEventHandler;
    type?: 'button' | 'submit' | 'reset';
  }
}
