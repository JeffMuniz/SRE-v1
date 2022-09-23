
import {
FC, FocusEvent, useCallback, useState,
} from 'react';
import {useFormikContext} from 'formik';
import {getAddressByZipCode} from '~/services';
import {
 maskMiddleware, onlyNumbersMask, zipCodeMask,
} from '~/utils';
import {
 AdditionalInfoInput,
 CityInput,
 FErrorModal,
 InlineWrapper,
 NeighborhoodInput,
 NumberInput,
 StateInput,
 StreetInput,
 TabletInlineWrapper,
 Wrapper,
 ZipCodeInput,
} from './address.styles';

type Context = EstablishmentDataPage.Form.FormValues;
type GetAddressByZipCode = EstablishmentService.GetAddressByZipCodeDTO;

const Address: FC = () => {
  const [ showErrorModal, setShowErrorModal ] = useState(false);

  const {
    errors,
    handleBlur,
    handleChange,
    touched,
    values,
    setValues,
  } = useFormikContext<Context>();

  const handleCepBlur = async(event: FocusEvent) => {
    handleBlur(event);

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
        <ZipCodeInput
          errorMessage={touched.zipCode && errors.zipCode}
          id='zip-code'
          label='cep:'
          name='zipCode'
          onBlur={handleCepBlur}
          onChange={event => maskMiddleware({
            event,
            mask: zipCodeMask,
            handleChange,
          })}
          placeholder='00000-000'
          showEditIndicator
          value={values.zipCode}
        />
        <StreetInput
          errorMessage={touched.street && errors.street}
          id='street'
          label='endereço:'
          name='street'
          placeholder='Av Paulista'
          value={values.street}
          disabled
        />
        <InlineWrapper>
          <NumberInput
            errorMessage={touched.number && errors.number}
            id='number'
            label='número:'
            name='number'
            onBlur={handleBlur}
            onChange={event => maskMiddleware({
              event,
              mask: onlyNumbersMask,
              handleChange,
            })}
            placeholder='0000'
            showEditIndicator
            value={values.number}
            maxLength={6}
          />
          <AdditionalInfoInput
            errorMessage={touched.additionalInfo && errors.additionalInfo}
            id='additional-info'
            label='complemento:'
            name='additionalInfo'
            onBlur={handleBlur}
            onChange={handleChange}
            placeholder='Apto 101'
            showEditIndicator
            value={values.additionalInfo}
          />
        </InlineWrapper>
      </TabletInlineWrapper>
      <TabletInlineWrapper>
        <NeighborhoodInput
          errorMessage={touched.neighborhood && errors.neighborhood}
          id='neighborhood'
          label='bairro:'
          name='neighborhood'
          value={values.neighborhood}
          disabled
        />
        <CityInput
          errorMessage={touched.city && errors.city}
          id='city'
          label='cidade:'
          name='city'
          placeholder='São Paulo'
          value={values.city}
          disabled
        />
        <StateInput
          errorMessage={touched.state && errors.state}
          id='state'
          label='estado:'
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

export default Address;
