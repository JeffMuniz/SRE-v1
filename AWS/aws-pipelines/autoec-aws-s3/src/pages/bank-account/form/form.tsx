
import {FC, useMemo} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {BANK_OPTIONS, ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
  accountDigitValidation,
  accountValidation,
  agencyValidation,
  bankValidation, maskMiddleware, onlyNumbersMask, validationBase,
} from '~/utils';
import {BankAccountPageStore} from '../bank-account.page.store';
import {
 AccountInput, AgencyInput, BankInput, ContinueButton, DigitInput, FSpacer, Form as FormEl, InlineWrapper, TabletInlineWrapper, Wrapper,
} from './form.styles';

type FormValues = BankAccountPage.Form.FormValues;

const Form: FC = () => {

  const history = useHistory();

  const handleFormSubmission = async(values: FormValues) => {
    await BankAccountPageStore.submitData(values);
    history.push(ROUTES.DATA_CONFIRMATION);
  };

  const {
    account,
    agency,
    bank,
    digit,
  } = CartDataStore.state.bankAccount;

  const {isLoading} = BankAccountPageStore.state;

  const bankOptions = useMemo(() => {
    return BANK_OPTIONS.map(({realName}) => realName);
  }, [ ]);

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{
          account,
          agency,
          bank,
          digit,
        }}
        validationSchema={validationBase().shape({
          account: accountValidation(),
          agency: agencyValidation(),
          bank: bankValidation({options: bankOptions}),
          digit: accountDigitValidation(),
        })}
        onSubmit={handleFormSubmission}
      >
        {({
          errors,
          handleBlur,
          handleChange,
          handleSubmit,
          isValid,
          setFieldValue,
          touched,
          values,
        }) => (
          <FormEl onSubmit={event => {
            // The if block bellow is meant to prevent all fields
            // from becoming touched if the user hits enter when
            // the autocomplete options dropdown is opened.
            if(
              (!touched.bank && !values.bank) ||
              (!touched.account && !values.account) ||
              (!touched.agency && !values.agency) ||
              (!touched.digit && !values.digit)
            ) {
              event.preventDefault();
              return;
            }
            handleSubmit(event);
          }}>
            <BankInput
              onBlur={handleBlur}
              onChange={handleChange}
              value={values.bank}
              errorMessage={touched.bank && errors.bank}
              onAutocompleteOptionSelect={({option}) => {
                setFieldValue('bank', option);
              }}
              autocompleteOptions={bankOptions}
              autoFocus
            />
            <TabletInlineWrapper>
              <AgencyInput
                onBlur={handleBlur}
                onChange={event => maskMiddleware({
                  event,
                  mask: onlyNumbersMask,
                  handleChange,
                })}
                value={values.agency}
                errorMessage={touched.agency && errors.agency}
              />
              <InlineWrapper>
                <AccountInput
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.account}
                  errorMessage={touched.account && errors.account}
                />
                <DigitInput
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.digit}
                  errorMessage={touched.digit && errors.digit}
                />
              </InlineWrapper>
            </TabletInlineWrapper>
            <FSpacer />
            <ContinueButton isLoading={isLoading} disabled={!isValid}>
              continuar
            </ContinueButton>
          </FormEl>
        )}
      </Formik>
    </Wrapper>
  );
};

export default observer(Form);
