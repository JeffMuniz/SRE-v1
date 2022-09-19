
type StyledComponent = ExternalModules.StyledComponents.StyledComponent;

declare namespace AcquirersPage {

  type Acquirer = 'getnet'
    | 'safrapay'
    | 'cielo'
    | 'stone'
    | 'pag-seguro'
    | 'rede';

  interface RouteMatchParams {
    acquirer: Acquirer;
  }

  namespace Options {
    type List = Array<{
      Logo: StyledComponent<'img', any>;
      value: Acquirer;
    }>;

    interface FormValues {
      acquirers: Array<Acquirer>;
    }

    interface SelectionHandlerParams {
      selected: Acquirer;
      current: Array<Acquirer>;
      setValues: (values: FormValues) => void;
    }
  }

  namespace OptionConfig {

    namespace Form {
      interface FormValues {
        affiliationCodes: Array<string>;
      }

      interface HandleAddLineParams {
        values: FormValues;
        setValues: (values: FormValues) => void;
      }

      interface HandleInputChangeParams {
        index: number;
        setValues: (values: FormValues) => void;
        values: FormValues;
        event: ExternalModules.React.ChangeEvent;
      }

    }

    namespace SlipModal {
      interface Props {
        show: boolean;
        acquirer: Acquirer;
        onCloseClick: ExternalModules.React.MouseEventHandler;
      }

      type EcCodeInfo = {
        text: string;
        digits: number;
      };
    }
  }

}
