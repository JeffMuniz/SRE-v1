
declare namespace DigitInput {

  interface Props {
    autoFocus?: boolean;
    className?: string;
    digits: number;
    label?: string;
    onChange: (value: string) => void;
  }

  type InputRefs = Array<HTMLInputElement>;

  interface State {
    raw: string;
    splitted: Array<string>;
  }
}
