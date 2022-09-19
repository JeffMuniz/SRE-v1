
import {FC} from 'react';
import {useFormikContext} from 'formik';
import {maskMiddleware, phoneMask} from '~/utils';
import {
 CompanyNameInput, EstablishmentPhoneInput, TabletInlineWrapper, TradingNameInput, Wrapper,
} from './identity.styles';

type Context = EstablishmentDataPage.Form.FormValues;

const Identity: FC = () => {

  const {
    errors,
    handleChange,
    touched,
    values,
    initialValues,
  } = useFormikContext<Context>();

  return (
    <Wrapper>
      <TabletInlineWrapper>
        <CompanyNameInput
          errorMessage={touched.companyName && errors.companyName}
          id='company-name'
          label='nome social:'
          name='companyName'
          value={values.companyName}
          disabled
        />
        <TradingNameInput
          errorMessage={touched.tradingName && errors.tradingName}
          id='trading-name'
          label='nome fantasia:'
          name='tradingName'
          onChange={handleChange}
          placeholder={initialValues.tradingName}
          showEditIndicator
          value={values.tradingName}
          autoFocus
        />
      </TabletInlineWrapper>
      <EstablishmentPhoneInput
        id='establishment-phone'
        name='establishmentPhone'
        label='telefone do estabelecimento:'
        onChange={event => maskMiddleware({
          event,
          mask: phoneMask,
          handleChange,
        })}
        placeholder='00 00000-0000'
        showEditIndicator
        value={values.establishmentPhone}
        errorMessage={
          touched.establishmentPhone
          && errors.establishmentPhone
        }
      />
    </Wrapper>
  );
};

export default Identity;
