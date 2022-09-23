import {
FC, useCallback, useMemo, useState,
} from 'react';
import {Formik} from 'formik';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {BANK_OPTIONS, ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
  accountDigitValidation,
  accountValidation,
  agencyValidation,
  bankValidation,
  emailValidation,
  nameValidation,
  phoneValidation,
  validationBase,
  zipCodeValidation,
} from '~/utils';
import {exists} from '~/utils/validation/validation.util';
import {DataConfirmationPageStore} from '../data-confirmation.page.store';
 import Acquirers from './acquirers/acquirers';
 import BankAccount from './bank-account/bank-account';
 import Establishment from './establishment/establishment';
import {
  DCCheckedLabel, DCPageSubtitle, FErrorModal, FinishButton, Form as FormEl, Wrapper,
 } from './form.styles';
 import Personal from './personal/personal';
 import Products from './products/products';

type FormValues = DataConfirmationPage.Form.FormValues;

const Form: FC = () => {
  const history = useHistory();
  const [ showErrorModal, setShowErrorModal ] = useState(false);

  const handleFinishButtonClick = async(values: FormValues) => {
    try {
      await DataConfirmationPageStore.finishOrder(values);
    } catch(error) {
      setShowErrorModal(true);
      return;
    }
    history.push(ROUTES.CONGRATS);
  };

  const handleCloseErrorModalButtonClick = useCallback(() => {
    setShowErrorModal(false);
  }, [ ]);

  const {isLoading} = DataConfirmationPageStore.state;
  const {establishment, selectedOwner, bankAccount} = CartDataStore.state;

  const bankOptions = useMemo(() => {
    return BANK_OPTIONS.map(({realName}) => realName);
  }, [ ]);

  return (
    <Wrapper>
      <Formik<FormValues>
        initialValues={{
          name: selectedOwner.name,
          cpf: selectedOwner.cpf,
          email: selectedOwner.email,
          phone: selectedOwner.phone,
          cnpj: establishment.cnpj,
          additionalInfo: establishment.address.additionalInfo,
          city: establishment.address.city,
          companyName: establishment.name,
          establishmentPhone: establishment.phone,
          neighborhood: establishment.address.neighborhood,
          number: establishment.address.number,
          state: establishment.address.state,
          street: establishment.address.street,
          tradingName: establishment.tradingName,
          zipCode: establishment.address.zipCode,
          account: bankAccount.account,
          agency: bankAccount.agency,
          bank: bankAccount.bank,
          digit: bankAccount.digit,
        }}
        validationSchema={validationBase().shape({
          email: emailValidation(),
          phone: phoneValidation(),
          establishmentPhone: phoneValidation(),
          number: exists(),
          tradingName: nameValidation(),
          zipCode: zipCodeValidation(),
          account: accountValidation(),
          agency: agencyValidation(),
          bank: bankValidation({options: bankOptions}),
          digit: accountDigitValidation(),
        })}
        onSubmit={handleFinishButtonClick}
      >
        {({handleSubmit, isValid}) => (
          <FormEl onSubmit={handleSubmit}>
            <DCPageSubtitle>
              dados do proprietário
            </DCPageSubtitle>
            <DCCheckedLabel label='dados corretos' />
            <Personal />

            <DCPageSubtitle>
              dados do estabelecimento
            </DCPageSubtitle>
            <DCCheckedLabel label='dados corretos' />
            <Establishment />

            <DCPageSubtitle>
              produtos escolhidos
            </DCPageSubtitle>
            <Products />

            <DCPageSubtitle>
              maquininhas habilitadas
            </DCPageSubtitle>
            <Acquirers />

            <DCPageSubtitle>
              dados bancários:
            </DCPageSubtitle>
            <BankAccount />

            <FinishButton isLoading={isLoading} disabled={!isValid}>
              finalizar cadastro
            </FinishButton>
          </FormEl>
        )}
      </Formik>
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

export default observer(Form);
