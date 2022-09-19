
type ChangeEvent = ExternalModules.React.ChangeEvent;

declare namespace MaskingUtil {

  type MiddlewareParams = {
    event: ChangeEvent;
    mask: (value: string) => string;
    handleChange: (event: ChangeEvent) => void;
  }

}
