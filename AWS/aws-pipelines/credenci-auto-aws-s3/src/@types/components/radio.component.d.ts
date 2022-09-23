
declare namespace Radio {
  interface Props {
    className?: string;
    id?: string;
    label: string;
    name?: string;
    onChange?: ExternalModules.React.FormEventHandler;
    onClick?: ExternalModules.React.MouseEventHandler;
    value?: string;
    checked?: boolean;
  }
}
