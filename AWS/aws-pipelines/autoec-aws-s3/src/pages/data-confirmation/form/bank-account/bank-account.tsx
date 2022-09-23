
import {FC, useMemo} from 'react';
import {useFormikContext} from 'formik';
import {BANK_OPTIONS} from '~/consts';
import {maskMiddleware, onlyNumbersMask} from '~/utils';
import {
 AccountInput, AgencyInput, BankInput, DigitInput, Wrapper,
} from './bank-account.styles';

type FormValues = DataConfirmationPage.Form.FormValues;

const BankAccount: FC = () => {

  const {
    errors,
    handleChange,
    touched,
    values,
    setFieldValue,
  } = useFormikContext<FormValues>();

  const bankOptions = useMemo(() => {
    return BANK_OPTIONS.map(({realName}) => realName);
  }, [ ]);

  return (
    <Wrapper>
      <BankInput
        onChange={handleChange}
        value={values.bank}
        errorMessage={touched.bank && errors.bank}
        onAutocompleteOptionSelect={({option}) => {
          setFieldValue('bank', option);
        }}
        autocompleteOptions={bankOptions}
      />
      <AgencyInput
        onChange={event => maskMiddleware({
          event,
          mask: onlyNumbersMask,
          handleChange,
        })}
        value={values.agency}
        errorMessage={touched.agency && errors.agency}
      />
      <AccountInput
        onChange={handleChange}
        value={values.account}
        errorMessage={touched.account && errors.account}
      />
      <DigitInput
        onChange={handleChange}
        value={values.digit}
        errorMessage={touched.digit && errors.digit}
      />
    </Wrapper>
  );
};

export default BankAccount;
