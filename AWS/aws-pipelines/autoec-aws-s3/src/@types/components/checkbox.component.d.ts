
declare namespace Checkbox {
  interface Props {
    className?: string;
    id?: string;
    label?: string;
    name?: string;
    onChange?: ExternalModules.React.ChangeEventHandler<HTMLInputElement>,
    checked?: boolean;
  }
}
