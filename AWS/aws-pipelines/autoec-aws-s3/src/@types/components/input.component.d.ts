
declare namespace Input {
  type Props = {
    autocompleteOptions?: Array<string>;
    autoFocus?: boolean;
    disabled?: boolean;
    className?: string;
    errorMessage?: string;
    id?: string;
    label?: string;
    maxLength?: number;
    name?: string;
    onAutocompleteOptionSelect?: ({index, option}: { index: number, option: string }) => void;
    onBlur?: ExternalModules.React.FocusEventHandler;
    onChange?: ExternalModules.React.ChangeEventHandler;
    onFocus?: ExternalModules.React.FocusEventHandler;
    placeholder?: string;
    showEditIndicator?: boolean;
    type?: string;
    value?: string;
  };

  type State = {
    isFocused: boolean;
  };

  namespace AutocompleteOptions {
    type Props = {
      inputRef: ExternalModules.React.MutableRefObject<HTMLInputElement>;
      onSelect: ({index, option}: { index: number, option: string }) => void;
      options: Array<string>;
      searchString: string;
      show: boolean;
    };

    type State = {
      selected: number;
    };
  }
}
