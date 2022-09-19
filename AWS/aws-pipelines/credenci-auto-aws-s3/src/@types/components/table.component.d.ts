
declare namespace Table {

  interface Props {
    className?: string;
    matrix?: Array<Array<string | ExternalModules.React.ReactNode>>;
    highlightedColumns?: Array<number>;
  }

}
