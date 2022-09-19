
import {FC, useMemo} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {customValidation, validationBase} from '~/utils';
import {AcquirersPageStore} from '../acquirers.page.store';
import {
 ACheckbox, CheckboxesWrapper, CieloLogo, ContinueButton, Form, GetnetLogo, OPageSubtitle, OSpacer, PagSeguroLogo, RedeLogo, SafrapayLogo, StoneLogo, Wrapper,
} from './options.styles';

type FormValues = AcquirersPage.Options.FormValues;
type List = AcquirersPage.Options.List;
type SelectionHandlerParams = AcquirersPage.Options.SelectionHandlerParams;

const Options: FC = () => {

  const options = useMemo<List>(() => [{
    Logo: GetnetLogo,
    value: 'getnet',
  }, {
    Logo: SafrapayLogo,
    value: 'safrapay',
  }, {
    Logo: CieloLogo,
    value: 'cielo',
  }, {
    Logo: RedeLogo,
    value: 'rede',
  }, {
    Logo: StoneLogo,
    value: 'stone',
  }, {
    Logo: PagSeguroLogo,
    value: 'pag-seguro',
  }], [ ]);

  const history = useHistory();

  const handleFormSubmission = async(values: FormValues) => {
    await AcquirersPageStore.submitSelectedOptions(values);
    const filtered = values.acquirers.filter(a => (
      a === 'cielo' ||
      a === 'getnet' ||
      a === 'safrapay' ||
      a === 'rede'
    ));

    if(filtered.length > 0) {
      history.push(`${ROUTES.ACQUIRERS}/${filtered[0]}`);
    } else {
      history.push(ROUTES.FEES_AND_TARIFFS);
    }
  };

  const handleSelection = ({
    selected,
    setValues,
    current,
  }: SelectionHandlerParams) => {
    const toSet = [ ...current ];
    const index = current.findIndex(c => c === selected);
    if(index !== -1) {
      toSet.splice(index, 1);
    } else {
      toSet.push(selected);
    }

    setValues({acquirers: toSet});
  };

  const {acquirers} = CartDataStore.state;

  return (
    <Wrapper>
      <OPageSubtitle>
        a mac hoje trabalha com as maquininhas abaixo.<br />em quais delas você deseja habilitar os cartões da mac?
      </OPageSubtitle>
      <Formik<FormValues>
        initialValues={{
          acquirers: acquirers.map(a => a.name),
        }}
        validationSchema={validationBase().shape({
          acquirers: customValidation()
            .array()
            .min(1),
        })}
        validateOnMount
        onSubmit={handleFormSubmission}
      >
        {({
          handleSubmit,
          isValid,
          setValues,
          values,
        }) => (
          <Form onSubmit={handleSubmit}>
            <CheckboxesWrapper>
              {options.map(({Logo, value}, index) => (
                <ACheckbox
                  id={`acquirer-${index}`}
                  key={`acquirer-${index}`}
                  onChange={() => handleSelection({
                    current: values.acquirers,
                    selected: value,
                    setValues,
                  })}
                  checked={values.acquirers.includes(value)}
                >
                  <Logo
                    onClick={() => handleSelection({
                      current: values.acquirers,
                      selected: value,
                      setValues,
                    })}
                  />
                </ACheckbox>
              ))}
            </CheckboxesWrapper>
            <OSpacer />
            <ContinueButton disabled={!isValid}>
              continuar
            </ContinueButton>
          </Form>
        )}
      </Formik>
    </Wrapper>
  );
};

export default observer(Options);
