
import {
 FC, useCallback, useMemo,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
 customValidation, maskMiddleware, onlyNumbersMask, validationBase,
} from '~/utils';
import {AcquirersPageStore} from '../../acquirers.page.store';
import {
 AddLineLabel, AffiliationNumberInput, CirclePlusIcon, ContinueButton, FSpacer, Form as FormEl, Wrapper,
} from './form.styles';

type FormValues = AcquirersPage.OptionConfig.Form.FormValues;
type HandleAddLineParams = AcquirersPage.OptionConfig.Form.HandleAddLineParams;
type HandleInputChangeParams = AcquirersPage.OptionConfig.Form.HandleInputChangeParams;

const Form: FC = () => {

  const history = useHistory();

  const {acquirers} = CartDataStore.state;
  const {acquirer} = AcquirersPageStore.state;

  const handleFormSubmission = useCallback(async(values: FormValues) => {
    await AcquirersPageStore.submitAffiliationCodes(values);

    // PS: Only the following should collect 'affiliationCodes'.
    const filtered = acquirers.filter(a => (
      a.name === 'macna' ||
      a.name === 'getnet' ||
      a.name === 'safrapay' ||
      a.name === 'rede'
    ));

    const index = filtered.findIndex(a => {
      return a.name === acquirer;
    });

    if(index === filtered.length - 1) {
      try {
        await AcquirersPageStore.sendAcquirersData();
      } catch(error) {
        return;
      }

      history.push(ROUTES.FEES_AND_TARIFFS);
    } else {
      const next = filtered[index + 1];
      history.push(`${ROUTES.ACQUIRERS}/${next.name}`);
    }
  }, [
    acquirer,
    acquirers,
    history,
  ]);

  const handleAddLineLabelClick = ({
    values,
    setValues,
  }: HandleAddLineParams) => {
    setValues({
      affiliationCodes: [
        ...values.affiliationCodes,
        '',
      ],
    });
  };

  const handleInputChange = ({
    event,
    index,
    setValues,
    values,
  }: HandleInputChangeParams) => {
    const toSet = [ ...values.affiliationCodes ];
    toSet[index] = (event.target as any).value;
    setValues({affiliationCodes: toSet});
  };

  const {isLoading} = AcquirersPageStore.state;

  const selectedCodes = useMemo(() => {
    return acquirers.find(a => {
      return a.name === acquirer;
    }).affiliationCodes;
  }, [ acquirer, acquirers ]);

  return (
    <Wrapper>
      <Formik<FormValues>
        key={acquirer}
        initialValues={{
          affiliationCodes: selectedCodes.length ? selectedCodes : [ '' ],
        }}
        validationSchema={validationBase().shape({
          affiliationCodes: customValidation()
            .array()
            .min(1)
            .max(3)
            .test(values => {
              const errors = values.filter(v => (!v || v.length !== 10));
              return errors.length === 0;
            }),
        })}
        onSubmit={handleFormSubmission}
        validateOnMount
      >
        {({
          handleSubmit,
          isValid,
          setValues,
          values,
        }) => (
          <FormEl onSubmit={handleSubmit}>
            {values.affiliationCodes.map((ac, index) => (
              <AffiliationNumberInput
                key={index}
                label={
                  `Número${values.affiliationCodes.length > 1 ? 's' : ''} do Estabelecimento:`
                }
                value={values.affiliationCodes[index]}
                onChange={event => maskMiddleware({
                  event,
                  handleChange: e => handleInputChange({
                    event: e,
                    values,
                    setValues,
                    index,
                  }),
                  mask: onlyNumbersMask,
                })}
                maxLength={10}
              />
            ))}
            {values.affiliationCodes.length < 3 && (
              <AddLineLabel onClick={() => handleAddLineLabelClick({
                values,
                setValues,
              })}>
                <CirclePlusIcon />
                adicionar linha
              </AddLineLabel>
            )}
            <FSpacer />
            <ContinueButton disabled={!isValid} isLoading={isLoading}>
              continuar
            </ContinueButton>
          </FormEl>
        )}
      </Formik>
    </Wrapper>
  );
};

export default observer(Form);
