
import {
FC, useCallback, useState,
} from 'react';
import {useFormikContext} from 'formik';
import {getAddressByZipCode} from '~/services';
import {
cnpjMask, maskMiddleware, onlyNumbersMask, phoneMask, zipCodeMask,
} from '~/utils';
import {
 AdditionalInfoInput, CNPJInput, CityInput, FErrorModal, NameInput, NeighborhoodInput, NumberInput, PhoneInput, StateInput, StreetInput, TabletInlineWrapper, TradingNameInput, Wrapper, ZipCodeInput,
} from './establishment.styles';

type FormValues = DataConfirmationPage.Form.FormValues;
type GetAddressByZipCode = EstablishmentService.GetAddressByZipCodeDTO;

const Establishment: FC = () => {
  const [ showErrorModal, setShowErrorModal ] = useState(false);

  const {
    errors,
    handleChange,
    touched,
    values,
    setValues,
  } = useFormikContext<FormValues>();

  const handleCepBlur = async() => {
    let address: GetAddressByZipCode;

    try {
      address = await getAddressByZipCode({zipCode: onlyNumbersMask(values.zipCode)});
    } catch(error) {
      setShowErrorModal(true);
      return;
    }
    setValues({
      ...values,
      street: address.street,
      neighborhood: address.neighborhood,
      city: address.city,
      state: address.uf,
      number: '',
      additionalInfo: '',
    });
  };

  const handleCloseErrorModalButtonClick = useCallback(() => {
    setShowErrorModal(false);
  }, [ ]);

  return (
    <Wrapper>
      <TabletInlineWrapper>
        <CNPJInput
          id='cnpj'
          name='cnpj'
          onChange={event => maskMiddleware({
            event,
            mask: cnpjMask,
            handleChange,
          })}
          errorMessage={touched.cnpj && errors.cnpj}
          value={values.cnpj}
          disabled
        />
        <NameInput
          errorMessage={touched.companyName && errors.companyName}
          id='company-name'
          name='companyName'
          onChange={handleChange}
          value={values.companyName}
          disabled
        />
      </TabletInlineWrapper>
      <TabletInlineWrapper>
        <TradingNameInput
          errorMessage={touched.tradingName && errors.tradingName}
          id='tranding-name'
          name='tradingName'
          onChange={handleChange}
          value={values.tradingName}
        />
        <PhoneInput
          errorMessage={touched.establishmentPhone && errors.establishmentPhone}
          id='establishment-phone'
          name='establishmentPhone'
          onChange={event => maskMiddleware({
            event,
            mask: phoneMask,
            handleChange,
          })}
          placeholder='00 00000-0000'
          value={values.establishmentPhone}
        />
      </TabletInlineWrapper>
      <TabletInlineWrapper>
        <ZipCodeInput
          errorMessage={touched.zipCode && errors.zipCode}
          id='zip-code'
          name='zipCode'
          onChange={event => maskMiddleware({
            event,
            mask: zipCodeMask,
            handleChange,
          })}
          value={values.zipCode}
          onBlur={handleCepBlur}
        />
        <StreetInput
          errorMessage={touched.street && errors.street}
          id='street'
          name='street'
          onChange={handleChange}
          value={values.street}
          disabled
        />
        <NumberInput
          errorMessage={touched.number && errors.number}
          id='number'
          name='number'
          onChange={event => maskMiddleware({
            event,
            mask: onlyNumbersMask,
            handleChange,
          })}
          value={values.number}
          maxLength={6}
        />
      </TabletInlineWrapper>
      <TabletInlineWrapper>
        <AdditionalInfoInput
          errorMessage={touched.additionalInfo && errors.additionalInfo}
          id='additional-info'
          name='additionalInfo'
          onChange={handleChange}
          value={values.additionalInfo}
        />
        <NeighborhoodInput
          errorMessage={touched.neighborhood && errors.neighborhood}
          id='neighborhood'
          name='neighborhood'
          value={values.neighborhood}
          disabled
        />
        <CityInput
          errorMessage={touched.city && errors.city}
          id='city'
          name='city'
          value={values.city}
          disabled
        />
        <StateInput
          errorMessage={touched.state && errors.state}
          id='state'
          name='state'
          value={values.state}
          disabled
        />
      </TabletInlineWrapper>
      <FErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={showErrorModal}
        text={`É necessário entrar em contato com a central de vendas
          4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar
          o cadastro do seu estabelecimento comercial
        `}
      />
    </Wrapper>
  );
};

export default Establishment;
